using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace course
{
    public partial class EditDeviceForm : Form
    {
        private List<DisplayDevice> devices;

        public EditDeviceForm(List<DisplayDevice> devices)
        {
            InitializeComponent();
            this.devices = devices;
        }

        private void buttonFind_Click(object sender, EventArgs e)
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

            using (var form = new DeviceForm(device))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Оновлюємо пристрій у списку
                    int idx = devices.FindIndex(d => d.Number == number);
                    if (idx >= 0)
                        devices[idx] = form.Device;

                    MessageBox.Show("Пристрій оновлено!");
                }
            }
        }
    }
}

