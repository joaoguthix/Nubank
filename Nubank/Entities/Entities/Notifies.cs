using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{

    /* Class where a list of validations normally used only
       for global and generic notifies will be defined*/
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

        /*public bool ValidaIdDebit(int idValue, string nomePropriedade)
        {
            if(idValue >= 0)
            {
                Notitycoes.Add(new Notifies
                {
                    Mensagem = "Cartão já existe",
                    NomePropriedade = nomePropriedade,
                }) ;
                return false;

            }
            return true;
        }*/
    }
}
