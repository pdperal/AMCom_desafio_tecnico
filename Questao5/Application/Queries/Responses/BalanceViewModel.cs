namespace Questao5.Application.Queries.Responses
{
    public class BalanceViewModel
    {
        public int NumeroConta { get; private set; }
        public string NomeTitular { get; private set; }
        public DateTime DataConsulta { get => DateTime.Now; }
        public double Saldo { get; private set; }

        public BalanceViewModel(int numeroConta, string nomeTitular, double saldo)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            Saldo = saldo;
        }
    }
}
