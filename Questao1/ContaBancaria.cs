using System.Globalization;

namespace Questao1
{
    class ContaBancaria {

        private const double _taxaSaque = 3.5;
        public int NumeroConta { get; init; }
        public string NomeTitular { get; private set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int numeroConta, string nomeTitular, double saldo = 0)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            Saldo = saldo;
        }

        public void Deposito(double valor)
            => Saldo += valor;
        public void Saque(double valor)
            => Saldo -= (valor + _taxaSaque);

        public override string ToString() 
            => string.Concat("Conta: ", NumeroConta, ", Titular: ", NomeTitular, ", Saldo: $ ", Saldo.ToString("0.00").Replace(",", "."));
    }
}
