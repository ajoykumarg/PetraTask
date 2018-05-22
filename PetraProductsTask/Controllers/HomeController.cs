using PetraProductsTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetraProductsTask.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Show all the Articles
        /// </summary>
        /// <returns></returns>
        public ActionResult GetArticles()
        {
            using (PetraTaskDBEntities pEntities = new PetraTaskDBEntities())
            {
                var articles = pEntities.Articles.OrderBy(a => a.ArticleName).ToList();
                return Json(new { data = articles }, JsonRequestBehavior.AllowGet);
            }            
        }
        /// <summary>
        /// Get the Article for Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetArticleForEdit(int id)
        {
            using (PetraTaskDBEntities pEntities = new PetraTaskDBEntities())
            {
                var article = pEntities.Articles.Where(a => a.ArticleID==id).FirstOrDefault();
                return View(article);
            }  
        }
        /// <summary>
        /// Action for Article Details
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(string name)
        {
            using (PetraTaskDBEntities pEntities = new PetraTaskDBEntities())
            {
                var article = pEntities.Articles.Where(a => a.ArticleName.ToUpper().Trim() == name.ToUpper().Trim()).FirstOrDefault();
                return View(article);
            }
        }
        /// <summary>
        /// Updated / Edit the Article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateArticle(Article article)
        {
            bool status = false;
            if(ModelState.IsValid)
            {
                using (PetraTaskDBEntities pEntities = new PetraTaskDBEntities())
                {
                    if(article.ArticleID > 0)
                    {
                        var ar = pEntities.Articles.Where(a => a.ArticleID == article.ArticleID).FirstOrDefault();

                        if(ar!=null)
                        {
                            ar.ArticleNumber = article.ArticleNumber;
                            ar.ArticleName = article.ArticleName;
                            ar.Description = article.Description;
                            ar.ArticlePrice = article.ArticlePrice;
                        }
                    }
                    pEntities.SaveChanges();
                    status = true;
                }
            }

            return new JsonResult { Data = new { status = status} };
        }
	}
}