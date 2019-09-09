using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Cliente
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }
        
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        public long IdCliente { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        public string Cpf { get; set; }
    }    
}