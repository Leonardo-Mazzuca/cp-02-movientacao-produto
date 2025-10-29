using Domain;

namespace Repository
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly List<Produto> _produtos;
        public RelatorioRepository()
        {
            _produtos = new List<Produto>
            {
                new Produto { Nome = "Leite", Categoria = Produto.CategoriaProduto.PERECIVEL, Quantidade_Estoque = 10, Quantidade_Minima = 15, Preco_Unitario = 5.50, DataValidade = DateTime.Now.AddDays(5) },
                new Produto { Nome = "Arroz", Categoria = Produto.CategoriaProduto.NAO_PERECIVEL, Quantidade_Estoque = 50, Quantidade_Minima = 20, Preco_Unitario = 7.99, DataValidade = null },
                new Produto { Nome = "Queijo", Categoria = Produto.CategoriaProduto.PERECIVEL, Quantidade_Estoque = 3, Quantidade_Minima = 10, Preco_Unitario = 12.00, DataValidade = DateTime.Now.AddDays(3) }
            };
        }

        public IEnumerable<Produto> ObterTodosProdutos()
        {
            return _produtos;
        }

        public IEnumerable<Produto> ObterProdutosPereciveis()
        {
            return _produtos.Where(p => p.Categoria == Produto.CategoriaProduto.PERECIVEL);
        }

        public IEnumerable<Produto> ObterProdutosComEstoqueAbaixoDoMinimo()
        {
            return _produtos.Where(p => p.AbaixoDaQuantidadeMinima());
        }
    }
}
