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
            using var banco = new ContextBase(_OptionsBuilder);
            return await banco.DebitCards.Where(exDebitCard).AsNoTracking().ToListAsync();
        }

        public async Task<DebitCard> GetEntityById(int id)
        {
            using var banco = new ContextBase(_OptionsBuilder);
            return await banco.DebitCards.FindAsync(id);
        }

        public async Task Delete(DebitCard objeto)
        {
            using var banco = new ContextBase(_OptionsBuilder);
            banco.Entry(objeto).State = EntityState.Deleted;
            await banco.SaveChangesAsync();
        }
    }

}
