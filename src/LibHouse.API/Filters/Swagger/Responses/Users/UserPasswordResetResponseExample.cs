using LibHouse.Infrastructure.Controllers.Responses.Users;
using Swashbuckle.AspNetCore.Filters;

namespace LibHouse.API.Filters.Swagger.Responses.Users
{
    public class UserPasswordResetResponseExample : IExamplesProvider<UserPasswordResetResponse>
    {
        public UserPasswordResetResponse GetExamples()
        {
            return new(passwordResetToken: "CfDJ8BfWSeGztkBHvxcCnitIZP5kDl1swdqCO0G95UP+A8w4Te5ZODtdhRSC/EsEkv54FC2FeZa5r1b/XkT6JFdMxre45J1DKxWfEh0+hdmBr3JNQeQQszEduzNiheip8Ufz7jCtPECZBTZ04XoxLrrFOBuTCnB/RJN55Nr1YUkYIrQaZD4Eh7BSEtpm1fMl6sq2n0UkfxOgwceR9dMWRFaFSZfDMq9f/+oppV0tOcr9FSdYOsIJ2dk7BRnpFUriUWAVlw==");
        }
    }
}