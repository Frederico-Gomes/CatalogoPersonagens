using ApiCatalogoPersonagens.InputModel;
using ApiCatalogoPersonagens.Services;
using ApiCatalogoPersonagens.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class PersonagensController : ControllerBase
    {
        private readonly IPersonagemService _personagemService;

        public PersonagensController(IPersonagemService personagemService)
        {
            _personagemService = personagemService;
        }

        private ActionResult VerificaPersonagens(IEnumerable<PersonagemViewModel> personagens)
        {
            if (personagens.Count() == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok();
            }
        }

        // Retorna uma lista de objetos contendo todos os personagems.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonagemViewModel>>> ObterTodos([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade =5)
        {
            var personagens= await _personagemService.ObterTodos(pagina, quantidade);

            if(personagens.Count == 0)
            {
                return NoContent();
            }
            return Ok();
            // precisa de testes
            //return VerificaPersonagens(personagens);

        }


        // Retorna uma lista de objetos com todos os personagems com determinado nome
        [HttpGet("/personagem/{nomePersonagem:string}")]
        public async Task<ActionResult<List<PersonagemViewModel>>> ObterPNome(string nomePersonagem)
        {
            var result = await _personagemService.ObterPNome(1, 4, nomePersonagem);
            return Ok();
        }

        // Retorna uma lista de objetos com todos os personagem de determinado filme
        [HttpGet("/filme/{nomeFIlme:string}")]
        public async Task<ActionResult<List<PersonagemViewModel>>> ObterPFilme(string nomeFIlme)
        {
            var result = await _personagemService.ObterPFilme(1, 4, nomeFIlme);
            return Ok();
        }

        [HttpGet("/idPersonagem/{idPersonagem:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> ObterPersonagem([FromRoute] Guid idPersonagem)
        {
            var personagem = await _personagemService.ObterPersonagem(idPersonagem);

            if(personagem == null) return NoContent();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<PersonagemViewModel>> InserirPersonagem([FromBody]PersonagemInputModel personagemInputModel)
        {
            try
            {
                var personagem = await _personagemService.InserirPersonagem(personagemInputModel);
                return Ok(personagem);
            }
            catch(Exception ex)
            {
                return UnprocessableEntity("Ja existe um personagem com este nome para este filme");
            }
        }
        [HttpPut("{idPersonagem:guid}")]
        public async Task<ActionResult> AtualizarPersonagem([FromRoute]Guid idPersonagem,[FromBody] PersonagemInputModel personagemInputModel)
        {
            try
            {
                await _personagemService.AtualizarPersonagem(idPersonagem, personagemInputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Personagem não existe");
            }
        }
        [HttpDelete("{idPersonagem:guid}")]
        public async Task<ActionResult> ApagarPersonagem(Guid idPersonagem, object personagem)
        {
            return Ok();
        }

    }
}
