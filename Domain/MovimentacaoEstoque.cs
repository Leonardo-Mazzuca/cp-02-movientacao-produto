using System;

namespace Domain
{
    public class MovimentacaoEstoque
    {
        public enum TipoMovimentacao
        {
            ENTRADA,
            SAIDA
        }

        public TipoMovimentacao Tipo { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string? Lote { get; set; }
        public DateTime? DataValidade { get; set; }

        public MovimentacaoEstoque() { }

        public MovimentacaoEstoque(
            TipoMovimentacao tipo,
            int quantidade,
            DateTime dataMovimentacao,
            string? lote = null,
            DateTime? dataValidade = null)
        {
            Tipo = tipo;
            Quantidade = quantidade;
            DataMovimentacao = dataMovimentacao;
            Lote = lote;
            DataValidade = dataValidade;
        }

        public void Registrar(Produto produto)
        {

            if (Quantidade <= 0)
                throw new InvalidOperationException("A quantidade deve ser maior que zero.");

            if (produto.Categoria == Produto.CategoriaProduto.PERECIVEL)
            {
                if (string.IsNullOrWhiteSpace(Lote) || DataValidade == null)
                    throw new InvalidOperationException("Produtos perecíveis devem ter lote e data de validade.");
            }

            if (Tipo == TipoMovimentacao.SAIDA)
            {
                if (produto.Quantidade_Estoque < Quantidade)
                    throw new InvalidOperationException("Estoque insuficiente para realizar a saída.");

                produto.AtualizarEstoque(-Quantidade);
            }
            else if (Tipo == TipoMovimentacao.ENTRADA)
            {
                produto.AtualizarEstoque(Quantidade);
            }

            if (produto.AbaixoDaQuantidadeMinima())
            {
                Console.WriteLine($"Produto '{produto.Nome}' está abaixo da quantidade mínima ({produto.Quantidade_Estoque}/{produto.Quantidade_Minima}).");
            }

            Console.WriteLine($"Movimentação registrada: {this}");
        }

        public override string ToString()
        {
            return $"Tipo: {Tipo}, Quantidade: {Quantidade}, Data: {DataMovimentacao:dd/MM/yyyy}" +
                   $"{(Lote != null ? $", Lote: {Lote}" : "")}" +
                   $"{(DataValidade != null ? $", Validade: {DataValidade:dd/MM/yyyy}" : "")}";
        }
    }
}
