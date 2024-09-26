using AutoMapper;
using Lawadmin.WebAPI.Dtos.Case;
using Lawadmin.WebAPI.Dtos.Court;
using Lawadmin.WebAPI.Dtos.Month;
using Lawadmin.WebAPI.Dtos.Year;
using Lawadmin.WebAPI.Entities;

namespace Lawadmin.WebAPI.Mappers;

public class RequestToEntities : Profile
{
    
    public RequestToEntities()
    {
        CreateMap<CourtCaseRequest, CourtCase>();
        CreateMap<CourtRequest, Court>();
        CreateMap<CaseMonthRequest, CaseMonth>();
        CreateMap<CaseYearRequest, CaseYear>();
        CreateMap<CourtCaseUpdate, CourtCase>();
        CreateMap<CourtUpdate, Court>();
        CreateMap<CaseMonthUpdate, CaseMonth>();
        CreateMap<CaseYearUpdate, CaseYear>();
    }
}