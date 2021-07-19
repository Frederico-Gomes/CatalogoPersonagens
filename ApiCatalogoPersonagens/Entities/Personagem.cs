using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Entities
{
    public class Personagem
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Ator { get; set; }
        public string Filme { get; set; }
        public byte Importancia { get; set; }
        public byte Existencia { get; set; }
    }
}
