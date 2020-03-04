using System;
using ParkLot.Domain.ValueObjects;

namespace ParkLot.Domain.Entities
{
    public partial class ParkingLot
    {
        public Ticket ReceiveCar(Car car)
        {
            ParkingCars.Add(car);
            
            return new Ticket(Address, car.PlateNumber);
        }

        public Car TakeCar(Ticket ticket)
        {
            if (ticket.IsValidTicket(this))
            {
                var car = ParkingCars.Find(
                    parkingCar => parkingCar.PlateNumber.Equals(ticket.PlateNumber));
                ParkingCars.Remove(car);
                
                return car;
            }
            
            throw new Exception("Invalid ticket.");
        }
    }
}