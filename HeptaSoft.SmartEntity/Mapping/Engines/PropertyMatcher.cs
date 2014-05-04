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
            const string ignoreChar = ">";

            if (propertyNameToMatch.ToLower() == candidate.ToLower())
            {
                return true;
            }

            if (propertyNameToMatch.Replace(ignoreChar, string.Empty).ToLower() == candidate.Replace(ignoreChar, string.Empty).ToLower())
            {
                return true;
            }

            return false;
        }
    }
}
