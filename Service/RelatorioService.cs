
using Domain;

namespace Service
{
    public class RelatorioService : IRelatorioService
    {
        public double CalcularValorTotalEstoque(IEnumerable<Produto> produtos)
        {
            if (produtos == null || !produtos.Any())
                return 0;

            return produtos.Sum(p => p.Quantidade_Estoque * p.Preco_Unitario);
        }

        public IEnumerable<Produto> ListarProdutosVencendoEm7Dias(IEnumerable<Produto> produtos)
        {
            if (produtos == null)
                return new List<Produto>();

            DateTime hoje = DateTime.Now;
            DateTime limite = hoje.AddDays(7);

            return produtos
                .Where(p => p.Categoria == Produto.CategoriaProduto.PERECIVEL)
                .Where(p => p.DataValidade != null && p.DataValidade <= limite && p.DataValidade > hoje)
                .ToList();
        }

        public IEnumerable<Produto> ListarProdutosAbaixoDoMinimo(IEnumerable<Produto> produtos)
        {
            if (produtos == null)
                return new List<Produto>();

            return produtos.Where(p => p.AbaixoDaQuantidadeMinima()).ToList();
        }
    }
}
