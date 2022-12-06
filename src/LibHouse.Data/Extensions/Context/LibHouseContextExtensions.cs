using LibHouse.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibHouse.Data.Extensions.Context
{
    public static class LibHouseContextExtensions
    {
        public static async Task CleanContextDataAsync(this LibHouseContext libHouseContext)
        {
            await libHouseContext.Database.EnsureDeletedAsync();
            await libHouseContext.Database.MigrateAsync();
        }
    }
}