# scrap-mechanic-save-editor
Simple save editor for Scrap Mechanic

WARNING: This shouldn't break your save, but you should backup before editing anyways.

This program can change items in your hotbar. Does not currently support mods.

# How to use
1. Copy translation files from Scrap Mechanic files (This only needs to be done once, or when the game is updated)
- Open your Scrap Mechanic installation directory
- Go to "\Survival\Gui\Language" and open the folder with the language you want to use
- Copy "inventoryDescriptions.json"
- Paste into "{SaveEditorFolder}\Resources"
- Go to "\Data\Gui\Language" and open the folder with the language you want to use
- Copy "InventoryItemDescriptions.json" into the same folder as the first file
- THE FILES MUST BE NAMED THIS WAY
2. Open "ScrapMechanicSaveEditor.exe"
3. Open the save file
4. Edit your save
5. Save and close the save file

# Notes
- You can get items that you normally cannot get into your inventory. These mostly start with "obj_". You should be able to place them, but you CANNOT destroy some of them.
- You can set any amount of any item, but you shouldn't exceed the normal stack size!  
Example: The game normally allows you to have a maximum of 10 Bearings in one slot, if you set more with the editor, you will not be able to place them until you split the stack.
- You also shouldn't edit your tools.

## If the program doesn't open
The game files probably weren't copied properly, the Resources folder should look like this.
![image](https://user-images.githubusercontent.com/50177915/170706393-61131121-7d59-41b9-9e25-8e6cbd01ea01.png)
