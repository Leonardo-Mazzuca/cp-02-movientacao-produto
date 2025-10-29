using Microsoft.AspNetCore.Mvc;
using Service;
using Repository;
using Domain;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoService _movimentacaoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public MovimentacaoController(
            IMovimentacaoService movimentacaoService,
            IProdutoRepository produtoRepository,
            IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoService = movimentacaoService;
            _produtoRepository = produtoRepository;
            _movimentacaoRepository = movimentacaoRepository;
        }

        [HttpPost("{codigoProduto}")]
        public IActionResult RegistrarMovimentacao(int codigoProduto, [FromBody] MovimentacaoEstoque movimentacao)
        {
            if (movimentacao == null)
                return BadRequest(new { mensagem = "Os dados da movimentação são obrigatórios." });

            var produto = _produtoRepository.ObterPorCodigo(codigoProduto);
            if (produto == null)
                return NotFound(new { mensagem = "Produto não encontrado." });

            if (movimentacao.Quantidade <= 0)
                return BadRequest(new { mensagem = "A quantidade deve ser maior que zero." });

            if (produto.Categoria == Produto.CategoriaProduto.PERECIVEL)
            {
                if (string.IsNullOrWhiteSpace(movimentacao.Lote) || movimentacao.DataValidade == null)
                    return BadRequest(new { mensagem = "Produtos perecíveis devem conter lote e data de validade." });

                if (movimentacao.DataValidade <= DateTime.Now)
                    return BadRequest(new { mensagem = "Produto perecível com validade expirada não pode ser movimentado." });
            }

            if (movimentacao.Tipo == MovimentacaoEstoque.TipoMovimentacao.SAIDA &&
                produto.Quantidade_Estoque < movimentacao.Quantidade)
            {
                return BadRequest(new
                {
                    mensagem = $"Estoque insuficiente. O produto '{produto.Nome}' possui apenas {produto.Quantidade_Estoque} unidades."
                });
            }

            if (movimentacao.Tipo == MovimentacaoEstoque.TipoMovimentacao.ENTRADA)
                produto.AtualizarEstoque(movimentacao.Quantidade);
            else if (movimentacao.Tipo == MovimentacaoEstoque.TipoMovimentacao.SAIDA)
                produto.AtualizarEstoque(-movimentacao.Quantidade);

            movimentacao.DataMovimentacao = DateTime.Now;
            _movimentacaoRepository.Adicionar(movimentacao);

            string alerta = produto.AbaixoDaQuantidadeMinima()
                ? $"Atenção: Produto '{produto.Nome}' está abaixo da quantidade mínima ({produto.Quantidade_Estoque}/{produto.Quantidade_Minima})."
                : "Movimentação concluída com sucesso.";

            return Ok(new
            {
                mensagem = alerta,
                produto = new
                {
                    produto.Nome,
                    produto.Categoria,
                    produto.Quantidade_Estoque,
                    produto.Quantidade_Minima,
                    produto.Preco_Unitario
                },
                movimentacao = new
                {
                    movimentacao.Tipo,
                    movimentacao.Quantidade,
                    movimentacao.Lote,
                    movimentacao.DataValidade,
                    movimentacao.DataMovimentacao
                }
            });
        }

        [HttpGet]
        public IActionResult ListarMovimentacoes()
        {
            var movimentacoes = _movimentacaoRepository.ObterTodas();
            return Ok(movimentacoes);
        }

        [HttpGet("produto/{codigoProduto}")]
        public IActionResult ListarPorProduto(int codigoProduto)
        {
            var produto = _produtoRepository.ObterPorCodigo(codigoProduto);
            if (produto == null)
                return NotFound(new { mensagem = "Produto não encontrado." });

            var movimentacoes = _movimentacaoRepository.ObterPorProduto(produto);
            if (!movimentacoes.Any())
                return Ok(new { mensagem = "Nenhuma movimentação encontrada para este produto." });

            return Ok(movimentacoes);
        }
    }
}
