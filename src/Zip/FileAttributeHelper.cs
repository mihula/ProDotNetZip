using System.IO;
using Interop = System.Runtime.InteropServices;

namespace Ionic.Zip
{
    /// <summary>
    /// Helper class for file attribute operations.
    /// </summary>
    /// <example>
    /// FileAttributes attributesToCheck = (FileAttributes)0x27; // Example attributes
    /// FileAttributes validAttributes = FileAttributeHelper.StripInvalidAttributes(attributesToCheck);
    /// <para/>
    /// Console.WriteLine($"Original Attributes: {attributesToCheck}");
    /// Console.WriteLine($"Valid Attributes: {validAttributes}");
    /// </example>
    public static class FileAttributeHelper
    {
        // Define a mask of valid attributes for Windows and non-Windows systems
        private static readonly FileAttributes ValidAttributesWindows =
            FileAttributes.ReadOnly |
            FileAttributes.Hidden |
            FileAttributes.System |
            FileAttributes.Directory |
            FileAttributes.Archive |
            FileAttributes.Device |
            FileAttributes.Normal |
            FileAttributes.Temporary |
            FileAttributes.SparseFile |
            FileAttributes.ReparsePoint |
            FileAttributes.Compressed |
            FileAttributes.Offline |
            FileAttributes.NotContentIndexed |
            FileAttributes.Encrypted |
            FileAttributes.IntegrityStream |
            FileAttributes.NoScrubData;

        private static readonly FileAttributes ValidAttributesNonWindows =
            FileAttributes.ReadOnly |
            FileAttributes.Hidden |
            FileAttributes.System |
            FileAttributes.Directory |
            FileAttributes.Archive |
            FileAttributes.Normal |
            FileAttributes.Temporary;

        /// <summary>
        /// Strips invalid file attributes, leaving only those valid for the current operating system. 
        /// </summary> 
        /// <param name="attributes">The file attributes to be validated.</param> 
        /// <returns>A set of valid file attributes.</returns>
        public static FileAttributes StripInvalidAttributes(FileAttributes attributes)
        {
            var validAttributes = Interop.RuntimeInformation.IsOSPlatform(Interop.OSPlatform.Windows) ? ValidAttributesWindows : ValidAttributesNonWindows;
            return attributes & validAttributes;
        }
    }
}