using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RealEstateOfficeBusinessLogic;
using RealStateOfficeModels.Auth;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly AuthBL authBL;
    private readonly TokenService tokenService;
    private readonly RefreshTokenBL refreshTokenBL;
    private readonly EmailVerificationBL emailVerificationBL;
    private readonly PasswordResetBL passwordResetBL;
    private readonly EmailService emailService;

    public AuthController(
    AuthBL authBL,
    TokenService tokenService,
    RefreshTokenBL refreshTokenBL,
    EmailVerificationBL emailVerificationBL,
    PasswordResetBL passwordResetBL,
    EmailService emailService)
    {
        this.authBL = authBL;
        this.tokenService = tokenService;
        this.refreshTokenBL = refreshTokenBL;
        this.emailVerificationBL = emailVerificationBL;
        this.passwordResetBL = passwordResetBL;
        this.emailService = emailService;
    }


    [HttpPost("register")]
    public IActionResult Register(RegisterModel model)
    {

        var oldUser =
 authBL.GetUserByEmail(
     model.Email
 );


        if (oldUser != null)
        {

            if (oldUser.IsVerified)
            {
                return BadRequest(
                "Email already exists"
                );
            }


            // موجود لكن غير مفعل
            string newToken =
            Random.Shared
            .Next(100000, 999999)
            .ToString();



            emailVerificationBL.Add(
                oldUser.UserID,
                newToken
            );


            emailService.SendEmail(
                oldUser.Email,
                newToken
            );


            return Ok(
            "New verification code sent"
            );
        }


        var user =
        authBL.RegisterClient(model);


        if (user == null)
        {
            return BadRequest(
                "Register failed"
            );
        }

        string verifyToken =
        Random.Shared
        .Next(100000, 999999)
        .ToString();


        emailVerificationBL.Add(
        user.UserID,
        verifyToken
        );

        emailService.SendEmail(
    user.Email,
    verifyToken
);

        // string accessToken =
        //tokenService.CreateToken(
        //    user.UserID,
        //    user.RoleName
        //);



        //string refreshToken =
        //tokenService.GenerateRefreshToken();



        //refreshTokenBL.Add(
        //    user.UserID,
        //    refreshToken
        //);



        //return Ok(new
        //{
        //    AccessToken = accessToken,

        //    RefreshToken = refreshToken,

        //    UserID = user.UserID,

        //    Username = user.Username,

        //    Email = user.Email,

        //    Phone = user.Phone
        //});
        return Ok(
"Check your email to verify your account"
);
    }

    [HttpPost("verify-email")]
    public IActionResult VerifyEmail(
 VerifyEmailModel model)
    {

        var user =
        authBL.GetUserByEmail(
            model.Email
        );


        if (user == null)
        {
            return BadRequest(
            "User not found"
            );
        }

        Console.WriteLine(user.UserID);
        Console.WriteLine(model.Code);

        bool result =
        emailVerificationBL.Verify(
            user.UserID,
            model.Code
        );

        Console.WriteLine(user.UserID);
        Console.WriteLine(model.Code);

        if (!result)
        {
            return BadRequest(
            "Invalid verification code"
            );
        }



        authBL.VerifyUser(
            user.UserID
        );



        return Ok(
        "Email verified successfully"
        );
    }


    [EnableRateLimiting("login")]
    [HttpPost("login")]
    public IActionResult Login(LoginModel model)
    {

        var user =
        authBL.Login(
            model.Email,
            model.Password
        );


        if (user == null)
        {
            return Unauthorized(
                "Invalid Email or Password"
            );
        }
        if (user.IsVerified == false)
        {
            return BadRequest(
            "Please verify your email first"
            );
        }


        string accessToken =
        tokenService.CreateToken(
            user.UserID,
            user.RoleName
        );



        string refreshToken =
        tokenService.GenerateRefreshToken();



        refreshTokenBL.Add(
            user.UserID,
            refreshToken
        );



        return Ok(new
        {
            AccessToken = accessToken,

            RefreshToken = refreshToken,

            UserID = user.UserID,

            Username = user.Username,

            Email = user.Email,

            Phone = user.Phone
        });
    }






    [HttpPost("refresh-token")]
    public IActionResult RefreshToken(
        [FromBody] string token)
    {

        var refresh =
        refreshTokenBL.Get(token);



        if (refresh == null)
            return Unauthorized();



        if (refresh.IsRevoked)
            return Unauthorized();



        if (refresh.ExpiryDate < DateTime.Now)
            return Unauthorized();



        var user =
        authBL.GetUserById(
            refresh.UserID
        );


        if (user == null)
            return Unauthorized();



        string newAccessToken =
        tokenService.CreateToken(
            user.UserID,
            user.RoleName
        );



        return Ok(new
        {
            AccessToken = newAccessToken
        });
    }






    [HttpPost("logout")]
    public IActionResult Logout(
        [FromBody] string refreshToken)
    {

        bool result =
        refreshTokenBL.Revoke(
            refreshToken
        );


        if (!result)
        {
            return BadRequest(
                "Invalid token"
            );
        }


        return Ok(
            "Logout successfully"
        );
    }

    [EnableRateLimiting("forgot")]
    [HttpPost("forgot-password")]

    public IActionResult ForgotPassword(
 ForgotPasswordModel model)
    {

        var user =
        authBL.GetUserByEmail(
            model.Email
        );


        if (user == null)
        {
            return BadRequest(
            "Email not found");
        }



        string code =
        Random.Shared
        .Next(100000, 999999)
        .ToString();



        passwordResetBL.Add(
            user.UserID,
            code
        );

        emailService.SendEmail(
    model.Email,
    code
);

        // هنا بعدين هنحط Email Service


        return Ok(
        "Verification code sent"
        );

    }

    [HttpPost("verify-code")]
    public IActionResult VerifyCode(
VerifyCodeModel model)
    {

        bool result =
        passwordResetBL.Verify(
        model.Code
        );


        if (!result)
        {
            return BadRequest(
            "Invalid code");
        }


        return Ok(
        "Code verified"
        );

    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword(
ResetPasswordModel model)
    {


        var user =
        authBL.GetUserByEmail(
        model.Email);


        if (user == null)
        {
            return BadRequest();
        }



        bool valid =
        passwordResetBL.Verify(
        model.Code);


        if (!valid)
        {
            return BadRequest(
            "Invalid code");
        }



        string hash =
BCrypt.Net.BCrypt.HashPassword(
model.NewPassword
);


        authBL.UpdatePassword(
        user.UserID,
        hash);


        passwordResetBL.Delete(
model.Code
);
        return Ok(
        "Password changed successfully"
        );

    }

}