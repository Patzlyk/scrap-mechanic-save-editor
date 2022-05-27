using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrapMechanicSaveEditor
{
    public partial class MainForm : Form
    {
        private GameResources gameResources;

        /// <summary>
        /// List of controls that will be disabled if a save is not loaded
        /// </summary>
        private List<Control> controlsDisabledWithoutSave;
        /// <summary>
        /// List of controls that will be disabled if a save is loaded
        /// </summary>
        private List<Control> controlsDisabledWithSave;
        /// <summary>
        /// Full path of the current save file
        /// </summary>
        private string currentSaveFilePath;
        private GameSave gameSave;

        public MainForm()
        {
            InitializeComponent();

            controlsDisabledWithoutSave = new List<Control>
            {
                hotbarItem0,
                hotbarItem1,
                hotbarItem2,
                hotbarItem3,
                hotbarItem4,
                hotbarItem5,
                hotbarItem6,
                hotbarItem7,
                hotbarItem8,
                saveSaveFileButton,
                closeSaveFileButton,
                hotbarCount0,
                hotbarCount1,
                hotbarCount2,
                hotbarCount3,
                hotbarCount4,
                hotbarCount5,
                hotbarCount6,
                hotbarCount7,
                hotbarCount8
            };
            controlsDisabledWithSave = new List<Control>
            {
                openSaveFileButton
            };

            // Load resources from game files
            gameResources = JsonIO.LoadGameResources();
            gameResources.partUUIDs = gameResources.partNameDict.Keys.ToArray();
            gameResources.partNames = gameResources.partNameDict.Values.ToArray();
        }

        /// <summary>
        /// Enables controls after a save is loaded
        /// Loads control values from the gameSave
        /// </summary>
        private void EnableEditor()
        {
            hotbarItem0.DataSource = gameResources.partNames;
            hotbarItem1.DataSource = gameResources.partNames;
            hotbarItem2.DataSource = gameResources.partNames;
            hotbarItem3.DataSource = gameResources.partNames;
            hotbarItem4.DataSource = gameResources.partNames;
            hotbarItem5.DataSource = gameResources.partNames;
            hotbarItem6.DataSource = gameResources.partNames;
            hotbarItem7.DataSource = gameResources.partNames;
            hotbarItem8.DataSource = gameResources.partNames;

            hotbarItem0.BindingContext = new BindingContext();
            hotbarItem1.BindingContext = new BindingContext();
            hotbarItem2.BindingContext = new BindingContext();
            hotbarItem3.BindingContext = new BindingContext();
            hotbarItem4.BindingContext = new BindingContext();
            hotbarItem5.BindingContext = new BindingContext();
            hotbarItem6.BindingContext = new BindingContext();
            hotbarItem7.BindingContext = new BindingContext();
            hotbarItem8.BindingContext = new BindingContext();

            // Load values from the game save
            hotbarItem0.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[0])];
            hotbarItem1.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[1])];
            hotbarItem2.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[2])];
            hotbarItem3.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[3])];
            hotbarItem4.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[4])];
            hotbarItem5.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[5])];
            hotbarItem6.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[6])];
            hotbarItem7.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[7])];
            hotbarItem8.SelectedItem = gameResources.partNames[Program.IndexOfArray(gameResources.partUUIDs, gameSave.hotbarItems[8])];

            hotbarCount0.Value = gameSave.hotbarCounts[0];
            hotbarCount1.Value = gameSave.hotbarCounts[1];
            hotbarCount2.Value = gameSave.hotbarCounts[2];
            hotbarCount3.Value = gameSave.hotbarCounts[3];
            hotbarCount4.Value = gameSave.hotbarCounts[4];
            hotbarCount5.Value = gameSave.hotbarCounts[5];
            hotbarCount6.Value = gameSave.hotbarCounts[6];
            hotbarCount7.Value = gameSave.hotbarCounts[7];
            hotbarCount8.Value = gameSave.hotbarCounts[8];

            foreach (Control control in controlsDisabledWithoutSave)
            {
                control.Enabled = true;
            }

            foreach (Control control in controlsDisabledWithSave)
            {
                control.Enabled = false;
            }
        }

        /// <summary>
        /// Disables controls after a file is closed
        /// </summary>
        private void DisableEditor()
        {
            foreach (Control control in controlsDisabledWithoutSave)
            {
                control.Enabled = false;
            }

            foreach (Control control in controlsDisabledWithSave)
            {
                control.Enabled = true;
            }
        }

        /// <summary>
        /// Opens the file choosing dialog
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OpenSaveFileButton_Click(object sender, EventArgs e)
        {
            openSaveFileDialog.ShowDialog();
        }

        /// <summary>
        /// Called when a file is opened
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OpenSaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            // Read the saveFile
            currentSaveFilePath = openSaveFileDialog.FileName;
            gameSave = SqLiteIO.LoadSave(currentSaveFilePath);


            // Enable the editor controls
            currentSaveFileLabel.Text = openSaveFileDialog.SafeFileName;
            EnableEditor();
        }

        /// <summary>
        /// Called when the "Save file" button is pressed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void SaveSaveFileButton_Click(object sender, EventArgs e)
        {
            // Read values from the controls
            gameSave.hotbarItems[0] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem0.SelectedItem)];
            gameSave.hotbarItems[1] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem1.SelectedItem)];
            gameSave.hotbarItems[2] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem2.SelectedItem)];
            gameSave.hotbarItems[3] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem3.SelectedItem)];
            gameSave.hotbarItems[4] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem4.SelectedItem)];
            gameSave.hotbarItems[5] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem5.SelectedItem)];
            gameSave.hotbarItems[6] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem6.SelectedItem)];
            gameSave.hotbarItems[7] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem7.SelectedItem)];
            gameSave.hotbarItems[8] = gameResources.partUUIDs[Program.IndexOfArray(gameResources.partNames, hotbarItem8.SelectedItem)];

            gameSave.hotbarCounts[0] = (int)hotbarCount0.Value;
            gameSave.hotbarCounts[1] = (int)hotbarCount1.Value;
            gameSave.hotbarCounts[2] = (int)hotbarCount2.Value;
            gameSave.hotbarCounts[3] = (int)hotbarCount3.Value;
            gameSave.hotbarCounts[4] = (int)hotbarCount4.Value;
            gameSave.hotbarCounts[5] = (int)hotbarCount5.Value;
            gameSave.hotbarCounts[6] = (int)hotbarCount6.Value;
            gameSave.hotbarCounts[7] = (int)hotbarCount7.Value;
            gameSave.hotbarCounts[8] = (int)hotbarCount8.Value;

            // Write the save into the file
            SqLiteIO.SaveSave(gameSave);
        }

        /// <summary>
        /// Called when the "Close file" button is pressed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void CloseSaveFileButton_Click(object sender, EventArgs e)
        {
            currentSaveFilePath = null;

            // Disable the editor controls
            currentSaveFileLabel.Text = "No file open";
            DisableEditor();
        }
    }
}
