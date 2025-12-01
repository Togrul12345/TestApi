namespace Application.Common.Interfaces
{
    public interface ISmsSenderService<TResultType>
    {
        Task<TResultType> SendSmsAsync(string phoneNumber, string message, string senderName);
    }
}
