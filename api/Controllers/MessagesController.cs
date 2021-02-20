using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs;
using api.Entities;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public MessagesController(IMessageRepository messageRepository, 
            IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }


        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();

            if (username.ToLower() == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("You can't send message to yourself");

            var sender = await _userRepository.GetUserByUserNameAsync(username);
            var recipient = await _userRepository.GetUserByUserNameAsync(createMessageDto.RecipientUsername);

            if (recipient == null) return BadRequest("No recipient for the message found");

            var message = new Message
            {
                SenderId = sender.Id,
                SenderUsername = username,
                RecipientId = recipient.Id,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveAllAsync()) 
            {
                return Ok( _mapper.Map<MessageDto>(message));
            }

            return BadRequest("failed to add the messasge");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]
            MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();

            var messages = await _messageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize,
                messages.TotalCount, messages.TotalPages);

            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var loggedInUsername = User.GetUsername();

            var messageThread = await _messageRepository.GetMessageThread(loggedInUsername, username);

            if (messageThread == null) return NotFound("No messages found");

            return Ok(messageThread);
        }


        /*
            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteMessage(int id)
            {
                var username = User.GetUsername();

                var message = await _unitOfWork.MessageRepository.GetMessage(id);

                if (message.Sender.UserName != username && message.Recipient.UserName != username)
                    return Unauthorized();

                if (message.Sender.UserName == username) message.SenderDeleted = true;

                if (message.Recipient.UserName == username) message.RecipientDeleted = true;

                if (message.SenderDeleted && message.RecipientDeleted)
                    _unitOfWork.MessageRepository.DeleteMessage(message);

                if (await _unitOfWork.Complete()) return Ok();

                return BadRequest("Problem deleting the message");
            }
        */
    }
}