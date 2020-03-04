using System.Collections.Generic;
using ParkLot;
using ParkLot.Domain.Entities;
using ParkLot.Domain.ValueObjects;

namespace ParkingLot.Tests.TestHelpers
{
    public class ParkingLotTestDataBuilder
    {
        private static readonly List<string> PARKING_LOT_ADDRESSES = new List<string>
        {
            "A road", "B street", "C cross", "D underground"
        };

        private const string TEST_CAR_PLATE_NUMBER = "Test Car";

        private int _countOfParkingLots;
        private short _spaceCountOfEachParkingLot;
        private string _unavailableParkingAddress;

        public static ParkingLotTestDataBuilder Create() => new ParkingLotTestDataBuilder();

        public ParkingLotTestDataBuilder SetCountOfParkingLots(int count)
        {
            _countOfParkingLots = count;
            return this;
        }

        public ParkingLotTestDataBuilder SetCountOfEachParkingSpace(short count)
        {
            _spaceCountOfEachParkingLot = count;
            return this;
        }

        public ParkingLotTestDataBuilder SetUnavailableParkingAddress(string address)
        {
            _unavailableParkingAddress = address;
            return this;
        }

        //Code smell in here.
        public List<ParkLot.Domain.Entities.ParkingLot> Build()
        {
            var result = new List<ParkLot.Domain.Entities.ParkingLot>();

            for (var i = 0; i < _countOfParkingLots; i++)
            {
                var spaces = new List<Car>(_spaceCountOfEachParkingLot);

                if (PARKING_LOT_ADDRESSES[i].Equals(_unavailableParkingAddress))
                {
                    for (var j = 0; j < _spaceCountOfEachParkingLot; j++)
                    {
                        spaces.Add(new Car(TEST_CAR_PLATE_NUMBER));
                    }
                }

                result.Add(new ParkLot.Domain.Entities.ParkingLot(PARKING_LOT_ADDRESSES[i], _spaceCountOfEachParkingLot));
            }

            return result;
        }
    }
}