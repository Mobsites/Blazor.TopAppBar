dotnet clean
dotnet publish -c Release -o ./pub
robocopy ./pub/wwwroot ./../../docs /E
rmdir /s /q  "./pub"