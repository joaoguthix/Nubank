using Domain.Interfaces;
using Entities.Entities;
using Infrastucture.Configuration;
using Infrastucture.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastucture.Repository.Repositories
{
    public class RepositoryDebitCard : RepositoryGenerics<DebitCard>, IDebitCard

    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryDebitCard()
        {

            _OptionsBuilder = new DbContextOptions<ContextBase>();

        }

        public async Task<List<DebitCard>> ListarDebitCards(Expression<Func<DebitCard, bool>> exDebitCard)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await banco.DebitCards.Where(exDebitCard).AsNoTracking().ToListAsync();
            }
        }
    }
}
