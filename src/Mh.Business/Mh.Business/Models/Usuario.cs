using System;
using System.Collections.Generic;
using System.Text;

namespace Mh.Business.Models
{
    public class Usuario : Entity 
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Escolaridade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
