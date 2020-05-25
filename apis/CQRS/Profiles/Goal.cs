using AutoMapper;
using CQRSDemo.DTO;
using CQRSDemo.Models;

namespace CQRSDemo.Profiles
{
    public class GoalProfile: Profile
    {
       public GoalProfile()
       {
            CreateMap<Goal, GoalDTO>();
       }
    }
}
