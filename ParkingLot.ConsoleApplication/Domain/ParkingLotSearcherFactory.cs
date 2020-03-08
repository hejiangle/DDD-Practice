using System;
using System.Collections.Generic;
using System.Linq;
using ParkLot.Domain.Entities;
using ParkLot.Domain.ValueObjects;

namespace ParkLot.Domain
{
    public static class ParkingLotSearcherFactory
    {
        public static IParkingLotSearcher Create(ParkingBoyType type)
        {
            switch (type)
            {
                case ParkingBoyType.Junior:
                    return new JuniorParkingLotSearcher();
                case ParkingBoyType.Senior:
                    return new SeniorParkingLotSearcher();
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(type), type, "No such parking boy type.");
            }
        }
    }

    public class SeniorParkingLotSearcher : IParkingLotSearcher
    {
        public ParkingLot Search(List<ParkingLot> parkingLots)
        {
            throw new NotImplementedException();
        }
    }

    public class JuniorParkingLotSearcher : IParkingLotSearcher
    {
        public ParkingLot Search(List<ParkingLot> parkingLots)
        {
            return parkingLots.First(parkingLot => parkingLot.IsAvailable);
        }
    }
}