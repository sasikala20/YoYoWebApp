using System.Collections.Generic;
using YoYo_Web_App.Models;

namespace YoYo_Web_App.Repository.Interfaces
{
    public interface IAthleteRepository
    {
          List<Athlete> GetAthletesList();
          List<FitnessRating> GetFitnessRatings();
          Athlete Warn(int id);
          Athlete Stop(int id,FitnessRating fitnessRating);
    }
}