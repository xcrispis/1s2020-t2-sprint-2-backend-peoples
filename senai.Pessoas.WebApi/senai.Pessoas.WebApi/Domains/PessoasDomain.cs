using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Pessoas.WebApi.Domains
{
    /// <summary>
    /// Classe que representa a tabela Pessoas
    /// </summary>
    public class PessoasDomain
    {
        public int IdPessoas { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        
        public string datanascimento { get; set; }

    }
}
