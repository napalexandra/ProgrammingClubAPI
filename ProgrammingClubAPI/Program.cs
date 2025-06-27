using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Helpers;
using ProgrammingClubAPI.Repositories;
using ProgrammingClubAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    //Our API is using JWT authentication.
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    // All endpoints are protected by [Authorize] attribute.
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//inregistram clasa ProgrammingClubDataContext in containerul de dependente 
builder.Services.AddDbContext<ProgrammingClubDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<ProgrammingClubAuthDataContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionAuth")));

//Transient = de fiecare data cand se cere o instanta a clasei, se va crea una noua
//Scoped = se va crea o instanta a clasei pentru fiecare request HTTP
builder.Services.AddTransient<IMembersRepository, MembersRepository>();
builder.Services.AddTransient<IMembersService, MembersService>();

//builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Logging.AddLog4Net("log4net.config");

//configurare infrastructura pentru gestionare useri 
//builder.Services.AddIdentityCore<IdentityUser>()
//    .AddRoles<IdentityRole>()
//    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("ProgrammingClubAuthentication")
//    .AddEntityFrameworkStores<ProgrammingClubAuthDataContext>()
//    .AddDefaultTokenProviders();

//reguli validare pass
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
});

//configurare autentificare JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
           {
               context.HandleResponse();
               context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
               var payload = new
               {
                   StatusCode = context.Response.StatusCode,
                   Message = "You are not authorized to access this resource. Please provide a valid token."
               };
               await context.Response.WriteAsJsonAsync(payload);
           },

            OnAuthenticationFailed = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Authentication failed. Please check your token."
                });
            }
        };
    });

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Allows clients to see the API versions
    options.AssumeDefaultVersionWhenUnspecified = true; // Default version if not specified
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0); // Default version
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Format for versioned API groups
    options.SubstituteApiVersionInUrl = true; // Substitute version in URL
});

builder.Services.ConfigureOptions<ConfigureSwagger>();

//inregistram toate handlerele din assembly-ul in care se clasa Program.cs
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddMemoryCache();

var app = builder.Build();

//orice cerere HTTP pe care o facem va trece prin clasa middleware
app.UseMiddleware<ProgrammingClubAPI.Middleware.CorrelationMiddleware>();

//
var versionDescriptionsProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var item in versionDescriptionsProvider.ApiVersionDescriptions)

        {
            options.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
