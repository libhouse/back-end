using System.ComponentModel;

namespace LibHouse.Business.Entities.Users
{
    public enum Gender
    {
        [Description("Male")]
        Male,
        [Description("Female")]
        Female,
        [Description("Others")]
        Others,
        [Description("NotDeclared")]
        NotDeclared,
    }
}