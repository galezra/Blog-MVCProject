using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Shauli_Blog.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }



        public virtual ICollection<Comment> Comments { get; set; }
    }
}