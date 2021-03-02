using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs;
using api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IMastersRepository
    {
     //customers
         Task<ICollection<Customer>> GetCustomers(string customerType);
         Task<Customer> GetCustomerByNameAsync(string customeName, string customerType);
         Task<bool> CustomerExistsByNameCityTypeAsync(string cutomerName, string cityName, string customerType);
         Task<Customer> GetCustomerByIdAsync(int Id);
         Task<bool> CustomerExistsByIdAsync(int Id);
         void AddCustomer(Customer customer);
         void EditCustomer(Customer customer);
         void DeleteCustomer (Customer customer);

     //professions
         Task<ICollection<Profession>> GetProfessions();
         Task<Profession> GetProfession(int Id);
         Task<bool> ProfessionExistsByName(string professionName);
         Task<Profession> GetProfessionByName(string professionName);
         Task<bool> ProfessionExistsById(int Id);
         void AddProfession(string ProfessionName);
         void EditProfession(Profession profession);
         void DeleteProfession (Profession profession);

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