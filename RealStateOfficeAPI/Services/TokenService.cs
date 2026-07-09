using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{

    private readonly IConfiguration configuration;


    public TokenService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }



    public string CreateToken(
        int userID,
        string role)
    {


        var claims = new List<Claim>
        {

            new Claim(
                "UserID",
                userID.ToString()
            ),


            new Claim(
                ClaimTypes.Role,
                role
            )

        };



        var key =
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
            configuration["Jwt:Key"]!
            ));



        var credentials =
        new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );



        var token =
        new JwtSecurityToken(

            issuer:
            configuration["Jwt:Issuer"],


            audience:
            configuration["Jwt:Audience"],


            claims: claims,


            expires:
            DateTime.Now.AddMinutes(
                double.Parse(
                configuration["Jwt:ExpireMinutes"]!
                )),


            signingCredentials:
            credentials

        );



        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }




    public string GenerateRefreshToken()
    {

        return Guid.NewGuid()
            .ToString();

    }

}