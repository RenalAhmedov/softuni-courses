using ForumApp.Data;
using ForumApp.Data.Entities;
using ForumApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly ForumAppDbContext data;

        public PostsController(ForumAppDbContext data)
        {
            this.data = data;
        }

        public IActionResult All()
        {
            var posts = this.data
                .Posts
                .Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .ToList();

            return View(posts);
        }

        public IActionResult Add()
        => View();

        [HttpPost]
        public IActionResult Add(PostFormModel model)
        {
            var post = new Post()
            {
                Title = model.Title,
                Content = model.Content,
            };

            this.data.Posts.Add(post);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var post = this.data.Posts.Find(id);

            return View(new PostFormModel()
            {
                Title = post.Title,
                Content = post.Content
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, PostFormModel model)
        {
            var post = this.data.Posts.Find(id);
            post.Title = model.Title;
            post.Content = model.Content;

            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Delete(int id)
        {
            var post = this.data.Posts.Find(id);

            this.data.Posts.Remove(post);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }
    }
}
