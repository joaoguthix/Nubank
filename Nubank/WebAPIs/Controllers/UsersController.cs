using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;
using WebAPIs.Models;
using WebAPIs.Token;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;
        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISendGridClient sendGridClient,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sendGridClient = sendGridClient;
            _configuration = configuration;
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromQuery] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
            {
                return Unauthorized();
            }

            var resultado = await
                _signInManager.PasswordSignInAsync(
                    login.Email,
                    login.Senha,
                    false,
                    lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                // Recupera Usuário Logadomonte
                var userCurrent = await _userManager.FindByEmailAsync(login.Email);
                var idUsuario = userCurrent.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                .AddSubject("Empresa - Canal Dev Net Core")
                .AddIssuer("Teste.Securiry.Bearer")
                .AddAudience("Teste.Securiry.Bearer")
                .AddClaim("idUsuario", idUsuario)
                .AddExpiry(5)
                .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuarioIdentity")]
        public async Task<IActionResult> AdicionaUsuarioIdentity([FromQuery] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Ok("Falta alguns dados");


            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                CPF = login.CPF,
                Tipo = TipoUsuario.Comum,
            };

            var resultado = await _userManager.CreateAsync(user, login.Senha);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }



            // Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao confirmar usuários");

        }
        //[AllowAnonymous]
        //[Produces("application/json")]
        //[HttpPost("/api/AdicionaUsuarioAdminIdentity")]
        //public async Task<IActionResult> AdicionaUsuarioAdminIdentity([FromQuery] Login login)
        //{

        //    if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
        //        return Ok("Falta alguns dados");

        //    var user = new ApplicationUser
        //    {
        //        UserName = login.Email,
        //        Email = login.Email,
        //        CPF = login.CPF,
        //        Tipo = TipoUsuario.Admin,
        //    };

        //    var resultado = await _userManager.CreateAsync(user, login.Senha);

        //    if (resultado.Errors.Any())
        //    {
        //        return Ok(resultado.Errors);
        //    }

        //    // Geração de Confirmação caso precise
        //    var userId = await _userManager.GetUserIdAsync(user);
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        //    string fromEmail = _configuration.GetSection("SendGridEmailSettings")
        //    .GetValue<string>("FromEmail");

        //    string fromName = _configuration.GetSection("SendGridEmailSettings")
        //    .GetValue<string>("FromName");

        //    var msg = new SendGridMessage()
        //    {
        //        From = new EmailAddress(fromEmail, fromName),
        //        Subject = "Nubank",
        //        PlainTextContent = "Obrigado por escolher nossos Serviços!",
        //    };
        //    msg.AddTo(user.Email);
        //    var response = await _sendGridClient.SendEmailAsync(msg);
        //    string message = response.IsSuccessStatusCode ? "Email Send Successfully" :
        //    "Email Sending Failed";
        //    return Ok(code);
        //    // retorno email 
        //    //  code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        //    //  var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

        //    // if (resultado2.Succeeded)
        //    //else
        //    //  return Ok("Erro ao confirmar usuários");

        //}

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuarioAdminIdentity")]
        public async Task<IActionResult> AdicionaUsuarioAdminIdentity([FromQuery] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Ok("Falta alguns dados");

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                CPF = login.CPF,
                Tipo = TipoUsuario.Admin,
            };

            var resultado = await _userManager.CreateAsync(user, login.Senha);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            // Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedCode = Encoding.UTF8.GetBytes(code);
            var base64Code = WebEncoders.Base64UrlEncode(encodedCode);

            string fromEmail = _configuration.GetSection("SendGridEmailSettings")
                .GetValue<string>("FromEmail");

            string fromName = _configuration.GetSection("SendGridEmailSettings")
                .GetValue<string>("FromName");

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromName),
                Subject = "Nubank - Confirmação de E-mail",
                PlainTextContent = $"Obrigado por escolher nossos serviços! Por favor, confirme seu e-mail clicando no link abaixo:\n\n" +
                                   $"{Request.Scheme}://{Request.Host}/api/ConfirmarEmail?userId={userId}&code={base64Code}",
                HtmlContent = $"<!DOCTYPE html><html><body><p>Obrigado por escolher nossos serviços! Por favor, confirme seu e-mail clicando no link abaixo:</p><br/>" +
                              $"<a href=\"{Request.Scheme}://{Request.Host}/api/ConfirmarEmail?userId={userId}&code={base64Code}\">Confirmar E-mail</a></body></html>"
            };

            msg.AddTo(user.Email);
            var response = await _sendGridClient.SendEmailAsync(msg);
            string message = response.IsSuccessStatusCode ? "E-mail enviado com sucesso" : "Falha ao enviar o e-mail";

            return Ok(new { Message = message, UserId = userId, Code = base64Code });
        }

        [AllowAnonymous]
        [HttpGet("/api/ConfirmarEmail")]
        public async Task<IActionResult> ConfirmarEmail(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("ID do usuário ou código de confirmação inválidos");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado.Succeeded)
            {
                // Email confirmado com sucesso
                return Ok("E-mail confirmado com sucesso");
            }
            else
            {
                // Falha ao confirmar o email
                return BadRequest("Falha ao confirmar o e-mail");
            }
        }

        //[HttpGet]
        //[Route("send-text-mail")]
        //public async Task<IActionResult> SendPlainTextEmail(string toEmail)
        //{
        //    string fromEmail = _configuration.GetSection("SendGridEmailSettings")
        //    .GetValue<string>("FromEmail");

        //    string fromName = _configuration.GetSection("SendGridEmailSettings")
        //    .GetValue<string>("FromName");

        //    var msg = new SendGridMessage()
        //    {
        //        From = new EmailAddress(fromEmail, fromName),
        //        Subject = "Nubank",
        //        PlainTextContent = "Obrigado por escolher nossos Serviços!"
        //    };
        //    msg.AddTo(toEmail);
        //    var response = await _sendGridClient.SendEmailAsync(msg);
        //    string message = response.IsSuccessStatusCode ? "Email Send Successfully" :
        //    "Email Sending Failed";
        //    return Ok(message);
        //}


    }
}

