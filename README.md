<p align="center">
  <img src="https://imgur.com/UHITThI.png" alt="Guided by Fate"/>
</p>

<h3 align="center">Fate?</h3>
<p align="center">
  Finding the true path the story will take...
</p>
<p align="center">
  <a href="https://discord.gg/pzJ7qVB">
    <img alt="Tower of God Discord" src="https://img.shields.io/discord/191901830526009344.svg?label=&logo=discord&logoColor=ffffff&color=7389D8&labelColor=6A7EC2">
  </a>
  <a href="https://www.reddit.com/r/TowerofGod">
	<img alt="Tower of God Reddit" src="https://aleen42.github.io/badges/src/reddit.svg">
  </a>
</p>

Can find downloads [here](https://github.com/RoloEdits/tog-bingo/releases)

I made this app for Tower of God Wiki/Reddit Discord, but there is no reason this couldn't be used by other communities. This is the format that the App is based around:

<p align="center">
  <img src="https://user-images.githubusercontent.com/12489689/169417021-1cef0ba1-3d51-4867-a5d0-2b85d8d93f07.png" width="500" alt="Bingo"/>
</p>



# Usage

I tried to make the app as simple to interface with as can be. After having downloaded the App, simply put it in a directory of your choice. I have imagined it would go into a folder, and then along with it, another folder to hold the Chapter Spreadsheets, but they dont have to be in the same place for the App to work. 

Example:
```
Tower Of God Bingo
    ->  Chapters
    ->  tog-bingo.exe
```

### Spreadsheet

To start with, the spreadsheet:

![image](https://user-images.githubusercontent.com/12489689/169411171-709933cb-412d-416f-8788-b41f319d4d52.png)

The app works on a per file basis, so each new chapter, or just any new bingo in general, would have its own file. All entries are done on the first sheet. Column A is where all names go. Column B is where their guesses go. The names should be continuous, with no empy row in-between any names. If done as such, no names past the break would be analyzed.

The only valid characters are `Y` for Yes, `N` for No, and `P` for Pass. Any other character will be scored as an incorrect match, and their score would suffer from it. A player can only Pass on the bonus sqaures. If they attempt to Pass on sqaures they can't, the app will mark it as an incorrect guess by them. Case doesnt matter, nor any spaces.

### Path

Getting the path to the file is pretty simple. In the normal Windows Explorer, you can click the arrow on the top right and more options will cascade. Simply click the file to highlight it, and then click "Copy path". This will copy it to the clipboard.

![image](https://user-images.githubusercontent.com/12489689/169411953-563c7b3b-7abb-4711-a8fd-705c7b1171d1.png)

If you are using OneCommander, there is an option on the bottom of the right-click context menu.

![image](https://user-images.githubusercontent.com/12489689/169410927-370a32b6-07ac-4a81-af19-12c3a5d6157e.png)

(Note: Keyboard shortcut only works in OneCommander)

On Linux you can simply just copy the file as if to copy the file,  and when you paste inside a text field, it will paste the path to the file.

### Running App
Note: **The Spreadsheet Must Not be Opened By Another Program When Using App**

On Windows you can simply open it like any other program.

On Linux you can right-click in an empy space inside the directory, and click open in terminal.

![image](https://user-images.githubusercontent.com/12489689/169414950-17b5a7c7-e26b-4248-ad15-aa03782a9762.png)

This will open the current directory in the terminal. From there type `./` and then the program name. Just typing tog and pressing `TAB` should be enough to auto complete. 

![image](https://user-images.githubusercontent.com/12489689/169415250-55369479-c423-4205-a49f-e31bf32f88cc.png)

### Using App

Upon running the app, it will ask you to decide on a choice:

![image](https://user-images.githubusercontent.com/12489689/169417941-175d5098-88e5-42ba-8e5f-462105187cf6.png)

#### Default
Entering `1` will load the default settings based on this format:

<img src="https://user-images.githubusercontent.com/12489689/169417653-5d974de1-aeb1-4a75-942c-6ee1993f184a.png" width="300" alt="Bingo"/>

That is to say a 4x3 grid, with the bonus being the final column. Think of it like a 3x3 + 1 Column:

`10` `10` `10`  `20`

`30` `30` `30`  `60`

`50` `50` `50`  `100`

After entering `1` and pressing `ENTER` to submit, it will inform you the settings were loaded and now ask for the path you should have copied to your clipboard.

On Windows you can simply `Right-Mouse CLICK` and it should paste the path.

You can also press `CTRL + SHFT + V` on your keyboard to paste it. This option will also work on Linux.

One you have pasted it, press `ENTER` to submit it. The Program will now ask for you to enter a Key. This Key is the correct sequence of the bingo once the correct bingo is known. The players guesses are compared against the known correct Key.

The order is read from left to right, and row to row.

-->

`10` `10` `10`  `20`


-->

`30` `30` `30`  `60`


-->

`50` `50` `50`  `100`

For the default settings, the key must be 12 charachters long. If you do not enter enough, the program will tell you, and ask to be closed. You will then need to run the App again and reenter the `PATH` and then give a valid key entry. As with the Guess entries, only a `Y` and `N` should be considered valid. A Key should not contain any other characters. Case doesnt matter, nor any spaces.

![image](https://user-images.githubusercontent.com/12489689/169419350-000775c1-ccc9-4e2b-be2e-f871794a148b.png)

Upon entering in the key press `ENTER` to submit. If there are no errors, it will tell you it successfully completed the operation, and tell you how many guesses have been gone through. 

![image](https://user-images.githubusercontent.com/12489689/169420331-65a668fa-ca67-4a58-be43-81662bae4580.png)

The output file is saved in the same location as the previously entered path, and takes the same name as well. It saves it as a Markdown file, but it also functions just fine as a normal text document. The output file is pre ordered from highest score to lowest, with the names corrosponding.

![image](https://user-images.githubusercontent.com/12489689/169420412-2306a525-4104-4f3b-9434-35929b0e1a55.png)

![image](https://user-images.githubusercontent.com/12489689/169420438-0e8599fa-ed9d-4642-8ded-765928293b41.png)

### Windows Video Demo

https://user-images.githubusercontent.com/12489689/169410567-bd73cd2a-0bc5-404c-b2b3-1dfb9e746ded.mp4


### Linux Video Demo

https://user-images.githubusercontent.com/12489689/169419831-eedaaec2-5261-47f6-9537-bdf750da77e8.mp4



