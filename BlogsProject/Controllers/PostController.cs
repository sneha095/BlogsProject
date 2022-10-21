using BlogsProject.Models;
using BlogsProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var blogs = await postRepository.GetPostsAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetPostById(int Id)
        {
            var blog = await postRepository.GetPostsByIdAsync(Id);
            if (blog == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                return Ok(blog);
            }
        }


        [HttpGet("dateTime")]
        public async Task<IActionResult> GetPostsCreatedLast10Days()
        {
            var blog = await postRepository.GetBlogsLastMonths();
            if (blog == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                return Ok(blog);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPost([FromBody] BlogPost blog)
        {
            try
            {
                if (blog == null)
                {
                    return BadRequest();
                }
               var newDriver = await postRepository.AddPostAsync(blog);

                return CreatedAtAction(nameof(GetPostById), new { id = newDriver.BlogId }, blog);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(int id, BlogPost blogPost)
        {
            try
            {
                if (id != blogPost.PostId)
                {

                    return BadRequest("Blog Id Mismatch");

                }
                var blogPostToUpdate = await postRepository.GetPostsByIdAsync(id);
                if (blogPostToUpdate == null)
                {
                    return NotFound($"Blog POst with Id={id} not found");
                }

                await postRepository.UpdatePostAsync(blogPost);
                return Ok(blogPostToUpdate);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating post details");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int Id)
        {
            try
            {
                var blogToDelete = await postRepository.DeletePostAsync(Id);
                
                return Ok(blogToDelete);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

    }
}
