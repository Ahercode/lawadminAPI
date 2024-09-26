using AutoMapper;
using Lawadmin.WebAPI.Dtos.Case;
using Lawadmin.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Implementation;

namespace Lawadmin.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CourtCasesController  : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly LawAdminDB_Context _context;
    public CourtCasesController(IMapper mapper, LawAdminDB_Context context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllCourtCases()
    {
        var courtCases = await _context.CourtCases
            .Join( _context.Courts,
                        courtCase => courtCase.CourtId,
                        court => court.Id,
                        (courtCase, court) => new { courtCase, court })
            .Join( _context.CaseMonths,
                        courtCase => courtCase.courtCase.CaseMonthId,
                        caseMonth => caseMonth.Id,
                        (jointResult, caseMonth) => new { jointResult, caseMonth }
                        )
            .Join(_context.CaseYears,
                caseMonth => caseMonth.caseMonth.CaseYearId,
                caseYear => caseYear.Id,
                (jointResult, caseYear) => new CourtCaseResponse
                {
                    Id = jointResult.jointResult.courtCase.Id,
                    CaseTitle = jointResult.jointResult.courtCase.CaseTitle,
                    CatchPhrase = jointResult.jointResult.courtCase.CatchPhrase,
                    SuitNumber = jointResult.jointResult.courtCase.SuitNumber,
                    HeadNote = jointResult.jointResult.courtCase.HeadNote,
                    CaseContent = jointResult.jointResult.courtCase.CaseContent,
                    CourtId = jointResult.jointResult.courtCase.CourtId,
                    CaseMonthId = jointResult.jointResult.courtCase.CaseMonthId,
                    Status = jointResult.jointResult.courtCase.Status,
                    CourtName = jointResult.jointResult.court.Name,
                    Quoroms = jointResult.jointResult.courtCase.Quoroms,
                    YearOfJudgement = caseYear.Name,
                    MonthName = jointResult.caseMonth.Name
                    
                }
            )
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();
        var courtCasesDto = _mapper.Map<IEnumerable<CourtCaseResponse>>(courtCases);
        
        return Ok(courtCasesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetACourtCase(int id)
    {
        var courtCase = await _context.CourtCases.FindAsync(id);
        var courtCaseDto = _mapper.Map<CourtCaseResponse>(courtCase);
        
        return courtCaseDto == null ? NotFound("Court case not found") : Ok(courtCaseDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCourtCase(CourtCaseRequest createCourtCaseRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var courtCase = _mapper.Map<CourtCase>(createCourtCaseRequest);

            await _context.CourtCases.AddAsync(courtCase);
            await _context.SaveChangesAsync();
        
            return Ok("Court case created successfully");
            
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourtCase(int id, CourtCaseUpdate updateCourtCaseRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        var courtCase = await _context.CourtCases.FindAsync(id);
        if(courtCase == null)
            return NotFound("Court case not found");

        try
        {
            _mapper.Map(updateCourtCaseRequest, courtCase);

            _context.CourtCases.Update(courtCase);
            await _context.SaveChangesAsync();
        
            return Ok("Court case updated successfully");
            
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourtCase(int id)
    {
        var courtCase = await _context.CourtCases.FindAsync(id);
        if(courtCase == null)
            return NotFound("Court case not found");

        try
        {
            _context.CourtCases.Remove(courtCase);
            await _context.SaveChangesAsync();
        
            return Ok("Court case deleted successfully");
            
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
}