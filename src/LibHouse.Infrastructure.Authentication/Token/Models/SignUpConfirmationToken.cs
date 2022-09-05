using Ardalis.GuardClauses;
using System.Web;

namespace LibHouse.Infrastructure.Authentication.Token.Models
{
    public class SignUpConfirmationToken
    {
        public string Value { get; }
        public string EncodedValue => HttpUtility.UrlEncode(Value);

        public SignUpConfirmationToken(string value, bool isEncoded = false)
        {
            Guard.Against.NullOrEmpty(value, nameof(value), "O valor do token é obrigatório");

            Value = isEncoded ? HttpUtility.HtmlDecode(value) : value;
        }

        public override string ToString()
        {
            return $"Confirmation token: {Value}";
        }
    }
}