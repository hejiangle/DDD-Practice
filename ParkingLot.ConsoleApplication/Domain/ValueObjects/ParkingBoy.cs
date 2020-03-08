using System;
using System.Collections.Generic;
using ParkLot.Domain.Entities;

namespace ParkLot.Domain.ValueObjects
{
    public class ParkingBoy
    {
        private readonly IParkingLotSearcher _parkingLotSearcher;
        private readonly List<ParkingLot> _parkingLots;

        public ParkingBoy(List<ParkingLot> parkingLots, ParkingBoyType type)
        {
            _parkingLots = parkingLots;
            _parkingLotSearcher = new ParkingLotSearcherFactory().Create(type);
        }

        public Ticket Parking(Car car, string parkingLotAddress)
        {
            var specificParkingLot = _parkingLots.Find(
                parkingLot => parkingLot.Address.Equals(parkingLotAddress));

            if (specificParkingLot.IsAvailable)
            {
                var ticket = specificParkingLot.ReceiveCar(car);

//                _parkingLotRepository.UpdateParkingLotSpaceInfo(ticket);
                
                return ticket;
            }

            throw new Exception("The given parking lot is not available to park car.");
        }
        
        public string SearchParkingLot()
        {
            return _parkingLotSearcher.Search(_parkingLots).Address;
        }
    }
}