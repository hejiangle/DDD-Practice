using System;
using System.Linq;
using ParkLot.Domain.ValueObjects;
using ParkLot.Infrastructure.Factories;

namespace ParkLot.Domain.Entities
{
    public partial class ParkingLot
    {
        public Ticket ReceiveCar(Car car)
        {
            var availableSpace = CarSpaces.First<Space>(space => space.IsAvailable);
            
            availableSpace.ParkingCar = car;
            
            return TicketFactory.CreateTicket(this, car, Entities.ParkingLot.DEFAULT_PARKING_DURATION_IN_MINUTE);
        }

        public Car TakeCar(Ticket ticket)
        {
            //The following business logic can be refactored.
            var isCorrectParkingLot = Address.Equals(ticket.ParkLotAddress);
            var isStillInDuration = (DateTime.UtcNow - ticket.ParkingStartTime).TotalMinutes <= ticket.Duration;
            
            if (isCorrectParkingLot && isStillInDuration)
            {
                var carSpace = CarSpaces.Find(space => space.Code.Equals(ticket.SpaceCode));
                var car = carSpace.ParkingCar;
                carSpace.ParkingCar = null;
                
                return car;
            }
            
            throw new Exception("Invalid ticket.");
        }
    }
}