using System.Collections.Generic;
using Domain;

namespace Service
{
    public interface IRelatorioService
    {
        double CalcularValorTotalEstoque(IEnumerable<Produto> produtos);
        IEnumerable<Produto> ListarProdutosVencendoEm7Dias(IEnumerable<Produto> produtos);
        IEnumerable<Produto> ListarProdutosAbaixoDoMinimo(IEnumerable<Produto> produtos);
    }
}
