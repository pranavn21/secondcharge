using AutoMapper;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO.Car;
using secondcharge.api.Models.DTO.User;
using secondcharge.api.Models.DTO.Location;
using secondcharge.api.Models.DTO.VehicleListing;

namespace secondcharge.api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<Car, AddCarRequestDto>().ReverseMap();
            CreateMap<Car, UpdateCarRequestDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, AddUserRequestDto>().ReverseMap();
            CreateMap<User, UpdateUserRequestDto>().ReverseMap();

            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, AddLocationRequestDto>().ReverseMap();
            CreateMap<Location, UpdateLocationRequestDto>().ReverseMap();

            CreateMap<VehicleListing, VehicleListingDto>().ReverseMap();
            CreateMap<VehicleListing, AddVehicleListingRequestDto>().ReverseMap();
            CreateMap<VehicleListing, UpdateVehicleListingRequestDto>().ReverseMap();
        }
    }
}
