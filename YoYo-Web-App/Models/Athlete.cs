namespace YoYo_Web_App.Models
{
public class Athlete
{
        public int id { get; set; }
        public string name { get; set; }
        public bool isWarned { get; set; }
        public bool isStopped { get; set; }
        public int speedLevel { get; set; }
        public int shuttleNumber { get; set; }
}

}