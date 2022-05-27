using System;
using System.Windows.Forms;

namespace ScrapMechanicSaveEditor
{
    internal static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Parses an UUID
        /// </summary>
        /// <param name="originalUUID">UUID without "-"</param>
        /// <returns>UUID in the format "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"</returns>
        /// <exception cref="Exception">Thrown if the length of the UUID is not correct</exception>
        public static string ParseUUID(string originalUUID)
        {
            // Check the length of the original UUID
            if (originalUUID.Length != 32) throw new Exception("The UUID is not the correct length");

            string parsedUUID = originalUUID;

            // Insert hyphens into the UUID
            parsedUUID = parsedUUID.Insert(8, "-");
            parsedUUID = parsedUUID.Insert(13, "-");
            parsedUUID = parsedUUID.Insert(18, "-");
            parsedUUID = parsedUUID.Insert(23, "-");

            // Convert into lowercase
            parsedUUID = parsedUUID.ToLower();

            return parsedUUID;
        }

        /// <summary>
        /// Get the index of an item in an array
        /// </summary>
        /// <param name="array">Array the item should be in</param>
        /// <param name="item">Item of which the index should be found</param>
        /// <returns>Index of the item</returns>
        /// <exception cref="Exception">Thrown if the array does not contain the item</exception>
        public static int IndexOfArray(Array array, dynamic item)
        {
            for (int i = 0; i < array.Length; i++)
            {
                dynamic currentArrayItem = array.GetValue(i);

                if (currentArrayItem.Equals(item))
                {
                    return i;
                }
            }

            throw new Exception("Item not found in array");
        }
    }
}
