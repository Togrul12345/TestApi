using Domain.Common.Interfaces;
using Domain.Common.Utility;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Infrastructure.Services.Domain
{
    public class LocalizationService : ILocalizationService
    {                                                                                                                                                                                                                                   
        private readonly IConfiguration _configuration;                                                                                                                         
        private readonly Dictionary<string, Dictionary<string, string>> _resources;

        public string this[string key] => GetResourceString(key);

        public LocalizationService(IConfiguration configuration)
        {   
            _configuration = configuration;
            _resources = new Dictionary<string, Dictionary<string, string>>();
            LoadResources();
        }

        public void LoadResources()
        {
            var supportedCultures = _configuration.GetSection("SupportedCultures").Get<string[]>();
            foreach (var cultureName in supportedCultures!)
            {
                var filePath = Path.Combine("Resources", $"{cultureName}.json");
                if (File.Exists(filePath))
                {
                    var jsonString = File.ReadAllText(filePath);
                    var resource = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
                    _resources[cultureName] = resource!;
                }
            }
        }
            
        public string GetResourceString(string key)
        {
            if (_resources.TryGetValue(Utilities.GetCultureLanguageName(), out var resources))
            {
                if (resources.TryGetValue(key, out var message))
                {
                    return message;
                }
            }
            return $"[{key}]";
        }

        public string GetResourceString(string key, string lng)
        {
            if (_resources.TryGetValue(lng, out var resources))
            {
                if (resources.TryGetValue(key, out var message))
                {
                    return message;
                }
            }
            return $"[{key}]";
        }

        public string GetResourceStringWithValues(string key, params string[] values)
        {
            if (_resources.TryGetValue(Utilities.GetCultureLanguageName(), out var resources))
            {
                if (resources.TryGetValue(key, out var message))
                {
                    return string.Format(message, values);
                }
            }
            return $"[{key}]";
        }
    }
}
