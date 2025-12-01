using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Results
{
    public interface IResult<TSuccess, TError>
    {
        bool IsSuccess { get; }
        TSuccess Success { get; }
        TError Error { get; }
    }
}
