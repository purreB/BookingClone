using AutoMapper;
using BookingClone.Application.DTOs;
using BookingClone.Domain.Bookings;
using BookingClone.Domain.Hotels;
using BookingClone.Domain.Users;

namespace BookingClone.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Booking mappings
        CreateMap<Booking, BookingDto>().ReverseMap();

        // Hotel mappings (OwnerId is string on DTO for TypeScript export; Guid on domain)
        CreateMap<Hotel, HotelDto>()
            .ForMember(d => d.OwnerId, o => o.MapFrom(s => s.OwnerId.ToString()));
        CreateMap<HotelDto, Hotel>()
            .ForMember(d => d.OwnerId, o => o.MapFrom(s => OwnerIdFromDto(s.OwnerId)))
            .ForMember(d => d.Owner, o => o.Ignore())
            .ForMember(d => d.Rooms, o => o.Ignore());

        // HotelRoom mappings
        CreateMap<HotelRoom, HotelRoomDto>().ReverseMap();

        // Guest mappings
        CreateMap<Guest, GuestDto>().ReverseMap();

        // StaffUser mappings
        CreateMap<StaffUser, StaffUserDto>().ReverseMap();
    }

    private static Guid OwnerIdFromDto(string ownerId) =>
        Guid.TryParse(ownerId, out var id) ? id : Guid.Empty;
}
