namespace Elevator
{
    public interface IPerson
    {
        int Id { get; }

        // Weight in pounds for elevate safety check
        double Weight { get; }
        // Floor inputted by person when they got on the elevator
        int DestinationFloor { get; set; } // TODO: Possibly use an array since the person can press multiple buttons
    }
}