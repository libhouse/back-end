using LibHouse.Business.Application.Localizations.Outputs;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Controllers.Http.Localizations.Interfaces
{
    public interface IHttpAddress
    {
        void OnPostalCodeSearch(Func<string, Task<OutputPostalCodeSearch>> on);
    }
}