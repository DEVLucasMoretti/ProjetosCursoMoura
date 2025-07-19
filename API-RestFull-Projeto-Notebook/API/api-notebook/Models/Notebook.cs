using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models
{
    public class Notebook
    {
        

        [Required(ErrorMessage ="Preencha o campo corretamente")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o campo corretamente")]
        public double Valor { get; set; }

        public int Id { get; set; }
        public string Opcionais { get; set; }

        
        public Notebook() {     }
        
        public void OpcionalToString()
        {
            this.Opcionais = Opcionais == null ? "" : Opcionais;
        }
    }
}
