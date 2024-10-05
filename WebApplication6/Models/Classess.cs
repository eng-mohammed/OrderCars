namespace WebApplication6.Models
{
    // Models/Order.cs
    public class Order
    {
        public int OrderId { get; set; }
        public string? OrderName { get; set; }
        public ICollection<OrderCar>? OrderCars { get; set; }  // Navigation property
    }

    // Models/Car.cs
    public class Car
    {
        public int CarId { get; set; }
        public string? CarName { get; set; }
        public ICollection<OrderCar>? OrderCars { get; set; }  // Navigation property
    }

    // Models/OrderCar.cs (junction table for Order and Car)
    public class OrderCar
    {
        public int OrderCarId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }

        public string? DriverName { get; set; }  // Input from form
        public DateTime CreatedDate { get; set; }
    }

    // ViewModels/OrderCarViewModel.cs
    public class OrderCarViewModel
    {
        public int OrderId { get; set; }
        public string? OrderName { get; set; }

        public List<CarViewModel>? Cars { get; set; }
    }

    public class CarViewModel
    {
        public int CarId { get; set; }
        public string? CarName { get; set; }
        public bool IsSelected { get; set; }  // Checkbox
        public string? DriverName { get; set; }  // Input field for Driver's Name
    }

}
