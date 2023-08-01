using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Xml.Linq;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceService;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitCardController : ControllerBase
    {

        private readonly IMapper _IMapper;
        private readonly IDebitCard _IDebitCard;
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;
        private readonly IServiceDebitCard _IServiceDebitCard;
        private readonly IMemoryCache _IMemoryCache;
        public DebitCardController(IMapper IMapper, IDebitCard IDebitCard,
            ISendGridClient sendGridClient, IConfiguration configuration,
            IServiceDebitCard IServiceDebitCard, IMemoryCache IMemoryCache)
        {
            _IMapper = IMapper;
            _IDebitCard = IDebitCard;
            _IServiceDebitCard = IServiceDebitCard;
            _sendGridClient = sendGridClient;
            _configuration = configuration;
            _IMemoryCache = IMemoryCache;

        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add([FromQuery] DebitCardViewModel debitCard)
        {
            var userId = await RetornarIdUsuarioLogado();
            debitCard.UserId = userId;

            var verificaCartao = await _IServiceDebitCard.VerifyCard(new DebitCard
            {
                UserId = userId,
                NameDebitCard = debitCard.NameDebitCard,
                NumberDebitCard = debitCard.NumberDebitCard
            });

            if (verificaCartao)
            {
                throw new ("Card already exists");

            }
            

            var debitCardMap = _IMapper.Map<DebitCard>(debitCard);
            /*debitCardMap.NameDebitCard = debitCard.NameDebitCard; ;*/
            /*await _IDebitCard.Add(debitCardMap);*/
            await _IServiceDebitCard.Adicionar(debitCardMap);

            return debitCardMap.Notitycoes;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPut("/api/Update")]
        public async Task<List<Notifies>> Update(DebitCardViewModel debitCard)
        {
            debitCard.UserId = await RetornarIdUsuarioLogado();

            var debitCardMap = _IMapper.Map<DebitCard>(debitCard);
            /*await _IDebitCard.Update(debitCardMap);*/
            await _IServiceDebitCard.Atualizar(debitCardMap);

            return debitCardMap.Notitycoes;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/Delete")]
        public async Task<ActionResult<List<Notifies>>> Delete(int id)
        {
            var debitCard = await _IServiceDebitCard.GetByEntityId(id);
            if (debitCard == null)
            {
                return BadRequest("Debit card not found");
            }

            var userId = await RetornarIdUsuarioLogado();
            if (debitCard.UserId != userId)
            {
                return Forbid();
            }

            await _IDebitCard.Delete(debitCard);

            return debitCard.Notitycoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetEntityById")]
        public async Task<DebitCardViewModel> GetEntityById([FromQuery]DebitCard debitCard)
        {
            debitCard = await _IDebitCard.GetEntityById(debitCard.Id);
            var debitCardMap = _IMapper.Map<DebitCardViewModel>(debitCard);

            return debitCardMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/List")]
        public async Task<List<DebitCardViewModel>> List()
        {
            var debitCard = await _IDebitCard.List();
            var debitCardMap = _IMapper.Map<List<DebitCardViewModel>>(debitCard);

            return debitCardMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListDebitCardAtivos")]
        public async Task<List<DebitCardViewModel>> ListarDebitCardsAtivos()
        {
            var debitCard = await _IServiceDebitCard.ListarDebitCardsAtivos();
            var debitCardMap = _IMapper.Map<List<DebitCardViewModel>>(debitCard);

            return debitCardMap;
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }

            return string.Empty;
        }

        [HttpPost]
        [Route("send-text-mail")]
        public async Task<IActionResult> SendPlainTextEmail([FromQuery] string toEmail)
        {
            string fromEmail = _configuration.GetSection("SendGridEmailSettings")
            .GetValue<string>("FromEmail");

            string fromName = _configuration.GetSection("SendGridEmailSettings")
            .GetValue<string>("FromName");

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromName),
                Subject = "Nubank",
                PlainTextContent = "Obrigado por escolher nossos Serviços!"
            };
            msg.AddTo(toEmail);
            var response = await _sendGridClient.SendEmailAsync(msg);
            string message = response.IsSuccessStatusCode ? "Email Send Successfully" :
            "Email Sending Failed";
            return Ok(message);
        }

    }
}
