using System.Linq;
using ParkLot.Domain.Entities;

namespace ParkLot.Domain.ValueObjects
{
    public partial class Ticket
    {
        public bool IsValidTicket(ParkingLot parkingLot)
        {
            return parkingLot.Address.Equals(ParkLotAddress)
                   && parkingLot.ParkingCars.Any(car => car.PlateNumber.Equals(PlateNumber));
        }
    }
}