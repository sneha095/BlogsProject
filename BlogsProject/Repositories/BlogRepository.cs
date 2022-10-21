using BlogsProject.Models;
using BlogsProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsProject.Repositories
{
    public class BlogRepository: IBlogRepository
    {
        private readonly bloggerdbContext _context;

        public BlogRepository(bloggerdbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Blog>>GetBlogsAsync()
        {
            return await _context.Blogs.Include(p=>p.BlogPosts).ToListAsync();
        }
        
        public async Task<IEnumerable<Blog>>GetBlogByIdAsync(int Id)
        {
            return await _context.Blogs.Where(d => d.BlogId == Id).ToListAsync();
        }
        
        public async Task<Blog> AddBlogAsync(Blog blog)
        {
            var result = await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Blog> UpdateBlogAsync(Blog blog)
        {
            var result =  _context.Blogs.FirstOrDefault(e => e.BlogId == blog.BlogId);
            if(result!=null)
            {
                result.BlogName = blog.BlogName;
                result.BlogPosts = blog.BlogPosts;
                result.Description = blog.Description;
                result.UserId = blog.UserId;
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Blog> DeleteBlogAsync(int Id)
        {
            var result = _context.Blogs.FirstOrDefault(e => e.BlogId == Id);
            if(result != null)
            {
                 _context.Blogs.Remove(result);

                return result;
            }
            return null;
        }

        //public async Task<IEnumerable<Blog>> GetBlogPostsCreatedLast10Days()
        //{
        //    DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-30);
        //    IEnumerable<BlogPost> objects = await _context.Blogs.Join(
        //                  _context.BlogPosts,
        //                  blog => blog.BlogId,
        //                  blogPost => blogPost.BlogId,
        //                  (blog, blogPost) => new
        //                  {
        //                      postId = blog.BlogName,
        //                      content = blog.BlogId,
        //                      blogPosts = blog.BlogPosts,

        //                  }).Where(x => x.createdDate >= currenteDate).ToListAsync();

        //    Console.WriteLine(objects);
        //    return (IEnumerable<Blog>)objects;

        //}
    }
}
