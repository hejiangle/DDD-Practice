using System;
using System.Linq;
using ParkLot.Domain.Entities;
using ParkLot.Domain.ValueObjects;

namespace ParkLot.Infrastructure.Factories
{
    public static class TicketFactory
    {
        public static Ticket CreateTicket(ParkingLot parkingLot, Car car, int duration)
        {
            var parkingSpaceCode = parkingLot.CarSpaces.First(space => car.Equals(space.ParkingCar)).Code;
            
            return new Ticket(DateTime.UtcNow, parkingLot.Address, parkingSpaceCode, car.PlateNumber, duration);
        }
    }
}