namespace Elevator
{
    public class Person : IPerson
    {
        // Member variables
        public int Id { get; }
        public double Weight { get; }
        public int DestinationFloor { get; set; }

        public Person(int id, double weight, int destinationFloor)
        {
            Id = id;
            Weight = weight;
            DestinationFloor = destinationFloor;
        }
    }
}