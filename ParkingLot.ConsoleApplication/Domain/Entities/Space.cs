namespace ParkLot.Domain.Entities
{
    //This class may be over-design...because no requirement mentioned.
    public class Space
    {
        public Space(string code)
        {
            Code = code;
        }

        public string Code { get; }
        
        public Car ParkingCar { get; set; }

        public bool IsAvailable => ParkingCar == null;
    }
}