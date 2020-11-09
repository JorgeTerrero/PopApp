using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopApp.Core.Dtos;
using PopApp.Core.Entities;
using PopApp.Core.Interfaces.Services;

namespace PopAppMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyServices _repo;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyServices repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var company = await _repo.GetCompanies();
                var companyDto = _mapper.Map<IEnumerable<Company>>(company);
                return Ok(companyDto);

            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComany(int id)
        {
            try
            {
                var company = await _repo.GetCompany(id);
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);

            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCompany(CompanyDto companyDto)
        {
            try
            {
                var company = _mapper.Map<Company>(companyDto);
                company.IsActive = true;
                await _repo.CreateCompany(company);
                return Ok("Company Was Registred!");

            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyDto companyDto)
        {
            try
            {
                var company = _mapper.Map<Company>(companyDto);
                await _repo.UpdateCompany(id, company);
                return Ok("Company was Updated");

            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _repo.DeleteCompany(id);
                return Ok("Company was Deleted");

            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
    }
}
