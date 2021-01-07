using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using YoYo_Web_App.Models;
using YoYo_Web_App.Repository.Interfaces;

namespace YoYo_Web_App.Repository
{
    public class AthleteRepository : IAthleteRepository
    {
        private List<Athlete>  _athletes = new List<Athlete>{
                new Athlete{
                    id = 1,
                    name = "Athlete 1",
                    isWarned = false,
                    isStopped = false,
                    speedLevel = 0,
                    shuttleNumber = 0
                },
                new Athlete{
                    id = 2,
                    name = "Athlete 2",
                    isWarned = false,
                    isStopped = false,
                    speedLevel = 0,
                    shuttleNumber = 0
                },
                new Athlete{
                    id = 3,
                    name = "Athlete 3",
                    isWarned = false,
                    isStopped = false,
                    speedLevel = 0,
                    shuttleNumber = 0
                },
                new Athlete{
                    id = 4,
                    name = "Athlete 4",
                    isWarned = false,
                    isStopped = false,
                    speedLevel = 0,
                    shuttleNumber = 0
                }
            };

       
        public List<Athlete> GetAthletesList()
        {
            return _athletes;

        }

        public List<FitnessRating> GetFitnessRatings()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Data\\fitnessrating_beeptest.json");
             var json = System.IO.File.ReadAllText(filePath);
           return JsonConvert.DeserializeObject<List<FitnessRating>>(json);
        }

        public Athlete Warn(int id)
        {
           var athlete = _athletes.Where(ath => ath.id == id).FirstOrDefault();
           athlete.isWarned = true;
            return athlete;
        }

        public Athlete Stop(int id,FitnessRating fitnessRating)
        {
           var athlete = _athletes.Where(ath => ath.id == id).FirstOrDefault();
           athlete.isStopped = true;
           athlete.speedLevel=Convert.ToInt32(fitnessRating.speedLevel);
           athlete.shuttleNumber=Convert.ToInt32(fitnessRating.shuttleNo);
            return athlete;
        }
    }
}