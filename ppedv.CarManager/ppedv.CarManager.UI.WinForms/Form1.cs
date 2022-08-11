using ppedv.CarManager.Logic.CarService;
using ppedv.CarManager.Model;

namespace ppedv.CarManager.UI.WinForms
{
    public partial class Form1 : Form
    {
        CarService carService = new CarService(new Data.EfCore.EfRepository());

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = carService.Repository.GetAll<Car>();
        }
    }
}