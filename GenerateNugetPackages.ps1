nuget.exe sources Remove -Name "KeTePongoOrchardCore" 
nuget.exe sources Add -Name "KeTePongoOrchardCore" -Source "https://pkgs.dev.azure.com/ketepongo/KTP/_packaging/KeTePongoOrchardCore/nuget/v3/index.json" -username jersio@hotmail.com -password t4p76c7idjjktllwriofrzpc4v4xth7uaejx7ibwif4kl246n4sq
dotnet clean
dotnet restore /p:VersionSuffix=rc1.private.3
dotnet build --configuration Release
dotnet restore /p:VersionSuffix=rc1.private.3
dotnet pack --configuration Release /p:VersionSuffix=rc1.private.3
dotnet restore /p:VersionSuffix=rc1.private.3
dotnet pack --configuration Release /p:VersionSuffix=rc1.private.3
#Get-ChildItem src\ -Recurse -File *.nupkg | Foreach {dotnet nuget push --source "KeTePongoOrchardCore" --api-key KeTePongo $_.FullName }
dotnet nuget push --source "KeTePongoOrchardCore" --api-key KeTePongo src\YesSql.Core\bin\Release\YesSql.Core.2.0.0-rc1.private.3.nupkg
dotnet nuget push --source "KeTePongoOrchardCore" --api-key KeTePongo src\YesSql.Abstractions\bin\Release\YesSql.Abstractions.2.0.0-rc1.private.3.nupkg
dotnet nuget push --source "KeTePongoOrchardCore" --api-key KeTePongo src\YesSql.Provider.Common\bin\Release\YesSql.Provider.Common.1.0.0-rc1.private.3.nupkg
dotnet nuget push --source "KeTePongoOrchardCore" --api-key KeTePongo src\YesSql.Provider.MySql\bin\Release\YesSql.Provider.MySql.1.0.0-rc1.private.3.nupkg
dotnet nuget push --source "KeTePongoOrchardCore" --api-key KeTePongo src\YesSql.Provider.PostgreSql\bin\Release\YesSql.Provider.PostgreSql.1.0.0-rc1.private.3.nupkg
dotnet nuget push --source "KeTePongoOrchardCore" --api-key KeTePongo src\YesSql.Provider.Sqlite\bin\Release\YesSql.Provider.Sqlite.1.0.0-rc1.private.3.nupkg
dotnet nuget push --source "KeTePongoOrchardCore" --api-key KeTePongo src\YesSql.Provider.SqlServer\bin\Release\YesSql.Provider.SqlServer.1.0.0-rc1.private.3.nupkg


