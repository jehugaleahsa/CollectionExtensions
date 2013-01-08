nuget pack ../CollectionExtensions/CollectionExtensions.csproj -Prop Configuration=Release -Build
nuget push *.nupkg
del *.nupkg