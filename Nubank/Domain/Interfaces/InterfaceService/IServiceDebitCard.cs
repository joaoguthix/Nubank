using Entities.Entities;

namespace Domain.Interfaces.InterfaceService
{
    public interface IServiceDebitCard
    {
        Task Adicionar(DebitCard objeto);

        Task Atualizar(DebitCard objeto);

        Task<List<DebitCard>> ListarDebitCardsAtivos();
    }
}
