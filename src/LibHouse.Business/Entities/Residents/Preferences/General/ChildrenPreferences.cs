namespace LibHouse.Business.Entities.Residents.Preferences.General
{
    public record ChildrenPreferences
    {
        public ChildrenPreferences(bool acceptChildren)
        {
            AcceptChildren = acceptChildren;
        }

        public bool AcceptChildren { get; }

        public bool RequiresAcceptanceOfChildren()
        {
            return AcceptChildren;
        }
    }
}