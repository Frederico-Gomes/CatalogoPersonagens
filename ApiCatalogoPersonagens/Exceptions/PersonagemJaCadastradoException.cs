using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Exceptions
{
    public class PersonagemJaCadastradoException : Exception
    {
        public PersonagemJaCadastradoException()
            : base("Este personagem ja está cadastrado")
        {
        }
    }
}
