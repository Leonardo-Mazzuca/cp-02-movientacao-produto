using Microsoft.AspNetCore.Mvc;
using Service;
using Repository;
using Domain;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;
        private readonly IProdutoRepository _produtoRepository;

        public RelatorioController(IRelatorioService relatorioService, IProdutoRepository produtoRepository)
        {
            _relatorioService = relatorioService;
            _produtoRepository = produtoRepository;
        }

        // GET /api/relatorio/valor-total-estoque
        [HttpGet("valor-total-estoque")]
        public IActionResult CalcularValorTotalEstoque()
        {
            var produtos = _produtoRepository.ObterTodos();
            var valorTotal = _relatorioService.CalcularValorTotalEstoque(produtos);
            return Ok(new { ValorTotalEstoque = valorTotal });
        }

        // GET /api/relatorio/vencendo-em-7-dias
        [HttpGet("vencendo-em-7-dias")]
        public IActionResult ListarProdutosVencendoEm7Dias()
        {
            var produtos = _produtoRepository.ObterTodos();
            var lista = _relatorioService.ListarProdutosVencendoEm7Dias(produtos);
            return Ok(lista);
        }

        // GET /api/relatorio/abaixo-minimo
        [HttpGet("abaixo-minimo")]
        public IActionResult ListarProdutosAbaixoDoMinimo()
        {
            var produtos = _produtoRepository.ObterTodos();
            var lista = _relatorioService.ListarProdutosAbaixoDoMinimo(produtos);

            if (!lista.Any())
                return Ok(new { mensagem = "Nenhum produto abaixo da quantidade mínima." });

            return Ok(lista);
        }
    }
}
