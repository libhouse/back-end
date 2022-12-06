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

        public override async Task AddAsync(Address address)
        {
            _ = await ExecuteStatementAsync($@"
                EXEC [Business].[sp_address_addAddress]
                    @AddressId={address.Id},
                    @Description={address.GetName()},
                    @Number={address.GetNumber()},
                    @Complement={address.GetComplement()},
                    @Neighborhood={address.GetNeighborhoodName()},
                    @PostalCode={address.GetPostalCodeNumber()},
                    @City={address.GetCityName()},
                    @FederativeUnit={address.GetAbbreviationOfTheFederativeUnit()}");
        }

        public async Task<IEnumerable<Address>> GetAddressesByPostalCodeAndNumberAsync(string postalCodeNumber, ushort addressNumber)
        {
            return await _dbSet.Where(address => address.PostalCode.PostalCodeNumber == postalCodeNumber && address.AddressNumber.Number == addressNumber).ToListAsync();
        }

        public async Task<Address> GetFirstAddressFromPostalCodeAsync(string postalCodeNumber)
        {
            List<Address> postalCodeAddresses = await _dbSet.Where(address => address.PostalCode.PostalCodeNumber == postalCodeNumber).ToListAsync();
            return postalCodeAddresses.OrderBy(a => a.GetNumber()).FirstOrDefault();
        }
    }
}