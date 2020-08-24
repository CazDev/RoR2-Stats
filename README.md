# RoR Stats

![image](https://ibb.co/9TL3Xt1)

## How to use sharpmonoinjector
After you have your dll file, you'll need to have a way to inject it into the game. There are 2 option Command line or with a batch file.
### Command line
1. Once you have sharpmonoinjector downloaded from [here](https://github.com/warbler/SharpMonoInjector/releases/download/v2.2/SharpMonoInjector.Console.zip), extract the zip.

2. Next open a command prompt window as administrator and type the command:

`cd "[path to folder with smi.exe in it]"` 

obviously replacing [path to folder with smi.exe in it] with the proper path (keep the quotes). For example my command is:

`cd "C:\Users\Username\Documents\My Cheats\RoR2\SharpMonoInjector.Console\SharpMonoInjector.Console"`

3. You can tell if you're in the proper directory if you type `dir` and you see SharpMonoInjector.dll and smi.exe listed

4. Once you are in the proper directory and Risk of Rain 2 is open, type 

`smi.exe inject -p "Risk of Rain 2" -a "[Path to UmbraRoR.dll]" -n UmbraRoR -c Loader -m Load` 

again replacing [Path to UmbraRoR.dll] with the proper path (keep the quotes)

### Batch file
1. Once you have sharpmonoinjector downloaded from [here](https://github.com/warbler/SharpMonoInjector/releases/download/v2.2/SharpMonoInjector.Console.zip), extract the zip.
2. Right click on your Desktop -> New -> Text Document
3. Open the text document and paste the following
```
@echo off
cd "[path to folder with smi.exe]"
cls
smi.exe inject -p "Risk of Rain 2" -a "[Path to UmbraRoR.dll]" -n UmbraRoR -c Loader -m Load
pause
```
4. Replace [path to folder with smi.exe] and [Path to UmbraRoR.dll] with the proper paths (keep quotes if they are there)
5. Press ctrl+shift+s and name it `start.bat` (make sure you replace .txt with .bat)
6. If everything was done properly, while the game is open just run `start.bat` as administrator and the Menu should automatically be injected into the game


# Set Up Dev Environment
### Requirements
- [ ] Microsoft Visual Studios 2017 or later
- [ ] A Mono Injector. [Sharpmonoinjector](https://github.com/warbler/SharpMonoInjector) is recommended and is used in this tutorial.

1. Clone or Download -> Download ZIP 
2. Extract ZIP file
3. Navigate to where you extracted the zip and open the .sln file (if you cannot see file extentions, go to the view tab at the top of file explorer and on the right check the "File name extensions" box)
4. In  Visual Studios Right click on the project > add > Reference... (Image shows where to right click)
![Annotation 2020-04-18 181944](https://user-images.githubusercontent.com/12210881/79672593-8471f500-81a1-11ea-9863-b60943be5108.png)
 

5. Click browse. The required resources are found in > `\Steam\steamapps\common\Risk of Rain 2\Risk of Rain 2_Data\Managed`
```
1) Assembly-CSharp.dll
2) netstandard.dll
3) Rewired_Core.dll
4) System.dll
5) UnityEngine.CoreModule.dll
6) UnityEngine.dll
7) UnityEngine.IMGUIModule.dll
8) UnityEngine.Networking.dll
9) UnityEngine.TextRenderingModule.dll
```
6. You will also have to add ```Octokit.dll``` that is in the project's source folder. `Umbra-Mod-Menu-master\Octokit.dll`. This is used to check for updates.

### Build
1. Press ctrl+b to build dll and it should be located where you found the .sln file -> bin -> Release (or Debug) -> UmbraRoR.dll
 
# Resources:
https://github.com/0xd4d/dnSpy

https://github.com/BennettStaley/RoR2ModMenu

https://github.com/Lodington/RoRCheats

https://github.com/octokit/octokit.net
