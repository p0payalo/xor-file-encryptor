# Usage
- Download or clone repository
- Compile project with Visual Studio

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
4. Threads count (Min: 1, Max: 100, Default: 20)

##### For example:
- For file `start Encryptor.exe "D:\Staff\MyFile.png" -f mysuperpass`
- For directory `start Encryptor.exe "D:\Staff" -d mysuperpass`
- For directory with threads `start Encryptor.exe "D:\Staff" -d mysuperpass 30`

###### For decrypt file or directory use same password which used for encrypt