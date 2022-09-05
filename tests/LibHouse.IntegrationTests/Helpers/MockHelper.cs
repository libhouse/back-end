using Microsoft.AspNetCore.Identity;
using Moq;

namespace LibHouse.IntegrationTests.Helpers
{
    internal static class MockHelper
    {
        internal static Mock<UserManager<IdentityUser>> CreateMockForUserManager()
        {
            return new(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        }
    }
}