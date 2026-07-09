using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
public class EmailService
{

    private readonly IConfiguration configuration;


    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }



    public void SendEmail(
        string email,
        string code)
    {

        var message = new MimeMessage();



        // ايميل البرنامج اللي هيبعت
        message.From.Add(
            new MailboxAddress(
                "Real Estate Office",
                configuration["Email:Address"]
            )
        );



        // ايميل المستخدم اللي هيستقبل
        message.To.Add(
            MailboxAddress.Parse(email)
        );



        message.Subject =
        "Password Reset Code";



        message.Body =
        new TextPart("plain")
        {
            Text =
            $"Your verification code is: {code}"
        };




        using var smtp =
        new SmtpClient();



        smtp.Connect(
            "smtp.gmail.com",
            587,
            SecureSocketOptions.StartTls
        );



        smtp.Authenticate(
            configuration["Email:Address"],
            configuration["Email:Password"]
        );



        smtp.Send(message);



        smtp.Disconnect(true);

    }

}
