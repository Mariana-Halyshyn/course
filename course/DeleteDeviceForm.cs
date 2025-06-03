using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace course
{
    public partial class DeleteDeviceForm : Form
    {
        private List<DisplayDevice> devices;

        public DeleteDeviceForm(List<DisplayDevice> devices)
        {
            InitializeComponent();
            this.devices = devices;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxNumber.Text, out int number))
            {
                MessageBox.Show("Введіть коректний номер");
                return;
            }

            var device = devices.FirstOrDefault(d => d.Number == number);
            if (device == null)
            {
                MessageBox.Show("Пристрій не знайдено");
                return;
            }

            devices.Remove(device);
            MessageBox.Show("Пристрій видалено!");
        }
    }
}

