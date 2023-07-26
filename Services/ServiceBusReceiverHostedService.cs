using Azure.Messaging.ServiceBus;
using Device_Management.Services.Email;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Device_Management.Services
{
    public class ServiceBusReceiverHostedService : IHostedService
    {
        private ServiceBusProcessor _processor;
        private readonly IEmailService _emailService;

        public ServiceBusReceiverHostedService(ServiceBusClient serviceBusClient, IEmailService emailService)
        {
            _processor = serviceBusClient.CreateProcessor("devices", new ServiceBusProcessorOptions());
            _emailService = emailService;
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
            string body = args.Message.Body.ToString();
            Console.WriteLine("message received!");
            Console.WriteLine($"Received: {body}");
            var jsonMessage = JObject.Parse(body);

            if (jsonMessage["temperature"] != null && float.TryParse(jsonMessage["temperature"].ToString(), out float temperature))
            {
                //Console.WriteLine($"temperature reading {temperature}");
                if (temperature >= 30)
                {
                    // TOOO: send an email
                    Console.WriteLine($"Sending alert email!!!! temperature: {temperature}");
                    // Use the email service to send the alert email
                    await _emailService.SendAlertEmailAsync(temperature, jsonMessage);


                    // store the alert info in a table
                    // You will need to define what "storing the alert info in a table" means for your application.
                }
            } else
            {
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
