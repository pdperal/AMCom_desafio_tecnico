using Questao5.Domain.Enumerators;

namespace Questao5.Domain
{
    public class Constants
    {
        public const int SALDO_INICIAL = 0;
        public const char DEBIT_TRANSACTION_IDENTIFIER = 'D';
        public const char CREDIT_TRANSACTION_IDENTIFIER = 'C';

        public static Dictionary<AccountBalanceErrorsEnum, string> BalanceErrors = new()
        {
            { AccountBalanceErrorsEnum.INVALID_ACCOUNT, "Apenas contas correntes cadastradas podem consultar o saldo" },
            { AccountBalanceErrorsEnum.INACTIVE_ACCOUNT, "Apenas contas correntes ativas podem consultar o saldo" },
        };

        public static Dictionary<AccountTransactionErrorsEnum, string> TransactionErrors = new()
        {
            { AccountTransactionErrorsEnum.INVALID_ACCOUNT, "Apenas contas correntes cadastradas podem receber movimentação" },
            { AccountTransactionErrorsEnum.INACTIVE_ACCOUNT, "Apenas contas correntes ativas podem receber movimentação" },
            { AccountTransactionErrorsEnum.INVALID_VALUE, "Apenas valores positivos podem ser recebidos" },
            { AccountTransactionErrorsEnum.INVALID_TYPE, "Apenas os tipos “débito” ou “crédito” podem ser aceitos" },
            { AccountTransactionErrorsEnum.GENERIC_ERROR, "Não foi possível processar a transação" },
        };
    }
}
