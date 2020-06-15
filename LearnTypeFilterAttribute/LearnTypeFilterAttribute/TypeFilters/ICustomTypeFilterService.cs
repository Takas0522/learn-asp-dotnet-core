namespace LearnTypeFilterAttribute.TypeFilters
{
    public interface ICustomTypeFilterService
    {
        bool AuthCheck(AccessType accessType, string setting, string customValue);
    }
}