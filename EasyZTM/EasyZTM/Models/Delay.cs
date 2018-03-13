using System;

namespace EasyZTM.Models
{
    public class Delay
    {
        public string Id { get; set; }
        public string EstimatedTime { get; set; }
        public string Headsign { get; set; }
        public int RouteId { get; set; }
        public int TripId { get; set; }
        public string Status { get; set; }
        public string TheoreticalTime { get; set; }
        public string Timestamp { get; set; }
        public int Trip { get; set; }
        public int VehicleCode { get; set; }
        public int VehicleId { get; set; }
        public string Minutes
        {
            get { return CalculateMinutes(); }
            set { Minutes = value; }
        }

        public string CalculateMinutes()
        {
            TimeSpan estimetedMinutes = TimeSpan.Parse(EstimatedTime);
            TimeSpan diff = estimetedMinutes - DateTime.Now.TimeOfDay;
            if (diff.Minutes == 0 || diff.Minutes == 1)
                return "Odjazd!";
            else
                return $"za {Convert.ToString(diff.Minutes - 1)} min";
        }
    }
}