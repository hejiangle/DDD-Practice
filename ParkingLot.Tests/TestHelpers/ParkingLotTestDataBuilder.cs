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

        private static readonly List<string> SPACE_CODE_PREFIX = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"
        };

        private static readonly List<string> SPACE_CODE_SUFFIX = new List<string>
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };

        private const string TEST_CAR_PLATE_NUMBER = "Test Car";

        private int _countOfParkingLots;
        private int _spaceCountOfEachParkingLot;
        private string _unavailableParkingAddress;
        private string _unavailableCodeForAllParkingLots;

        public static ParkingLotTestDataBuilder Create() => new ParkingLotTestDataBuilder();

        public ParkingLotTestDataBuilder SetCountOfParkingLots(int count)
        {
            _countOfParkingLots = count;
            return this;
        }

        public ParkingLotTestDataBuilder SetCountOfEachParkingSpace(int count)
        {
            _spaceCountOfEachParkingLot = count;
            return this;
        }

        public ParkingLotTestDataBuilder SetUnavailableParkingAddress(string address)
        {
            _unavailableParkingAddress = address;
            return this;
        }

        public ParkingLotTestDataBuilder SetUnavailableSpaceCode(string code)
        {
            _unavailableCodeForAllParkingLots = code;
            return this;
        }

        //Code smell in here.
        public List<ParkLot.Domain.Entities.ParkingLot> Build()
        {
            var result = new List<ParkLot.Domain.Entities.ParkingLot>();

            for (var i = 0; i < _countOfParkingLots; i++)
            {
                var spaces = new List<Space>(_spaceCountOfEachParkingLot);

                foreach (var codePrefix in SPACE_CODE_PREFIX)
                {
                    foreach (var codeSuffix in SPACE_CODE_SUFFIX)
                    {
                        var code = $"{codePrefix} {codeSuffix}";
                        spaces.Add(code.Equals(_unavailableCodeForAllParkingLots)
                            ? new Space(code) {ParkingCar = new Car(TEST_CAR_PLATE_NUMBER)}
                            : new Space(code));

                        if (spaces.Count == _spaceCountOfEachParkingLot)
                        {
                            break;
                        }
                    }

                    if (spaces.Count == _spaceCountOfEachParkingLot)
                    {
                        break;
                    }
                }

                if (PARKING_LOT_ADDRESSES[i].Equals(_unavailableParkingAddress))
                {
                    spaces.ForEach(space => space.ParkingCar = new Car(TEST_CAR_PLATE_NUMBER));
                }

                result.Add(new ParkLot.Domain.Entities.ParkingLot(spaces, PARKING_LOT_ADDRESSES[i]));
            }

            return result;
        }
    }
}