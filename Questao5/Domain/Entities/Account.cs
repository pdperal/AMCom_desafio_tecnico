namespace Questao5.Domain.Entities
{
    public class Account
    {
        public string IdContaCorrente { get; private set; }
        public int Numero { get; private set; }
        public string Nome { get; private set; }
        public int Ativo { get; private set; }

        public Account(string idContaCorrente, int numero, string nome, int ativo)
        {
            IdContaCorrente = idContaCorrente;
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
        }

        public bool IsActive()
            => Ativo == 1;
    }
}
