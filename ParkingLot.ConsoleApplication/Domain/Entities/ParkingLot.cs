using System;
using System.Collections.Generic;
using System.Linq;
using ParkLot.Domain.ValueObjects;
using ParkLot.Infrastructure.Factories;

namespace ParkLot.Domain.Entities
{
    public class ParkingLot
    {
        private const int DEFAULT_PARKING_DURATION_IN_MINUTE = 60;
        
        public ParkingLot(List<Space> carSpaces, string address)
        {
            CarSpaces = carSpaces;
            Address = address;
        }

        public string Address { get; }

        public List<Space> CarSpaces { get; }

        public bool IsAvailable => CarSpaces.Any(space => space.IsAvailable);

        public Ticket ReceiveCar(Car car)
        {
            var availableSpace = CarSpaces.First(space => space.IsAvailable);
            
            availableSpace.ParkingCar = car;
            
            return TicketFactory.CreateTicket(this, car, DEFAULT_PARKING_DURATION_IN_MINUTE);
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