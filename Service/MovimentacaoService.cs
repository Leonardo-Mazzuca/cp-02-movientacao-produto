using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Repository;

namespace Service
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
        }

        public bool RegistrarMovimentacao(Produto produto, MovimentacaoEstoque movimentacao)
        {
            try
            {
                if (movimentacao.Quantidade <= 0)
                {
                    Console.WriteLine("Quantidade deve ser maior que zero.");
                    return false;
                }

                if (produto.Categoria == Produto.CategoriaProduto.PERECIVEL)
                {
                    if (movimentacao.DataValidade == null || string.IsNullOrWhiteSpace(movimentacao.Lote))
                    {
                        Console.WriteLine("Produtos perecíveis devem ter lote e data de validade informados.");
                        return false;
                    }

                    if (movimentacao.DataValidade <= DateTime.Now)
                    {
                        Console.WriteLine("Data de validade inválida (produto já vencido).");
                        return false;
                    }
                }

                if (movimentacao.Tipo == MovimentacaoEstoque.TipoMovimentacao.SAIDA &&
                    produto.Quantidade_Estoque < movimentacao.Quantidade)
                {
                    Console.WriteLine($"Estoque insuficiente. Produto '{produto.Nome}' tem apenas {produto.Quantidade_Estoque} unidades.");
                    return false;
                }

                if (movimentacao.Tipo == MovimentacaoEstoque.TipoMovimentacao.ENTRADA)
                {
                    produto.AtualizarEstoque(movimentacao.Quantidade);
                    Console.WriteLine($"Entrada registrada: +{movimentacao.Quantidade} no produto '{produto.Nome}'.");
                }
                else if (movimentacao.Tipo == MovimentacaoEstoque.TipoMovimentacao.SAIDA)
                {
                    produto.AtualizarEstoque(-movimentacao.Quantidade);
                    Console.WriteLine($"Saída registrada: -{movimentacao.Quantidade} do produto '{produto.Nome}'.");
                }

  
                movimentacao.DataMovimentacao = DateTime.Now;
                _movimentacaoRepository.Adicionar(movimentacao);

                if (produto.AbaixoDaQuantidadeMinima())
                {
                    Console.WriteLine($"Atenção: Produto '{produto.Nome}' está abaixo da quantidade mínima!");
                }

                Console.WriteLine($"Movimentação concluída: {movimentacao}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar movimentação: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<MovimentacaoEstoque> ListarMovimentacoes()
        {
            return _movimentacaoRepository.ObterTodas();
        }

        public IEnumerable<MovimentacaoEstoque> ListarMovimentacoesPorProduto(Produto produto)
        {
            return _movimentacaoRepository.ObterPorProduto(produto);
        }
    }
}
