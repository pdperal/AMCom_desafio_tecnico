using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public AccountRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<Account?> GetAccountInformation(GetAccountDto dto)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            try
            {
                connection.Open();
                var queryResult = await connection
                    .QueryAsync<AccountQueryDto>($"select * from contacorrente where idcontacorrente = '{dto.Id}'");

                return queryResult is null ? default 
                    : queryResult
                    .Select(x => x.ToEntity())
                    .SingleOrDefault();
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
