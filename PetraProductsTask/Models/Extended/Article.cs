using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PetraProductsTask.Models
{
    [MetadataType(typeof(ArticleMetaData))]
    public partial class Article
    {

    }

    public class ArticleMetaData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vänligen ange artikelnummer")]
        public string ArticleNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vänligen ange artikelnamn")]
        public string ArticleName { get; set; }

        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}", ArticleNumber, ArticleName);

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}