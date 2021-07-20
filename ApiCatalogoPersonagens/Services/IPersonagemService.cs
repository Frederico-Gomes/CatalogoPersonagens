using ApiCatalogoPersonagens.InputModel;
using ApiCatalogoPersonagens.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Services
{
    public interface IPersonagemService : IDisposable
    {
        Task<List<PersonagemViewModel>> ObterTodos(int pagina, int quantidade);
        Task<List<PersonagemViewModel>> ObterPNome(int pagina, int quantidade, string nome);
        Task<List<PersonagemViewModel>> ObterPFilme(int pagina, int quantidade, string filme);
        Task<PersonagemViewModel> ObterPersonagem(Guid idPersonagem);
        Task<PersonagemViewModel> ObterPersonagem(string nome, string filme);
        Task<PersonagemViewModel> InserirPersonagem(PersonagemInputModel personagemInputModel);
        Task AtualizarPersonagem(Guid idPersonagem, PersonagemInputModel personagemInputModel);
    }
}
