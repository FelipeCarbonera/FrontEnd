using Frontend.DataAccess;
using Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryApiConnection _categoryApi = new CategoryApiConnection();
        
        /// <summary>
        /// Retorna a View Index das categorias, com a lista de todas as Categorias
        /// </summary>
        /// <param>Sem parâmetro</param>
        public ActionResult Index()
        {
            var categories = _categoryApi.GetAllCategories();
            return View(categories);
        }

        /// <summary>
        /// Retorna uma PartialView, para poder ser utilizada no model do bootstrap com o objeto de Categoria para edição
        /// </summary>
        /// <param name="id">O Id da Categoria para edição</param>
        public ActionResult Edit(long id)
        {
            var cat = _categoryApi.GetCategoryById(id);
            return PartialView(cat);
        }
        
        /// <summary>
        /// Método que recebe o objeto de Categoria editado para atualizar
        /// O método redireciona para a View Index
        /// </summary>
        /// <param name="newCat">O objeto de Categoria para ser atualizado</param>
        [HttpPost]
        public ActionResult SaveEdit(Category newCat)
        {
            var response = _categoryApi.UpdateCategory(newCat);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Retorna a View de criação, podendo ser como uma Partial View
        /// </summary>
        /// <param name="partial">Booleano que indica se a View sera retornada como Partial, para ser utilizada pelo modal do Bootstrap</param>
        public ActionResult Create(bool partial = false)
        {
            ViewBag.Modal = false;
            if (partial)
            {
                ViewBag.Modal = true;
                return PartialView();
            }

            return View();
        }

        /// <summary>
        /// Recebe um objeto de Categoria para ser adicionado no DB
        /// O método redireciona para a View Index
        /// </summary>
        /// <param name="cat">O objeto de Categoria para ser adicionado</param>
        [HttpPost]
        public ActionResult SaveCreate(Category cat)
        {
            var response = _categoryApi.CreateCategory(cat);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Recebe uma lista de Ids de Categorias escolhidos para exclusão
        /// O método redireciona para uma View para confirmar a exclusão das Categorias escolhidas
        /// </summary>
        /// <param name="collection">Contém a lista de Ids de Categorias selecionados para exclusão</param>
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            var idsCategoryDelete = collection["ids-category-delete"].Split(',');
            List<Category> categories = new List<Category>();

            foreach (var id in idsCategoryDelete)
            {
                categories.Add(_categoryApi.GetCategoryById(Convert.ToInt64(id)));
            }

            return View(categories);
        }

        /// <summary>
        /// Recebe uma lista de Ids de Categorias CONFIRMADOS para exclusão
        /// O método redireciona para a View Index
        /// </summary>
        /// <param name="ids">Contém a lista de Ids de Categorias CONFIRMADOS para exclusão</param>
        [HttpPost]
        public ActionResult ConfirmDelete(List<long> ids)
        {
            _categoryApi.DeleteCategoryById(ids);
            return RedirectToAction("Index");
        }
    }
}