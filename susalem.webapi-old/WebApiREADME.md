# 苏萨冷 Web Api

## 介绍

本项目主要使用Abp Vnext  版本6.0.3 的WebApi 框架

主要包括Identity.Api 模块 、Mes模块、DC数采模块

---

### 技术架构

- [x] 框架： Abp Vnext  Version : 6.0.3

- [x] 数据库：Mysql

- [x] 授权集成：Identity、OpenIddic 

- [x] 映射集成：AutoMapper

- [x] 接口调试：SwaggerUI

---

## 项目启动方式

>  ***目前使用我自己的服务器数据库请善待！！！！***

### 初始化数据配置

1. *** 配置应用程序、clientId等信息 ：路径：services/susalem/Susalem.Domain/OpenIddict/OpenIddictDataSeedContributor***

   ******

   ``` C#
     //Web Client 应用
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
   
     // Swagger Client swagger
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

   

2. ***配置管理员密码邮箱等 路径：services/susalem/Susalem.Domain/Data/SusalemDbMigrationService***

   ``` c#
       private async Task SeedDataAsync(Tenant tenant = null)
       {
           Logger.LogInformation($"Executing {(tenant == null ? "host" : tenant.Name + " tenant")} database seed...");
   
           await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
               .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, SusalemSettings.AdminEmailDefaultValue) // 邮箱
               .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, SusalemSettings.AdminPasswordDefaultValue) // 密码
           );
       }
   ```

   

   

***

### 初始化数据库

> 方式一

 ``` c#
 // service/susalem/Susalem.DbMigrator
 ```

> 将DbMigrator项目设置为启动项目 直接启动即可



> 方式二

```c#
// service/susalem/Susalem.HttpApi.Host 
```

> 将Susalem.HttpApi.Host  设置为启动项目
> 打开程序包管理控制台 将程序包控制台设置为Susalem.EntityFrameworkCore
>
> 执行EFCodeFirst 脚本 update-database即可

---

### 将service/susalem/Susalem.HttpApi.Host设置为启动项启动即可

## 开发文档（规范）

### 1. 数据库表 

> 1. 找到Susalem.Mes.Domain  在文件夹Susalem 下添加Entity实体
>
> 2. Susalem.Mes.EntityFrameworkCore 的IMesDbContext 添加DBSet
>
> 3. SusalemDbContextModelCreatingExtensions配置表结构索引等
>
> 4. 执行CodeFirst迁移命令
>
> 5. add-migration add_Table_xxx  创建出来的migration - script 需要检查后再执行 6
>
> 6. update-database
>
>    

### 2.开发流程

> 1. 入参出参需要在Susalem.Mes.Application.Contracts项目下创建Dto 进行数据传输 不可直接返回实体
> 2. 需要在Susalem.Mes.Application.Contracts项目下定义接口规范 如 IMaterialAppService
> 3. 在 Susalem.Mes.Application 定义实现如MaterialAppService
> 4. 暂定先 每个块如：物料管理添加 为一个独立的AppService
> 5. Appservice数据访问方式 使用private readonly IRepository<Entity,key> _repository 直接构造注入即可

```text
注意:
接口规范需要继承 IMaterialAppService: IApplicationService
实现需要集成    MaterialAppService:MesAppService,IMaterialAppService
目前没有划分领域
暂时用不到Domain的MaterialManager
也暂时没有用到自定义Repository
程序由于使用了UnitOfWork
如果需要立即入库请使用如：_repository.InsertAsync(entity,true) 框架中第二个参数为autoSave true为立即保存
```

# 其他待补充

> 逐步完善中

