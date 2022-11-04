using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dados;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly ApplicationDbContext _context;
        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var produtos = _context.Produtos.Include(p => p.Categoria)
            .OrderBy(p => p.Nome);

            if(!produtos.Any())
            return View(new List<Produto>());

            return View(produtos.ToList());
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            ViewBag.Categorias = _context.Categorias.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Produto modelo)
        {
            if(modelo.Id == 0)
                _context.Produtos.Add(modelo);
            else
                {
                    var ProdutoJaSalvo = _context.Produtos.First(p => p.Id == modelo.Id);

                    ProdutoJaSalvo.Nome = modelo.Nome;
                    ProdutoJaSalvo.CategoriaId = modelo.CategoriaId;
                }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Deletar(Produto modelo)
        {
            _context.Produtos.Remove(modelo);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var produto = _context.Produtos.First(p => p.Id == id);
            ViewBag.Categorias = _context.Categorias.ToList();

            return View("Salvar", produto);
        }
    }
}