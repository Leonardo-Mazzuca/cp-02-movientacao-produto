using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly List<Produto> _produtos;

        public ProdutoRepository()
        {
            _produtos = new List<Produto>();
        }

        public void Adicionar(Produto produto)
        {
            _produtos.Add(produto);
        }

        public Produto? ObterPorCodigo(int codigoSku)
        {
            return _produtos.FirstOrDefault(p => p.Codigo_Sku == codigoSku);
        }

        public List<Produto> ObterTodos()
        {
            return _produtos.ToList();
        }

        public List<Produto> ObterAbaixoDoMinimo()
        {
            return _produtos
                .Where(p => p.AbaixoDaQuantidadeMinima())
                .ToList();
        }
    }
}
