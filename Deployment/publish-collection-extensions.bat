msbuild ../CollectionExtensions.sln /p:Configuration=Release
nuget pack ../CollectionExtensions/CollectionExtensions.csproj -Prop Configuration=Release
nuget push *.nupkg
del *.nupkg