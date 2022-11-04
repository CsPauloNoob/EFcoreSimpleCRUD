using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Dados;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categorias = _context.Categorias.ToList();
            return View(categorias);
        }


        [HttpPost]
        public async Task<IActionResult> Salvar(Categoria modelo)
        {
                        
            if(modelo.Id == 0)

                _context.Add(modelo);

            else{

                var categoria = _context.Categorias.First(x => x.Id == modelo.Id);
                categoria.Nome = modelo.Nome;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Editar(int id)
        {
            var categoria = _context.Categorias.First(x => x.Id == id);

            return View("Salvar", categoria);
        }


        public async Task<IActionResult> Deletar(int id)
        {
            var categoria = _context.Categorias.First(x => x.Id == id);
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}