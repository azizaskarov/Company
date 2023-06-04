using Company.Api.Context;
using Company.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public CompaniesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateCompany(CreateCompanyModel createCompany)
    {
        if (await  _dbContext.Companies.AnyAsync(c => c.Name == createCompany.Name))
            return BadRequest("Company already!");

        var company = new Entities.Company()
        {
            Name = createCompany.Name,
        };

        _dbContext.Companies.Add(company);
        await _dbContext.SaveChangesAsync();

        return Ok(new { Company = company });
    }

    [HttpGet("GetAll")]
    public IActionResult GetCompanies()
    {
        return Ok(_dbContext.Companies);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
        if (company == null)
            return NotFound();

        return Ok(company);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCompany(Guid id, UpdateCompanyModel model)
    {
        var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
        if (company == null)
            return NotFound();

        company.Name = model.Name;
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
        if (company == null)
            return NotFound();

        _dbContext.Companies.Remove(company);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}