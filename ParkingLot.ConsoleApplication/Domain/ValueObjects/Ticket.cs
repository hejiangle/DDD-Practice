using System;

namespace ParkLot.Domain.ValueObjects
{
    public partial class Ticket
    {
        public Ticket(string parkLotAddress,
            string plateNumber)
        {
            ParkLotAddress = parkLotAddress;
            PlateNumber = plateNumber;
        }
       
        public string ParkLotAddress { get; }

        public string PlateNumber { get; }
    }
}