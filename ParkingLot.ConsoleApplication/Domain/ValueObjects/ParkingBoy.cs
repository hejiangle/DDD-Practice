using System;
using System.Collections.Generic;
using System.Linq;
using ParkLot.Domain.Entities;

namespace ParkLot.Domain.ValueObjects
{
    public class ParkingBoy
    {
        private readonly List<ParkingLot> _parkingLots;

        public ParkingBoy(List<ParkingLot> parkingLots)
        {
            _parkingLots = parkingLots;
        }

        public Ticket Parking(Car car, string parkingLotAddress = null)
        {
            return parkingLotAddress == null
                ? ParkingInAnyParkingLot(car)
                : ParkingInSpecificParkingLot(car, parkingLotAddress);
        }

        private Ticket ParkingInSpecificParkingLot(Car car, string parkingLotAddress)
        {
            throw new NotImplementedException();
        }

        private Ticket ParkingInAnyParkingLot(Car car)
        {
            var availableParkingLot = _parkingLots.FirstOrDefault(parkingLot => parkingLot.IsAvailable);

            if (availableParkingLot == null)
            {
                throw new Exception("No available parking lot to parking the car.");
            }

            return availableParkingLot.ReceiveCar(car);
        }
    }
}