using Azure.Messaging.ServiceBus;


namespace Device_Management.Services
{
    public class ServiceBusReceiverHostedService : IHostedService
    {
        private ServiceBusProcessor _processor;

        public ServiceBusReceiverHostedService(ServiceBusClient serviceBusClient)
        {
            _processor = serviceBusClient.CreateProcessor("test", new ServiceBusProcessorOptions());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            return _processor.StartProcessingAsync(cancellationToken);
        }

        private Task MessageHandler(ProcessMessageEventArgs args)
        {
            // handle your message here
            return Task.CompletedTask;
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            // handle your error here
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync(cancellationToken);
            await _processor.DisposeAsync();
        }
    }

}
