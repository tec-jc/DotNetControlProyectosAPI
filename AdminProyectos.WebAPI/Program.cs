using AdminProyectos.WebAPI.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// CONFIGURAR SWAGGER PARA TRABAJAR CON AUTENTICACIÓN CON TOKENS JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestión de Proyectos", Version = "v1" });

    // *** Incluir  JWT Authentication ***
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Ingresar tu token de JWT Authentication",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
    // ******************************************
});

// Configurar políticas de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LoggedInPolice", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

// CLAVE SECRETA PARA FIRMAR LOS TOKENS
var key = "4s9tORbMKcRa1JHx7qcCBn8iPgfFCL7zZ+ksykX5VSan2+vsmlHvjWdcWWTpOLFjJyNUbogaOIXD//L/lZdqmg==";


// Configurar el JWT
builder.Services
 .AddAuthentication(x =>
 {
     // Configurar la autentificaion de JWT por defecto en la Web API
     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
 })
  .AddJwtBearer(x =>
  {
      // Agregar las configuracion por defecto al JWT
      x.RequireHttpsMetadata = false;
      x.SaveToken = true;
      x.TokenValidationParameters = new TokenValidationParameters
      {
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
          ValidateAudience = false,
          ValidateIssuerSigningKey = true,
          ValidateIssuer = false
      };
  });

// Aplicar la inyeccion de dependencia para JWT
builder.Services.AddSingleton<IJwtAuthenticationService>(new JwtAuthenticationService(key));

// Establecer el alcance de los mapeos con AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
