namespace API.Services.UserServices
{
    public interface IUserService
    {
        Task AssignStatus(bool status, int userId);
    }
}

