using LibHouse.Business.Entities.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibHouse.Business.Entities.Localizations
{
    public interface IAddressRepository : IEntityRepository<Address>
    {
        Task<Address> GetFirstAddressFromPostalCodeAsync(string postalCodeNumber);
        Task<IEnumerable<Address>> GetAddressesByPostalCodeAndNumberAsync(string postalCodeNumber, ushort addressNumber);
    }
}