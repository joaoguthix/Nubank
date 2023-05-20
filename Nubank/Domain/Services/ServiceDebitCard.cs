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

            if (validarTitulo)
            {
                objeto.CreationDate = DateTime.Now;
                objeto.AlteredDate = DateTime.Now.AddYears(5);
                objeto.Ativo = true;
                await _IDebitCard.Add(objeto);
            }
        }

        /*public  Task<bool> VerifyCard(string userId)
        {
            int userIdInt;
            bool success = int.TryParse(userId, out userIdInt);
            if (!success)
            {
                // handle invalid userId value here
            }
            var debitCards =  _IDebitCard.List(new DebitCard() { UserId = userIdInt });
            return debitCards.Any();
        }*/


        public async Task Atualizar(DebitCard objeto)
        {
            var validarTitulo = objeto.ValidarPropriedadeString(objeto.NameDebitCard, "NameDebitCard");
            if (validarTitulo)
            {
                objeto.Ativo = true;
                await _IDebitCard.Add(objeto);
            }
        }

        /*Method that defines a search for active cards*/
        public async Task<List<DebitCard>> ListarDebitCardsAtivos()
        {
            return await _IDebitCard.ListarDebitCards(n => n.Ativo);
        }


        /*Method to check if the user already has a card*/
        public async Task<bool> VerifyCard(DebitCard objeto)
        {
            
            /*bool success =*/ 
            /*if (!int.TryParse(objeto.UserId, out int userIdAsInt))
            {
                throw new ArgumentException("Valor inválido para UserId");
            }*/
            var debitCards = await _IDebitCard.ListarDebitCards( dc => dc.UserId == objeto.UserId
            && dc.NameDebitCard == objeto.NameDebitCard && dc.NumberDebitCard == objeto.NumberDebitCard);

            return debitCards.Any();
        }
    }

}
