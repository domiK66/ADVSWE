# Aquarium Management Application
Advanced Software Engineering 1

![alt text](./image.jpg)



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

## Aufgabe 1: Store Hashed Password

Storing a password is a crucial thing. Your homework till next session is to implement a secure password storing method. Please consider the following hints:

    No plain text storage
    Do not invent something on your own

- Es sind 2 Mehtoden zu machen wie man das Passwort speichert. 
- Eine Methode zum verschlüsseln und eine zum entschlüsseln (Hash, Salt)
- Eine Class in ./DAL
- IPasswordHasher
- Unit tests

## Aufgabe 2: Repository Implementation
We created the repository, so we need to implement it now. 

This task is up to you and also your homework (if you do not manage to finish in time). 
For each of this implementations, at least one unittest must be created. 

The following repositories need to be implemented:


   public interface IAquariumItemRepository : IRepository<AquariumItem>
    {
        List<Coral> GetCorals();
        List<Animal> GetAnimals();
    }

 public interface IUserRepository : IRepository<User>
    {
        Task<User> Login(String username, String password);
    }

  public interface IAquariumRepository : IRepository<Aquarium>
    {
        Task<Aquarium> GetByName(string name);
    }



## Aufgabe 3: Service Implementation
Now it is up to you:

Please create the following Services with the following methods:

AquariumItem

    AddAquariumItem

AnimalService

    AddAnimal
    GetAnimals

CoralService

    AddCoral
    GetCorals

AquariumService

    GetForUser

UserService

    Login



## Aufgabe 4: Additional Controllers

Your homework till next session is to implement the following controller:

AquariumController

The Aquarium Controller should include the following endpoints:

    Get ({id})
    Create
    Edit  ({id})
    Coral ({id}/Coral) - Create - Get - GetAll - Edit
    Animal ({id}/Animal) - Create - Get - GetAll -  Edit
    ForUser (Returns all Aquariums for a specific user)
    Picture - Create / Get / Delete