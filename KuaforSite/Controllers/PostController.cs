using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstracts;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace KuaforSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpPost("CreatePost/UserName={userName}")]
        public async Task<IActionResult> CreatePost([FromBody] PostAddVM post, string userName)
        {
            post.UserName = userName;
            await _postService.CreatePostAsync(post);
            return Ok();
        }
        [Authorize]
        [HttpPut("UpdatePost")]
        public IActionResult UpdatePost(PostUpdateVM post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _postService.UpdatePost(post);
            return Ok();
        }
        [Authorize]
        [HttpDelete("DeletePost/post_id={post_id}")]
        public IActionResult DeletePost(int post_id)
        {
            var isOk = _postService.DeletePost(post_id);
            if (!isOk) return BadRequest();
            return Ok();
        }
        [HttpGet("GetPostsByUserAsync/user_name={user_name}")]
        public async Task<IActionResult> GetPostsByUserAsync(string user_name)
        {
            var (result,isSuccess) = await _postService.GetPostsByUserAsync(user_name);
            if (!isSuccess) return BadRequest();
            return Ok(result);
        }
        [HttpGet("GetPostsByHairdresser/hairdresser_id={hairdresser_id}")]
        public IActionResult GetPostsByHairdresser(int hairdresser_id)
        {
            var (result, isSuccess) = _postService.GetPostsByHairdresser(hairdresser_id);
            if (!isSuccess) return BadRequest();
            return Ok(result);
        }
        [HttpGet("GetAllPosts")]
        public IActionResult GetAllPosts()
        {
            var (result, isSuccess) = _postService.GetAllPosts();
            if (!isSuccess) return BadRequest();
            return Ok(result);
        }
        [HttpGet("GetHairdressers")]
        public IActionResult GetHairdressers()
        {
            var hairdresserList=_postService.GetHairdressers();

            return Ok(hairdresserList);
        }
    }
}
