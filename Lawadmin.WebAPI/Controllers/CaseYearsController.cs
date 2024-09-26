using AutoMapper;
using Lawadmin.WebAPI.Dtos.Year;
using Lawadmin.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaseYearsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly LawAdminDB_Context _context;
    
    public CaseYearsController(LawAdminDB_Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllCaseYears()
    {
        var caseYears = await _context.CaseYears
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();
        var caseYearsDto = _mapper.Map<IEnumerable<CaseYearResponse>>(caseYears);
        
        return  Ok(caseYearsDto) ;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetACaseYear(int id)
    {
        var caseYear = await _context.CaseYears.FindAsync(id);
        var caseYearDto = _mapper.Map<CaseYearResponse>(caseYear);
        
        return caseYearDto == null ? NotFound("Case year not found") : Ok(caseYearDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCaseYear(CaseYearRequest createCaseYearRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var caseYear = _mapper.Map<CaseYear>(createCaseYearRequest);

            await _context.CaseYears.AddAsync(caseYear);
            await _context.SaveChangesAsync();
        
            return Ok("Case year created successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest($"An error occurred while creating the case year {e}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCaseYear(int id, CaseYearUpdate updateCaseYearRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        var caseYear = await _context.CaseYears.FindAsync(id);
        if(caseYear == null)
            return NotFound("Case year not found");

        try
        {
            _mapper.Map(updateCaseYearRequest, caseYear);

            _context.CaseYears.Update(caseYear);
            await _context.SaveChangesAsync();
        
            return Ok("Case year updated successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest($"An error occurred while updating the case year {e}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaseYear(int id)
    {
        var caseYear = await _context.CaseYears.FindAsync(id);
        if(caseYear == null)
            return NotFound("Case year not found");

        try
        {
            _context.CaseYears.Remove(caseYear);
            await _context.SaveChangesAsync();
        
            return Ok("Case year deleted successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest($"An error occurred while deleting the case year {e}");
        }
    }
}