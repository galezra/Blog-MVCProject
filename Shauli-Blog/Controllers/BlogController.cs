using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shauli_Blog.DAL;
using Shauli_Blog.Models;

namespace Shauli_Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogContex db = new BlogContex();
        public ActionResult Index(string searchString)
        {
            var posts = from s in db.Posts
                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Author.Contains(searchString)
                                               || s.Content.Contains(searchString)); 
            }

            return View(posts.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // POST: /Blog/CreateComment
        /*
         * Method which adds new comment to given post.
         * Called after submitting the form in _PostComment partial view 
         */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                // Add new comment to Comment table in DB
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        //
        // GET: /Blog/Administration
        /*
         * Method which handles the Admin view
         */
        public ActionResult Administration()
        {
            return View(db.Posts.ToList());
        }


        //
        // GET: /Blog/Edit
        //
        public ActionResult Edit(int id=0)
        {

            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Administration");
            }
            return View(post);
        }

        //
        // GET: /Blog/Details/5
        //
        public ActionResult Details(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // GET: /Blog/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Administration");
        }
        //TODO - FIX MANAGE COMMENT
        public ActionResult ManageComments(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Blog/DeleteComment/5
        /*
        * Method which handles the comments deletion
        */
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            // Before deletion, get this comment's related post ID
            int postId = comment.PostId;
            // Delete comment from DB
            db.Comments.Remove(comment);
            db.SaveChanges();

            // Display Manage Comments view of the deleted comment
            return RedirectToAction("ManageComments", new { id = postId });
        }

        public ActionResult AddNewPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewPost(Post post)
        {
            // Get current date and update post PublishDate property 
            DateTime currDate = DateTime.Now;
            post.PublishDate = currDate;

            ModelState["PublishDate"].Errors.Clear(); // Required to make ModelState valid

            if (ModelState.IsValid)
            {
                // Add new post to Post table in DB
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Administration");
            }

            return View(post);
        }



    }
}