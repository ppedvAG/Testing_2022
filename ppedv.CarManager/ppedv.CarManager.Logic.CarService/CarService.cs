using ppedv.CarManager.Model;

namespace ppedv.CarManager.Logic.CarService
{
    public class CarService
    {

        public void SetNewColor(Car car, string newColor)
        {
            if(car==null)
                throw new ArgumentNullException("car");

            if (string.IsNullOrWhiteSpace(newColor))
                throw new ArgumentNullException("newcolor");

            if (newColor.ToLower() == "pink")
                throw new ArgumentException("Pinke Autos sind out!");

            car.Color = newColor;
        }
    }
}