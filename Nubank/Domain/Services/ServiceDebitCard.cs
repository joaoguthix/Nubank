using Domain.Interfaces.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceDebitCard : IServiceDebitCard
    {
        private readonly IServiceDebitCard _IServiceDebitCard;

        public ServiceDebitCard(IServiceDebitCard IServiceDebitCard)
        {
            _IServiceDebitCard = IServiceDebitCard;
        }

    }
}
