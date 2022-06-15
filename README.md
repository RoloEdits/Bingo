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

I tried to make the app as simple to interface with as can be. After having downloaded the App, simply put it in a directory of your choice. I have imagined it would go into a folder, and then along with it, another folder to hold the Chapter Spreadsheets, but they don't have to be in the same place for the App to work.

Example:

```bash
Tower Of God Bingo
    ->  Chapters
    ->  tog-bingo.exe
```

If you need a free and open scource desktop spreadsheet app I recommend [OnlyOffice](https://www.onlyoffice.com/). Otherwise a Google Spreadsheet can be downloaded in the `xlsx` format.

## Spreadsheet

To start with, the spreadsheet:

![image](https://user-images.githubusercontent.com/12489689/173170873-e45541d8-8687-42cf-9de5-b1e7f93971d1.png)

The app works on a per file basis, so each new chapter, or just any new bingo in general, would have its own file. All entries are done on the first sheet. Column A is where all names go. Column B is where their guesses go. The names should be continuous, with no empty row in-between any names. If done as such, no names past the break would be analyzed.

The only valid characters are `Y` for Yes, `N` for No, and `P` for Pass. Any other character will be scored as an incorrect match, and their score would suffer from it. A player can only Pass on the bonus squares. If they attempt to Pass on squares they can't, the app will mark it as an incorrect guess by them. Case doesn't matter, nor any spaces.

### Path

Getting the path to the file is pretty simple. In the normal Windows Explorer, you can click the arrow on the top right and more options will cascade. Simply click the file to highlight it, and then click "Copy path". This will copy it to the clipboard.

![image](https://user-images.githubusercontent.com/12489689/169411953-563c7b3b-7abb-4711-a8fd-705c7b1171d1.png)

If you are using OneCommander, there is an option on the bottom of the right-click context menu.

![image](https://user-images.githubusercontent.com/12489689/169410927-370a32b6-07ac-4a81-af19-12c3a5d6157e.png)

(Note: Keyboard shortcut only works in OneCommander)

On Linux you can simply just copy the file as if to copy the file,  and when you paste inside a text field, it will paste the path to the file.

## Running App

Note: **The Spreadsheet Must Not be Opened By Another Program When Using App**

On Windows you can simply open it like any other program.

On Linux, first you must make sure its executable.

![image](https://user-images.githubusercontent.com/12489689/169674313-db774b5f-8c7f-4920-bfb0-57b999d05c47.png)

Then you can right-click in an empty space inside the directory, and click open in terminal.

![image](https://user-images.githubusercontent.com/12489689/169414950-17b5a7c7-e26b-4248-ad15-aa03782a9762.png)

This will open the current directory in the terminal. From there type `./` and then the program name. Just typing tog and pressing `TAB` should be enough to auto complete.

![image](https://user-images.githubusercontent.com/12489689/169415250-55369479-c423-4205-a49f-e31bf32f88cc.png)

### Using App

Upon running the app, it will ask you to decide on a choice:

![image](https://user-images.githubusercontent.com/12489689/173170956-40d189fe-c05c-45c1-84ff-693cb4fa0ada.png)

#### Default

Entering `1` will load the default settings based on this format:

<img src="https://user-images.githubusercontent.com/12489689/169417653-5d974de1-aeb1-4a75-942c-6ee1993f184a.png" width="300" alt="Bingo"/>

That is to say a 4x3 grid, with the bonus being the final column. Think of it like a 3x3 + 1 Column:

`10` `10` `10`  `20`

`30` `30` `30`  `60`

`50` `50` `50`  `100`

After entering `1` and pressing `ENTER` to submit, it will inform you the settings were loaded and now ask for the path you should have copied to your clipboard.

On Windows you can simply `Right-Mouse CLICK` and it should paste the path.

You can also press `CTRL + SHIFT + V` on your keyboard to paste it. This option will also work on Linux.

One you have pasted it, press `ENTER` to submit it. The Program will now ask for you to enter a Key. This Key is the correct sequence of the bingo once the correct bingo is known. The players guesses are compared against the known correct Key.

The order is read from left to right, and row to row.

-->

`10` `10` `10`  `20`

-->

`30` `30` `30`  `60`

-->

`50` `50` `50`  `100`

For the default settings, the key must be 12 characters long. If you do not enter enough, the program will tell you, and ask to be closed. You will then need to run the App again and reenter the `PATH` and then give a valid key entry. As with the Guess entries, only a `Y` and `N` should be considered valid. A Key should not contain any other characters. Case doesn't matter, nor any spaces.

![image](https://user-images.githubusercontent.com/12489689/173171119-cced4e5e-9adc-4bf1-8123-6c90bea28767.png)

Upon entering in the key press `ENTER` to submit. If there are no errors, it will tell you it successfully completed the operation, and tell you how many guesses have been gone through.

![image](https://user-images.githubusercontent.com/12489689/173171157-dd749d61-994c-444f-b0b3-b71c55fa1794.png)

The output file is saved in the same location as the previously entered path, and takes the same name as well. It saves it as a Markdown file, but it also functions just fine as a normal text document. The output file is pre ordered from highest score to lowest, with the names corresponding. Along with the scores, it also outputs two tables, the first one shows the correct bingo answer, and the second shows the total number of correct picks for the respective squares. Its output in a percentage format.

![image](https://user-images.githubusercontent.com/12489689/173171191-fb17375b-8d13-4986-94ea-455608ffc82c.png)

![image](https://user-images.githubusercontent.com/12489689/173171215-2466641f-fccc-41d3-a0e8-9d242dfd48e5.png)

## Markdown to PNG

This process in manual for now, and uses the free and open source Markdown reader [MarkText](https://github.com/marktext/marktext).

With the file open in MarkText click the hamburger menu and go to `File` -> `Export` -> `HTML`.

![image](https://user-images.githubusercontent.com/12489689/173171858-dbb76428-fd02-4d1b-a191-58c10547a513.png)

From there a box will pop up. You can search through and tweak things if you'd like, one of which i'd recommend being the font size, but you can also just leave it default which is what I have done in this example. Just name the file what you want, and click save.

![image](https://user-images.githubusercontent.com/12489689/173171903-9e20d599-a56d-4962-bed6-a95ada1c69e0.png)

I have kept the same name as the other files for this bingo.

![image](https://user-images.githubusercontent.com/12489689/173171966-a3716588-4678-4ec0-970a-7593eff83cdc.png)

Next you'll want to open the new HTML file. This should open your default browser.

![image](https://user-images.githubusercontent.com/12489689/173172004-e7f52e38-25e7-4d7b-85d2-d556504c6b46.png)

To get a thinner, and in my opinion, a nicer looking final image, you can just make the browser window itself as thin as you need.

![image](https://user-images.githubusercontent.com/12489689/173172062-f076d221-5cfa-47ae-aa07-e6af7ac29cce.png)

The viewport size changes and so does the table rendered.

From here, if you are on FireFox, or a fork of it, you can just right click and save as screenshot. and save as a full page.

![image](https://user-images.githubusercontent.com/12489689/173172082-2f5e9c84-083e-42f1-b3c5-aadf56040c90.png)

![image](https://user-images.githubusercontent.com/12489689/173172090-5d5b0de5-bb41-4610-9507-80513cfcdb71.png)

If you are on a Chromium based browser, like Google Chrome, Edge, Brave, etc., you can get an extension, or you can use the built in option that's hidden behind dev tools.

As I couldn't possibly cover every extension, I'll only be showing the dev tools option. To enter the dev mode, press `F12` on your keyboard. this will bring up a menu in the browser.

![image](https://user-images.githubusercontent.com/12489689/173172151-f941255b-5ed3-4ad5-9ffe-faab1aa52854.png)

From there press `CTRL` + `SHIFT` + `P` to bring up the command pallet. Type in `Shot` and select "Capture full-size screenshot" and then name it and choose where to save it. I name it the same as the other folders and place it in the same folder.

![image](https://user-images.githubusercontent.com/12489689/173172397-4ff80e9a-40d1-4378-83c0-07f2ce2ca4d3.png)

And with that you have a PNG image with that width you set.

![image](https://user-images.githubusercontent.com/12489689/173172260-cfdf1d18-fb7c-4015-ac8a-0e7937053ff7.png)

## Custom

Besides the default configuration, the solver also supports custom inputs.

Press `2` and press `ENTER` and initially it will ask you for an input of some value and also show what the default settings are for reference.

![image](https://user-images.githubusercontent.com/12489689/173171468-fc4cf19d-b831-4acb-ab10-29a705c5f68b.png)

This amount is how many columns there are in total, including the bonus columns.

Default settings loads a value of 4.

`0` `0` `0` `0`

In this example I will be using a column value of 5.

`0` `0` `0` `0` `0`

![image](https://user-images.githubusercontent.com/12489689/173171523-d6ab255f-bacc-41b7-87d3-082c771b8c1a.png)

Next it asks for a row input. I'll also be using a value of 5, for a total of 25 total squares.

`0`

`0`

`0`

`0`

`0`

![image](https://user-images.githubusercontent.com/12489689/173171543-7e929aaa-41af-4de3-bb07-0b95f0af7453.png)

Now it asks for the bonus column amount. In the default config, of the 4 total columns, the 1 final column is the bonus. Like a 3x3 grid, plus an extra row.

`0` `0` `0`  +  `0`

For this example I will be saying that out of the 5 total columns, 2 will be bonus.

`0` `0` `0`  +  `0` `0`

![image](https://user-images.githubusercontent.com/12489689/173171565-541ebaf0-f7d8-458b-9f62-575278145a80.png)

Next is how much each row will increment over the current row. In the default config the first row is worth 10 per normal square, with a value of 20 here, the next row will go up to 30. Then the following row will go up another 20 to equal 50.

`10` `10` `10` + `0`

+20

`20` `20` `20` + `0`

For this example I will use 50 as the value:

`10` `10` `10` + `0`

+50

`60` `60` `60` + `0`

![image](https://user-images.githubusercontent.com/12489689/173171591-9c85ccbd-dfbf-496d-a9a6-d62c35dab01b.png)

Next is deciding the multiplier the bonus squares will have over their rows normal value. In the default config a value of 2 means that the bonus squares are worth twice the amount.

`10` `10` `10` + `20`

`30` `30` `30` + `60`

I will be using a 4 here, making each bonus square worth four times the rows base amount.

![image](https://user-images.githubusercontent.com/12489689/173171608-a283c0c7-4e9b-4367-ac81-f2b63b360f00.png)

Finally we have the starting rows base amount. This is what the worth is of the normal squares on the very first row. For this value I will use a 20 base value.

So for this example we have a 5x5 grid, of which the final 2 columns are bonus columns, and which the first row starts with a base value of 20, and a 4 times multiplier for the bonus squares, with each rows base value going up by 50 from the previous.

`20`  `20`  `20`  + `80` `80`

`70`  `70` `70`  + `280` `280`

`120` `120` `120` + `480` `480`

`170` `170` `170` + `680` `680`

`220` `220` `220` + `880` `880`

![image](https://user-images.githubusercontent.com/12489689/173171632-62bd7be5-da12-4c23-91d4-1925a8e3a79b.png)

It then asks to input the path. This is done just as normal.

![image](https://user-images.githubusercontent.com/12489689/173171671-fc1959c5-81a8-40e5-ac4c-e98b37cf9e52.png)

And finally you enter the key. And with that you are done. The solver will output in the same manner as before, making a markdown file with the same name and location as the original spreadsheet. Also ordered from highest score to lowest. Also with the table of the correct answer, and the correct amount of guesses.

![image](https://user-images.githubusercontent.com/12489689/173171729-c3eb7321-6d1f-4c10-952f-b91409ee8f1d.png)
