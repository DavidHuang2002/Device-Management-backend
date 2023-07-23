//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Azure.Messaging.ServiceBus;

//namespace Device_Management.Services.Azure
//{


//    public class AzureServiceBusConsumer
//    {
//        private readonly string _client;
//        private readonly string _queueName;

//        public AzureServiceBusConsumer(string connectionString, string queueName)
//        {
//            _connectionString = connectionString;
//            _queueName = queueName;
//        }

//        public async Task StartListeningAsync(CancellationToken cancellationToken = default)
//        {
//            var queueClient = new QueueClient(_connectionString, _queueName);

//            // Register the message handler and receive messages in a loop
//            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
//            {
//                MaxConcurrentCalls = 1,
//                AutoComplete = false // Set to true if you want messages to be automatically completed after the handler executes.
//            };

//            queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

//            // Keep the consumer running until cancellation is requested.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                await Task.Delay(TimeSpan.FromSeconds(1));
//            }

//            await queueClient.CloseAsync();
//        }

//        private async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
//        {
//            // Add your message processing logic here
//            var messageBody = Encoding.UTF8.GetString(message.Body);
//            // Example: Trigger email alert based on the message content using the EmailService.
//            await EmailService.SendEmailAlertAsync(messageBody);

//            // Complete the message to remove it from the queue (only if AutoComplete is set to false).
//            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
//        }

//        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
//        {
//            // Add your exception handling logic here.
//            // This method is called when there is an exception while processing a message.
//            return Task.CompletedTask;
//        }
//    }

//}
