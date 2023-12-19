using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace news_blog.Model
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
    }
}
