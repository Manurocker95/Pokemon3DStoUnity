using System.IO;

namespace SFB
{
    /// <summary>Represents a platform-specific file with a Stream.</summary>
    public class ItemWithStream
    {
        /// <summary>Gets/Sets the item filename.</summary>
        public string Name { get; set; }
        public string FullName { get; set; }

        public string Path { get; set; }

        /// <summary>Sets the item file Stream.</summary>
        public Stream Stream { private get; set; }

        /// <summary>
        /// Indicates if this item has valid data.
        /// </summary>
        public bool HasData => Name != null || Stream != null;

        /// <summary>
        /// Opens the Stream to read data from this item.
        /// </summary>
        /// <returns>The opened Stream.</returns>
        public Stream OpenStream()
        {
            if (Stream == null && Name != null)
            {
                return File.OpenRead(Name);
            }
            return Stream;
        }
    }
}