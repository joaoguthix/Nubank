using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IDebitCard : IGeneric<DebitCard>
    {
        Task<List<DebitCard>> ListarDebitCards(Expression<Func<DebitCard, bool>> exDebitCard);
    }
}
