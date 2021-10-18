using ApiCatalogoJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoSqlServerRepository : IJogoRepository
    {
        private readonly SqlConnection  sqlConnection;

        public JogoSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade, object sqlDataReader, string Nome, string Produtora, double Preco, Guid Id)
        {
            var jogos = new  List<Jogo>();

            var comando =  $"Select * from Jogos order by id offset {(( pagina  -  1 ) *  quantidade )} rows fetch next { quantidade } rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader SQLDataReader = await  sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo)
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return jogos;
        }

        public async Task<Jogo> Obter(Guid id, object sqlDataReader)
        {
            Jogo jogo = null;

            var comando =  $"Select * from Jogos where Id = '{ id }' ";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader SQLDataReader = await  sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                Jogo = new Jogo
                {
                    Id = (Guid)sqlDataReader[" Id "],
                    Nome = (string)sqlDataReader[" Nome "],
                    Produtora = (string)sqlDataReader[" Produtora "],
                    Preco = (double)sqlDataReader[" Preco "]
                };
            }

            await sqlConnection.CloseAsync();

            jogo de retorno;
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora, object sqlDataReader, Guid Id, string Nome, string Produtora, double Preco)
        {
            var jogos = new  List<Jogo>();

            var comando =  $"Select * from Jogos onde Nome = '{ nome }' e Produtora = '{ produtora }' ";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader SQLDataReader = await  sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo)
                {
                    Id = (Guid)sqlDataReader[" Id "],
                    Nome = (string)sqlDataReader[" Nome "],
                    Produtora = (string)sqlDataReader[" Produtora "],
                    Preco = (double)sqlDataReader[" Preco "]
                });
            }

            await sqlConnection.CloseAsync();

            return jogos;
        }

        public async Task  Inserir(Jogo jogo)
        {
            var comando =  $"Inserir valores Jogos (Id, Nome, Produtora, Preco) ('{ jogo . Id }', '{ jogo . Nome }', '{ jogo . Produtora }', { jogo . Preco . ToString () . Substitua ( " , " , ". " )}) ";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Jogo jogo)
        {
            var comando =  $"Update Jogos set Nome = '{ jogo.Nome }', Produtora = '{ jogo.Produtora }', Preco = { jogo . Preco . ToString (). Replace ( " , " , ". " )} onde Id = '{ jogo . Id }' ";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover (Guid id)
        {
            var comando =  $"Deletar de Jogos onde Id = '{ id }'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            await sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}