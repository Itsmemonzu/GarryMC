<div align="center">

## GarryMC <br>

### GarryMC is an asynchronous Minecraft server lookup tool that runs in your terminal!

[![Build](https://github.com/Itsmemonzu/GarryMC/actions/workflows/build.yml/badge.svg)](https://github.com/Itsmemonzu/GarryMC/actions/workflows/build.yml)
</div>

## Installation
This program is dependant on [Spectre.Console](https://github.com/spectreconsole/spectre.console) and [Fluent Command Line Parser](https://github.com/fclp/fluent-command-line-parser). To build the program, you need to use:

```bash
$ dotnet publish -r win-x64 -c Release
```

If you want to make it self-contained then add `--self-contained true` at the end. Example:

```bash
$ dotnet publish -r win-x64 -c Release --self-contained true
```

Make sure to replace `win-x64` with your OS.

Finally, Your build will be located in: `GarryMC\obj\Release\net7.0\<yourOS>\publish`

<br>

## Running

Once the build is complete, you can simply run your terminal in the build directory and type:

```bash
# for Java
$ garrymc --address hypixel.net

# for Bedrock
$ garrymc --address mco.mineplex.com --bedrock
```

<br>

## Contribution
Contributing to GarryMC is simple. You have to fork the repository and clone it. Make your changes. After you are done, just push the changes to your fork and make a pull request. 

We hope that you will be making some amazing changes!

<br>

## License

Licensed under the [MIT License](./LICENSE).