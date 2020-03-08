using System;
using System.Collections.Generic;
using Moq;
using ParkingLot.Tests.TestHelpers;
using ParkLot;
using ParkLot.ApplicationService;
using ParkLot.Domain.ValueObjects;
using ParkLot.Infrastructure.Repositories;
using Xunit;

namespace ParkingLot.Tests
{
    public class JuniorParkingBoyTests
    {
        private readonly Mock<IParkingLotRepository> _mockParkingLotRepository;
        private readonly ParkingBoyManager _parkingBoyManager;

        public JuniorParkingBoyTests()
        {
            _mockParkingLotRepository = new Mock<IParkingLotRepository>();
            _parkingBoyManager = new ParkingBoyManager(_mockParkingLotRepository.Object);
        }

        [Fact]
        public void ShouldParkingInTheFirstParkingLot()
        {
            var parkingLots = ParkingLotTestDataBuilder
                .Create()
                .SetCountOfParkingLots(2)
                .SetCountOfEachParkingSpace(20)
                .Build();
            
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(parkingLots);
            _mockParkingLotRepository
                .Setup(x => x.GetParkingLotByAddress(It.IsAny<string>()))
                .Returns(parkingLots[0]);
            
            var parkingBoy = _parkingBoyManager.Assign(ParkingBoyType.Junior);

            var address = parkingBoy.SearchParkingLot();
            var ticket = parkingBoy.Parking(new Car("QM.AE86"), address);
            
            Assert.Equal("QM.AE86", ticket.PlateNumber);
            Assert.Equal("A road", ticket.ParkLotAddress);
        }

        [Fact]
        public void ShouldParkingInTheAvailableParkingLotWhenTheFirstParkingLotIsUnavailable()
        {
            var parkingLots = ParkingLotTestDataBuilder
                .Create()
                .SetCountOfParkingLots(2)
                .SetCountOfEachParkingSpace(20)
                .SetUnavailableParkingAddress("A road")
                .Build();
            
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(parkingLots);
            _mockParkingLotRepository
                .Setup(x => x.GetParkingLotByAddress(It.IsAny<string>()))
                .Returns(parkingLots[1]);
            
            var parkingBoy = _parkingBoyManager.Assign(ParkingBoyType.Junior);


            var address = parkingBoy.SearchParkingLot();
            var ticket = parkingBoy.Parking(new Car("QM.AE86"), address);
            
            Assert.Equal("QM.AE86", ticket.PlateNumber);
            Assert.Equal("B street", ticket.ParkLotAddress);
        }

        [Fact]
        public void ShouldGetParkingCarWhenTicketIsValid()
        {
            var parkingLots = ParkingLotTestDataBuilder
                .Create()
                .SetCountOfParkingLots(2)
                .SetCountOfEachParkingSpace(20)
                .Build();
            
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(parkingLots);
            _mockParkingLotRepository
                .Setup(x => x.GetParkingLotByAddress(It.IsAny<string>()))
                .Returns(parkingLots[0]);
            
            var parkingBoy = _parkingBoyManager.Assign(ParkingBoyType.Junior);
            
            var address = parkingBoy.SearchParkingLot();
            var ticket = parkingBoy.Parking(new Car("QM.AE86"), address);

            var car = parkingLots
                .Find(parkingLot => parkingLot.Address.Equals(ticket.ParkLotAddress))
                .TakeCar(ticket);
            
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
                    .Build());
            
            var parkingLots = _mockParkingLotRepository.Object.GetAllParkingLots();

            var ticket = new Ticket(parkingLots[0].Address, new Car("QM.AE86").PlateNumber);
                
            Assert.Throws<Exception>(() => parkingLots[0].TakeCar(ticket));
        }
    }
}