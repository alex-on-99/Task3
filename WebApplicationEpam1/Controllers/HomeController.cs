using LibraryDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace WebApplicationEpam1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CommunicationWithController cwc = new CommunicationWithController();
            ArticlesList articles = new ArticlesList(cwc.GetArticles() as List<Article>);
            articles.List.Sort();
            foreach (var article in articles.List)
            {
                article.Text = article.Text.Length > 200 ? article.Text.Substring(0,200) + "..." : article.Text;
            }

            if (HttpContext.Request.Cookies.AllKeys.Contains("voting"))
            {
                ViewBag.CookieValue = true;
            }
            else
            {
                ViewBag.CookieValue = false;
            }

            return View(articles);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string vote = form["voting"];
            CommunicationWithController cwc = new CommunicationWithController();
            cwc.SetVoting(vote);
            ArticlesList articles = new ArticlesList(cwc.GetArticles() as List<Article>);
            articles.List.Sort();
            foreach (var article in articles.List)
            {
                article.Text = article.Text.Length > 200 ? article.Text.Substring(0,200) + "..." : article.Text;
            }
            var idCookie = new HttpCookie("voting") {  Value = vote };
            Response.Cookies.Add(idCookie);
            ViewBag.CookieValue = true;
            return View(articles);
        }

        public ActionResult Article(int id)
        {
            CommunicationWithController cwc = new CommunicationWithController();
            Article article = cwc.GetArticleById(id);
            List<Tag> tags = cwc.GeTagsById(id);
            StringBuilder sb = new StringBuilder("Tags: ");
            for (int i = 0; i < tags.Count; i++)
            {
                if (i != tags.Count - 1)
                {
                    sb.Append("#" + tags[i].Name + ", ");
                }
                else
                {
                    sb.Append("#" + tags[i].Name + ".");
                }
            }
            ViewData["Tags"] = sb.ToString();
            return View(article);
        }

        public ActionResult Guest()
        {
            CommunicationWithController cwc = new CommunicationWithController();
            ReviewesList reviewes = new ReviewesList(cwc.GetReviews() as List<Review>);
            reviewes.List.Sort();
            return View(reviewes);
        }

        [HttpPost]
        public ActionResult Guest(FormCollection form)
        {
            CommunicationWithController cwc = new CommunicationWithController();
            cwc.CreateReview(form["Name"], form["Text"]);
            ReviewesList reviewes = new ReviewesList(cwc.GetReviews() as List<Review>);
            reviewes.List.Sort();
            return View(reviewes);
        }

        public ActionResult Profile(FormCollection form)
        {
            // if user answers questions in form, other form will be opened
            if (HttpContext.Request.HttpMethod == "POST")
            {
                ViewData["Name"] = form["nameQuestion"];
                ViewData["Age"] = form["ageQuestion"];
                ViewData["Dish"] = form["dishQuestion"];
                ViewData["Fruit"] = form["fruit"];
                ViewData["Vegetable"] = form["vegetable"];
                List<string> badHabits = new List<string>();
                if (form["badHabit1"] != null)
                    badHabits.Add(form["badHabit1"]);
                if (form["badHabit2"] != null)
                    badHabits.Add(form["badHabit2"]);
                if (form["badHabit3"] != null)
                    badHabits.Add(form["badHabit3"]);
                if (badHabits.Count > 0)
                {
                    ViewData["BadHabits"] = badHabits;
                }

                return View("ProfileReady");
            }

            return View();
        }
    }
}