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
    public class ProductApiConnection
    {
        private CategoryApiConnection _catApi = new CategoryApiConnection();

        private HttpClient _api = new HttpClient();

        public ProductApiConnection()
        {
            string path = HttpContext.Current.Server.MapPath("~/");
            string UrlApiProduct = File.ReadAllText(path + @"\..\UrlApiProduct.txt");

            _api.BaseAddress = new Uri(UrlApiProduct);
        }

        // retorna uma lista com todos produtos
        public List<Product> GetAllProducts()
        {
            List<Product> products;

            // chama o metodo Get da api
            var response = _api.GetAsync("product");
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                // converte o retorno da api em uma lista de produtos
                var read = result.Content.ReadAsAsync<List<Product>>();
                read.Wait();

                products = read.Result;

                // para cada produto na lista, chama a api de categoria para popular os objetos com suas respectivas categorias
                products.ForEach(p => p.Category = _catApi.GetCategoryById(p.ID_Category));
            }
            else
            {
                // como na classe de categoria, nao esta sendo feito o tratamento para cada status de retorno.
                products = new List<Product>(); ;
            }



            return products;
        }

        // retorno um objeto de Produto de acordo com o id informado
        public Product GetProductById(long id)
        {
            Product product;

            // chama o metodo Get da api, passando um id de produto como parametro
            var response = _api.GetAsync("product/" + id);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                // converto o retorno da api em um objeto de Produto
                var read = result.Content.ReadAsAsync<Product>();
                read.Wait();

                product = read.Result;

                // chama a api de categoria para popular a categoria do produto
                product.Category = _catApi.GetCategoryById(product.ID_Category);
            }
            else
            {
                product = new Product();
            }

            return product;
        }

        // realiza o update de um produto
        public bool UpdateProduct(Product prod)
        {
            // converte o objeto de produto em json e depois em stringContent para poder ser utilizado como parametro
            var json = JsonConvert.SerializeObject(prod);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // chama o metodo Put da api, passando o objeto de produto como paramentro para ser atualizado
            var response = _api.PutAsync("product", stringContent);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                // converto o retorno da api para booleano
                var read = result.Content.ReadAsAsync<bool>();
                read.Wait();

                return read.Result;
            }

            return false;
        }

        // cria um novo produto
        public bool CreateProduct(Product prod)
        {
            // converte o objeto de produto em json e depois em stringContent para poder ser utilizado como parametro
            var json = JsonConvert.SerializeObject(prod);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // chama o metodo Post da api, passando o objeto de produto como paramentro para ser atualizado
            var response = _api.PostAsync("product", stringContent);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                // converto o retorno da api para booleano
                var read = result.Content.ReadAsAsync<bool>();
                read.Wait();

                return read.Result;
            }

            return false;
        }

        // realiza a exclusão de um produto
        public bool DeleteProductById(long id)
        {
            // chama o metodo Delete da api, passando o id do produto que se deseja excluir
            var response = _api.DeleteAsync("product/" + id);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                // converto o retorno da api em booleano
                var read = result.Content.ReadAsAsync<bool>();
                read.Wait();

                return read.Result;
            }

            return false;
        }
    }
}