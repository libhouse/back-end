using LibHouse.Business.Entities.Localizations;
using LibHouse.Infrastructure.Cache.Providers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Cache.Decorators.Memory
{
    public class AddressMemoryCachingDecorator<T> : IAddressRepository
        where T : IAddressRepository
    {
        private readonly T _addressRepository;
        private readonly MemoryCaching _memoryCaching;

        public AddressMemoryCachingDecorator(
            T addressRepository, 
            MemoryCaching memoryCaching)
        {
            _addressRepository = addressRepository;
            _memoryCaching = memoryCaching;
        }

        public async Task AddAsync(Address entity)
        {
            await _addressRepository.AddAsync(entity);
        }

        public async Task<int> CountAsync(Expression<Func<Address, bool>> expression)
        {
            return await _addressRepository.CountAsync(expression);
        }

        public async Task<int> ExecuteStatementAsync(FormattableString statement)
        {
            return await _addressRepository.ExecuteStatementAsync(statement);
        }

        public async Task<Address> FirstAsync(Expression<Func<Address, bool>> expression)
        {
            return await _addressRepository.FirstAsync(expression);
        }

        public async Task<Address> GetAddressByPostalCodeAndNumberAsync(string postalCodeNumber, ushort addressNumber)
        {
            return await _addressRepository.GetAddressByPostalCodeAndNumberAsync(postalCodeNumber, addressNumber);
        }

        public async Task<List<Address>> GetAsync(
            Expression<Func<Address, bool>> expression = null, 
            int? skip = null,
            int? take = null)
        {
            return await _addressRepository.GetAsync(expression, skip, take);
        }

        public async Task<Address> GetByIdAsNoTrackingAsync(Guid id)
        {
            return await _addressRepository.GetByIdAsNoTrackingAsync(id);
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task<Address> GetFirstAddressFromPostalCodeAsync(string postalCodeNumber)
        {
            bool addressExistsInCache = _memoryCaching.CheckIfResourceExists(postalCodeNumber);
            if (addressExistsInCache)
            {
                return _memoryCaching.GetResource<Address>(postalCodeNumber);
            }
            Address addressFromDatabase = await _addressRepository.GetFirstAddressFromPostalCodeAsync(postalCodeNumber);
            if (addressFromDatabase is not null)
            {
                return await _memoryCaching.GetOrCreateResourceAsync(
                    key: postalCodeNumber,
                    slidingExpiration: TimeSpan.FromMinutes(5),
                    absoluteExpiration: TimeSpan.FromMinutes(5),
                    resource: addressFromDatabase
                );
            }
            return addressFromDatabase;
        }

        public async Task<Projection> GetProjectionAsync<Projection>(
            Expression<Func<Address, bool>> expression, 
            Expression<Func<Address, Projection>> projection)
        {
            return await _addressRepository.GetProjectionAsync(expression, projection);
        }

        public void Remove(Address entity)
        {
            _addressRepository.Remove(entity);
        }

        public void Update(Address entity)
        {
            _addressRepository.Update(entity);
        }
    }
}