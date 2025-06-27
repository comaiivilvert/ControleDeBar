using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class TelaProduto : TelaBase<Produto>, ITela
    {
        public TelaProduto(RepositorioProduto repositorioProduto) : base("Produto", repositorioProduto)
        {
        }

        public override void VisualizarRegistros(bool exibirCabecalho)
        {
            if (exibirCabecalho)
                ExibirCabecalho();

            Console.WriteLine("Visualização de Produtos");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -30} | {2, -30}",
                "Id", "Nome", "Preço"
            );

            Produto[] produtos = repositorio.SelecionarRegistros();

            for (int i = 0; i < produtos.Length; i++)
            {
                Produto p = produtos[i];

                if (p == null)
                    continue;


                Console.WriteLine(
                  "{0, -10} | {1, -30} | {2, -30}",
                    p.Id, p.Nome, p.Valor.ToString("C2")
                );
            }

            ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        protected override Produto ObterDados()
        {
            bool conseguiuConverter = false;

            string nome = "";

            while (!conseguiuConverter)
            {
                Console.Write("Digite o nome do produto: ");
                nome = Console.ReadLine();

                if (string.IsNullOrEmpty(nome) || nome.Length < 3)
                {
                    ApresentarMensagem("Digite um produto válido!", ConsoleColor.DarkYellow);
                    ObterDados();
                }
                else
                    conseguiuConverter = true;
            }

            bool conseguiuConverterValor = false;

            decimal preco = 0;

            while (!conseguiuConverterValor)
            {
                Console.Write("Digite o valor do produto: ");
                conseguiuConverterValor = decimal.TryParse(Console.ReadLine(), out preco);

                if (!conseguiuConverterValor)
                {
                    ApresentarMensagem("Digite um valor válido!", ConsoleColor.DarkYellow);
                    ObterDados();
                }
            }

            return new Produto (nome,preco);
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Cadastro de {nomeEntidade}");

            Console.WriteLine();

            Produto novoRegistro = ObterDados();

            string erros = novoRegistro.Validar();

            if (erros.Length > 0)
            {
                ApresentarMensagem(
                    string.Concat(erros, "\nDigite ENTER para continuar..."),
                    ConsoleColor.Red
                );

                CadastrarRegistro();

                return;
            }

            Produto[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Produto produtoRegistrado = registros[i];

                if (produtoRegistrado == null)
                    continue;

                if (produtoRegistrado.Nome == novoRegistro.Nome)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Um produto com este nome já foi cadastrado!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    CadastrarRegistro();
                    return;
                }
            }

            repositorio.CadastrarRegistro(novoRegistro);

            ApresentarMensagem($"{nomeEntidade} cadastrado/a com sucesso!", ConsoleColor.Green);
        }

    }
}
