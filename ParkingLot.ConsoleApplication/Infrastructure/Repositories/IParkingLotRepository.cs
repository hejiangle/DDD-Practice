using System.Collections.Generic;
using ParkLot.Domain.Entities;
using ParkLot.Domain.ValueObjects;

namespace ParkLot.Infrastructure.Repositories
{
    public interface IParkingLotRepository
    {
        List<ParkingLot> GetAllParkingLots();

        void UpdateParkingLotSpaceInfo(Ticket ticket);
    }
}