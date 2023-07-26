using Azure.Communication.Email;
using Azure;
using Newtonsoft.Json.Linq;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.Options;

namespace Device_Management.Services.Email
{
    public class EmailOptions
    {
        public string ConnectionString { get; set; }
    }

    public interface IEmailService
    {
        Task SendAlertEmailAsync(float temperature, JObject? message);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailClient _emailClient;
        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailClient = new EmailClient(emailOptions.Value.ConnectionString);
        }


        public async Task SendAlertEmailAsync(float temperature, JObject? message)
        {
            var deviceId = "placeholder";
            var subject = "Abnormal Temperature Alert";
            var htmlContent = $"<html><body><h1>{subject}</h1><br/><h4>Abnormal temperature {temperature} detected on device {deviceId}.</h4><p>full message info: {message}</p></body></html>";
            var sender = "DoNotReply@85598235-8fc4-4184-a4d5-d7c14c6f4cbf.azurecomm.net";
            var recipient = "davidhuang1203@hotmail.com";

            try
            {
                Console.WriteLine("Sending email...");
                EmailSendOperation emailSendOperation = await _emailClient.SendAsync(
                    Azure.WaitUntil.Completed,
                    sender,
                    recipient,
                    subject,
                    htmlContent);
                EmailSendResult statusMonitor = emailSendOperation.Value;

                Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

                /// Get the OperationId so that it can be used for tracking the message for troubleshooting
                string operationId = emailSendOperation.Id;
                Console.WriteLine($"Email operation id = {operationId}");
            }
            catch (RequestFailedException ex)
            {
                /// OperationID is contained in the exception message and can be used for troubleshooting purposes
                Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
            }
        }
    }
}
