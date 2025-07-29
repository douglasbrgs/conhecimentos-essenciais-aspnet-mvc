using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppSemTemplate.Data;
using AppSemTemplate.Models;
using Microsoft.AspNetCore.Authorization;
using AppSemTemplate.Extensions;

namespace AppSemTemplate.Controllers
{
    [Authorize]
    [Route("meus-produtos")]
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [ClaimsAuthorize("Produtos", "VI")]
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User.Identity;

            return View(await _context.Produtos.ToListAsync());
        }

        [ClaimsAuthorize("Produtos", "VI")]
        [Route("detalhar/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [ClaimsAuthorize("Produtos", "AD")]
        [Route("criar-novo")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Produtos", "AD")]
        [HttpPost("criar-novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Imagem,Valor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [ClaimsAuthorize("Produtos", "ED")]
        [Route("editar/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [ClaimsAuthorize("Produtos", "ED")]
        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Imagem,Valor")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [ClaimsAuthorize("Produtos", "EX")]
        [Route("excluir/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [ClaimsAuthorize("Produtos", "EX")]
        [HttpPost("excluir/{id:int}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
