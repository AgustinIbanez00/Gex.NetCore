dotnet build
dotnet ef database drop -f
Remove-Item Migrations -Recurse
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
