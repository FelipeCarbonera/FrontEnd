using Frontend.DataAccess;
using Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private ProductApiConnection _prodApi = new ProductApiConnection();

        /// <summary>
        /// Retorna a View Index, página inicial do Site, com a lista de todas os Produtos cadastrados
        /// </summary>
        /// <param>Sem parâmetro</param>
        public ActionResult Index()
        {
            return View(_prodApi.GetAllProducts());
        }
    }
}