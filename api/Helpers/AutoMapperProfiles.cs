using System;
using System.Linq;
using api.DTOs;
using api.Entities;
using api.Extensions;
using AutoMapper;

namespace api.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserProfessionDto, UserProfession>();
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.UserProfessions, opt => opt.MapFrom(src => string.Join(",", src.UserProfessions)))
                .ForMember(dest => dest.UserPhones, opt => opt.MapFrom(src => string.Join(",", src.UserPhones.Select(x => x.PhoneNo))))
                .ForMember(dest => dest.PassportNo, opt => opt.MapFrom(src => string.Join(",", src.UserPassports.Where(x => x.IsValid==true).Select(x => x.PassportNo))));
            CreateMap<Customer, AssociateIdAndNameDto>();
            CreateMap<UserProfessionDto, UserProfession>()
                .ForMember(dest => dest.ProfessionName, opt => opt.MapFrom<ProfNameResolver>())
                .ForMember(dest => dest.IndustryName, opt => opt.MapFrom<IndNameResolver>());
            CreateMap<Photo, PhotoDto>();
        
            CreateMap<MemberUpdateDto, AppUser>();
        
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<RegisterCustomerDto, AppUser>();
        
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => 
                    src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => 
                    src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<EditQDto, Qualification>();
            CreateMap<EditProfDto, Profession>();
            CreateMap<EditIndsDto, Industry>();
        }
    }
}