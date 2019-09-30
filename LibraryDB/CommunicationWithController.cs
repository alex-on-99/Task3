using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDB
{
    public class CommunicationWithController : IArticleCommunication, IReviewCommunication
    {
        // Method gets all articles from database
        public ICollection<Article> GetArticles()
        {
            using (var ctx = new BlogContext("BlogContext"))
            {
                ICollection<Article> articles = ctx.Articles.ToList();
                return articles;
            }
        }

        public Article GetArticleById(int id)
        {
            using (var ctx = new BlogContext("BlogContext"))
            {
                return ctx.Articles.First(art => art.Id == id);
            }
        }

        // Method gets all reviews from database
        public ICollection<Review> GetReviews()
        {
            using (var ctx = new BlogContext("BlogContext"))
            {
                List<Review> reviews = ctx.Reviewes.ToList();
                return reviews;
            }
        }

        // Add new review in database
        public void CreateReview(string name, string text)
        {
            using (var ctx = new BlogContext("BlogContext"))
            {
                ctx.Reviewes.Add(
                    new Review
                    {
                        Name = name,
                        Text = text,
                        Date = DateTime.Now
                    });
                ctx.SaveChanges();
            }
        }

        public List<Tag> GeTagsById(int id)
        {
            using (var ctx = new BlogContext("BlogContext"))
            {
                List<ArticleTag> articleTags = ctx.Articles.First(art => art.Id == id).ArticleTags.ToList();
                List<Tag> tags = new List<Tag>();
                foreach(var articleTag in articleTags)
                {
                    tags.Add(articleTag.Tag);
                }
                return tags;
            }
        }

        public void SetVoting(string str)
        {
            using (var ctx = new BlogContext("BlogContext"))
            {
                switch (str)
                {
                    case "bad":
                        ctx.Votings.First().Bad++;
                        break;
                    case "normal":
                        ctx.Votings.First().Normal++;
                        break;
                    case "good":
                        ctx.Votings.First().Good++;
                        break;
                }
                ctx.Votings.First().Count++;
                ctx.SaveChanges();
            }
        }
    }
}