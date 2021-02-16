using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs;
using api.Entities;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        //Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<string> GetUserGender(string username);
        Task<bool> SaveAllAsync();
    }
}