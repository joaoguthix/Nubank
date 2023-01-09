﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Notifies
    {
        public Notifies()
        {
            Notitycoes= new List<Notifies>();
        }


        [NotMapped]
        public string NomePropriedade { get; set; }
        [NotMapped]
        public string Mensagem { get; set; }
        [NotMapped]
        public List<Notifies> Notitycoes { get; set; }


        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(nomePropriedade) || string.IsNullOrWhiteSpace(valor))
            {
                Notitycoes.Add(new Notifies{

                    Mensagem = "Campo obrigatório",
                    NomePropriedade = nomePropriedade,
                });
                return false;

            }
            return true;

        }
        public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {

                    Mensagem = "Campo obrigatório",
                    NomePropriedade = nomePropriedade,
                });
                return false;

            }
            return true;

        }
    }
}
