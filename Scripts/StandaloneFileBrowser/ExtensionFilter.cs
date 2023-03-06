namespace SFB
{
    /// <summary>
    /// Represents a file picker extension filter.
    /// </summary>
    public struct ExtensionFilter
    {
        /// <summary>
        /// Filter description.
        /// </summary>
        public string Name;

        /// <summary>
        /// Filter extensions.
        /// </summary>
        public string[] Extensions;

        /// <summary>Represents a file picker extension filter.</summary>
        /// <param name="filterName">The filter description.</param>
        /// <param name="filterExtensions">The filter extensions.</param>
        public ExtensionFilter(string filterName, params string[] filterExtensions)
        {
            Name = filterName;
            Extensions = filterExtensions;
        }
    }
}