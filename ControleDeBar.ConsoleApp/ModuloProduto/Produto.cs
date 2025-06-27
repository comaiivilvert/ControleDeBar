using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class Produto : EntidadeBase<Produto>
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public Produto(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
        }

  
        public override void AtualizarRegistro(Produto registroAtualizado)
        {
            Nome = registroAtualizado.Nome;
            Valor = registroAtualizado.Valor;
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (Nome.Length < 2 || Nome.Length > 100)
                erros += "O campo \"Nome\" deve conter entre 2 e 100 caracteres.";

            if (Decimal.Round(Valor, 2) != Valor)
                erros += "O campo \"Preço\" deve conter 2 casas decimais.";

            return erros;
        }
    }
}
