using System;

namespace Domain
{
    public class Produto
    {
        public int Codigo_Sku { get; set; }
        public string Nome { get; set; } = string.Empty;

        public enum CategoriaProduto
        {
            PERECIVEL,
            NAO_PERECIVEL
        }

        public CategoriaProduto Categoria { get; set; }
        public double Preco_Unitario { get; set; }
        public int Quantidade_Minima { get; set; }
        public int Quantidade_Estoque { get; set; } = 0;
        public DateTime? DataValidade { get; set; }

        public DateTime Data_Criacao { get; set; } = DateTime.Now;

        public void AtualizarEstoque(int quantidade)
        {
            Quantidade_Estoque += quantidade;
        }

        public bool AbaixoDaQuantidadeMinima()
        {
            return Quantidade_Estoque < Quantidade_Minima;
        }

        public override string ToString()
        {
            return $"{Nome} (SKU: {Codigo_Sku}) - Estoque: {Quantidade_Estoque}, Mínimo: {Quantidade_Minima}";
        }
    }
}
