using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web_api.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Marca é obrigatório preencher.")]
        [StringLength(50, ErrorMessage = "O campo Marca deve ter no máximo 50 caracteres.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório preencher.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo AnoModelo é obrigatório preencher.")]
        public int AnoModelo { get; set; }

        [Required(ErrorMessage = "O campo DataFabricacao é obrigatório preencher.")]
        public DateTime DataFabricacao { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório preencher.")]
        public double Valor { get; set; }

        [StringLength(500, ErrorMessage = "O campo Opcionais pode ter no máximo 500 caracteres.")]
        public string Opcionais { get; set; }

        public Veiculo(){}

        public void OpcionalToString()
        {
            this.Opcionais = Opcionais == null ? "" : Opcionais;
        }
    }
}