using AutoMapper;
using Lawadmin.WebAPI.Dtos.Month;
using Lawadmin.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CaseMonthsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly LawAdminDB_Context _context;
    
    public CaseMonthsController(LawAdminDB_Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllCaseMonths()
    {
        var caseMonths = await _context.CaseMonths
            .Join( _context.CaseYears,
                jointResult => jointResult.CaseYearId,
                caseYear => caseYear.Id,
                (jointResult, caseYear) => new CaseMonthResponse
                {
                    Id = jointResult.Id,
                    Status = jointResult.Status,
                    Name = jointResult.Name,
                    CaseYearId = jointResult.CaseYearId,
                    YearName = caseYear.Name,
                    YearMonth = caseYear.Name!.Trim() + " - " + jointResult.Name!.Trim()
                }
                )
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();
        var caseMonthsDto = _mapper.Map<IEnumerable<CaseMonthResponse>>(caseMonths);
        
        return  Ok(caseMonthsDto) ;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetACaseMonth(int id)
    {
        var caseMonth = await _context.CaseMonths.FindAsync(id);
        var caseMonthDto = _mapper.Map<CaseMonthResponse>(caseMonth);
        
        return caseMonthDto == null ? NotFound("Case month not found") : Ok(caseMonthDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCaseMonth(CaseMonthRequest createCaseMonthRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var caseMonth = _mapper.Map<CaseMonth>(createCaseMonthRequest);

            await _context.CaseMonths.AddAsync(caseMonth);
            await _context.SaveChangesAsync();
        
            return Ok("Case month created successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest($"An error occurred while creating case month {e}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCaseMonth(int id, CaseMonthUpdate updateCaseMonthRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        var caseMonth = await _context.CaseMonths.FindAsync(id);
        if(caseMonth == null)
            return NotFound("Case month not found");

        try
        {
            _mapper.Map(updateCaseMonthRequest, caseMonth);
            await _context.SaveChangesAsync();
        
            return Ok("Case month updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Error updating CaseMonth{e}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaseMonth(int id)
    {
        var caseMonth = await _context.CaseMonths.FindAsync(id);
        if(caseMonth == null)
            return NotFound("Case month not found");

        try
        {
            _context.CaseMonths.Remove(caseMonth);
            await _context.SaveChangesAsync();
        
            return Ok("Case month deleted successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Error deleting CaseMonth{e}");
        }
    }
}