using System.Collections.Generic;
using Domain;

namespace Repository
{
    public interface IProdutoRepository
    {
        void Adicionar(Produto produto);
        Produto? ObterPorCodigo(int codigoSku);
        List<Produto> ObterTodos();
        List<Produto> ObterAbaixoDoMinimo();
    }
}
