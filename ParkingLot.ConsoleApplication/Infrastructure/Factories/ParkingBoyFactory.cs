using System.Collections.Generic;
using ParkLot.Domain.Entities;
using ParkLot.Domain.ValueObjects;

namespace ParkLot.Infrastructure.Factories
{
    public static class ParkingBoyFactory
    {
        public static ParkingBoy CreateParkingBoy(List<ParkingLot> parkingLots)
        {
            return new ParkingBoy(parkingLots);
        }
    }
}