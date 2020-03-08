using System.Collections.Generic;
using ParkLot.Domain.Entities;

namespace ParkLot.Domain.ValueObjects
{
    public interface IParkingLotSearcher
    {
        ParkingLot Search(List<ParkingLot> parkingLots);
    }
}