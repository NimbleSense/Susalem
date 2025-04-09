# Susalem Web Api

> Solution path: susalem.webapi/susalem.api/susalem.api.sln

## introduce

This project mainly uses the WebApi framework of Abp Vnext version 6.0.3 

It mainly includes Identity.Api module, Mes module and DC digital mining module

---

### 技术架构

- [x] Frame: Abp Vnext Version: 6.0.3

- [x] database: Mysql

- [x] Authorization integration: Identity, OpenIddic

- [x] Mapping integration: AutoMapper

- [x] Interface debugging: SwaggerUI

---

## Project start-up mode

>  *** Currently use my own server database, please be kind!！！！***

### Initialize the data configuration

1. *** Configure application, clientId and other information: path:services/susalem/Susalem.Domain/OpenIddict/OpenIddictDataSeedContributor***

   ******

   ``` C#
     //Web Client Application
     var webClientId = configurationSection["Susalem_Web:ClientId"];
     if (!webClientId.IsNullOrWhiteSpace())
     {
         var webClientRootUrl = configurationSection["Susalem_Web:RootUrl"].EnsureEndsWith('/');
   
         /* Susalem_Web client is only needed if you created a tiered
          * solution. Otherwise, you can delete this client. */
         await CreateApplicationAsync(
             name: webClientId,
             type: OpenIddictConstants.ClientTypes.Confidential,
             consentType: OpenIddictConstants.ConsentTypes.Implicit,
             displayName: "Web Application",
             secret: configurationSection["Susalem_Web:ClientSecret"] ?? "1q2w3e*",
             grantTypes: new List<string> //Hybrid flow
             {
                 OpenIddictConstants.GrantTypes.AuthorizationCode,
                 OpenIddictConstants.GrantTypes.Implicit
             },
             scopes: commonScopes,
             redirectUri: $"{webClientRootUrl}signin-oidc",
             clientUri: webClientRootUrl,
             postLogoutRedirectUri: $"{webClientRootUrl}signout-callback-oidc"
         );
     }
   
     // Swagger Client swagger Application
     var swaggerClientId = configurationSection["Susalem_Swagger:ClientId"];
     if (!swaggerClientId.IsNullOrWhiteSpace())
     {
         var swaggerRootUrl = configurationSection["Susalem_Swagger:RootUrl"].TrimEnd('/');
   
         await CreateApplicationAsync(
             name: swaggerClientId,
             type: OpenIddictConstants.ClientTypes.Public,
             consentType: OpenIddictConstants.ConsentTypes.Implicit,
             displayName: "Swagger Application",
             secret: null,
             grantTypes: new List<string>
             {
                 OpenIddictConstants.GrantTypes.AuthorizationCode,
             },
             scopes: commonScopes,
             redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
             clientUri: swaggerRootUrl
         );
     }
   ```

   

2. ***Configure the administrator password, mailbox, and other paths:services/susalem/Susalem.Domain/Data/SusalemDbMigrationService***

   ``` c#
       private async Task SeedDataAsync(Tenant tenant = null)
       {
           Logger.LogInformation($"Executing {(tenant == null ? "host" : tenant.Name + " tenant")} database seed...");
   
           await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
               .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, SusalemSettings.AdminEmailDefaultValue) // email
               .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, SusalemSettings.AdminPasswordDefaultValue) // password
           );
       }
   ```

   

   

***

### Initialize the database

> Method One

 ``` c#
// service/susalem/Susalem.DbMigrator
 ```

> Set the DbMigrator project to start the project to start directly



> Method Two

```c#
// service/susalem/Susalem.HttpApi.Host 
```

> Set the Susalem.HttpApi.Host to start the project
> Open the Package Management Console and set the package console to Susalem.EntityFrameworkCore
>
> Execthe EFCodeFirst script update-database

---

### Set service/susalem/Susalem.HttpApi.Host to start up

## Development Documentation (Specification)

### 1. The database table

> 1. Find Susalem.Mes.Domain in the folder Susalem, add Entity entity
>
> 2. Susalem.Mes.EntityFrameworkCore of IMesDbContext was added with the DBSet
>
> 3. SusalemDbContextModelCreatingExtensions configuration table structure index, etc
>
> 4. Execute the CodeFirst migration command
>
> 5. The migration-script created by add-migration add _ Table _ xxx needs to be checked before the execution of 6
>
> 6. update-database
>
> 

### 2.Development process

> 1. Entry and exit participation needs to be in Susalem.Mes.Application. Contracts Create Dto for data transfer can not be directly returned to the entity
> 2. is required in Susalem.Mes.Application. Contracts Project to define interface specifications such as IMaterialAppService
> 3 in Susalem.Mes.Application defined implementation as in MaterialAppService
> 4. Add each block such as material management as an independent AppService
> 5. Appservice data access method using private readonly IRepository <Entity, key> _repository directly construct injection

```text
pay attention to:
The interface specification needs to inherit the IMaterialAppService: IApplicationService
Implementation requires the integration of MaterialAppService: MesAppService, IMaterialAppService
There is currently no field division
Use use MaterialManager without Domain
A custom Repository is also not yet being used
Program due to the use of the UnitOfWork
If you need to enter the storage immediately, please use the second parameter of autoSave true in the _repository.InsertAsync (entity, true) frame to save immediately
```

# Other to be supplemented

> Gradually improve ……

