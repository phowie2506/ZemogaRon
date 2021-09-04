using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZemogaRon.Data;
using ZemogaRon.Filters;
using ZemogaRon.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZemogaRon.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly ZemogaRonContext context;

        public PostsController(ZemogaRonContext context)
        {
            this.context = context;
        }

        
        [HttpGet("all")]
        public IEnumerable<Post> GetAllPosts()
        {
            
          return context.Post?.ToList().Where(x => x.state == 2);
        }

        //List Own Post , Permission required
        [ApiKeyAuth("writterpermissions")]
        [HttpGet("GetByUserId")]
        public IEnumerable<Post> GetByUserId(int userid)
        {
            return context.Post?.ToList().Where(x => x.UserId == userid);
        }
        
        [HttpGet("{id}")]
        public Post GetbyId(int id)
        {
            return context.Post.FirstOrDefault(x => x.Id == id);
        }

        [ApiKeyAuth("writterpermissions")]
        [HttpPost("AddNewPost")]
        public async Task<ActionResult<string>> AddNewPost(Post post)
        {
            try
            {
                post.state = 1; //Pending approval
                context.Post.Add(post);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex) {
                return ex.Message;

            }
        }

        [ApiKeyAuth("writterpermissions")]
        [HttpPost("ModifyPost")]
        public async Task<ActionResult<string>> ModifyPost(Post post)
        {
            try
            {
                Post postprevius = context.Post.FirstOrDefault(x => x.Id == post.Id);
                if(postprevius.state != 1)
                {
                    return ValidationProblem("Can't modify this post, please check state");
                }
                else{
                    context.Post.Update(post);
                    await context.SaveChangesAsync();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        [ApiKeyAuth("editorpermissions")]
        [HttpGet("GetPendintPost")]
        public async Task<ActionResult<Post>> GetPendintPost()
        {

                return context.Post.FirstOrDefault(x => x.state == 2);
            
        }

        [ApiKeyAuth("editorpermissions")]
        [HttpPost("UpdatePostState")]
        public async Task<ActionResult<string>> UpdatePostState(int IdPost , int State,string comment)
        {
            try
            {
                Post post = context.Post.FirstOrDefault(x => x.Id == IdPost);
                post.state = State;
                if(State == 3)
                {
                    post.comment = comment;
                }

                
                context.Post.Update(post);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        [ApiKeyAuth("writterpermissions")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [ApiKeyAuth("writterpermissions")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
