using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.InputModel
{
    public class PersonagemInputModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Nome não deve conter mais de 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Ator não deve conter mais de 100 caracteres")]
        public string Ator { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Filme não deve conter mais de 20 caracteres")]
        public string Filme { get; set; }
        [Required]
        public byte Importancia { get; set; }
        [Required]
        public byte Existencia { get; set; }
    }
}
