using Entities.Entities;

namespace Domain.Interfaces.InterfaceService
{
    public interface IServiceDebitCard
    {
        Task Adicionar(DebitCard objeto);

        Task Atualizar(DebitCard objeto);

        Task<List<DebitCard>> ListarDebitCardsAtivos();

        Task<bool> VerifyCard(DebitCard objeto);

        Task Delete(DebitCard objeto);

        Task <DebitCard> GetByEntityId(int id);

    }
}
