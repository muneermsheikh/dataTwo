using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Entities;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    public class MastersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MastersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

//professions
        [HttpGet("prof")]
        public async Task<ActionResult<ICollection<Profession>>> GetProfessions()
        {
            var profs = await _unitOfWork.MastersRepository.GetProfessions();
            if (profs == null) return NotFound("No professions on record");
            return Ok(profs);
        }

        [HttpGet("profdto")]
        public async Task<ActionResult<ICollection<ProfessionDto>>> GetProfessionsDto()
        {
            var profs = await _unitOfWork.MastersRepository.GetProfessions();
            if (profs == null) return NotFound("No professions on record");
            var dto = new List<ProfessionDto>();
            foreach(var prof in profs)
            {
                dto.Add(new ProfessionDto(prof.Id, prof.Name + "-" + prof.Industry));
            }
            return Ok(dto);
        }

        [HttpPost("prof")]
        public async Task<ActionResult<Profession>> AddProfession(AddProfDto profAddDto)
        {
            //check if the profession and industry exist
            if (await _unitOfWork.MastersRepository.ProfessionExistsByName(profAddDto.Name, profAddDto.Industry)) 
                return BadRequest("profession with its related industry exists");
            
            _unitOfWork.MastersRepository.AddProfession(profAddDto.Name, profAddDto.Industry);

            if (await _unitOfWork.Complete()) 
                return Ok(await _unitOfWork.MastersRepository.GetProfessionByName(profAddDto.Name, profAddDto.Industry));
            
            return BadRequest("failed to add the profession");

        }

        [HttpPut("prof")]
        public async Task<ActionResult<bool>> EditProfession(EditProfDto profEditDto)
        {
            if (!await _unitOfWork.MastersRepository.ProfessionExistsById(profEditDto.Id))
                return NotFound("no such profession on record");
            
            var prof = _mapper.Map<EditProfDto, Profession>(profEditDto);

            _unitOfWork.MastersRepository.EditProfession(prof);

            if (await _unitOfWork.Complete()) return true;
            
            return BadRequest("failed to update the profession " + profEditDto.Name);

        }

        [HttpPost("prof/{prof}/{industry}")]
        public async Task<ActionResult<bool>> AddProfession(string prof, string industry)
        {
            if (string.IsNullOrEmpty(prof) || string.IsNullOrEmpty(industry)) return  BadRequest("null values provided");

            //check if industry exists
            if (await _unitOfWork.MastersRepository.ProfessionExistsByName(prof, industry))
                return BadRequest("either the industry '" + industry + "' does not exist, or the profession " + prof + " does not exist");

            _unitOfWork.MastersRepository.AddProfession(prof, industry);

            if (await _unitOfWork.Complete()) return true;

            return BadRequest("Failed to add the profession '" + prof + "' under the industry " + industry);
        }

        [HttpPut("prof")]
        public async Task<ActionResult<bool>> EditProfession([FromQuery] Profession profession)
        {
            _unitOfWork.MastersRepository.EditProfession(profession);
            if (await _unitOfWork.Complete()) return true;

            return BadRequest("Failed to update the profession");
        }
        
        [HttpDelete("prof")]
        public async Task<ActionResult<bool>> DeleteProfession([FromQuery] Profession profession)
        {
            _unitOfWork.MastersRepository.DeleteProfession(profession);
            if (await _unitOfWork.Complete()) return true;
            return BadRequest("Failed to delete the profession - a possible reason is relationship constraint");
        }

//industries
        [HttpGet("inds")]
        public async Task<ActionResult<ICollection<Industry>>> GetIndustries()
        {
            var inds = await _unitOfWork.MastersRepository.GetIndustries();
            if (inds == null) return NotFound("No industry on record");

            return Ok(inds);
        }

        [HttpGet("profOfInds/{industryName}")]
        public async Task<ActionResult<ICollection<Profession>>> GetProfessionsOfAnIndustry (string industryName)
        {
            var profs =await _unitOfWork.MastersRepository.GetProfessionsOfAnIndustry(industryName);
            if (profs == null || profs.Count == 0) return NotFound("No professions found under the industry " + industryName);

            return Ok(profs);
        }

        [HttpGet("profOfIndsLike/{industryName}")]
        public async Task<ActionResult<ICollection<Profession>>> GetProfessionsOfLikeIndustry (string industryName)
        {
            var profs =await _unitOfWork.MastersRepository.GetProfessionsOfLikeIndustry(industryName);
            if (profs == null || profs.Count == 0) return NotFound("No professions found under industry like " + industryName);

            return Ok(profs);
        }

        [HttpDelete("inds")]
        public async Task<ActionResult<bool>> DeleteIndustry(Industry industry)
        {
            _unitOfWork.MastersRepository.DeleteIndustry(industry);
            if (await _unitOfWork.Complete()) return true;
            return BadRequest("Failed to delete the industry - a possible reason is relationship constraint");
        }

        [HttpGet("inds/{industryName}")]
        public async Task<ActionResult<Industry>> GetIndustryName(string industryName)
        {
            return await _unitOfWork.MastersRepository.GetIndustryByName(industryName);
        }
        
        [HttpGet("indslike/{industryName}")]
        public async Task<ActionResult<IEnumerable<Industry>>> GetIndustryByNameLike(string industryName)
        {
            var inds = await _unitOfWork.MastersRepository.GetIndustryByNameLike(industryName);
            if (inds == null || inds.Count == 0 ) return NotFound();
            return Ok(inds);
        }



        [HttpPost("ind/{industry}")]
        public async Task<ActionResult<bool>> AddIndustry(string industry)
        {
            //check if industry exists
            if (await _unitOfWork.MastersRepository.IndustryExistsByName(industry))
                return BadRequest("the industry '" + industry + "' already exists!");

            _unitOfWork.MastersRepository.AddIndustry(industry);

            if (await _unitOfWork.Complete()) return true;

            return BadRequest("Failed to add the industry '" + industry + "'");
        }
        
        [HttpPut("inds")]
        public async Task<ActionResult<bool>> EditIndustry(Industry industry)
        {
            if (!await _unitOfWork.MastersRepository.IndustryExistsById(industry.Id)) return BadRequest();

            _unitOfWork.MastersRepository.EditIndustry(industry);
            
            if (await _unitOfWork.Complete()) return true;
            return BadRequest("Failed to update the industry name");
        }

//Qualifications
        [HttpPost("q/{qualification}")]
        public async Task<ActionResult<bool>> AddQualification(string qualification)
        {
            if (await _unitOfWork.MastersRepository.QualificationExistsByName(qualification))
                return BadRequest("Qualification '" + qualification + "' already exists!");

            _unitOfWork.MastersRepository.AddQualification(qualification);

            if (await _unitOfWork.Complete()) return true;

            return BadRequest("Failed to add qualification '" + qualification + "'");
        }

        [HttpPut]
        public async Task<ActionResult<bool>> EditQualification(EditQDto editq)
        {
            if (await _unitOfWork.MastersRepository.QualificationExistsById(editq.Id))
                return NotFound("the qualification entity not on record");

            var qualification = _mapper.Map<EditQDto, Qualification>(editq);

            _unitOfWork.MastersRepository.EditQualification(qualification);
            if (await _unitOfWork.Complete()) return true;

            return BadRequest("Failed to update the qualification - check if the qualification already exists");
        }


//qualifications
        [HttpDelete("q")]
        public async Task<ActionResult<bool>> DeleteQualification([FromQuery] Qualification qualification)
        {
            _unitOfWork.MastersRepository.DeleteQualification(qualification);
            if (await _unitOfWork.Complete()) return true;
            return BadRequest("Failed to delete the qualification - a possible reason is relationship constraint");
        }

        [HttpGet("q")]
        public async Task<ActionResult<ICollection<Qualification>>> GetQualifications()
        {
            var q = await _unitOfWork.MastersRepository.GetQualifications();
            if (q == null) return NotFound("No Qualification data on record");

            return Ok(q);
        }


    }
}