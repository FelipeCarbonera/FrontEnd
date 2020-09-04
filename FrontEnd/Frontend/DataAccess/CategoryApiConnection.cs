using Frontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Frontend.DataAccess
{
    public class CategoryApiConnection
    {
        private HttpClient _api = new HttpClient();

        public CategoryApiConnection()
        {
            string path = HttpContext.Current.Server.MapPath("~/");
            var UrlApiCategory = File.ReadAllText(path + @"\..\UrlApiCategory.txt");

            _api.BaseAddress = new Uri(UrlApiCategory);
        }

        // retorna a lista de categorias do db
        public List<Category> GetAllCategories()
        {
            List<Category> categories;

            // chama o metodo GET da api
            var response = _api.GetAsync("category");
            response.Wait();

            // recebe o resultado e verifica se o status do retorno é de sucesso
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                // lê o retorno transformando em uma lista de Categorias
                var read = result.Content.ReadAsAsync<List<Category>>();
                read.Wait();

                categories = read.Result;
            }
            else // caso o retorno seja diferente de sucesso
            {
                // caso seja necessário, pode ser realizado um tratamento específico para cada tipo de retorno, exemplo:
                // switch(result.StatusCode)
                // {
                //     case System.Net.HttpStatusCode.BadRequest:
                //         do something...
                //         break;
                // }

                categories = new List<Category>();
            }

            return categories;
        }

        // retorna um objeto de Categoria específico conforme o id informado
        public Category GetCategoryById(long id)
        {
            Category category;

            //chama o metodo da api que retorna uma categoria passando o id
            var response = _api.GetAsync("category/" + id);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                // converte o retorno em um objeto de Categoria
                var read = result.Content.ReadAsAsync<Category>();
                read.Wait();

                category = read.Result;
            }
            else
            {
                category = new Category();
            }

            return category;
        }

        // realiza o update de uma categoria
        public bool UpdateCategory(Category cat)
        {
            // converte o objeto de categoria em json e depois em stringContent, 
            var json = JsonConvert.SerializeObject(cat);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // chama o metodo put da api, mandando o objeto convertido como parametro
            var response = _api.PutAsync("category", stringContent);
            response.Wait();

            // recebe o retorno da api, (atualmente é somente um booleano, mas pode ser feito o tratamento de cada retorno)
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsAsync<bool>();
                read.Wait();

                return read.Result;
            }

            return false;
        }

        // realiza a criação de uma nova categoria
        public bool CreateCategory(Category cat)
        {
            // convert o objeto para json e depois para stringContent para poder enviar como parametro para a api
            var json = JsonConvert.SerializeObject(cat);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // chama o metodo Post da api e recebe o retorno
            var response = _api.PostAsync("category", stringContent);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsAsync<bool>();
                read.Wait();

                return read.Result;
            }

            return false;
        }

        // realiza a exclusão de uma Categoria
        public void DeleteCategoryById(List<long> ids)
        {
            foreach (var id in ids)
            {
                // chama o metodo Delete da api, passando o id da categoria que deseja ser excluida 
                var response = _api.DeleteAsync("category/" + id);
                response.Wait();

                // recebe o retorno da api
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<bool>();
                    read.Wait();
                }
            }
        }
    }
}