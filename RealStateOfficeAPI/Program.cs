// install library Microsoft.AspNetCore.Authentication.JwtBearer

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using RealEstateOfficeBusinessLogic;
using RealEstateOfficeDataAccess;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

    if (builder.Environment.IsProduction())
    {
        DbConnection.StringConnection =
            builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Connection string not found");
    }



// Add services to the container.

builder.Services.AddControllers();



// Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("Bearer",
    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {

        Name = "Authorization",

        Type =
        Microsoft.OpenApi.Models.SecuritySchemeType.Http,

        Scheme = "Bearer",

        BearerFormat = "JWT",

        In =
        Microsoft.OpenApi.Models.ParameterLocation.Header,


        Description =
        "Enter: Bearer {your token}"

    });



    options.AddSecurityRequirement(
    new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {

        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference =
                new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type =
                    Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,

                    Id = "Bearer"
                }
            },

            new string[]{}
        }

    });

});




// CORS

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowFlutter",
    policy =>
    {

        policy
        .AllowAnyOrigin()
        //«”„Õ ·√Ì „Êﬁ⁄ /  ÿ»Ìﬁ Ìﬂ·„ «·‹ API » «⁄Ì

        .AllowAnyHeader()

        .AllowAnyMethod();

    });

});






// Authentication

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;


    options.DefaultChallengeScheme =
    JwtBearerDefaults.AuthenticationScheme;


})

.AddJwtBearer(options =>
{

    options.TokenValidationParameters =
    new TokenValidationParameters
    {

        ValidateIssuer = true,

        ValidateAudience = true,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,



        ValidIssuer =
        builder.Configuration["Jwt:Issuer"],


        ValidAudience =
        builder.Configuration["Jwt:Audience"],



        RoleClaimType =
        ClaimTypes.Role,



        IssuerSigningKey =
        new SymmetricSecurityKey(

        Encoding.UTF8.GetBytes(
        builder.Configuration["Jwt:Key"]!)

        )

    };

});



// Authorization

builder.Services.AddAuthorization();





// Dependency Injection


builder.Services.AddScoped<TokenService>();


builder.Services.AddScoped<AuthBL>();

builder.Services.AddScoped<Autho>();



builder.Services.AddScoped<RefreshTokenBL>();

builder.Services.AddScoped<RefreshTokenDA>();
builder.Services.AddScoped<PasswordResetBL>();

builder.Services.AddScoped<PasswordResetDA>();


builder.Services.AddScoped<EmailVerificationBL>();

builder.Services.AddScoped<EmailVerificationDA>();


builder.Services.AddScoped<EmailService>();




// Rate Limiter

builder.Services.AddRateLimiter(options =>
{


    options.AddFixedWindowLimiter(
    "login",

    limiter =>
    {

        limiter.Window =
        TimeSpan.FromMinutes(1);


        limiter.PermitLimit = 5;

    });



    options.AddFixedWindowLimiter(
    "forgot",

    limiter =>
    {

        limiter.Window =
        TimeSpan.FromMinutes(5);


        limiter.PermitLimit = 3;

    });


});




// ﬂ· «·œÊ«· ﬁ»· «·build() Ê »⁄œÂ ·«“„ ÌﬂÊ‰Ê« ÃÊ« «·‹ builder.Services.AddAuthentication()
// Ê builder.Services.AddAuthorization() ⁄‘«‰ ·Ê ÕÿÌ‰«Â„ »—«Â„ „‘ ÂÌ‘ €·Ê«


var app = builder.Build();



// Exception Handler

app.UseExceptionHandler("/error");



// Swagger


    app.UseSwagger();

    app.UseSwaggerUI();





// HTTPS

app.UseHttpsRedirection();
app.UseStaticFiles();


// CORS

app.UseCors("AllowFlutter");



// Rate Limit

app.UseRateLimiter();



// Security Pipeline

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();


app.Run();