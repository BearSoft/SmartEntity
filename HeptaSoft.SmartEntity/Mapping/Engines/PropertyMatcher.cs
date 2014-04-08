namespace HeptaSoft.SmartEntity.Mapping.Engines
{
    internal class PropertyMatcher : IPropertyMatcher
    {
        /// <summary>
        /// Decides whether the two property names match.
        /// </summary>
        /// <param name="propertyNameToMatch">The first property name.</param>
        /// <param name="candidate">The other property name.</param>
        /// <returns></returns>
        public bool PropertyNamesMatch(string propertyNameToMatch, string candidate)
        {
            const string IgnoreChar = ">";

            if (propertyNameToMatch.ToLower() == candidate.ToLower())
            {
                return true;
            }

            if (propertyNameToMatch.Replace(IgnoreChar, string.Empty).ToLower() == candidate.Replace(IgnoreChar, string.Empty).ToLower())
            {
                return true;
            }

            return false;
        }
    }
}
