using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class Garcom : EntidadeBase<Garcom>
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Garcom(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }


        public override void AtualizarRegistro(Garcom registroAtualizado)
        {
            Nome = registroAtualizado.Nome;
            Cpf = registroAtualizado.Cpf;
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (Nome.Length < 3 || Nome.Length > 100)
                erros += "O campo \"Nome\" deve conter entre 3 e 100 caracteres.";

            if (Cpf.Length < 14)
                erros += "O campo \"CPF\" deve conter 14 digitos (incluindo pontos e hifen).";

            return erros;
        }
    }
}
