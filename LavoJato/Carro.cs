using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavoJato
{
    class Carro
    {
        private int Id { get; set; }
        private string Nome { get; set; }
        private int Ano { get; set; }

        public Carro()
        {
            
        }
        public Carro(int id, string nome, int ano)
        {
            Id = id;
            Nome = nome;
            Ano = ano;
        }
        public static bool RetirarCarroDoPatio(List<Carro> listaPatio, int idCarro)
        {
            bool removeu = false;
            foreach (var item in listaPatio)
            {
                if (idCarro == item.Id)
                {
                    listaPatio.Remove(item);
                    removeu = true;
                    break;
                }
            }
            return removeu;
        }        
        public static bool FinalizarPrograma(List<Carro> listaPatio, Queue<Carro> filaCarros, string opcao)
        {
            bool sair = false;

            if (opcao == "1")
            {
                if (listaPatio.Count() == 0 && filaCarros.Count() == 0)
                    sair = true;  
            }
            return sair;
        }

        public static string ImprimirCarros(Carro item)
        {
            return $"({item.Id}) - {item.Nome}";
        }

    }
}
