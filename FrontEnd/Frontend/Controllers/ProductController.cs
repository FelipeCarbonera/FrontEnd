using Frontend.DataAccess;
using Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class ProductController : Controller
    {
        private ProductApiConnection _prodApi = new ProductApiConnection();
        private CategoryApiConnection _catApi = new CategoryApiConnection();


        /// <summary>
        /// Retorna a View Index com a lista de todas os Produtos cadastrados.
        /// Realiza a verificação se contem Produtos cadastrados, caso não tenha, verifica se possui Categorias cadastradas
        /// Pois é necessário possuir pelo menos uma Categoria cadastrada para poder cadastrar um Produto
        /// </summary>
        /// <param>Sem parâmetro</param>
        public ActionResult Index()
        {
            var products = _prodApi.GetAllProducts();

            if (products.Count == 0)
                ViewBag.ContainsCategories = _catApi.GetAllCategories().Count > 0 ? true : false;

            return View(products);
        }

        /// <summary>
        /// Retorna uma PartialView, para poder ser utilizada no model do bootstrap com o objeto de Produto para edição
        /// Envia pela 'ViewBag.Categories' a lista de Categorias cadastradas, para poder inserir um novo Produto
        /// </summary>
        /// <param name="id">O Id do Produto para edição</param>
        public ActionResult Edit(long id)
        {
            var prod = _prodApi.GetProductById(id);
            ViewBag.Categories = _catApi.GetAllCategories();

            return PartialView(prod);
        }

        /// <summary>
        /// Método que recebe o objeto de Produto editado para atualizar
        /// O método redireciona para a View Index
        /// </summary>
        /// <param name="newProd">O objeto de Produto para ser atualizado</param>
        [HttpPost]
        public ActionResult SaveEdit(Product newProd)
        {
            var response = _prodApi.UpdateProduct(newProd);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Retorna a View de criação, podendo ser como uma Partial View
        /// Envia pela 'ViewBag.Categories' a lista de Categorias cadastradas, para poder inserir um novo Produto
        /// </summary>
        /// <param name="partial">Booleano que indica se a View sera retornada como Partial, para ser utilizada pelo modal do Bootstrap</param>
        public ActionResult Create(bool partial = false)
        {
            ViewBag.Modal = false;
            ViewBag.Categories = _catApi.GetAllCategories();

            if (partial)
            {
                ViewBag.Modal = true;
                return PartialView();
            }

            return View();
        }

        /// <summary>
        /// Recebe um objeto de Produto para ser adicionado no DB
        /// O método redireciona para a View Index
        /// </summary>
        /// <param name="prod">O objeto de Produto para ser adicionado</param>
        [HttpPost]
        public ActionResult SaveCreate(Product prod)
        {
            var response = _prodApi.CreateProduct(prod);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Recebe uma lista de Ids de Produtos escolhidos para exclusão
        /// O método redireciona para uma View para confirmar a exclusão dos Produtos escolhidos
        /// </summary>
        /// <param name="collection">Contém a lista de Ids de Produtos selecionados para exclusão</param>
        public ActionResult Delete(FormCollection collection)
        {
            if (collection["ids-product-delete"] == null)
                return RedirectToAction("Index");

            var idsProductsDelete = collection["ids-product-delete"].Split(',');

            List<Product> products = new List<Product>();

            foreach (var id in idsProductsDelete)
                products.Add(_prodApi.GetProductById(Convert.ToInt64(id)));

            return View(products);
        }

        /// <summary>
        /// Recebe uma lista de Ids de Produtos CONFIRMADOS para exclusão
        /// O método redireciona para a View Index
        /// </summary>
        /// <param name="ids">Contém a lista de Ids de Produtos CONFIRMADOS para exclusão</param>
        [HttpPost]
        public ActionResult ConfirmDelete(List<long> ids)
        {
            ids.ForEach(id => _prodApi.DeleteProductById(id));
            return RedirectToAction("Index");
        }
    }
}