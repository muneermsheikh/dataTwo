using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Entities;
using api.Helpers;
using api.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName.ToLower() == username.ToLower())
                .ProjectTo<MemberDto>( _mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<ICollection<AssociateIdAndNameDto>> GetCustomersWithoutPaginationAsync(string userType)
        {
            var custs = await _context.Customers
                .Where(x => x.CustomerType==userType && x.IsActive==true)
                //.Select(x => new {x.Id, x.CustomerName})
                .OrderBy(x => x.CustomerName)
                .ProjectTo<AssociateIdAndNameDto>( _mapper.ConfigurationProvider)
                .ToListAsync();
            return custs;
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();
            //query = query.Where(x => x.UserName != userParams.CurrentUsername);

            if (!string.IsNullOrEmpty(userParams.UserType)) query = query.Where(x => x.UserType == userParams.UserType);
            if (!string.IsNullOrEmpty(userParams.NameLike))
            {
                query = userParams.UserType switch
                {
                    "candidate" => query.Where(x => x.FullName.ToLower().Contains(userParams.NameLike)),
                    "employee" => query.Where(x => x.FullName.ToLower().Contains(userParams.NameLike)),
                    _ => query.Where(x => x.CompanyName.ToLower().Contains(userParams.NameLike))
                };
            }

            if (!string.IsNullOrEmpty(userParams.Status)) query = query.Where(x => x.Status == userParams.Status);
            if (!string.IsNullOrEmpty(userParams.Gender)) query = query.Where(x => x.Gender == userParams.Gender);
            if (!string.IsNullOrEmpty(userParams.AssociateId))  //parameters only accept string values
            {
                List<int> AssociateIds = userParams.AssociateId.Split(',').Select(int.Parse).ToList();
                if (AssociateIds.Count() > 0) query = query.Where(x => AssociateIds.Contains(x.AssociateId));
            }

            var minDob = DateTime.Today.AddYears(-userParams.MaxAge -1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);
            query = query.Where( u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

            if(!string.IsNullOrEmpty(userParams.ProfessionLike))
            {
                query = query.Where(x => string.Join("",x.UserProfessions.Select(x => x.ProfessionName)).Contains(userParams.ProfessionLike));
            }
            if(!string.IsNullOrEmpty(userParams.IndustryLike))
            {
                query = query.Where(x => string.Join("",x.UserProfessions.Select(x => x.IndustryName)).Contains(userParams.IndustryLike));
            }
            
            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(x => x.Created),
                _ => query.OrderBy(x => x.LastActive)
            };

            return await PagedList<MemberDto>.CreateAsync(
                query.ProjectTo<MemberDto>( _mapper.ConfigurationProvider).AsNoTracking(), 
                userParams.PageNumber, userParams.PageSize);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUserNameAsync (string username)
        {
            return await _context.Users
                .Where(x => x.UserName.ToLower() == username.ToLower())
                .Include(p => p.Photos)
                .SingleOrDefaultAsync();
        }

        public async Task<string> GetUserGender(string username)
        {
            return await _context.Users
                .Where(x => x.UserName.ToLower() == username.ToLower())
                .Select(x => x.Gender).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(x => x.Photos).ToListAsync();
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        private string[] ConvertCSVToArray(string csv_line)
        {
            //using Microsoft.VisualBasic.FileIO;   //For TextFieldParser
            StringReader csv_reader = new StringReader(csv_line);
            TextFieldParser csv_parser = new TextFieldParser(csv_reader);
            csv_parser.SetDelimiters(",");
            csv_parser.HasFieldsEnclosedInQuotes = true;
            string[] csv_array = csv_parser.ReadFields();
            return csv_array;
        }

    }
}