using System;

namespace LibHouse.Infrastructure.Authentication.Token.Models
{
    public class RefreshToken
    {
        public Guid Id { get; }
        public string UserId { get; }
        public string Token { get; }
        public string JwtId { get; }
        public bool IsUsed { get; private set; }
        public bool IsRevoked { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? RevokedAt { get; private set; }
        public DateTime ExpiresIn { get; }

        public RefreshToken(
            string token, 
            string jwtId, 
            string userId,
            DateTime createdAt, 
            DateTime expiresIn,
            bool isUsed = false,
            bool isRevoked = false,
            DateTime? revokedAt = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            JwtId = jwtId;
            IsUsed = isUsed;
            IsRevoked = isRevoked;
            CreatedAt = createdAt;
            ExpiresIn = expiresIn;
            RevokedAt = revokedAt;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
        }

        public void MarkAsRevoked()
        {
            if (!IsRevoked)
            {
                IsRevoked = true;
                RevokedAt = DateTime.UtcNow;
            }
        }

        public override string ToString()
        {
            return $"Refresh token {Token} pertencente ao usuário: {UserId}";
        }
    }
}