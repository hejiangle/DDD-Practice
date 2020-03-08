using ParkLot.Domain.ValueObjects;
using ParkLot.Infrastructure.Repositories;

namespace ParkLot.ApplicationService
{
    public class ParkingBoyManager
    {
        private readonly IParkingLotRepository _parkingLotRepository;

        public ParkingBoyManager(IParkingLotRepository parkingLotRepository)
        {
            _parkingLotRepository = parkingLotRepository;
        }

        public ParkingBoy Assign(ParkingBoyType type)
        {
            var parkingLots = _parkingLotRepository.GetAllParkingLots();
            
            return new ParkingBoy(parkingLots, type);
        }
    }
}