using System;

namespace ParkLot.Domain.ValueObjects
{
    public class Ticket
    {
        public Ticket(DateTime parkingStartTime,
            string parkLotAddress, string spaceCode,
            string plateNumber, int duration)
        {
            ParkingStartTime = parkingStartTime;
            ParkLotAddress = parkLotAddress;
            SpaceCode = spaceCode;
            PlateNumber = plateNumber;
            Duration = duration;
        }
       

        public string ParkLotAddress { get; }

        public string SpaceCode { get; }

        public string PlateNumber { get; }

        public DateTime ParkingStartTime { get; }

        public int Duration { get; }
    }
}