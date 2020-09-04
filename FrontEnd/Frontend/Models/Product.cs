using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Frontend.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "URL Imagem")]
        public string UrlImage { get; set; }
        
        [Display(Name = "Data Criação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Required]
        public long ID_Category { get; set; }

        [JsonIgnore]
        [Display(Name = "Categoria")]
        public virtual Category Category { get; set; }
    }
}