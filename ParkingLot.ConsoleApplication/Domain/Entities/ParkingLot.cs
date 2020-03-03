using System.Collections.Generic;
using System.Linq;

namespace ParkLot.Domain.Entities
{
    public partial class ParkingLot
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
    }
}