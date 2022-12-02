using Domain.Interfaces;
using Entities.Entities;
using Infrastucture.Configuration;
using Infrastucture.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Repositories
{
    public class RepositoryDebitCard : RepositoryGenerics<DebitCard>, IDebitCard

    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryDebitCard()
        {

            _OptionsBuilder = new DbContextOptions<ContextBase>();

        }

    }
}
