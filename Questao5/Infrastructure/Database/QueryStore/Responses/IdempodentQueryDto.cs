using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class IdempodentQueryDto
    {
        public string chave_idempotencia { get; set; }
        public string requisicao { get; set; }
        public string resultado { get; set; }
        public Idempodent ToEntity()
        {
            return new Idempodent(chave_idempotencia, requisicao, resultado);
        }
    }
}
