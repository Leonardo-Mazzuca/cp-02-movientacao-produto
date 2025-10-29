using System;
using System.Collections.Generic;
using Domain;

namespace Service
{
    public interface IMovimentacaoService
    {
        bool RegistrarMovimentacao(Produto produto, MovimentacaoEstoque movimentacao);
        IEnumerable<MovimentacaoEstoque> ListarMovimentacoes();
        IEnumerable<MovimentacaoEstoque> ListarMovimentacoesPorProduto(Produto produto);
    }
}
