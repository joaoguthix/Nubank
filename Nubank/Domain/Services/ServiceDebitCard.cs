using Domain.Interfaces;
using Domain.Interfaces.InterfaceService;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

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

        public async Task Atualizar(DebitCard objeto)
        {
            var validarTitulo = objeto.ValidarPropriedadeString(objeto.NameDebitCard, "NameDebitCard");
            if (validarTitulo)
            {
                objeto.Ativo = true;
                await _IDebitCard.Update(objeto);
            }
        }

        public async Task Delete(DebitCard objeto)
        {
            var deleteCard = await _IDebitCard.GetEntityById(objeto.Id);
            if (deleteCard != null)
            {
                await _IDebitCard.Delete(deleteCard);
            }
            // Não é necessário retornar nada
        }

        public Task<DebitCard> GetByEntityId(int id)
        {
            throw new NotImplementedException();
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
            var debitCards = await _IDebitCard.ListarDebitCards(dc => dc.UserId == objeto.UserId
            && dc.NameDebitCard == objeto.NameDebitCard && dc.NumberDebitCard == objeto.NumberDebitCard);

            return debitCards.Any();
        }


    }

}
