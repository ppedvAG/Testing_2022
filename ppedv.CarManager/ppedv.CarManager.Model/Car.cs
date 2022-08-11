namespace ppedv.CarManager.Model
{
    public class Car : Entity
    {
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string? Color { get; set; }
        public int KW { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public virtual Garage? Garage { get; set; }
    }

}