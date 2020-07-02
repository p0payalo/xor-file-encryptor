# Usage
Download or clone repository
#### 1. Windows
- Compile project with Visual Studio

or

- x32 `dotnet build --runtime win-x86`
- x64 `dotnet build --runtime win-x64`
#### 2. Linux
- `dotnet build --runtime linux-x64`
#### 3. Mac OS
- Yosemite `dotnet build --runtime osx.10.10-x64`
- El Capitan `dotnet build --runtime osx.10.11-x64`
- Other versions `dotnet build --runtime osx-x64`

# Dependency
- .NET Core 3.1
- Visual Studio 2017 or higher

# File encryption
###### Application parameters
1. Filepath of directorypath
2. Flags
    - `-f or -file` encrypt only 1 file
    - `-d or -dir` encrypt all files in directory
3. Password
4. Threads count (Optional, only for directories)
    - Maximum value: 100
    - Minumum value: 1
    - Default value: 20

##### For example:
- For file `start Encryptor.exe "D:\Staff\MyFile.png" -f mysuperpass`
- For directory `start Encryptor.exe "D:\Staff" -d mysuperpass`
- For directory with threads `start Encryptor.exe "D:\Staff" -d mysuperpass 30`

###### For decrypt file or directory use same password which used for encrypt