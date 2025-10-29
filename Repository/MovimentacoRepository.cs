
using Domain;

namespace Repository
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly List<MovimentacaoEstoque> _movimentacoes;

        public MovimentacaoRepository()
        {
            _movimentacoes = new List<MovimentacaoEstoque>();
        }

        public void Adicionar(MovimentacaoEstoque movimentacao)
        {
            _movimentacoes.Add(movimentacao);
        }

        public List<MovimentacaoEstoque> ObterTodas()
        {
            return _movimentacoes.ToList();
        }

        public List<MovimentacaoEstoque> ObterPorProduto(Produto produto)
        {
            return _movimentacoes
                .Where(m => m.Lote != null &&
                            m.Lote.Contains(produto.Nome, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
