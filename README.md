# StankUtilities

StankUtilites is a collection of code that I have found useful across many different Unity3D projects.

## Features

- ScriptableObject game architecture base ([inspired from Ryan Hipple](https://www.youtube.com/watch?v=raQ3iHhE_Kk))
- Flexible event system in code (inspired **heavily** from the code base of [Opsive's Ultimate Character Controller](https://opsive.com/assets/ultimate-character-controller/))
- Flexible system for storing game settings in a JSON format. Useful for adding an "options" menu to a game
- Loading Texture2D and/or Sprite object from a plain image file at runtime
- Reading from a ZIP archive at runtime
- A lot of other small things such as C# Reflection helper methods, a base Singleton pattern you can inherit GameObjects from, a debugger that logs with color, and more.
- Feel free to explore the code to see what else you find!

### Required Dependencies:
----

StankUtilities requires one dependency. This **is** included in the project. However, if you **are already using the dependency in your own Unity project, you do not need to import it.** If you do, there will be compiler errors.

If you want the latest version, click the link below.
  - [Json.NET from Newtonsoft](https://github.com/JamesNK/Newtonsoft.Json)

### Installation
----

There are two different ways to use this package.

One is for Unity3D. The other is for Visual Studio. I will cover both below.

#### Unity3D Installation
- Download and/or clone the project from GitHub
- Navigate to **StankUtilities/Unity Project/Assets/Plugins/StankUtilities/stank-utilities.dll**
- Copy the .dll file
- Paste the .dll into your Unity Project's **/Plugins/** folder.
- Done!

#### Visual Studio Installation
- Download and/or clone the project from GitHub
- Open **stank-utilities.sln** with Visual Studio
- Navigate to **Project->Manage NuGet Packages...**
- There will be a warning message that says you are missing packages. Click **Restore**
- Done!

### Help
----

**All** of StankUtilities's code is commented quite well. If you browse the source code, you will more than likely be able to find out what is going on.

If you encounter a bug, **please submit an issue on this GitHub page**.

If you need personal help, feel free to reach out to me!

**Twitter:** [@TheeKStank](https://twitter.com/TheeKStank)
**Discord Username & ID:** KyleStank, #7916

### Todos
----

 - Not sure at the moment
 - I will find something

### License
----

Do whatever you want with it.
