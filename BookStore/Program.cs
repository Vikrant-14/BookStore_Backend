using BookStore.Middleware;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.Commands.Service;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Interface;
using RepositoryLayer.Queries.Service;
using RepositoryLayer.Service;
using RepositoryLayer.Utility;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DBContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbBookStore"));
});

//User
builder.Services.AddScoped<IUserRL, UserRL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserCommand, UserCommand>();
builder.Services.AddScoped<IUserQuery, UserQuery>();

//Book
builder.Services.AddScoped<IBookRL, BookRL>();
builder.Services.AddScoped<IBookBL, BookBL>();
builder.Services.AddScoped<IBookCommand, BookCommand>();
builder.Services.AddScoped<IBookQuery, BookQuery>();

//CustomerDetails
builder.Services.AddScoped<ICustomerDetailsRL, CustomerDetailsRL>();
builder.Services.AddScoped<ICustomerDetailsBL, CustomerDetailsBL>();
builder.Services.AddScoped<ICustomerDetailsCommand, CustomerDetailsCommand>();
builder.Services.AddScoped<ICustomerDetailsQuery, CustomerDetailsQuery>();

//Cart
builder.Services.AddScoped<ICartRL, CartRL>();
builder.Services.AddScoped<ICartBL, CartBL>();
builder.Services.AddScoped<ICartCommand, CartCommand>();
builder.Services.AddScoped<ICartQuery, CartQuery>();

//JwtValidation
builder.Services.AddTransient<JwtValidation>();

builder.Services.AddControllers();

//Authentication(JWT)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.Audience = builder.Configuration["JWT:Audience"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        { securityScheme, Array.Empty<string>() }
        });
});

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

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
