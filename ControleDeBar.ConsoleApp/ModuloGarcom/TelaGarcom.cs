using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloMesa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    internal class TelaGarcom : TelaBase<Garcom>, ITela
    {
        public TelaGarcom(RepositorioGarcom repositorioGarcom) : base("Garçom", repositorioGarcom)

        {

        }

        public override void VisualizarRegistros(bool exibirCabecalho)
        {
            if (exibirCabecalho)
                ExibirCabecalho();

            Console.WriteLine("Visualização de Garçons");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -30} | {2, -30}",
                "Id", "Nome", "CPF"
            );

            Garcom[] garcons = repositorio.SelecionarRegistros();

            for (int i = 0; i < garcons.Length; i++)
            {
                Garcom g = garcons[i];

                if (g == null)
                    continue;


                Console.WriteLine(
                  "{0, -10} | {1, -30} | {2, -30}",
                    g.Id, g.Nome, g.Cpf
                );
            }

            ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        protected override Garcom ObterDados()
        {
            bool conseguiuConverter = false;

            string nome = "";

            while (!conseguiuConverter)
            {
                Console.Write("Digite o nome do garçom: ");
                nome = Console.ReadLine();

                if (string.IsNullOrEmpty(nome) || nome.Length < 3)
                {
                    ApresentarMensagem("Digite um nome válido!", ConsoleColor.DarkYellow);
                    ObterDados();
                }
                else
                    conseguiuConverter = true;
            }

            bool conseguiuConverterCpf = false;

            string cpf = "";

            while (!conseguiuConverterCpf)
            {
                Console.Write("Digite o CPF do garçom: ");
                cpf = Console.ReadLine();

                if (string.IsNullOrEmpty(cpf) || cpf.Length < 14)
                {
                    ApresentarMensagem("Digite um CPF válido!", ConsoleColor.DarkYellow);
                    ObterDados();
                }
                else
                    conseguiuConverterCpf = true;
            }
            return new Garcom(nome, cpf);
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Cadastro de {nomeEntidade}");

            Console.WriteLine();

            Garcom novoRegistro = ObterDados();

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

            Garcom[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Garcom garcomRegistrada = registros[i];

                if (garcomRegistrada == null)
                    continue;

                if (garcomRegistrada.Cpf == novoRegistro.Cpf)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Um Garçom com este CPF já foi cadastrado!");
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

        public override void ExcluirRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Exclusão de {nomeEntidade}");

            Console.WriteLine();

            VisualizarRegistros(false);

            bool conseguiuConverterId = false;

            int idSelecionado = 0;

            while (!conseguiuConverterId)
            {
                Console.Write("Digite o ID do registro que deseja selecionar: ");
                conseguiuConverterId = int.TryParse(Console.ReadLine(), out idSelecionado);

                if (!conseguiuConverterId)
                    ApresentarMensagem("Digite um número válido!", ConsoleColor.DarkYellow);
            }

            Console.WriteLine();

            repositorio.ExcluirRegistro(idSelecionado);

            ApresentarMensagem($"{nomeEntidade} excluído/a com sucesso!", ConsoleColor.Green);

        }
    }
}
