using Domain.Common.Helpers;
using Microsoft.Extensions.Configuration;
using System.Text;
using XSystem.Security.Cryptography;

namespace Infrastructure.Services.External
{
    public class AtlSmsService  /*ISmsSenderService<string>*/
    {
        private readonly IConfiguration configuration;

        public AtlSmsService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> SendSmsAsync(string phoneNumber, string message, string senderName)
        {
            string key = CreateMd5(configuration["SmsConfig:NotificationPassword"]);

            string url = GetNotificationSmsUrl(key, senderName, phoneNumber, message);

            string smsResult = await HttpHelper.GetMethodAsync(url);

            return smsResult;
        }

        private string CreateMd5(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }

        private string GetNotificationSmsUrl(string key, string senderName, string phoneNumber, string text)
        {
            return string.Format(configuration["SmsConfig:NotificationSmsUrl"], configuration["SmsConfig:NotificationLogin"], key, senderName, phoneNumber, text);
        }
    }
}
