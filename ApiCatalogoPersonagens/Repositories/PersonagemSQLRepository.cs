using ApiCatalogoPersonagens.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Repositories
{
    public class PersonagemSQLRepository : IPersonagemRepository
    {
        private readonly SqlConnection sqlConnection;

        public PersonagemSQLRepository(IConfiguration configuration)
        {
            this.sqlConnection = new SqlConnection(configuration.GetConnectionString("SqlServer"));
        }

        public async Task AtualizarPersonagem(Personagem personagemInputModel)
        {
            var comando = $"update Personagem set " +
               $"Id ='{personagemInputModel.Id}'," +
               $"Nome ='{personagemInputModel.Nome}'," +
               $"Ator ='{personagemInputModel.Ator}'," +
               $"Filme ='{personagemInputModel.Filme}'," +
               $"Importancia = '{personagemInputModel.Importancia}'," +
               $"Existencia = '{personagemInputModel.Existencia}' where Id = '{personagemInputModel.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection.Dispose();
        }

        public async Task InserirPersonagem(Personagem personagemInputModel)
        {
            var comando = $"insert Personagem (Id, Nome, Ator, Filme, Importancia, Existencia) values (" +
                $"'{personagemInputModel.Id}'," +
                $"'{personagemInputModel.Nome}'," +
                $"'{personagemInputModel.Ator}'," +
                $"'{personagemInputModel.Filme}'," +
                $"'{personagemInputModel.Importancia}'," +
                $"'{personagemInputModel.Existencia}');";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<Personagem> ObterPersonagem(Guid idPersonagem)
        {
            var comando = $"select * from Personagem where Id = '{idPersonagem}'";

            var personagens = await BuscaP(comando);

            if (personagens.Count == 0) return null;

            return personagens[0];
        }

        public async Task<Personagem> ObterPersonagem(string nome, string filme)
        {
            var comando = $"select * from Personagem where Nome = '{nome}' and Filme = '{filme}'";

            var personagens = await BuscaP(comando);

            if (personagens.Count == 0) return null;

            return personagens[0];
        }

        public async Task<List<Personagem>> ObterPFilme(int pagina, int quantidade, string filme)
        {

            var comando = $"select * from Personagem where Filme = '{filme}' " +
                $"order by Id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";
            
            var personagens = await BuscaP(comando);
            
            return personagens;
        }

        public async Task<List<Personagem>> ObterPNome(int pagina, int quantidade, string nome)
        {
            var comando = $"select * from Personagem where Nome = '{nome}' order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            var personagens = await BuscaP(comando);

     if (personagens.Count == 0)
            {
                return null;
            } 

            return personagens;
        }

        public async Task<List<Personagem>> ObterTodos(int pagina, int quantidade)
        {
            var comando = $"select * from Personagem order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";
            
            var personagens = await BuscaP(comando);
            
            return personagens;
        }

        public async Task RemoverPersonagem(Guid idPersonagem)
        {
            var comando = $" Delete from Personagem where Id='{idPersonagem}'";
            
            await sqlConnection.OpenAsync();
            
            SqlCommand sqlCommand = new(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            
            await sqlConnection.CloseAsync();
        }
    
        private async Task<List<Personagem>> BuscaP(string comando)
        {

            var personagens = new List<Personagem>();
            
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                personagens.Add(new Personagem
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Ator = (string)sqlDataReader["Ator"],
                    Filme = (string)sqlDataReader["Filme"],
                    Importancia = (byte)sqlDataReader["Importancia"],
                    Existencia = (byte)sqlDataReader["Existencia"]
                });
            }
            
            await sqlConnection.CloseAsync();
            
            return personagens;
        }
    }
}
