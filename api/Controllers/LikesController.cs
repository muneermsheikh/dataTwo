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
        private readonly IUnitOfWork _unitOfWork;
        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var loggedInUserId = User.GetUserId();        //the loggedin userid
            var likedUser = await _unitOfWork.UserRepository.GetUserByUserNameAsync(username);
            if (likedUser == null) return NotFound();
            var loggedInUser = await _unitOfWork.LikesRepository.GetUserWithLikes(loggedInUserId); //loggedin User
            if (loggedInUser.UserName == username) return BadRequest("You cannot like yourself");

            var userLike = await _unitOfWork.LikesRepository.GetUserLike(loggedInUserId, likedUser.Id);

            if (userLike != null) return BadRequest("You already likedhis user");

            userLike = new UserLike
            {
                SourceUserId = loggedInUserId,
                LikedUserId = likedUser.Id
            };

            loggedInUser.LikedUsers.Add(userLike);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to like the User");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<UserLike>>> GetUserLikes([FromQuery] LikesParams likesParams)
        {
            if (string.IsNullOrEmpty(likesParams.Predicate)) return BadRequest("predicate not specified");
            likesParams.UserId = User.GetUserId();

            var users = await _unitOfWork.LikesRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(users.CurrentPage,
                users.PageSize, users.TotalCount, users.TotalPages);
 
            return Ok(users);
        }
    }
}