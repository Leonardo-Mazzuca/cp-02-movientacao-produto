using Domain;

namespace Repository
{
    public interface IRelatorioRepository
    {
        IEnumerable<Produto> ObterTodosProdutos();
        IEnumerable<Produto> ObterProdutosPereciveis();
        IEnumerable<Produto> ObterProdutosComEstoqueAbaixoDoMinimo();
    }
}
