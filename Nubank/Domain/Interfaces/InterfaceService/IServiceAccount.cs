using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceService
{
    public interface IServiceAccount
    {

        Task Adicionar(CurrentAccount Objeto);

        Task Excluir(CurrentAccount Objeto);
    }
}
