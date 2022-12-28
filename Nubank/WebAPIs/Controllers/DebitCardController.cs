using AutoMapper;
using Domain.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitCardController : ControllerBase
    {

        private readonly IMapper _IMapper;
        private readonly IDebitCard _IDebitCard;

        public DebitCardController(IMapper IMapper, IDebitCard IDebitCard)
        {
            _IMapper = IMapper;
            _IDebitCard = IDebitCard;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(DebitCardViewModel debitCard)
        {
            debitCard.UserId = await RetornarIdUsuarioLogado();

            var debitCardMap = _IMapper.Map<DebitCard>(debitCard);
            await _IDebitCard.Add(debitCardMap);

            return debitCardMap.Notitycoes;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Update")]
        public async Task<List<Notifies>> Update(DebitCardViewModel debitCard)
        {
            debitCard.UserId = await RetornarIdUsuarioLogado();

            var debitCardMap = _IMapper.Map<DebitCard>(debitCard);
            await _IDebitCard.Update(debitCardMap);

            return debitCardMap.Notitycoes;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Delete")]
        public async Task<List<Notifies>> Delete(DebitCardViewModel debitCard)
        {
            debitCard.UserId = await RetornarIdUsuarioLogado();

            var debitCardMap = _IMapper.Map<DebitCard>(debitCard);
            await _IDebitCard.Delete (debitCardMap);

            return debitCardMap.Notitycoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/GetEntityById")]
        public async Task<DebitCardViewModel> GetEntityById(DebitCard debitCard)
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

        private async Task<string> RetornarIdUsuarioLogado()
        {
            if( User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }

            return string.Empty;
        }

    }
}
