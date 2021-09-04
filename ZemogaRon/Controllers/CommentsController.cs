using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZemogaRon.Data;
using ZemogaRon.Filters;
using ZemogaRon.Models;

namespace ZemogaRon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ZemogaRonContext _context;

        public CommentsController(ZemogaRonContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet("GetByPost")]
        public IEnumerable<Comment> GetByPost(int idPost)
        {
            var comment = _context.Comment?.ToList().Where(x => x.PostId == idPost);
            return comment;
        }
        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost("AddNewComment")]
        public async Task<ActionResult<string>> AddNewComment(Comment comment)
        {
            try
            {
               if(_context.Post.Where(x => x.Id == comment.PostId).FirstOrDefault().state != 2)
                {
                    return ValidationProblem("Can't add comment to this post");
                }

                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
