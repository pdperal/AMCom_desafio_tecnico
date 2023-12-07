using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Sqlite;
using System.Transactions;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class IdempotentRepository : IIdempotentRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public IdempotentRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<Idempodent> CheckForIdempondentRequestResult(GetIdempodentDto dto)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            try
            {
                connection.Open();
                var queryResult = await connection.QueryAsync<IdempodentQueryDto>($"select * from idempotencia where chave_idempotencia = '{dto.Id}'");

                return queryResult.Count() > 0 ?
                    queryResult
                    .Single()
                    .ToEntity()
                    : default;
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

        public async Task SaveIdempondentRequest(NewIdempodentDto newIdempodentDto)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            try
            {
                connection.Open();
                var queryResult = await connection.
                    ExecuteScalarAsync<int>("insert into idempotencia (chave_idempotencia, requisicao, resultado)" +
                                        "values (@chave_idempotencia, @requisicao, @resultado)",
                                        new
                                        {
                                            chave_idempotencia = newIdempodentDto.Id,
                                            requisicao = newIdempodentDto.Request,
                                            resultado = newIdempodentDto.Response,
                                        });
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
