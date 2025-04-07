using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavoJato
{
    class Program
    {
        static void Main(string[] args)
        {
            bool deuCerto = false;
            int id = 0;
            int opcao = 0;
            Queue<Carro> filaCarros = new Queue<Carro>();
            List<Carro> listaPatio = new List<Carro>();

            while (true)
            {

                Console.WriteLine("\r\n                        ██╗░░░░░░█████╗░██╗░░░██╗░█████╗░  ░░░░░██╗░█████╗░████████╗░█████╗░\r\n                        ██║░░░░░██╔══██╗██║░░░██║██╔══██╗  ░░░░░██║██╔══██╗╚══██╔══╝██╔══██╗\r\n                        ██║░░░░░███████║╚██╗░██╔╝███████║  ░░░░░██║███████║░░░██║░░░██║░░██║\r\n                        ██║░░░░░██╔══██║░╚████╔╝░██╔══██║  ██╗░░██║██╔══██║░░░██║░░░██║░░██║\r\n                        ███████╗██║░░██║░░╚██╔╝░░██║░░██║  ╚█████╔╝██║░░██║░░░██║░░░╚█████╔╝\r\n                        ╚══════╝╚═╝░░╚═╝░░░╚═╝░░░╚═╝░░╚═╝  ░╚════╝░╚═╝░░╚═╝░░░╚═╝░░░░╚════╝░");
                Console.WriteLine("");
                Console.Write("[1] - Adicionar carro na fila de lavagem\n[2] - Lavar carro da fila de lavagem\n[3] - Retirar carro do pátio\n[4] - Sair do lava jato\n[5] - Ver carros no sistema\nDigite uma opção: ");

                deuCerto = int.TryParse(Console.ReadLine(), out opcao) && opcao > 0 && opcao < 6;
                if (!deuCerto)
                {
                    Console.WriteLine("\nDigite uma opção váilida!");
                    Console.Write("Pressione Enter para continuar");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        deuCerto = false;
                        do
                        {

                            Console.Write("\nQual o ano do carro: ");
                            int anoCarro;
                            deuCerto = int.TryParse(Console.ReadLine(), out anoCarro) && anoCarro > 0;
                            if (!deuCerto)
                            {
                                Console.WriteLine("\nDigite um numero inteiro para ano!");
                                continue;
                            }

                            Console.Write("Qual o nome do carro: ");
                            string nomeCarro = Console.ReadLine();

                            Carro carro = new Carro(++id, nomeCarro, anoCarro);
                            filaCarros.Enqueue(carro);

                            Console.WriteLine($"Carro adicionado na fila - ID({id})");
                        } while (!deuCerto);
                        Console.Write("Pressione Enter para continuar");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 2:
                        if (filaCarros.Count() > 0)
                        {
                            listaPatio.Add(filaCarros.Dequeue());
                            Console.WriteLine("\nCarro lavado e mandado para o Pátio");
                        }
                        else
                            Console.WriteLine("\nSem carros para lavar.");
                        Console.Write("Pressione Enter para continuar");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 3:
                        deuCerto = false;
                        int idCarro;
                        if (listaPatio.Count() > 0)
                        {
                            do
                            {
                                Console.Write("\nQual o ID do carro: ");

                                deuCerto = int.TryParse(Console.ReadLine(), out idCarro);
                                if (!deuCerto)
                                {
                                    Console.WriteLine("\nDigite um numero inteiro!");
                                }
                            } while (!deuCerto);


                            if(Carro.RetirarCarroDoPatio(listaPatio, idCarro))
                                Console.WriteLine("Carro foi retirado com sucesso!");
                            else
                                Console.WriteLine("Carro não encontrado");
                        }
                        else
                            Console.WriteLine("\nNão tem carros no pátio");

                        Console.Write("Pressione Enter para continuar");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 4:
                        Console.Write("\nDigite 1 para sair.\nTecle Enter ou qualquer carácter  para continuar.\nDeseja encerrar o programa: ");//sim ou não
                        string opcaosair = Console.ReadLine();
                        if(opcaosair == "1")
                        {
                            if (Carro.FinalizarPrograma(listaPatio, filaCarros, opcaosair))
                            {
                                Console.Write("Programa encerrado");
                                Console.ReadLine();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Não é possivel encerar pois há carros lavando ou aguardando o dono para a entrega.");
                                Console.Write("Pressione Enter para continuar");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            
                        }
                        Console.Clear();
                        break;
                    case 5: //A desenvolver
                        
                        if (filaCarros.Count() > 0)
                        {
                            Console.WriteLine("\n----- Carros na fila para lavar -----");
                            foreach (var item in filaCarros)
                            {
                                Console.WriteLine(Carro.ImprimirCarros(item));
                            }
                        }
                        if (listaPatio.Count() > 0)
                        {
                            Console.WriteLine("\n----- Carros prontos no pátio -----");
                            foreach (var item in listaPatio)
                            {
                                Console.WriteLine(Carro.ImprimirCarros(item));
                            }
                        }

                        if(filaCarros.Count() == 0 && listaPatio.Count() == 0)
                            Console.WriteLine("Não há nenhum carro no sistema.");

                        Console.Write("Pressione Enter para continuar");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }

            }
        }

    }
}
