using ApiCatalogoPersonagens.Entities;
using ApiCatalogoPersonagens.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Repositories
{
    public class PersonagemRepository : IPersonagemRepository
    {
        private static Dictionary<Guid, Personagem> personagens = new()
        {
            {Guid.Parse("a731b8b0-31c1-429d-8b05-e47cdb8f9ce8"), new Personagem{ Id = Guid.Parse("a731b8b0-31c1-429d-8b05-e47cdb8f9ce8"), Nome = "Hermione Granger", Ator = "Emma Watson" , Filme ="Harry Potter e a Pedra Filosofal" , Importancia = (byte)Importancia.CoProtagonista, Existencia = (byte)Existencia.FiccionalFiccional } },
            {Guid.Parse("a922a6d4-424f-40bb-bc3c-4a447db51c9a"), new Personagem{ Id = Guid.Parse("a922a6d4-424f-40bb-bc3c-4a447db51c9a"), Nome = "Harry Potter", Ator ="Daniel Radcliffe"  , Filme ="Harry Potter e a Pedra Filosofal" , Importancia = (byte)Importancia.Protagonista, Existencia = (byte)Existencia.FiccionalFiccional } },
            {Guid.Parse("ffb6c662-682d-42b9-89b2-df64f9880007"), new Personagem{ Id = Guid.Parse("ffb6c662-682d-42b9-89b2-df64f9880007"), Nome = "Ron Weasley", Ator ="Rupert Grint,"  ,Filme ="Harry Potter e a Pedra Filosofal" , Importancia = (byte)Importancia.CoProtagonista, Existencia = (byte)Existencia.FiccionalFiccional } }
        };

        public Task AtualizarPersonagem(Personagem personagemInputModel)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task<Personagem> InserirPersonagem(Personagem personagemInputModel)
        {
            throw new NotImplementedException();
        }

        public Task<Personagem> ObterPersonagem(Guid idPersonagem)
        {
            if (!personagens.ContainsKey(idPersonagem))
                return null;
            return Task.FromResult(personagens[idPersonagem]);
        }

        public Task<Personagem> ObterPersonagem(string nome, string filme)
        {
            var per = personagens.Values.Where(p => p.Nome.Equals(nome) && p.Filme.Equals(filme)).ToList();
            if (per.Count.Equals(0))
                return null;

            return Task.FromResult(per[0]);
        }

        public Task<List<Personagem>> ObterPFilme(int pagina, int quantidade, string filme)
        {
            return Task.FromResult(personagens.Values.Where(p => p.Filme.Equals(filme)).ToList());
        }

        public Task<List<Personagem>> ObterPNome(int pagina, int quantidade, string nome)
        {
            return Task.FromResult(personagens.Values.Where(p => p.Nome.Equals(nome)).ToList());
        }

        public Task<List<Personagem>> ObterTodos(int pagina, int quantidade)
        {
            return Task.FromResult(personagens.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }
    }
}
