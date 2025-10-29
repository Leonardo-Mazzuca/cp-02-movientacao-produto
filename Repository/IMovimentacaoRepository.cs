using System.Collections.Generic;
using Domain;

namespace Repository
{
    public interface IMovimentacaoRepository
    {
        void Adicionar(MovimentacaoEstoque movimentacao);
        List<MovimentacaoEstoque> ObterTodas();
        List<MovimentacaoEstoque> ObterPorProduto(Produto produto);
    }
}
