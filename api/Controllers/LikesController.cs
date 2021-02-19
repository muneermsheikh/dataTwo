using System.Collections.Generic;
using System.Threading.Tasks;
using api.Entities;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IUserRepository _userRepository;
        public LikesController(ILikesRepository likesRepository,
        IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _likesRepository = likesRepository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike (string username)
        {
            var loggedInUserId = User.GetUserId();        //the loggedin userid
            var likedUser = await _userRepository.GetUserByUserNameAsync(username);
            if (likedUser == null) return NotFound();
            var loggedInUser = await _likesRepository.GetUserWithLikes(loggedInUserId); //loggedin User
            if (loggedInUser.UserName == username) return BadRequest("You cannot like yourself");

            var userLike = await _likesRepository.GetUserLike(loggedInUserId, likedUser.Id);

            if (userLike != null) return BadRequest("You already likedhis user");

            userLike = new UserLike {
                SourceUserId = loggedInUserId,
                LikedUserId = likedUser.Id
            };
            
            loggedInUser.LikedUsers.Add(userLike);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to like the User");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<UserLike>>> GetUserLikes([FromQuery] LikesParams likesParams)
        {
            if (string.IsNullOrEmpty(likesParams.Predicate)) return BadRequest("preedicate not specified");
            likesParams.UserId = User.GetUserId();

            var users = await _likesRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(users.CurrentPage,
                users.PageSize, users.TotalCount, users.TotalPages);
            
            return Ok(users);
        }
}
}