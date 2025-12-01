using Domain.Common.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Results
{
    public static class Result
    {

        public static IResult<DomainSuccess<T>, DomainError> Success<T>(DomainSuccess<T> success)
        {
            return new ResultImpl<T>
            {
                IsSuccess = true,
                Success = success,
                Error = null
            };
        }

        public static IResult<DomainSuccess<T>, DomainError> Fail<T>(DomainError error)
        {
            return new ResultImpl<T>
            {
                IsSuccess = false,
                Success = null,
                Error = error
            };
        }

        private class ResultImpl<T> : IResult<DomainSuccess<T>, DomainError>
        {
            public bool IsSuccess { get; set; }

            public DomainSuccess<T> Success { get; set; }

            public DomainError Error { get; set; }
        }
    }

}

