using AutoMapper;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;

namespace Proyecto_Aerolinea.Web.Core
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Airport, AirportDTO>().ReverseMap();
            CreateMap<Flight, FlightDTO>().ReverseMap();
            CreateMap<Aircraft, AircraftDTO>().ReverseMap();
            CreateMap<User, AccountUserDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>();

            CreateMap<ProjectRole, ProjectRoleDTO>().ReverseMap();
        }
    }
}
