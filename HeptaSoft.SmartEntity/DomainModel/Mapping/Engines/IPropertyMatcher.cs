namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Engines
{
    public interface IPropertyMatcher
    {
        /// <summary>
        /// Decides whether the two property names match.
        /// </summary>
        /// <param name="propertyNameToMatch">The first property name.</param>
        /// <param name="candidate">The other property name.</param>
        /// <returns></returns>
        bool PropertyNamesMatch(string propertyNameToMatch, string candidate);
    }
}