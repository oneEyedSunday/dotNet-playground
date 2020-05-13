using AutoMapper;
using web.Models;
using web.DTO;

namespace web.Profiles
{
  public class ClubProfile: Profile
  {
    public ClubProfile()
    {
      CreateMap<Club, ClubDTO>()
        .ForMember(dest => dest._Founded, opt => opt.MapFrom(src => src.Founded));
    }
  }
}
