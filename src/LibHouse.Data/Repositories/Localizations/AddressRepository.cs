using LibHouse.Business.Entities.Localizations;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibHouse.Data.Repositories.Localizations
{
    public class AddressRepository : EntityTypeRepository<Address>, IAddressRepository
    {
        public AddressRepository(LibHouseContext context)
            : base(context)
        {
        }

        public async Task<Address> GetFirstAddressFromPostalCodeAsync(string postalCodeNumber)
        {
            List<Address> postalCodeAddresses = await _dbSet.Where(address => address.PostalCode.PostalCodeNumber == postalCodeNumber).ToListAsync();
            return postalCodeAddresses.OrderBy(a => a.GetNumber()).FirstOrDefault();
        }
    }
}