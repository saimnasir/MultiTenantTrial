using Microsoft.EntityFrameworkCore;
using MultiTenantApp.Database;
using MultiTenantApp.AppServices;
using MultiTenantApp.Repositories;
using Patika.Framework.Shared.Interfaces;
using Patika.Framework.Domain.Services;
using Patika.Framework.Domain.Interfaces.Repository;
using Patika.Framework.Domain.LogDbContext;
using Patika.Framework.Domain.Interfaces.UnitOfWork;
using Patika.Framework.Shared.Entities;
using MultiTenantApp.Middlewares;
using MultiTenantApp.Services;
using Patika.Framework.Shared.Consts;
using Patika.Framework.Identity.Shared.IdentityServerDbContext;
using AuthConfiguration = Patika.Framework.Identity.Models.Configuration;
using Patika.Framework.Identity.JwtToken.Interfaces;
using Patika.Framework.Identity.JwtToken.Services;
using Patika.Framework.Shared.Services;
using Patika.Framework.Application.Contracts.Mapper;
using MultiTenantApp.Mapper;
using Patika.Framework.Identity.Shared.Interfaces.Validators;
using Patika.Framework.Identity.Shared.Services.Validators;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Patika.Framework.Identity.Extensions;
using MultiTenantApp.Consts;
using MultiTenantApp.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Patika.Framework.Identity.Service;
using Patika.Framework.Identity.Interface;
using Patika.Framework.Identity.Shared.IdentityServerDbContext.RepositoryInterfaces;
using Patika.Framework.Identity.Shared.IdentityServerDbContext.Repository;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
// Add services to the container.
Configuration appConfiguration = new();

config.GetSection("Configuration").Bind(appConfiguration);
builder.Services.AddSingleton(appConfiguration);

var authConfiguration = new AuthConfiguration();
config.GetSection("AuthConfiguration").Bind(authConfiguration);
builder.Services.AddSingleton(authConfiguration);

var authenticationParams = new ClientAuthenticationParams
{
    AuthServer = appConfiguration.AuthServerUrl,
    ClientId = appConfiguration.ClientId,
    ClientSecret = appConfiguration.ClientSecret
};

builder.Services.AddSingleton(authenticationParams);

// Add Validators

builder.Services.AddScoped<ICallbackValidator, CallbackValidator>();

builder.Services.AddScoped<Patika.Framework.Identity.AppleAuthProvider.Interfaces.IConfigurationValidator, Patika.Framework.Identity.AppleAuthProvider.Services.ConfigurationValidator>();
builder.Services.AddScoped<Patika.Framework.Identity.GoogleAuthProvider.Interfaces.IConfigurationValidator, Patika.Framework.Identity.GoogleAuthProvider.Services.ConfigurationValidator>();
builder.Services.AddScoped<Patika.Framework.Identity.JwtToken.Interfaces.IConfigurationValidator, Patika.Framework.Identity.JwtToken.Services.ConfigurationValidator>();
builder.Services.AddScoped<Patika.Framework.Identity.JwtToken.Interfaces.IJwtValidator, Patika.Framework.Identity.JwtToken.Services.JwtValidator>();
builder.Services.AddScoped<Patika.Framework.Identity.OktaAuthProvider.Interfaces.IConfigurationValidator, Patika.Framework.Identity.OktaAuthProvider.Services.ConfigurationValidator>();
builder.Services.AddScoped<Patika.Framework.Identity.FacebookAuthProvider.Interfaces.IConfigurationValidator, Patika.Framework.Identity.FacebookAuthProvider.Services.ConfigurationValidator>();

// Add App Services

builder.Services.AddScoped<IPassAppService, PassAppService>();
builder.Services.AddScoped<IPassRepository, PassRepository>();

builder.Services.AddSingleton<ITenantService, TenantService>();
builder.Services.AddScoped<MultiTenantServiceMiddleware>();
builder.Services.AddScoped<MappingProfile, GeneralMappingProfile>();
builder.Services.AddScoped<IUnitOfWorkHostWithInterface, IpassDbContext>();
 
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddSwaggerGen(c =>
{
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Name = "Authorization",
    //    Description = "Example: \"Bearer {token}\"",
    //    Type = SecuritySchemeType.ApiKey
    //});
    c.DocInclusionPredicate((name, api) => true);
    c.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }

        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            return new[] { controllerActionDescriptor.ControllerName };
        }

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Tenant Demo",
        Description = "Tenant Demo API"
    });

});

builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogWriter, LogWriter>();

builder.Services.AddDbContext<IpassDbContext>((sp, opt) =>
{
    opt.UseSqlServer(
        sp.GetService<Configuration>().RDBMSConnectionStrings.Single(m => m.Name.Equals(DbConnectionNames.Main)).FullConnectionString
    );
}, ServiceLifetime.Scoped);

builder.Services.AddDbContext<IdentityServerDbContext>((sp, opt) =>
{
    opt.UseSqlServer(
        sp.GetService<Configuration>().RDBMSConnectionStrings.Single(m => m.Name.Equals(DbConnectionNames.Auth)).FullConnectionString,
        buider => buider.MigrationsAssembly("MultiTenantApp")
    );
}, ServiceLifetime.Scoped);

builder.Services.AddDbContext<LogDbContext>((sp, opt) =>
{
    opt.UseSqlServer(
        sp.GetService<Configuration>().RDBMSConnectionStrings.Single(m => m.Name.Equals(DbConnectionNames.Log)).FullConnectionString,
        builder => builder.MigrationsAssembly("MultiTenantApp")
    );
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IUnitOfWorkHostWithInterface, IpassDbContext>();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("corsapp", policy =>
    {
        policy.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod()
        //.AllowCredentials()
        ;
    });
});

var app = builder.Build();

// initialize the database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IpassDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tenant Demo API");
        c.RoutePrefix = string.Empty;
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
    });

}

app.UseForwardedHeaders();
app.UseHttpsRedirection();
var publicGateWayUri = new Uri(appConfiguration.GatewayUrl);

app.Use(async (ctx, next) =>
{
    ctx.Request.Scheme = publicGateWayUri.Scheme;

    ctx.Request.Host = new HostString(publicGateWayUri.Authority);

    await next();
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("corsapp");

// middleware that reads and sets the tenant
app.UseMiddleware<MultiTenantServiceMiddleware>();

app.UseEndpoints(builder =>
{
    builder.MapControllers();
    builder.MapRazorPages();
    builder.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
