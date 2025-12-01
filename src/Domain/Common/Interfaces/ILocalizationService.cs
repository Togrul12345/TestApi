namespace Domain.Common.Interfaces
{
    public interface ILocalizationService
    {
        string this[string key] { get; } // indexer

        public string GetResourceString(string key);
        public string GetResourceString(string key, string lng);
        public string GetResourceStringWithValues(string key, params string[] values);
        public void LoadResources();
    }
}
