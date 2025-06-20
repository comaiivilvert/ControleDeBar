using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    internal class Produto : EntidadeBase<Produto>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public Produto(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }

  
        public override void AtualizarRegistro(Produto registroAtualizado)
        {
            Nome = registroAtualizado.Nome;
            Preco = registroAtualizado.Preco;
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (Nome.Length < 2 || Nome.Length > 100)
                erros += "O campo \"Nome\" deve conter entre 2 e 100 caracteres.";

            if (Decimal.Round(Preco, 2) != Preco)
                erros += "O campo \"Preço\" deve conter 2 casas decimais.";

            return erros;
        }
    }
}
