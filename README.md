# Demo criação de projeto de lib

## Introdução

Este projeto demonstra a criação de uma biblioteca .NET para configuração de logging usando Serilog. A biblioteca facilita a configuração de logs em formato JSON e a adição de propriedades customizadas aos logs.

## Comandos e passos executados

- Criação da base do projeto: `dotnet new classlib -n DotNetDemoLog`
- Adição dos pacotes NuGet para uso no projeto:
- Implementação
- Empacotamento: `dotnet pack -c Release`
- Publicação em pasta local para uso em projetos locais: `dotnet nuget push bin/Release/DotNetDemoLog.1.0.1.nupkg -s /caminho/pacotes/locais`
- Adicionando em um projeto: `dotnet add package DotNetDemoLog --source /caminho/pacotes/locais`

## Uso em projeto ASP.NET Core

```cs
using DotNetDemoLog;
using Serilog;

LogConfiguration.ConfigureLogger("appsettings.json");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    //Demais configurações
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
```

## Exemplo de appsettings.json

```json
{
  "AppSettings": {
    "Application": "MyCustomApp"
  },
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "Console"
      }
    ]
  }
}
```