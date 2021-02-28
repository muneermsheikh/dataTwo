using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class MastersRepository : IMastersRepository
    {
        private readonly DataContext _context;
        public MastersRepository(DataContext context)
        {
            _context = context;
        }


//Qualification
        public async Task<bool> QualificationExistsById (int Id)
        {
            return ((await _context.Qualifications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id))!=null);
        }
        
        public async Task<bool> QualificationExistsByName(string qualificationName)
        {
            return (( await _context.Qualifications.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name.ToLower() == qualificationName.ToLower()))!=null);
        }
        public void AddQualification(string QualificationName)
        {
            _context.Qualifications.Add(new Qualification(QualificationName));
        }

        public void DeleteQualification(Qualification qualification)
        {
            _context.Entry(qualification).State = EntityState.Deleted;
        }
        public void EditQualification(Qualification qualification)
        {
            _context.Entry(qualification).State = EntityState.Modified;
        }
        public async Task<Qualification> GetQualification(int Id)
        {
            return await _context.Qualifications.FindAsync(Id);
        }

        public async Task<Qualification> GetQualificationByName(string QualificationName)
        {
            return await _context.Qualifications.Where(x => x.Name == QualificationName).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Qualification>> GetQualifications()
        {
            return await _context.Qualifications.OrderBy(x => x.Name).ToListAsync();
        }


// Industry
        public void AddIndustry(string IndustryName)
        {
            _context.Industries.Add(new Industry(IndustryName));
        }

        public void DeleteIndustry(Industry industry)
        {
            _context.Entry(industry).State = EntityState.Deleted;
        }

        public void EditIndustry(Industry newIndustry)
        {
            _context.Entry(newIndustry).State = EntityState.Modified;
        }

        public async Task<ICollection<Industry>> GetIndustries()
        {
            return await _context.Industries.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Industry> GetIndustryByName(string industryName)
        {
            return await _context.Industries.Where(x => x.Name.ToLower() == industryName.ToLower()).FirstOrDefaultAsync();
        }
        public async Task<ICollection<Industry>> GetIndustryByNameLike(string industryName)
        {
            return await _context.Industries.Where(x => x.Name.ToLower().Contains(industryName.ToLower())).ToListAsync();
        }

        public async Task<Industry> GetIndustryById(int Id)
        {
            return await _context.Industries.FindAsync(Id);
        }

        public async Task<bool> IndustryExistsById (int Id)
        {
            return ((await _context.Industries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id)) != null);
        }
        public async Task<bool> IndustryExistsByName(string industryName)
        {
            return ((await _context.Industries.AsNoTracking().FirstOrDefaultAsync(x => x.Name.ToLower() == industryName.ToLower())) != null);
        }

//Profession
        public async Task<Profession> GetProfession(int Id)
        {
            return await _context.Professions.FindAsync(Id);
        }

        public async Task<bool> ProfessionExistsById (int Id)
        {
            return ((await _context.Professions.FindAsync(Id))!=null);
        }

        public async Task<bool> ProfessionExistsByName(string professionName, string industryName)
        {
            if ((await _context.Industries.AsNoTracking().FirstOrDefaultAsync(x => x.Name.ToLower() == industryName.ToLower()))!=null)
                return false;

            return ((await _context.Professions.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name.ToLower() == professionName.ToLower() &&
                x.Industry.ToLower() == industryName.ToLower()))!=null);
        }

        public async Task<ICollection<Profession>> GetProfessions()
        {
            return await _context.Professions.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<ICollection<Profession>> GetProfessionsOfAnIndustry (string industryName)
        {
            return await _context.Professions.Where(x => x.Industry.ToLower() == industryName.ToLower()).ToListAsync();
        }

        public async Task<ICollection<Profession>> GetProfessionsOfLikeIndustry (string industryName)
        {
            var lst = new List<string>();
            lst.Add(industryName);

            return await _context.Professions
                .Where(x => x.Industry.ToLower().Contains(industryName.ToLower())).ToListAsync();
        }
        
        public async Task<Profession> GetProfessionByName(string professionName, string industryName)
        {
            return await _context.Professions.FirstOrDefaultAsync(x => x.Name == professionName && x.Industry == industryName);
        }
        public void AddProfession(string ProfessionName, string IndustryName)
        {
            _context.Professions.Add(new Profession(ProfessionName, IndustryName));
        }
        
        public void DeleteProfession(Profession profession)
        {
            _context.Entry(profession).State = EntityState.Deleted;
        }

        
        public void EditProfession(Profession profession)
        {
            _context.Entry(profession).State = EntityState.Modified;
        }

       
    }
}