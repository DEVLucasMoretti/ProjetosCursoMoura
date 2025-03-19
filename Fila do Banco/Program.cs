using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fila
{
    class Program
    {
        static void Main(string[] args)
        {
            /* * Controlar uma fila de um banco com no máximo 5 posições(Array - inteiros)
               * 4 opções
               * 1 - Pegar senha(sequencial - valor inicial da senha é 1)
               * 2 - Chamar senha(chamar na sequência)
               * 3 - Imprimir a fila
               * 4 - Encerrar o programa(se a fila estiver vazia) */

            const int MAXIMODEPESSOASNAFILA = 5;
            int[] fila = new int[MAXIMODEPESSOASNAFILA];
            int indicePosicaoAtual = 0, indiceChamaSenha = 0, indiceAuxiliar = 0, senhaAtual = 0, opcaoEscolhida = 0;
            bool deuCerto = false, encerrarPrograma = false;

            Console.WriteLine("\r\n                  ███████╗██╗██╗░░░░░░█████╗░  ██████╗░░█████╗░  ██████╗░░█████╗░███╗░░██╗░█████╗░░█████╗░\r\n                  ██╔════╝██║██║░░░░░██╔══██╗  ██╔══██╗██╔══██╗  ██╔══██╗██╔══██╗████╗░██║██╔══██╗██╔══██╗\r\n                  █████╗░░██║██║░░░░░███████║  ██║░░██║██║░░██║  ██████╦╝███████║██╔██╗██║██║░░╚═╝██║░░██║\r\n                  ██╔══╝░░██║██║░░░░░██╔══██║  ██║░░██║██║░░██║  ██╔══██╗██╔══██║██║╚████║██║░░██╗██║░░██║\r\n                  ██║░░░░░██║███████╗██║░░██║  ██████╔╝╚█████╔╝  ██████╦╝██║░░██║██║░╚███║╚█████╔╝╚█████╔╝\r\n                  ╚═╝░░░░░╚═╝╚══════╝╚═╝░░╚═╝  ╚═════╝░░╚════╝░  ╚═════╝░╚═╝░░╚═╝╚═╝░░╚══╝░╚════╝░░╚════╝░");

            while (!encerrarPrograma)
            {
                do
                {
                    Console.Write("\nOpções: \n1 - Pegar senha\n2 - Chamar senha\n3 - Imprimir a fila\n4 - Encerrar o programa\nEscolha uma opção: ");

                    deuCerto = int.TryParse(Console.ReadLine(), out opcaoEscolhida);
                    if (!deuCerto || opcaoEscolhida > 4 || opcaoEscolhida < 1)
                    {
                        Console.WriteLine("\nDigite uma opção váilida!");
                        deuCerto = false;
                    }
                }
                while (!deuCerto);

                switch (opcaoEscolhida)
                {
                    case 1:
                        if (fila[indicePosicaoAtual] == 0)
                        {
                            fila[indicePosicaoAtual] = ++senhaAtual;
                            Console.WriteLine($"Sua senha é a {fila[indicePosicaoAtual]}.");
                            indicePosicaoAtual++;
                        }
                        else
                            Console.WriteLine("\n_______ A fila está cheia! _______");
                        if (indicePosicaoAtual > 4)
                            indicePosicaoAtual = 0;
                        break;
                    case 2:
                        if (fila[indiceChamaSenha] != 0)
                        {
                            Console.WriteLine($"Senha chamada é a {fila[indiceChamaSenha]}.");
                            fila[indiceChamaSenha] = 0;
                            indiceChamaSenha++;
                        }
                        else
                            Console.WriteLine("\n_______ A fila está vazia! _______");
                        if (indiceChamaSenha > 4)
                            indiceChamaSenha = 0;
                        break;
                    case 3:
                        Console.WriteLine("\nSituação da fila:");
                        indiceAuxiliar = indiceChamaSenha;
                        int contadorNumeroFila = 0;

                        for (int i = 0; i < MAXIMODEPESSOASNAFILA; i++)
                        {
                            if (fila[indiceAuxiliar] != 0)
                                Console.WriteLine($" {++contadorNumeroFila}º senha da fila a ser chamada: {fila[indiceAuxiliar]}");
                            indiceAuxiliar++;
                            if (indiceAuxiliar > 4)
                                indiceAuxiliar = 0;
                        }
                        if (contadorNumeroFila == 0)
                            Console.WriteLine("A fila está vazia!");
                        break;
                    case 4:
                        indiceAuxiliar = indicePosicaoAtual;
                        if (indiceAuxiliar == 0)
                            indiceAuxiliar = MAXIMODEPESSOASNAFILA;
                        if (fila[--indiceAuxiliar] == 0)
                        {
                            Console.WriteLine("\n_______ Programa finalizado _______");
                            encerrarPrograma = true;
                        }
                        else
                            Console.WriteLine("\nO programa não pode ser encerrado, pois há pessoas na fila ainda.");
                        break;
                }
                #region "Teste de Mesa"
                Console.WriteLine($"[{fila[0]}] [{fila[1]}] [{fila[2]}] [{fila[3]}] [{fila[4]}] ");
                #endregion
            }
        }
    }
}
