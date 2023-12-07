using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class AccountQueryDto
    {
        public string idcontacorrente { get; set; }
        public int numero { get; set; }
        public string nome { get; set; }
        public int ativo { get; set; }

        public Account ToEntity()
        {
            return new Account(idcontacorrente, numero, nome, ativo);
        }
    }

}
