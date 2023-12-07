using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public TransactionRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<List<Transaction>> GetAllTransactions(GetTransactionsDto account)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            try
            {
                connection.Open();
                var queryResult = await connection
                    .QueryAsync<TransactionQueryDto>($"select * from movimento where idcontacorrente = '{account.Id}'");

                return queryResult
                    .Select(x => x.ToEntity())
                    .ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<bool> SaveTransaction(NewTransactionDto transaction)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            try
            {
                connection.Open();
                var queryResult = await connection
                    .ExecuteScalarAsync<int>("insert into movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
                                        "values (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)", 
                                        new
                                        {
                                            idmovimento = transaction.Id,
                                            idcontacorrente = transaction.AccountId,
                                            datamovimento = transaction.TransactionDate,
                                            tipomovimento = transaction.TransactionType,
                                            valor = transaction.Amount
                                        });

                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
