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

            var personagemEntity = await _personagemRepository.ObterPersonagem(idPersonagem);

            if (personagemEntity == null) throw new PersonagemNaoCadastradoException();

            personagemEntity = _mapper.Map<Personagem>(personagemInputModel);
            personagemEntity.Id = idPersonagem;

            await _personagemRepository.AtualizarPersonagem(personagemEntity);
        }

        public async Task<PersonagemViewModel> InserirPersonagem(PersonagemInputModel personagemInputModel)
        {
           
             var personagemEntity = await _personagemRepository.ObterPersonagem(personagemInputModel.Nome, personagemInputModel.Filme);

            if (personagemEntity != null) throw new PersonagemJaCadastradoException();

            var personagemInsert = _mapper.Map<Personagem>(personagemInputModel);
            personagemInsert.Id = Guid.NewGuid();
            
            await _personagemRepository.InserirPersonagem(personagemInsert);
            
            return _mapper.Map<PersonagemViewModel>(personagemInsert);
        }

        public async Task<PersonagemViewModel> ObterPersonagem(Guid idPersonagem)
        {
            var personagem = await _personagemRepository.ObterPersonagem(idPersonagem);

            if (personagem == null) throw new PersonagemNaoCadastradoException();

            else return _mapper.Map<PersonagemViewModel>(personagem);
        }

        public async Task<PersonagemViewModel> ObterPersonagem(string nome, string filme)
        {
            var personagem = await _personagemRepository.ObterPersonagem(nome,filme);

            if (personagem == null) throw new PersonagemNaoCadastradoException();

            else return _mapper.Map<PersonagemViewModel>(personagem);
        }

        public async Task<List<PersonagemViewModel>> ObterPFilme(int pagina, int quantidade, string filme)
        {
            var personagens = await _personagemRepository.ObterPFilme(pagina, quantidade, filme);
            if (personagens.Count == 0) throw new PersonagemNaoCadastradoException();
            return _mapper.Map<List<PersonagemViewModel>>(personagens);
        }

        public async Task<List<PersonagemViewModel>> ObterPNome(int pagina, int quantidade, string nome)
        {
            var personagens = await _personagemRepository.ObterPNome(pagina, quantidade, nome);
           
            if (personagens == null) throw new PersonagemNaoCadastradoException();
            
            return _mapper.Map<List<PersonagemViewModel>>(personagens);
        }

        public async Task<List<PersonagemViewModel>> ObterTodos(int pagina, int quantidade)
        {
            
            var personagens = await _personagemRepository.ObterTodos(pagina, quantidade);
            
            if (personagens.Count == 0) throw new NenhumPersonagemCadastradoException();
            
            return _mapper.Map<List<PersonagemViewModel>>(personagens);
        }

        public void Dispose()
        {
            _personagemRepository?.Dispose();
        }

        public async Task RemoverPersonagem(Guid idPersonagem)
        {
            var personagemEntity = await _personagemRepository.ObterPersonagem(idPersonagem);

            if (personagemEntity == null) throw new PersonagemNaoCadastradoException();

            await _personagemRepository.RemoverPersonagem(idPersonagem); 
        }
    }
}
