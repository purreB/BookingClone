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

        // Hotel mappings
        CreateMap<Hotel, HotelDto>().ReverseMap();

        // HotelRoom mappings
        CreateMap<HotelRoom, HotelRoomDto>().ReverseMap();

        // Guest mappings
        CreateMap<Guest, GuestDto>().ReverseMap();

        // StaffUser mappings
        CreateMap<StaffUser, StaffUserDto>().ReverseMap();
    }
}
