using Domain.Interfaces;
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
        private readonly IDebitCard _IDebitCard;

        public ServiceDebitCard(IDebitCard IDebitCard)
        {
            _IDebitCard = IDebitCard;
        }

    }
}
