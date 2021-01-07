using System.Collections.Generic;
using YoYo_Web_App.Models;

namespace YoYo_Web_App.Services.Interfaces
{
    public interface IAthleteService
    {
         List<Athlete> GetAthletesList();

         List<FitnessRating> GetFitnessRatings();

         Athlete Warn(int id);

         Athlete Stop(int id,FitnessRating fitnessRating);
    }
}