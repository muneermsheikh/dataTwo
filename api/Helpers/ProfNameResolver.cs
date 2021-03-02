using System.Linq;
using api.Data;
using api.DTOs;
using api.Entities;
using AutoMapper;

namespace api.Helpers
{
    internal class ProfNameResolver : IValueResolver<UserProfessionDto, UserProfession, string>
    {
        private readonly DataContext _dataContext;
        public ProfNameResolver(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public string Resolve(UserProfessionDto source, UserProfession destination, string destMember, ResolutionContext context)
        {
            return _dataContext.Professions.Where(x => x.Id == source.Id).Select(x => x.Name).FirstOrDefault();
        }
    }
}