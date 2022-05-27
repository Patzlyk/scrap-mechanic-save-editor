using System;
using System.Data.SQLite;

namespace ScrapMechanicSaveEditor
{
    internal static class SqLiteIO
    {
        /// <summary>
        /// Read the game save database
        /// </summary>
        /// <param name="savePath">Path of the save</param>
        /// <returns>Parsed save</returns>
        public static GameSave LoadSave(string savePath)
        {
            // Create an empty game save
            GameSave gameSave = new GameSave();
            gameSave.filePath = savePath;

            // Copy the database to memory
            SQLiteConnection sourceDb = new SQLiteConnection("Data Source=" + savePath);
            sourceDb.Open();
            SQLiteConnection tempDb = new SQLiteConnection("Data Source=:memory:");
            tempDb.Open();
            sourceDb.BackupDatabase(tempDb, "main", "main", -1, null, 0);
            sourceDb.Close();



            // Read the database

            byte[] inventoryBytes;
            using (SQLiteCommand command = new SQLiteCommand())
            {
                command.CommandText = "SELECT data FROM Container WHERE id=1";
                command.Connection = tempDb;
                inventoryBytes = (byte[]) command.ExecuteScalar();
            }

            tempDb.Close();



            // Parse the read data

            // Parse the hotbar/inventory data
            for (int currentSlot = 0; currentSlot < 9; currentSlot++)
            {
                // Offset 11 bytes for the data start
                int slotByteOffset = 11 + 22 * currentSlot;

                // Get the part UUID and save it
                string uuid = "";
                for (int i = 0; i < 16; i++)
                {
                    uuid += inventoryBytes[slotByteOffset + 15 - i].ToString("X2");
                }
                string parsedUUID = Program.ParseUUID(uuid);
                gameSave.hotbarItems[currentSlot] = parsedUUID;

                // Get the part count and save it
                byte count = inventoryBytes[slotByteOffset + 21];
                gameSave.hotbarCounts[currentSlot] = count;
            }

            return gameSave;
        }

        /// <summary>
        /// Writes the save into the database
        /// </summary>
        /// <param name="gameSave">Game save</param>
        public static void SaveSave(GameSave gameSave)
        {
            // Open the database
            SQLiteConnection destinationDb = new SQLiteConnection("Data Source=" + gameSave.filePath);
            destinationDb.Open();

            // Retrieve the original inventory data
            byte[] originalInventoryBytes;
            using (SQLiteCommand command = new SQLiteCommand())
            {
                command.CommandText = "SELECT data FROM Container WHERE id=1";
                command.Connection = destinationDb;
                originalInventoryBytes = (byte[])command.ExecuteScalar();
            }



            // Write the new inventory data
            byte[] newInventoryBytes = new byte[originalInventoryBytes.Length];
            int currentlyWritenByte = 0;
            // Copy the first 11 bytes from the original savefile
            for (int i = 0; i < 11; i++)
            {
                newInventoryBytes[currentlyWritenByte] = originalInventoryBytes[currentlyWritenByte];
                currentlyWritenByte++;
            }
            // Write the new hotbar data
            for (int currentHotbarItem = 0; currentHotbarItem < 9; currentHotbarItem++)
            {
                // Remove hyphens from the UUID
                string simpleUUID = gameSave.hotbarItems[currentHotbarItem].Replace("-", "");

                // Reverse the UUID
                string[] splitUUIDReverse = new string[16];
                splitUUIDReverse[0] = simpleUUID.Substring(30, 2);
                splitUUIDReverse[1] = simpleUUID.Substring(28, 2);
                splitUUIDReverse[2] = simpleUUID.Substring(26, 2);
                splitUUIDReverse[3] = simpleUUID.Substring(24, 2);
                splitUUIDReverse[4] = simpleUUID.Substring(22, 2);
                splitUUIDReverse[5] = simpleUUID.Substring(20, 2);
                splitUUIDReverse[6] = simpleUUID.Substring(18, 2);
                splitUUIDReverse[7] = simpleUUID.Substring(16, 2);
                splitUUIDReverse[8] = simpleUUID.Substring(14, 2);
                splitUUIDReverse[9] = simpleUUID.Substring(12, 2);
                splitUUIDReverse[10] = simpleUUID.Substring(10, 2);
                splitUUIDReverse[11] = simpleUUID.Substring(8, 2);
                splitUUIDReverse[12] = simpleUUID.Substring(6, 2);
                splitUUIDReverse[13] = simpleUUID.Substring(4, 2);
                splitUUIDReverse[14] = simpleUUID.Substring(2, 2);
                splitUUIDReverse[15] = simpleUUID.Substring(0, 2);

                // Convert the hex strings into bytes
                byte[] finalUUIDBytes = new byte[16];
                finalUUIDBytes[0] = (byte)Convert.ToInt32(splitUUIDReverse[0], 16);
                finalUUIDBytes[1] = (byte)Convert.ToInt32(splitUUIDReverse[1], 16);
                finalUUIDBytes[2] = (byte)Convert.ToInt32(splitUUIDReverse[2], 16);
                finalUUIDBytes[3] = (byte)Convert.ToInt32(splitUUIDReverse[3], 16);
                finalUUIDBytes[4] = (byte)Convert.ToInt32(splitUUIDReverse[4], 16);
                finalUUIDBytes[5] = (byte)Convert.ToInt32(splitUUIDReverse[5], 16);
                finalUUIDBytes[6] = (byte)Convert.ToInt32(splitUUIDReverse[6], 16);
                finalUUIDBytes[7] = (byte)Convert.ToInt32(splitUUIDReverse[7], 16);
                finalUUIDBytes[8] = (byte)Convert.ToInt32(splitUUIDReverse[8], 16);
                finalUUIDBytes[9] = (byte)Convert.ToInt32(splitUUIDReverse[9], 16);
                finalUUIDBytes[10] = (byte)Convert.ToInt32(splitUUIDReverse[10], 16);
                finalUUIDBytes[11] = (byte)Convert.ToInt32(splitUUIDReverse[11], 16);
                finalUUIDBytes[12] = (byte)Convert.ToInt32(splitUUIDReverse[12], 16);
                finalUUIDBytes[13] = (byte)Convert.ToInt32(splitUUIDReverse[13], 16);
                finalUUIDBytes[14] = (byte)Convert.ToInt32(splitUUIDReverse[14], 16);
                finalUUIDBytes[15] = (byte)Convert.ToInt32(splitUUIDReverse[15], 16);

                // Write the 16 bytes of the reverse UUID
                for (int i = 0; i < 16; i++)
                {
                    newInventoryBytes[currentlyWritenByte] = finalUUIDBytes[i];
                    currentlyWritenByte++;
                }

                // Copy the next 5 bytes
                for (int i = 0; i < 5; i++)
                {
                    newInventoryBytes[currentlyWritenByte] = originalInventoryBytes[currentlyWritenByte];
                    currentlyWritenByte++;
                }

                // Write the part count
                newInventoryBytes[currentlyWritenByte] = (byte) gameSave.hotbarCounts[currentHotbarItem];
                currentlyWritenByte++;
            }
            // Copy the rest of the inventory data
            for (int i = 0; i < originalInventoryBytes.Length - 11 - 9 * 22; i++)
            {
                newInventoryBytes[currentlyWritenByte] = originalInventoryBytes[currentlyWritenByte];
                currentlyWritenByte++;
            }


            // Rewrite the inventory field with the new data
            using (SQLiteCommand command = new SQLiteCommand())
            {
                command.CommandText = "UPDATE Container SET data=(?) WHERE id=1";
                command.Parameters.Add(new SQLiteParameter("", newInventoryBytes));
                command.Connection = destinationDb;
                command.ExecuteNonQuery();
            }

            // Close the database
            destinationDb.Close();
        }
    }
}
