using AutoMapper;
using CQRSDemo.Models;
using CQRSDemo.DTO;

namespace CQRSDemo.Profiles
{
  public class PersonProfile: Profile
  {
    public PersonProfile()
    {
      CreateMap<Person, PersonDTO>()
        .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => string.Join('-', src.Dob.Year, src.Dob.Month, src.Dob.Day)))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.Join(' ', src.FirstName, src.MiddleName, src.LastName)));
    }
  }
}
