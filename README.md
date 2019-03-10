# PictureManager

PictureManager is a PoC solution for getting information ([pictures](http://jsonplaceholder.typicode.com/photos) and [albums](http://jsonplaceholder.typicode.com/albums)) from external urls. It consists on:

* `PictureManager`: .NET Standard 2.0 library to handle business logic.

* `PictureManager.Api`: .NET Core 2.2 WebApi that allows accessing `PictureManager` functionality through REST controllers. This is the app you'd like to run to test the code (`dotnet build` and `dotnet run` is enough!).
.NET Core 2.2 SDK is required to compile this project (as well as tests projects) but `TargetFramework` could safely be changed to `netcoreapp2.1` or `netcoreapp2.0` versions in those `.csproj` files).