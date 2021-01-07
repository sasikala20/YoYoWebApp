using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YoYo_Web_App.Models;
using YoYo_Web_App.Services.Interfaces;

namespace YoYo_Web_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AthleteController : ControllerBase
    {
        private IAthleteService _athleteService;

        public AthleteController(IAthleteService athleteService)
        {
            this._athleteService = athleteService;
        }

        [HttpGet("GetAthletes")]
        public List<Athlete> GetAthletes()
        {
           return _athleteService.GetAthletesList();
        }

         [HttpGet("GetRatings")]
        public List<FitnessRating> GetRatings()
        {
           return _athleteService.GetFitnessRatings();
        }

         [HttpPost("Warn")]
        public Athlete Warn(int id)
        {
           return _athleteService.Warn(id);
        }

         [HttpPost("Stop")]
        public Athlete Stop(int id,FitnessRating previousFitnessrating)
        {
           return _athleteService.Stop(id,previousFitnessrating);
        }
    }
}
