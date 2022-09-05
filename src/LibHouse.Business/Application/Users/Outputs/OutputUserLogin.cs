using System;
using System.Collections.Generic;

namespace LibHouse.Business.Application.Users.Outputs
{
    public record OutputUserLogin
    {
        public bool IsSuccess { get; init; }
        public string LoginMessage { get; init; }
        public Guid? UserId { get; init; }
        public string UserFullName { get; init; }
        public DateTime? UserBirthDate { get; init; }
        public string UserGender { get; init; }
        public string UserEmail { get; init; }
        public string UserType { get; init; }
        public string LoginToken { get; init; }
        public DateTime? ExpiresInLoginToken { get; init; }
        public string LoginRenewalToken { get; init; }
        public DateTime? ExpiresInLoginRenewalToken { get; init; }
        public IEnumerable<KeyValuePair<string, string>> Claims { get; init; }

        public OutputUserLogin(
            bool isSuccess = false, 
            string loginMessage = "",
            Guid? userId = null, 
            string userFullName = "", 
            DateTime? userBirthDate = null, 
            string userGender = "", 
            string userEmail = "", 
            string userType = "", 
            string loginToken = "", 
            DateTime? expiresInLoginToken = null, 
            string loginRenewalToken = "", 
            DateTime? expiresInLoginRenewalToken = null, 
            IEnumerable<KeyValuePair<string, string>> claims = null)
        {
            IsSuccess = isSuccess;
            LoginMessage = loginMessage;
            UserId = userId;
            UserFullName = userFullName;
            UserBirthDate = userBirthDate;
            UserGender = userGender;
            UserEmail = userEmail;
            UserType = userType;
            LoginToken = loginToken;
            ExpiresInLoginToken = expiresInLoginToken;
            LoginRenewalToken = loginRenewalToken;
            ExpiresInLoginRenewalToken = expiresInLoginRenewalToken;
            Claims = claims;
        }
    }
}