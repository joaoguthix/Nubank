using Domain.Interfaces.Generics;
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
    public class RepositoryAccount : RepositoryGenerics<CurrentAccount>, IAccount
    {

        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryAccount()
        {

            _OptionsBuilder = new DbContextOptions<ContextBase>();

        }
    }
}
