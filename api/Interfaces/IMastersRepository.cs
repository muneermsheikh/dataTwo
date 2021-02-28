using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs;
using api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IMastersRepository
    {
         Task<ICollection<Profession>> GetProfessions();
         Task<Profession> GetProfession(int Id);
         Task<bool> ProfessionExistsByName(string professionName, string industryName);
         Task<Profession> GetProfessionByName(string professionName, string industryName);
         Task<bool> ProfessionExistsById(int Id);
         void AddProfession(string ProfessionName, string IndustryName);
         void EditProfession(Profession profession);
         void DeleteProfession (Profession profession);
         Task<ICollection<Profession>> GetProfessionsOfAnIndustry(string industryName);
         Task<ICollection<Profession>> GetProfessionsOfLikeIndustry (string industryName);

    // Industry
         Task<ICollection<Industry>> GetIndustries();
         Task<Industry> GetIndustryByName(string industryName);
         Task<ICollection<Industry>> GetIndustryByNameLike(string industryName);
         Task<bool> IndustryExistsByName(string industryName);
         Task<bool> IndustryExistsById(int Id);
         Task<Industry> GetIndustryById(int Id);
         void AddIndustry(string IndustryName);
         void EditIndustry(Industry industry);
         void DeleteIndustry (Industry industry);
         //void UpdateIndustry(Industry industry);

//qualifications
         Task<ICollection<Qualification>> GetQualifications();
         Task<Qualification> GetQualification(int Id);
         Task<bool> QualificationExistsById(int Id);
         Task<bool> QualificationExistsByName(string qualificationName);
         Task<Qualification> GetQualificationByName(string QualificationName);
         void AddQualification(string QualificationName);
         void EditQualification(Qualification qualification);
         void DeleteQualification (Qualification qualification);
         //void UpdateQualification(Qualification qualification);
    }
}