using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Exceptions
{
    public class PersonagemNaoCadastradoException : Exception
    {
        public PersonagemNaoCadastradoException()
            : base("Este personagem não está cadastrado")
        {
        }
    }
}
