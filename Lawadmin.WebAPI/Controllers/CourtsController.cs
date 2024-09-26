using AutoMapper;
using Lawadmin.WebAPI.Dtos.Court;
using Lawadmin.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CourtsController : ControllerBase
{
    private readonly IMapper _mapper;
    private  readonly LawAdminDB_Context _context;
    
    public CourtsController(LawAdminDB_Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllCourts()
    {
        var courts = await _context.Courts
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();
        var courtsDto = _mapper.Map<IEnumerable<CourtResponse>>(courts);
        
        return  Ok(courtsDto) ;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetACourt(int id)
    {
        var court = await _context.Courts.FindAsync(id);
        var courtDto = _mapper.Map<CourtResponse>(court);
        
        return courtDto == null ? NotFound("Court not found") : Ok(courtDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCourt(CourtRequest createCourtRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var court = _mapper.Map<Court>(createCourtRequest);

            await _context.Courts.AddAsync(court);
            await _context.SaveChangesAsync();
        
            return Ok("Court created successfully");
            
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error {e}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourt(int id, CourtUpdate updateCourtRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null)
                return NotFound("Court not found");

            _mapper.Map(updateCourtRequest, court);
            await _context.SaveChangesAsync();
        
            return Ok("Court updated successfully");
            
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error {e}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourt(int id)
    {
        try
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null)
                return NotFound("Court not found");

            _context.Courts.Remove(court);
            await _context.SaveChangesAsync();
        
            return Ok("Court deleted successfully");
            
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error {e}");
        }
    }
    
}