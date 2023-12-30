# [GeminiChat.DotNet](https://www.nuget.org/packages/GeminiChat.DotNet)
[![Nuget](https://img.shields.io/nuget/v/GeminiChat.DotNet.svg)](https://www.nuget.org/packages/GeminiChat.DotNet)
[![NuGet](https://img.shields.io/nuget/dt/GeminiChat.DotNet.svg)](https://www.nuget.org/packages/GeminiChat.DotNet)

## Installlation
```shell
> dotnet add package GeminiChat.DotNet
```

Package Manager:

```shell
PM > Install-Package GeminiChat.DotNet
```

## Getting Started
1. Your API Key for Gemini. You can get it from [here](https://makersuite.google.com/app/apikey).
2. Install this package on your project with CLI or Nuget Package Manager.

Sample Code:
```csharp
var service = new GeminiService("INPUT YOUR API KEY");
gemini.AppendMessage("Hi!");
var answer = await gemini.GetResponseAsync();
gemini.AppendMessage("Write a story about magic pack.");
var story = string.Empty;
await gemini.StreamResponseAsync((dataLine) =>
{
    story += dataLine;
});
```

**Pull requests are welcome to improve this library.**

