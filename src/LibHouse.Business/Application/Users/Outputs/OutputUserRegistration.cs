namespace LibHouse.Business.Application.Users.Outputs
{
    public record OutputUserRegistration
    {
        public bool IsSuccess { get; init; }
        public string RegistrationToken { get; init; }
        public string RegistrationMessage { get; init; }

        public OutputUserRegistration(
            bool isSuccess = false, 
            string registrationToken = "",
            string registrationMessage = "")
        {
            IsSuccess = isSuccess;
            RegistrationToken = registrationToken;
            RegistrationMessage = registrationMessage;
        }
    }
}