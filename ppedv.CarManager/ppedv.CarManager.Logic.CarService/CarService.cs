using ppedv.CarManager.Model;
using ppedv.CarManager.Model.Contracts;

namespace ppedv.CarManager.Logic.CarService
{
    public class CarService
    {
        public IRepository Repository { get; }

        public CarService(IRepository repository)
        {
            Repository = repository;
        }

        public Garage? GetGarageWithLeastCars()
        {
            return Repository.GetAll<Garage>()
                             .OrderBy(x => x.Cars.Count)
                             .ThenByDescending(x => x.Cars.Sum(y => y.Doors))
                             .FirstOrDefault();
        }


        public void SetNewColor(Car car, string newColor)
        {
            if (car == null)
                throw new ArgumentNullException("car");

            if (string.IsNullOrWhiteSpace(newColor))
                throw new ArgumentNullException("newcolor");

            if (newColor.ToLower() == "pink")
                throw new ArgumentException("Pinke Autos sind out!");

            car.Color = newColor;
        }

        public Garage GetRandomGarage()
        {
            return Repository.GetById<Garage>(Random.Shared.Next());
        }
    }
}