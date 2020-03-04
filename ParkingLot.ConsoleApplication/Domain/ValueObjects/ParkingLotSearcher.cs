using System.Collections.Generic;
using ParkLot.Domain.Entities;

namespace ParkLot.Domain.ValueObjects
{
    public abstract class ParkingLotSearcher
    {
        public abstract ParkingLot Search(List<ParkingLot> parkingLots);
    }
}