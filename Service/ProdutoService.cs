using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Repository;

namespace Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public bool CadastrarProduto(Produto produto)
        {
            if (!ValidarProdutoPorCategoria(produto, out string mensagemErro))
            {
                Console.WriteLine($"Erro ao cadastrar produto: {mensagemErro}");
                return false;
            }

            produto.Codigo_Sku = _produtoRepository.ObterTodos().Count + 1;
            produto.Data_Criacao = DateTime.Now;

            _produtoRepository.Adicionar(produto);

            Console.WriteLine($"Produto '{produto.Nome}' cadastrado com sucesso!");
            return true;
        }

        public bool ValidarProdutoPorCategoria(Produto produto, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                mensagemErro = "O nome do produto é obrigatório.";
                return false;
            }

            if (produto.Preco_Unitario <= 0)
            {
                mensagemErro = "O preço unitário deve ser maior que zero.";
                return false;
            }

            if (produto.Quantidade_Minima < 0)
            {
                mensagemErro = "A quantidade mínima não pode ser negativa.";
                return false;
            }

            if (produto.Categoria == Produto.CategoriaProduto.PERECIVEL && produto.Quantidade_Minima == 0)
            {
                mensagemErro = "Produtos perecíveis devem ter uma quantidade mínima maior que zero.";
                return false;
            }

            return true;
        }

        public List<Produto> ObterProdutosAbaixoDoEstoqueMinimo()
        {
            return _produtoRepository.ObterAbaixoDoMinimo();
        }

        public void ListarProdutos()
        {
            var produtos = _produtoRepository.ObterTodos();

            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            Console.WriteLine("📦 Lista de Produtos:");
            foreach (var p in produtos)
            {
                Console.WriteLine(p);
            }
        }
    }
}
