using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa;

public class TelaMesa : TelaBase<Mesa>, ITela
{
    public TelaMesa(RepositorioMesa repositorioMesa) : base("Mesa", repositorioMesa)
    {

    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Mesas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
            "Id", "Número", "Capacidade", "Status"
        );

        Mesa[] mesas = repositorio.SelecionarRegistros();

        for (int i = 0; i < mesas.Length; i++)
        {
            Mesa m = mesas[i];

            if (m == null)
                continue;

            string statusMesa = m.EstaOcupada ? "Ocupada" : "Disponível";

            Console.WriteLine(
              "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
                m.Id, m.Numero, m.Capacidade, statusMesa
            );
        }

        ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    protected override Mesa ObterDados()
    {
        bool conseguiuConverterNumero = false;

        int numero = 0;

        while (!conseguiuConverterNumero)
        {
            Console.Write("Digite o número da mesa: ");
            conseguiuConverterNumero = int.TryParse(Console.ReadLine(), out numero);

            if (!conseguiuConverterNumero)
            {
                ApresentarMensagem("Digite um número válido!", ConsoleColor.DarkYellow);
                Console.Clear();
            }
        }

        bool conseguiuConverterCapacidade = false;

        int capacidade = 0;

        while (!conseguiuConverterCapacidade)
        {
            Console.Write("Digite a capacidade da mesa: ");
            conseguiuConverterCapacidade = int.TryParse(Console.ReadLine(), out capacidade);

            if (!conseguiuConverterNumero)
            {
                ApresentarMensagem("Digite um número válido!", ConsoleColor.DarkYellow);
                Console.Clear();
            }
        }

        return new Mesa(numero, capacidade);
    }

    public override void CadastrarRegistro() 
    {
        ExibirCabecalho();

        Console.WriteLine($"Cadastro de {nomeEntidade}");

        Console.WriteLine();

        Mesa novoRegistro = ObterDados();

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

        Mesa[] registros = repositorio.SelecionarRegistros();

        for (int i = 0; i < registros.Length; i++)
        {
            Mesa mesaRegistrada = registros[i];

            if (mesaRegistrada == null)
                continue;

            if (mesaRegistrada.Numero == novoRegistro.Numero)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Uma mesa com este número já foi cadastrada!");
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