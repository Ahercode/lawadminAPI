using AutoMapper;
using Lawadmin.WebAPI.Dtos.Case;
using Lawadmin.WebAPI.Dtos.Court;
using Lawadmin.WebAPI.Dtos.Month;
using Lawadmin.WebAPI.Dtos.Year;
using Lawadmin.WebAPI.Entities;

namespace Lawadmin.WebAPI.Mappers;

public class EntitiesToResponse : Profile
{
    
    public EntitiesToResponse()
    {
        CreateMap<CourtCase, CourtCaseResponse>();
        CreateMap<Court, CourtResponse>();
        CreateMap<CaseMonth, CaseMonthResponse>();
        CreateMap<CaseYear, CaseYearResponse>();
    }
}