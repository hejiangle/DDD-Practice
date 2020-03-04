using System;
using System.Collections.Generic;
using ParkLot.Domain.Entities;
using ParkLot.Domain.ValueObjects;
using ParkLot.Infrastructure.Repositories;

namespace ParkLot.ApplicationService
{
    public class ParkingBoy
    {
        private readonly IParkingLotRepository _parkingLotRepository;

        public ParkingBoy(IParkingLotRepository parkingLotRepository)
        {
            _parkingLotRepository = parkingLotRepository;
        }

        public Ticket Parking(Car car, string parkingLotAddress)
        {
            var specificParkingLot = _parkingLotRepository.GetParkingLotByAddress(parkingLotAddress);

            if (specificParkingLot.IsAvailable)
            {
                var ticket = specificParkingLot.ReceiveCar(car);

                _parkingLotRepository.UpdateParkingLotSpaceInfo(ticket);
                
                return ticket;
            }

            throw new Exception("The given parking lot is not available to park car.");
        }
        
        public string SearchParkingLot(ParkingLotSearcher searcher)
        {
            var parkingLots = _parkingLotRepository.GetAllParkingLots();
            
            return searcher.Search(parkingLots).Address;
        }
    }
}