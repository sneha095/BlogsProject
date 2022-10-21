using BlogsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsProject.Services
{
    public interface IPostRepository
    {
        Task<IEnumerable<BlogPost>> GetPostsAsync();

        Task<IEnumerable<BlogPost>> GetPostsByIdAsync(int Id);

        Task<BlogPost> AddPostAsync(BlogPost blog);

        Task<BlogPost> UpdatePostAsync(BlogPost blog);

        Task<BlogPost> DeletePostAsync(int Id);

        Task<IEnumerable<BlogPost>> GetBlogsLastMonths();
    }
}
