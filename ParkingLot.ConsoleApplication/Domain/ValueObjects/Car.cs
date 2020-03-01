namespace ParkLot
{
    public class Car
    {
        public Car(string plateNumber)
        {
            PlateNumber = plateNumber;
        }

        public string PlateNumber { get;}

        public override bool Equals(object obj)
        {
            if (!(obj is Car car))
            {
                return false;
            }

            return PlateNumber.Equals(car.PlateNumber);
            
        }

        public override int GetHashCode()
        {
            return PlateNumber.GetHashCode();
        }
    }
}