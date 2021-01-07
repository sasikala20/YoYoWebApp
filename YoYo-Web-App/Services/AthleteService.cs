using System.Collections.Generic;
using YoYo_Web_App.Models;
using YoYo_Web_App.Repository.Interfaces;
using YoYo_Web_App.Services.Interfaces;

namespace YoYo_Web_App.Services
{
    public class AthleteService : IAthleteService
    {
        private IAthleteRepository _athleteRepository;

        public AthleteService(IAthleteRepository athleteRepository)
        {
            this._athleteRepository= athleteRepository;
        }

        public List<Athlete> GetAthletesList()
        {
           return _athleteRepository.GetAthletesList();
        }

        public List<FitnessRating> GetFitnessRatings()
        {
            return _athleteRepository.GetFitnessRatings();
        } 

        public Athlete Warn(int id){
            return _athleteRepository.Warn(id);
        }

        public Athlete Stop(int id,FitnessRating fitnessRating){
            return _athleteRepository.Stop(id,fitnessRating);
        }
    }
}