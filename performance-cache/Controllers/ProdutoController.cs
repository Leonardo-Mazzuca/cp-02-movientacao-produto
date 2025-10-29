using Domain;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        // POST /api/produto
        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            try
            {
                var cadastrado = _produtoService.CadastrarProduto(produto);

                if (!cadastrado)
                    return BadRequest(new { message = "Falha ao cadastrar produto. Verifique os dados informados." });

                return Ok(new { message = $"Produto '{produto.Nome}' cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/produto
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var produtos = _produtoService.ObterTodos();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/produto/abaixo-do-minimo
        [HttpGet("abaixo-do-minimo")]
        public IActionResult GetAbaixoDoMinimo()
        {
            try
            {
                var produtos = _produtoService.ObterProdutosAbaixoDoEstoqueMinimo();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
