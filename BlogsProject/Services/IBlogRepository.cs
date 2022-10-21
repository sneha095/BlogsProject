using BlogsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsProject.Services
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetBlogsAsync();

        Task<IEnumerable<Blog>> GetBlogByIdAsync(int Id);

        Task<Blog> AddBlogAsync(Blog blog);

        Task<Blog> UpdateBlogAsync(Blog blog);

        Task<Blog> DeleteBlogAsync(int Id);

      //  Task<IEnumerable<BlogPost>> GetBlogPostsCreatedLast10Days();

    }
}
