namespace Questao5.Domain.Enumerators
{
    public enum AccountBalanceErrorsEnum
    {
        INVALID_ACCOUNT = 0,
        INACTIVE_ACCOUNT = 1
    }

    public enum AccountTransactionErrorsEnum
    {
        INVALID_ACCOUNT = 0,
        INACTIVE_ACCOUNT = 1,
        INVALID_VALUE = 2,
        INVALID_TYPE = 3,
        GENERIC_ERROR = 4
    }
}
