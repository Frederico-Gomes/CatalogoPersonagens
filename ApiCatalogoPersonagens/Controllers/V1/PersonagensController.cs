using ApiCatalogoPersonagens.Exceptions;
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


        /// <summary>
        /// Busca todos os personagens de maneira paginada e ordenado por ID
        /// </summary>
        /// <param name="pagina"> Indica qual pagina está sendo consultada. Minimo 1</param>
        /// <param name="quantidade">Indica quantidade de registros por pagina</param>
        /// <response code = "200"> Retorna lista de personagens </response>
        /// <response code = "404"> Nenhum  Personagem Cadastrado</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonagemViewModel>>> ObterTodos([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade =3)
        {
            try
            {
                var personagens= await _personagemService.ObterTodos(pagina, quantidade);
                return Ok(personagens);

            }
            catch(NenhumPersonagemCadastradoException)
            {
                return NotFound("Nenhum personagem cadastrado");
            }

        }

        /// <summary>
        /// Busca personagens pelo nome
        /// </summary>
        /// <param name="nomePersonagem">Nome dos personagens na busca</param>
        /// <param name="pagina"> Indica qual pagina está sendo consultada. Minimo 1</param>
        /// <param name="quantidade">Indica quantidade de registros por pagina</param>
        /// <response code = "200"> Retorna lista de personagens com respectivo nome</response>
        /// <response code = "404"> Nenhum personagem com nome cadastrado </response>
        [HttpGet("/personagem/{nomePersonagem}")]
        public async Task<ActionResult<List<PersonagemViewModel>>> ObterPNome(string nomePersonagem, [FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 3)
        {
            try
            {
            var personagens = await _personagemService.ObterPNome(pagina, quantidade, nomePersonagem); 
            return Ok(personagens);

            }
            catch (PersonagemNaoCadastradoException)
            {

                return NotFound("Nenhum personagem com o nome cadastrado");
            }
        }

        /// <summary>
        /// Busca todos os personagems de determinado filme
        /// </summary>
        /// <param name="nomeFilme"> Nome do filme </param>
        /// <param name="pagina"> Indica qual pagina está sendo consultada. Minimo 1</param>
        /// <param name="quantidade">Indica quantidade de registros por pagina</param>
        /// <response code = "404"> Nenhum Personagem cadastrado no filme </response>
        /// <response code = "200"> Retorna todos os personagens que aparecem no filme</response>
        [HttpGet("/filme/{nomeFilme}")]
        public async Task<ActionResult<List<PersonagemViewModel>>> ObterPFilme(string nomeFilme, [FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 3)
        {
            try
            {
                var personagens = await _personagemService.ObterPFilme(pagina, quantidade, nomeFilme);
                return Ok(personagens);

            }
            catch (PersonagemNaoCadastradoException)
            {

                return NotFound("Nenhum personagem Cadastrado no filme");
            }
        }

        /// <summary>
        /// Busca um personagem pelo ID
        /// </summary>
        /// <param name="idPersonagem">ID do personagem </param>
        /// <response code = "200"> Retorna o personagem solicitado </response>
        /// <response code = "404"> Não existe personagem cadastrado com tal id </response>
        [HttpGet("/idPersonagem/{idPersonagem:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> ObterPersonagem([FromRoute] Guid idPersonagem)
        {
            try
            {
                var personagem = await _personagemService.ObterPersonagem(idPersonagem);
                return Ok(personagem);

            }
            catch (PersonagemNaoCadastradoException)
            {

                return NotFound("Personagem não cadastrado");
            }



        }

        /// <summary>
        /// Busca um Personagem de determinado filme
        /// </summary>
        /// <param name="nome">Nome do personagem para busca</param>
        /// <param name="filme">Nome do filme para a busca</param>
        /// <response code = "200"> Retorna o personagem solicitado </response>
        /// <response code = "404"> Não existe personagem esse personagem no filme solicitado </response>
        [HttpGet("/buscaP/{nome}/{filme}")]
        public async Task<ActionResult<PersonagemViewModel>> ObterPersonagem([FromRoute] string nome, [FromRoute] string filme)
        {
            try
            {
                var personagem = await _personagemService.ObterPersonagem(nome,filme);
                return Ok(personagem);

            }
            catch (PersonagemNaoCadastradoException)
            {

                return NotFound("Personagem não cadastrado");
            }
        }

        /// <summary>
        /// Insere um personagem no banco de dados
        /// </summary>
        /// <remarks>
        ///     Não podem existir personagens em um filme com o mesmo nome
        /// </remarks>
        /// <param name="personagemInputModel"> Personagem a ser inserido</param>
        ///  <response code = "200"> Personagem criado com sucesso </response>
        ///  <response code = "409"> Personagem com mesmo nome ja existe no filme </response>
        [HttpPost]
        public async Task<ActionResult<PersonagemViewModel>> InserirPersonagem([FromBody]PersonagemInputModel personagemInputModel)
        {
            try
            {
                var personagem = await _personagemService.InserirPersonagem(personagemInputModel);
                return Ok(personagem);
            }
            catch (PersonagemJaCadastradoException)
            {
                return Conflict("Ja existe um personagem com este nome neste filme este filme");
            }
        }

        /// <summary>
        /// Modifica os dados de determinado personagem
        /// </summary>
        /// <param name="idPersonagem">Id do personagem que sofrerá as alterações </param>
        /// <param name="personagemInputModel">Dados novos que devem ser modificados no personagem </param>
        /// <response code = "200">Personagem alterado com sucesso </response>
        /// <response code = "404">Personagem não existe </response>
        [HttpPut("{idPersonagem:guid}")]
        public async Task<ActionResult> AtualizarPersonagem([FromRoute]Guid idPersonagem,[FromBody] PersonagemInputModel personagemInputModel)
        {
            try
            {
                await _personagemService.AtualizarPersonagem(idPersonagem, personagemInputModel);
                return Ok();
            }
            catch (PersonagemNaoCadastradoException)
            {
                return NotFound("Personagem não existe");
            }
        }

        /// <summary>
        /// Remove um personagem do banco de dados
        /// </summary>
        /// <param name="idPersonagem">Id do personagem para remoção</param>
        /// <response code = "200">Personagem removido com sucesso</response>
        /// <response code = "404">Personagem Não existe</response>
        [HttpDelete("{idPersonagem:guid}")]
        public async Task<ActionResult> RemoverPersonagem(Guid idPersonagem)
        {
            try
            {
                await _personagemService.RemoverPersonagem(idPersonagem);
                return Ok();
            }
            catch (PersonagemNaoCadastradoException)
            {
                return NotFound("Não foi possivel encontrar o personagem");
            }
        }

    }
}
