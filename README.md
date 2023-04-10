# GarryMC
GarryMC is an asynchronous Minecraft server lookup tool that runs in your terminal using [Spectre.Console](https://github.com/spectreconsole/spectre.console). 

## Installation & Usage 
This program is dependant on [Spectre.Console](https://github.com/spectreconsole/spectre.console). To build the program, you need to use
```
dotnet publish -r win-x64 -c Release
```
Make sure to replace win-x64 with your OS. if you want to make it Self-Contained then add ```--self-contained true``` at the end. Example:
```r
dotnet publish -r win-x64 -c Release\
```

Your build will be located in GarryMC\obj\Release\net7.0\yourOS\publish

