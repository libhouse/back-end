using System.ComponentModel;

namespace LibHouse.Business.Entities.Users
{
    public enum UserType
    {
        [Description("Resident")]
        Resident,
        [Description("Owner")]
        Owner,
    }
}