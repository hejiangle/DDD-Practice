using System.Collections.Generic;
using System.Linq;

namespace ParkLot.Domain.Entities
{
    public partial class ParkingLot
    {
        public ParkingLot(string address, short capacity)
        {
            ParkingCars = new List<Car>(capacity);
            Address = address;
        }

        public string Address { get; }

        public List<Car> ParkingCars { get; }

        public bool IsAvailable => ParkingCars.Count < ParkingCars.Capacity;
    }
}