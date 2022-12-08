using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Swashbuckle.AspNetCore.Filters;

namespace LibHouse.API.Filters.Swagger.Requests.Users
{
    public class ConfirmUserPasswordResetViewModelExample : IExamplesProvider<ConfirmUserPasswordResetViewModel>
    {
        public ConfirmUserPasswordResetViewModel GetExamples()
        {
            return new()
            {
                ConfirmPassword = "Senh@1234567",
                Password = "Senh@1234567",
                PasswordResetToken = "CfDJ8BfWSeGztkBHvxcCnitIZP5kDl1swdqCO0G95UP+A8w4Te5ZODtdhRSC/EsEkv54FC2FeZa5r1b/XkT6JFdMxre45J1DKxWfEh0+hdmBr3JNQeQQszEduzNiheip8Ufz7jCtPECZBTZ04XoxLrrFOBuTCnB/RJN55Nr1YUkYIrQaZD4Eh7BSEtpm1fMl6sq2n0UkfxOgwceR9dMWRFaFSZfDMq9f/+oppV0tOcr9FSdYOsIJ2dk7BRnpFUriUWAVlw==",
                UserEmail = "lucas.dirani@gmail.com"
            };
        }
    }
}