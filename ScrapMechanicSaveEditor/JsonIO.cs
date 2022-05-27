using Newtonsoft.Json;
using System.IO;

namespace ScrapMechanicSaveEditor
{
    internal static class JsonIO
    {
        /// <summary>
        /// Loads the part names from the game files
        /// </summary>
        public static GameResources LoadGameResources()
        {
            GameResources gameResources = new GameResources();

            // Load parts from the survival file
            using (StreamReader r = new StreamReader("Resources\\inventoryDescriptions.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);

                // Add the parts into the dictionary
                foreach (var item in array)
                {
                    gameResources.partNameDict.Add(item.Name, item.Value.title.Value);
                }
            }

            // Load parts from the creative file
            using (StreamReader r = new StreamReader("Resources\\inventoryItemDescriptions.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);

                foreach (var item in array)
                {
                    // Check if the parts UUID is already in the dictionary, if not, add it
                    if (gameResources.partNameDict.ContainsKey(item.Name))
                    {
                        continue;
                    }

                    gameResources.partNameDict.Add(item.Name, item.Value.title.Value);
                }
            }

            return gameResources;
        }
    }
}
