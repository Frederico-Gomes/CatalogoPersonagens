using ApiCatalogoPersonagens.Entities;
using ApiCatalogoPersonagens.Exceptions;
using ApiCatalogoPersonagens.InputModel;
using ApiCatalogoPersonagens.Repositories;
using ApiCatalogoPersonagens.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Services
{
    public class PersonagemService : IPersonagemService
    {
        private readonly IPersonagemRepository _personagemRepository;
        private readonly IMapper _mapper;

        public PersonagemService(IPersonagemRepository personagemRepository, IMapper mapper)
        {
            _personagemRepository = personagemRepository;
            _mapper = mapper;
        }


        public async Task AtualizarPersonagem(Guid idPersonagem, PersonagemInputModel personagemInputModel)
        {
            // Procura no banco um personagem com o id selecionado
            var personagemEntity = await _personagemRepository.ObterPersonagem(idPersonagem);

            // Se o personagem não existir lança excessão
            if (personagemEntity == null) throw new PersonagemNaoCadastradoException();

            // Atualiza os dados do personagem selecionado
            personagemEntity = _mapper.Map<Personagem>(personagemInputModel);
            personagemEntity.Id = idPersonagem;
            // Atualiza os dados no banco de dados
            await _personagemRepository.AtualizarPersonagem(personagemEntity);
        }

        public async Task<PersonagemViewModel> InserirPersonagem(PersonagemInputModel personagemInputModel)
        {
            // Verifica se o personagem ja existe no banco de dados
          //  var personagemEntity = await _personagemRepository.ObterPersonagem(personagemInputModel.Nome, personagemInputModel.Filme);
            
            // Se o personagem existir é lancada uma exceção
            //if (personagemEntity != null)
           // {
             //   throw new PersonagemJaCadastradoException();
            //}

            // Mapeia os dados da entidade para um modelo de input
            var personagemInsert = _mapper.Map<Personagem>(personagemInputModel);
            personagemInsert.Id = Guid.NewGuid();
            
            // Faz a inserção dos dados no Banco
            await _personagemRepository.InserirPersonagem(personagemInsert);
            
            // Retorna um modelo que o usuário pode ter acesso
            return _mapper.Map<PersonagemViewModel>(personagemInsert);
        }

        public async Task<PersonagemViewModel> ObterPersonagem(Guid idPersonagem)
        {
            var personagem = await _personagemRepository.ObterPersonagem(idPersonagem);

            if (personagem == null) return null;

            else return _mapper.Map<PersonagemViewModel>(personagem);
        }

        public async Task<PersonagemViewModel> ObterPersonagem(string nome, string filme)
        {
            var personagem = await _personagemRepository.ObterPersonagem(nome,filme);

            if (personagem == null) return null;

            else return _mapper.Map<PersonagemViewModel>(personagem);
        }

        public async Task<List<PersonagemViewModel>> ObterPFilme(int pagina, int quantidade, string filme)
        {
            var personagens = await _personagemRepository.ObterPFilme(pagina, quantidade, filme);
            return _mapper.Map<List<PersonagemViewModel>>(personagens);
        }

        public async Task<List<PersonagemViewModel>> ObterPNome(int pagina, int quantidade, string nome)
        {
            var personagens = await _personagemRepository.ObterPNome(pagina, quantidade, nome);
            return _mapper.Map<List<PersonagemViewModel>>(personagens);
        }

        public async Task<List<PersonagemViewModel>> ObterTodos(int pagina, int quantidade)
        {
            var personagens = await _personagemRepository.ObterTodos(pagina, quantidade);
            return _mapper.Map<List<PersonagemViewModel>>(personagens);
        }

        //Fecha a conexão com o banco de dados
        public void Dispose()
        {
            _personagemRepository?.Dispose();
        }

        public async Task RemoverPersonagem(Guid idPersonagem)
        {
            await _personagemRepository.RemoverPersonagem(idPersonagem); 
        }
    }
}
