using System;
using System.Collections.Generic;
using System.Linq;
using ParkLot.Domain.Entities;
using ParkLot.Domain.ValueObjects;

namespace ParkLot.Domain
{
    public class ParkingLotSearcherFactory
    {
        private readonly IParkingLotSearcher _juniorParkingLotSearcher;
        private readonly IParkingLotSearcher _seniorParkingLotSearcher;
        
        public ParkingLotSearcherFactory()
        {
            _juniorParkingLotSearcher = new JuniorParkingLotSearcher();
            _seniorParkingLotSearcher = new SeniorParkingLotSearcher();
        }

        public IParkingLotSearcher Create(ParkingBoyType type)
        {
            switch (type)
            {
                case ParkingBoyType.Junior:
                    return _juniorParkingLotSearcher;
                case ParkingBoyType.Senior:
                    return _seniorParkingLotSearcher;
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