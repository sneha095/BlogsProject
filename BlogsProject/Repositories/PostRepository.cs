using BlogsProject.Models;
using BlogsProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsProject.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly bloggerdbContext _context;

        public PostRepository(bloggerdbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetPostsAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }

      
        public async Task<IEnumerable<BlogPost>> GetPostsByIdAsync(int Id)
        {
            return await _context.BlogPosts.Where(d => d.PostId == Id).ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetBlogsLastMonths()
        {
            DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-30);
            return await _context.BlogPosts.Where(d => d.CreatedDate >= currenteDate).ToListAsync();
        }

        public async Task<BlogPost> AddPostAsync(BlogPost blog)
        {
            var result = await _context.BlogPosts.AddAsync(blog);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BlogPost> UpdatePostAsync(BlogPost blogPost)
        {
            var result = _context.BlogPosts.FirstOrDefault(e => e.PostId == blogPost.PostId);
            if (result != null)
            {
                result.Blog = blogPost.Blog;
                result.BlogId = blogPost.BlogId;
                result.Content = blogPost.Content;
                result.CreatedDate = blogPost.CreatedDate;
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<BlogPost> DeletePostAsync(int Id)
        {
            var result = _context.BlogPosts.FirstOrDefault(e => e.PostId == Id);
            if (result != null)
            {
                _context.BlogPosts.Remove(result);
                _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
