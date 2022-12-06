using System.Text;
using FarmerApp.Api;
using FarmerApp.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(o => { });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(
    options =>
        options.AddPolicy("FarmerApi",
            b =>
                b
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
        ));

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<FarmerDbContext>(options =>
    options.UseSqlServer(connectionString, b =>
        b.MigrationsAssembly("FarmerApp.Api")));

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddScoped<ModelTypeService>();
builder.Services.AddScoped<SpendService>();
builder.Services.AddScoped<WarehouseReceptionService>();
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


/*builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "FarmerApp", Version = "v1" })
);*/

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("FarmerApi");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();