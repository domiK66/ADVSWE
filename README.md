# Advanced Software Engineering 1

Aquarium Management Application

## Dotnet Usage

```bash
# restore/install projects and packages
dotnet restore

# run a project
dotnet run

# run tests
dotnet test

# list all new cmds
dotnet new -l

# manage solution file - add/remove projects
dotnet sln add ADVSWE.sln path/to/project.cspr

# add reference to another project
dotnet add reference path/to/project.csproj path/to/reference.csproj

# add a 3rd party package from nuget
dotnet add package Serilog
dotnet add package Newtonsoft.Json
dotnet add package BCrypt.Net-Next
```

## Aufgabe 1

- Es sind 2 Mehtoden zu machen wie man das Passwort speichert. 
- Eine Methode zum verschlüsseln und eine zum entschlüsseln (Hash, Salt)
- Eine Class in ./DAL
- IPasswordHasher
- Unit tests

## Aufgabe 2