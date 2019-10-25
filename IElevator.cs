using Elevator.enums;

namespace Elevator
{
    public interface IElevator
    {
        string Id { get; }
        
        // Weight capacity of elevator in pounds
        double TotalWeightCapacity { get; }
        double Weight { get; set; }

        // Capacity of elevator in human persons
        int TotalPersonCapacity { get; }
        int Capacity { get; set; }
        
        // Top floor (max) and bottom floor (min) of elevator
        int TopFloor { get; }
        int BottomFloor { get; }

        // Flag determining if the elevator is full of people
        bool IsFull { get; set; }
        
        // Elevator status
        ElevatorStatus Status { get; set; }

        // Toggle emergency for the emergency button on the elevator
        void ToggleEmergency();

        // Basic ToString function to return Elevator and ID#
        string ToString();

        // Adding and Remove a person that enters the elevator
        void AddPerson(Person person);
        void RemovePerson(Person person);

        // Move up and down functions
        void MoveUp();
        void MoveDown();

        // Check floor for occupants that should get off
        void CheckFloor();
    }
}