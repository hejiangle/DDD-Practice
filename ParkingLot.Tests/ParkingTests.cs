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
    public class ParkingTests
    {
        private readonly Mock<IParkingLotRepository> _mockParkingLotRepository;
        private readonly Mock<ParkingLotSearcher> _mockParkingLotSearcher;
        
        public ParkingTests()
        {
            _mockParkingLotRepository = new Mock<IParkingLotRepository>();
            _mockParkingLotSearcher = new Mock<ParkingLotSearcher>();
        }

        [Fact]
        public void ShouldParkingInTheFirstCarSapceInTheFirstParkingLot()
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
            
            _mockParkingLotSearcher
                .Setup(x => x.Search(It.IsAny<List<ParkLot.Domain.Entities.ParkingLot>>()))
                .Returns(new ParkLot.Domain.Entities.ParkingLot("A road", 20));
            
            var parkingBoy = new ParkingBoy(_mockParkingLotRepository.Object);

            var address = parkingBoy.SearchParkingLot(_mockParkingLotSearcher.Object);
            var ticket = parkingBoy.Parking(new Car("QM.AE86"), address);
            
            Assert.Equal("QM.AE86", ticket.PlateNumber);
            Assert.Equal("A road", ticket.ParkLotAddress);
        }

        [Fact]
        public void ShouldParkingInTheFirstSpaceInTheAvailableParkingLotWhenTheFirstParkingLotIsUnavailable()
        {
            var parkingLots = ParkingLotTestDataBuilder
                .Create()
                .SetCountOfParkingLots(2)
                .SetCountOfEachParkingSpace(20)
                .SetUnavailableParkingAddress("B street")
                .Build();
            
            _mockParkingLotRepository
                .Setup(repository => repository.GetAllParkingLots())
                .Returns(parkingLots);
            _mockParkingLotRepository
                .Setup(x => x.GetParkingLotByAddress(It.IsAny<string>()))
                .Returns(parkingLots[1]);
            
            _mockParkingLotSearcher
                .Setup(x => x.Search(It.IsAny<List<ParkLot.Domain.Entities.ParkingLot>>()))
                .Returns(new ParkLot.Domain.Entities.ParkingLot("A road", 20));
            
            var parkingBoy = new ParkingBoy(_mockParkingLotRepository.Object);

            var address = parkingBoy.SearchParkingLot(_mockParkingLotSearcher.Object);
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
            
            _mockParkingLotSearcher
                .Setup(x => x.Search(It.IsAny<List<ParkLot.Domain.Entities.ParkingLot>>()))
                .Returns(new ParkLot.Domain.Entities.ParkingLot("A road", 20));
            
            var parkingBoy = new ParkingBoy(_mockParkingLotRepository.Object);

            var address = parkingBoy.SearchParkingLot(_mockParkingLotSearcher.Object);
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