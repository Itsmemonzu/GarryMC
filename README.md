# GarryMC
GarryMC is an asynchronous Minecraft server lookup tool that runs in your terminal using [Spectre.Console](https://github.com/spectreconsole/spectre.console). 

## Installation & Usage 
This program is dependant on [Spectre.Console](https://github.com/spectreconsole/spectre.console). To build the program, you need to use
```
dotnet publish -r win-x64 -c Release
```
If you want to make it Self-Contained then add --self-contained true at the end. Example:
```
dotnet publish -r win-x64 -c Release --self-contained true
```
Make sure to replace win-x64 with your OS.

Your build will be located in GarryMC\obj\Release\net7.0\yourOS\publish

# Contribution
Contributing to GarryMC is simple. You have to fork the repository and clone it. Make your changes. After you are done, just push the changes to your fork and make a pull request. 

We hope that you will be making some amazing changes!