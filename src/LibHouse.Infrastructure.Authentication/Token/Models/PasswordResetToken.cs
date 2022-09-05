using Ardalis.GuardClauses;
using System.Web;

namespace LibHouse.Infrastructure.Authentication.Token.Models
{
    public class PasswordResetToken
    {
        public string Value { get; }
        public string EncodedValue => HttpUtility.UrlEncode(Value);

        public PasswordResetToken(string value, bool isEncoded = false)
        {
            Guard.Against.NullOrEmpty(value, nameof(value), "O valor do token é obrigatório");

            Value = isEncoded ? HttpUtility.HtmlDecode(value) : value;
        }

        public override string ToString()
        {
            return $"Password reset token: {Value}";
        }
    }
}