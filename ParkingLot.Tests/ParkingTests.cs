using System;
using Moq;
using ParkingLot.Tests.TestHelpers;
using ParkLot;
using ParkLot.Infrastructure.Factories;
using ParkLot.Infrastructure.Repositories;
using Xunit;

namespace ParkingLot.Tests
{
    public class ParkingTests
    {
        private readonly Mock<IParkingLotRepository> _mockParkingLotRepository;
        
        public ParkingTests()
        {
            _mockParkingLotRepository = new Mock<IParkingLotRepository>();
        }

        [Fact]
        public void ShouldParkingInTheFirstCarSapceInTheFirstParkingLot()
        {
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(ParkingLotTestDataBuilder
                    .Create()
                    .SetCountOfParkingLots(2)
                    .SetCountOfEachParkingSpace(20)
                    .Build());
            
            var parkingLots = _mockParkingLotRepository.Object.GetAllParkingLots();
            
            var parkingBoy = ParkingBoyFactory.CreateParkingBoy(parkingLots);

            var ticket = parkingBoy.Parking(new Car("QM.AE86"));
            
            Assert.Equal("QM.AE86", ticket.PlateNumber);
            Assert.Equal("A road", ticket.ParkLotAddress);
            Assert.Equal("A 0", ticket.SpaceCode);
            Assert.Equal(60, ticket.Duration);
            Assert.Equal(DateTime.UtcNow.Date, ticket.ParkingStartTime.Date);
        }

        [Fact]
        public void ShouldParkingInTheFirstSpaceInTheAvailableParkingLotWhenTheFirstParkingLotIsUnavailable()
        {
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(ParkingLotTestDataBuilder
                    .Create()
                    .SetCountOfParkingLots(2)
                    .SetCountOfEachParkingSpace(20)
                    .SetUnavailableParkingAddress("A road")
                    .Build());
            
            var parkingLots = _mockParkingLotRepository.Object.GetAllParkingLots();
            
            var parkingBoy = ParkingBoyFactory.CreateParkingBoy(parkingLots);

            var ticket = parkingBoy.Parking(new Car("QM.AE86"));
            
            Assert.Equal("QM.AE86", ticket.PlateNumber);
            Assert.Equal("B street", ticket.ParkLotAddress);
            Assert.Equal("A 0", ticket.SpaceCode);
            Assert.Equal(60, ticket.Duration);
            Assert.Equal(DateTime.UtcNow.Date, ticket.ParkingStartTime.Date);
        }

        [Fact]
        public void ShouldParkingInTheNextSpaceInTheFirstAvailableParkingLotWhenTheFirstSpaceIsUnavailable()
        {
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(ParkingLotTestDataBuilder
                    .Create()
                    .SetCountOfParkingLots(2)
                    .SetCountOfEachParkingSpace(20)
                    .SetUnavailableSpaceCode("A 0")
                    .Build());
            
            var parkingLots = _mockParkingLotRepository.Object.GetAllParkingLots();
            
            var parkingBoy = ParkingBoyFactory.CreateParkingBoy(parkingLots);

            var ticket = parkingBoy.Parking(new Car("QM.AE86"));
            
            Assert.Equal("QM.AE86", ticket.PlateNumber);
            Assert.Equal("A road", ticket.ParkLotAddress);
            Assert.Equal("A 1", ticket.SpaceCode);
            Assert.Equal(60, ticket.Duration);
            Assert.Equal(DateTime.UtcNow.Date, ticket.ParkingStartTime.Date);
        }

        [Fact]
        public void ShouldGetParkingCarWhenTicketIsValid()
        {
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(ParkingLotTestDataBuilder
                    .Create()
                    .SetCountOfParkingLots(2)
                    .SetCountOfEachParkingSpace(20)
                    .SetUnavailableSpaceCode("A 0")
                    .Build());
            
            var parkingLots = _mockParkingLotRepository.Object.GetAllParkingLots();
            
            var parkingBoy = ParkingBoyFactory.CreateParkingBoy(parkingLots);

            var ticket = parkingBoy.Parking(new Car("QM.AE86"));

            var car = parkingLots
                .Find(parkingLot => parkingLot.Address.Equals(ticket.ParkLotAddress))
                .GetCar(ticket);
            
            Assert.Equal("QM.AE86", car.PlateNumber);
        }

        [Fact]
        public void ShouldThrowExceptionWhenTicketIsInvalid()
        {
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(ParkingLotTestDataBuilder
                    .Create()
                    .SetCountOfParkingLots(2)
                    .SetCountOfEachParkingSpace(20)
                    .SetUnavailableSpaceCode("A 0")
                    .Build());
            
            var parkingLots = _mockParkingLotRepository.Object.GetAllParkingLots();
            var parkingBoy = ParkingBoyFactory.CreateParkingBoy(parkingLots);
            parkingBoy.Parking(new Car("QM.AE86"));
            
            var ticket = TicketFactory.CreateTicket(parkingLots[0], new Car("QM.AE86"), -15);

            Assert.Throws<Exception>(() => parkingLots[0].GetCar(ticket));
        }
    }
}