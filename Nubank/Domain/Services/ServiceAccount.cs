using Domain.Interfaces.InterfaceService;
using Entities.Entities;

namespace Domain.Services
{
    public class ServiceAccount : IServiceAccount
    {
        private readonly IServiceAccount _IServiceAccount;

        public ServiceAccount(IServiceAccount IServiceAccount)
        {
            _IServiceAccount = IServiceAccount;
        }

        public Task Adicionar(CurrentAccount Objeto)
        {
            Random value = new Random(10);
            Objeto.CreateAccountDate= DateTime.Now.AddYears(20);
            Objeto.Ativo = true;
            Objeto.Account = value;
            throw new NotImplementedException();
        }


        public Task Excluir(CurrentAccount Objeto)
        {
            throw new NotImplementedException();
        }

    }
}
