using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapMechanicSaveEditor
{
    internal class GameResources
    {
        /// <summary>
        /// Contains the UUID and name of every loaded part
        /// </summary>
        public Dictionary<string, string> partNameDict = new Dictionary<string, string>
        {
            { "00000000-0000-0000-0000-000000000000", "None" }
        };

        /// <summary>
        /// Contains part UUIDs in the same order as partNames
        /// </summary>
        public string[] partUUIDs;
        /// <summary>
        /// Contains part names in the same order as partUUIDs
        /// </summary>
        public string[] partNames;
    }
}
