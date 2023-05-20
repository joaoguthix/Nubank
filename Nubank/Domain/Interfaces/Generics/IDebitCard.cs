using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{

    /* Interface that inherits the basic operations of CRUD (Create, Read, Update, Delete),
     * and adds a new search operation for debit cards defining a search filter through an expression,
     *  where the filter parameters will be defined. */
    public interface IDebitCard : IGeneric<DebitCard>
    {
        Task<List<DebitCard>> ListarDebitCards(Expression<Func<DebitCard, bool>> exDebitCard);
    }
}
