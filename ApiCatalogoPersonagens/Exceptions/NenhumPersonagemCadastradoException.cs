using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Exceptions
{
    public class NenhumPersonagemCadastradoException : Exception
    {
        public NenhumPersonagemCadastradoException()
            :base ("Nenhum Personagem Cadastrado")
        {
        }
    }
}
