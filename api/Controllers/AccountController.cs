using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Entities;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService, IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("User taken");

        var user = _mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username;

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(user, "candidate");  // registerDto.UserRole);

        if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };
    }

    [HttpPost("registercustomer")]
    public async Task<ActionResult<UserDto>> RegisterCustomer(RegisterCustomerDto dto)
    {
        if (await UserExists(dto.Username)) return BadRequest("User taken");

        var user = _mapper.Map<AppUser>(dto);

        user.UserName = dto.Username;

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(user, dto.CustomerType);  // registerDto.UserRole);

        if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

        //add customer entity
        var customer = _mapper.Map<Customer>(dto);
        _uow.MastersRepository.AddCustomer(customer);
        if (!await _uow.Complete()) return BadRequest("Failed to update the customer entity");
        
        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LogInDto loginDto)
    {
        var user = await _userManager.Users
            .Where(x => x.UserName.ToLower() == loginDto.Username.ToLower())
            .Include(p => p.Photos)
            .SingleOrDefaultAsync();    //x => x.UserName.ToLower() == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("Invalid username");

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized();

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };

    }

    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
}