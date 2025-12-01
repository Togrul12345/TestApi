namespace Infrastructure.Services.Internal
{
    //public sealed class CustomerService : ICustomerService
    //{
    //    private readonly ILocalizationService _localizer;

    //    public CustomerService(ILocalizationService localizer)
    //    {
    //        _localizer = localizer;
    //    }

    //    public IResult<Customer, DomainError> ValidateExistingCustomer(Customer customer)
    //    {
    //        if (customer.CustomerStatusId == (int)CustomerStatuses.Blocked)
    //        {
    //            if (Utilities.GetDatetimeNowByEnvironment() <= customer.OtpRequestTime.AddDays(1))
    //                return Result.Failure<Customer, DomainError>(DomainError.BadRequest(_localizer[nameof(ResponseMessages.AccountBlocked24Hours)]));

    //            customer.CustomerStatusId = (int)CustomerStatuses.PendingVerification;
    //            customer.FailedOtpCodeAttemptCount = 0;
    //        }
    //        else if (customer.IsOtpRequest && Utilities.GetDatetimeNowByEnvironment() <= customer.OtpRequestTime.AddMinutes(1))
    //        {
    //            return Result.Failure<Customer, DomainError>(DomainError.BadRequest(_localizer[nameof(ResponseMessages.OtpRequestNotFinished)]));
    //        }

    //        return Result.Success<Customer, DomainError>(customer);
    //    }
    //}

}
