using Azure.Messaging.ServiceBus;
using Device_Management.Models;
using Device_Management.Services.Email;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;

namespace Device_Management.Services
{
    public class ServiceBusReceiverHostedService : IHostedService
    {
        private ServiceBusProcessor _processor;
        private readonly IEmailService _emailService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ServiceBusReceiverHostedService(ServiceBusClient serviceBusClient, IEmailService emailService, IServiceScopeFactory serviceScopeFactory)
        {
            _processor = serviceBusClient.CreateProcessor("devices", new ServiceBusProcessorOptions());
            _emailService = emailService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
            Console.WriteLine("start listening for messages");

            return _processor.StartProcessingAsync(cancellationToken);
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var alertService = scope.ServiceProvider.GetRequiredService<AlertService>();
            var deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();

            string body = args.Message.Body.ToString();
            Console.WriteLine("message received!");
            Console.WriteLine($"Received: {body}");
            var jsonMessage = JObject.Parse(body);

            if (jsonMessage["deviceId"] != null && int.TryParse(jsonMessage["deviceId"].ToString(), out int deviceId)) {
                await deviceService.UpdateDeviceStateAsync(deviceId, jsonMessage);
            }

            if (jsonMessage["temperature"] != null && float.TryParse(jsonMessage["temperature"].ToString(), out float temperature))
            {
                //Console.WriteLine($"temperature reading {temperature}");
                if (temperature >= 31.5)
                {
                    // TODO store the alert info in a table
                    // insert -- level: important, date: now, status: unacknowledged, desc: device over heat
                    var alert = new Alert
                    {
                        // TODO handle the case where DeviceId is not int
                        DeviceId = (int) jsonMessage["deviceId"],
                        Timestamp = DateTime.UtcNow,
                        Severity = "important",
                        Description = "Device over heat",
                        Status = "unacknowledged"
                    };

                    await alertService.CreateAlert(alert);
                    Console.WriteLine($"Stored the alert info in the database. Alert: {alert.ToJson()}");

                    // TODO dont send alert if two anormaly is too close

                    Console.WriteLine($"Sending alert email!!!! temperature: {temperature}");
                    // Use the email service to send the alert email.
                    // takes a long time so do not await
                    _emailService.SendAlertEmailAsync(temperature, jsonMessage);
                }
            } else {
                Console.WriteLine("---No temperarure found in message---");
            }

            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync(cancellationToken);
            await _processor.DisposeAsync();
        }
    }

}
