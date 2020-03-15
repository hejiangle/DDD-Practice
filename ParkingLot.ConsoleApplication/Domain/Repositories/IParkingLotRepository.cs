using System.Collections.Generic;
using ParkLot.Domain.Entities;
using ParkLot.Domain.ValueObjects;

namespace ParkLot.Infrastructure.Repositories
{
    public interface IParkingLotRepository
    {
        List<ParkingLot> GetAllParkingLots();

        ParkingLot GetParkingLotByAddress(string address);

        void UpdateParkingLotSpaceInfo(Ticket ticket);
    }
}