using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapMechanicSaveEditor
{
    internal class GameSave
    {
        /// <summary>
        /// Complete path of the save file
        /// </summary>
        public string filePath;

        /// <summary>
        /// Items stored in the hotbar
        /// </summary>
        public string[] hotbarItems = new string[9];
        /// <summary>
        /// Count of items in the hotbar, 0-255
        /// </summary>
        public int[] hotbarCounts = new int[9];
    }
}
