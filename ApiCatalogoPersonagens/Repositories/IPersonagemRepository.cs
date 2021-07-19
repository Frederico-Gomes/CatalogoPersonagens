using ApiCatalogoPersonagens.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Repositories
{
    public interface IPersonagemRepositorycs
    {
        Task<List<Personagem>> ObterTodos(int pagina, int quantidade);
        Task<List<Personagem>> ObterPNome(int pagina, int quantidade, string nome);
        Task<List<Personagem>> ObterPFilme(int pagina, int quantidade, string filme);
        Task<Personagem> ObterPersonagem(Guid idPersonagem);
        Task<Personagem> InserirPersonagem(Personagem personagemInputModel);
        Task AtualizarPersonagem(Guid idPersonagem, Personagem personagemInputModel);
    }
}
