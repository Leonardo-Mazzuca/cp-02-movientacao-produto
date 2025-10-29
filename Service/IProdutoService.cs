using System.Collections.Generic;
using Domain;

namespace Service
{
    public interface IProdutoService
    {
        bool CadastrarProduto(Produto produto);
        bool ValidarProdutoPorCategoria(Produto produto, out string mensagemErro);
        List<Produto> ObterProdutosAbaixoDoEstoqueMinimo();

        List<Produto> ListarProdutos();
    }
}
