using BlogsProject.Models;
using BlogsProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly IBlogRepository blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            try
            {
                var blogs = await blogRepository.GetBlogsAsync();
                return Ok(blogs);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("Id")]
        public async Task<IActionResult>GetBlogById(int Id)
        {
            var blog = await blogRepository.GetBlogByIdAsync(Id);
            if(blog==null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                return Ok(blog);
            }
        }

        //[HttpGet("dateTime")]
        //public async Task<IActionResult> GetBlogPostsCreatedLast10DaysAsync()
        //{
        //    IEnumerable<BlogPost> blog = await blogRepository.GetBlogPostsCreatedLast10Days();
        //    if (blog == null)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound);
        //    }
        //    else
        //    {
        //        return Ok(blog);
        //    }
        //}


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddBlog([FromBody]Blog blog)
        {
            try
            {
                if (blog.BlogName==null || blog==null)
                {
                  return BadRequest();
                }
                var newDriver = await blogRepository.AddBlogAsync(blog);
                return CreatedAtAction(nameof(GetBlogById), new { id = newDriver.BlogId}, blog);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(int id, Blog blog)
        {
            try
            {
                if (id != blog.BlogId)
                {
                   
                    return BadRequest("Blog Id Mismatch");

                }
                var blogToUpdate = await blogRepository.GetBlogByIdAsync(id);
                if (blogToUpdate == null)
                {
                    return NotFound($"Blog with Id={id} not found");
                }

                await blogRepository.UpdateBlogAsync(blog);
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating blog details");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBlog(int Id)
        {
            try
            {
                var blogToDelete = await blogRepository.DeleteBlogAsync(Id);
                return Ok(blogToDelete);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }


    }
}
