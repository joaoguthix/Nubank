using Domain.Interfaces;
using Domain.Interfaces.InterfaceService;
using Entities.Entities;

namespace Domain.Services
{
    public class ServiceDebitCard : IServiceDebitCard
    {
        private readonly IDebitCard _IDebitCard;

        public ServiceDebitCard(IDebitCard IDebitCard)
        {
            _IDebitCard = IDebitCard;
        }

        public async Task Adicionar(DebitCard objeto)
        {
            var validarTitulo = objeto.ValidarPropriedadeString(objeto.NameDebitCard, "NameDebitCard");
            var validaId = objeto.ValidaIdDebit(objeto.Id);
            if (validarTitulo || validaId)
            {
                objeto.CreationDate = DateTime.Now;
                objeto.AlteredDate = DateTime.Now.AddYears(5);
                objeto.Ativo = true;
                await _IDebitCard.Add(objeto);
            }
        }

        public async Task Atualizar(DebitCard objeto)
        {
            var validarTitulo = objeto.ValidarPropriedadeString(objeto.NameDebitCard, "NameDebitCard");
            if (validarTitulo)
            {
                objeto.Ativo = true;
                await _IDebitCard.Add(objeto);
            }
        }

        public async Task<List<DebitCard>> ListarDebitCardsAtivos()
        {
            return await _IDebitCard.ListarDebitCards(n => n.Ativo);
        }
    }
}
