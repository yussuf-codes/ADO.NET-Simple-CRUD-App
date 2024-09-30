## 🚀 Set up:

1. Create the database

2. Add SQL Server connection string to user secrets

    ```shell
    dotnet user-secrets --project Persistence set "ConnectionStrings:DefaultConnection" "<SQL Server Connection String>"
    ```

3. Create a console app

    ```shell
    dotnet new console --output Test --use-program-main
    dotnet sln add Test
    ```

4. Add Persistence project reference

    ```shell
    dotnet add Test reference Persistence
    ```

5. Inside the main method, create an `ISQLProvider` instance

    ```csharp
    ISQLProvider provider = new SQLServerProvider();
    ```

6. Create an `INotesRepository` instance, and inject it with the `ISQLProvider` instance

    ```csharp
    INotesRepository repository = new NotesRepository(provider);
    ```

7. Create an `NotesService` instance, and inject it with the `INotesRepository` instance

    ```csharp
    NotesService service = new(repository);
    ```
