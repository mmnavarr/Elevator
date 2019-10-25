using System;
using System.Collections.Generic;

using Elevator.enums;

namespace Elevator
{
    public class Elevator : IElevator
    {
        // Member variables
        public string Id { get; }
        public double TotalWeightCapacity { get; }
        public double Weight { get; set; }
        public int TotalPersonCapacity { get; }
        public int Capacity { get; set; }
        public int TopFloor { get; }
        public int BottomFloor { get; }
        public bool IsFull { get; set; }
        public ElevatorStatus Status { get; set; }


        // Constructor
        public Elevator(string id, int totalWeightCapacity, int totalPersonCapacity, int topFloor, int bottomFloor, bool isFull) {
            Id = id;
            TotalWeightCapacity = totalPersonCapacity;
            TotalPersonCapacity = totalPersonCapacity;
            IsFull = isFull;
            // Set top and lowest floor
            TopFloor = topFloor;
            BottomFloor = bottomFloor;
            // Set direction as IDLE for after instanciating
            Status = ElevatorStatus.IDLE;
            // Default 0 weight and capacity
            Weight = 0.0;
            Capacity = 0;
        }

        // Display Elevator string information
        public override string ToString() {
            return $"Elevator ID#: {Id}";
        }

        // Activates or deactivates emergency status
        public void ToggleEmergency() {
            // Determine wether to activate or disable Emergency status
            if (Status == ElevatorStatus.EMERGENCY)
            {
                Status = ElevatorStatus.IDLE;
                return;
            } else {
                Status = ElevatorStatus.EMERGENCY;
                return;
            }
        }

        // Location of the elevator based on floor
        private int _floorLocation;

        // List of people that are on the elevator
        // E.g. { floor: [ { personId, Person } ] }
        private SortedList<int, Dictionary<int, Person>> _occupants = new SortedList<int, Dictionary<int, Person>>();


        // Add and remove elevator weight
        private void AddWeight(double weight)
        {
            Weight += weight;
            
            // Check if new weight is above capacity, and make a beeping sound
            if (Weight > TotalWeightCapacity) {
                Console.WriteLine("BEEEEEEP");
            }
        }
        private void RemoveWeight(double weight)
        {
            Weight -= weight;
        }

        // Add and remove person capacity
        private void AddCapacity()
        {
            Capacity++;

            // Check if new people count is above capacity, and make a beeping sound
            if (Capacity > TotalPersonCapacity) {
                Console.WriteLine("BEEEEEEP");
            }
        }
        private void RemoveCapacity()
        {
            Capacity--;
        }

        // Add and remove people from elevator
        public void AddPerson(Person person)
        {
            // Add to weight and capacity counters
            AddWeight(person.Weight);
            AddCapacity();

            // Get the dictionary of occupants for the destination floors with default value
            Dictionary<int, Person> destinationOccupants = _occupants.GetValueOrDefault(person.DestinationFloor, new Dictionary<int, Person>());

            // Add person to the destination occupants list
            destinationOccupants[person.Id] = person;

            // Set the destination floor occupants with added person
            _occupants[person.DestinationFloor] = destinationOccupants;
        }
        public void RemovePerson(Person person)
        {
            // Remove from weight and capacity counters
            RemoveWeight(person.Weight);
            RemoveCapacity();

            // Get the dictionary of occupants for the destination floors with default value
            Dictionary<int, Person> destinationOccupants = _occupants.GetValueOrDefault(person.DestinationFloor, new Dictionary<int, Person>());

            // If person does not exist, happy halloween they're a ghost they shouldnt be on the elevator this is an error
            if (!destinationOccupants.ContainsKey(person.Id)) return;

            // Remove person from destination occupants
            destinationOccupants.Remove(person.Id);

             // Set the destination floor occupants with removed person
            _occupants[person.DestinationFloor] = destinationOccupants;
        }

        // Move up and move down functionality
        public void MoveUp() {
            // Short circuit if trying to go up on the top floor by reversing status (direction in this case)
            if (_floorLocation + 1 > TopFloor) {
                Status = ElevatorStatus.DESCENDING;
                return;
            }
            
            // Increment the current floor
            _floorLocation++;

            // Set elevator to ascending status
            Status = ElevatorStatus.ASCENDING;
        }

        public void MoveDown() {
            // Short circuit if trying to go down past lowest floor by reversing status (direction in this case)
            if (_floorLocation - 1 < BottomFloor) {
                Status = ElevatorStatus.ASCENDING;
                return;
            }
            
            // Decrement the current floor
            _floorLocation--;

            // Set elevator to desscending status
            Status = ElevatorStatus.DESCENDING;
        }

        // Check floor for occupants that should get off the elevator
        public void CheckFloor() {
            // Loop through occupants array and remove those that belong to this floor

            // Check that there are people for this floor otherwise, keep moving
            if (_occupants[_floorLocation].Count == 0) return;

            // Clear persons for the target floor with blank dictionary
            _occupants[_floorLocation] = new Dictionary<int, Person>();
        }
    }
}