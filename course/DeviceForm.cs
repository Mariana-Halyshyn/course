//using System;
//using System.Windows.Forms;

//namespace course
//{
//    public partial class DeviceForm : Form
//    {
//        public DisplayDevice Device { get; private set; }

//        public DeviceForm(DisplayDevice device = null)
//        {
//            InitializeComponent();

//            if (device != null)
//            {
//                textBoxName.Text = device.Name;
//                textBoxPower.Text = device.Power.ToString();
//                textBoxWeight.Text = device.Weight.ToString();
//                textBoxInterface.Text = device.Interface;
//                textBoxResolution.Text = device.Resolution;
//                Device = device;
//            }
//        }

//        private void DeviceForm_Load(object sender, EventArgs e)
//        {
//            // Можеш додати код, який буде виконуватись при завантаженні форми, якщо потрібно
//        }

//        private void buttonOK_Click(object sender, EventArgs e)
//        {
//            if (double.TryParse(textBoxPower.Text, out double power) &&
//                double.TryParse(textBoxWeight.Text, out double weight))
//            {
//                Device = new DisplayDevice
//                {
//                    Name = textBoxName.Text,
//                    Power = power,
//                    Weight = weight,
//                    Interface = textBoxInterface.Text,
//                    Resolution = textBoxResolution.Text
//                };
//                DialogResult = DialogResult.OK;
//            }
//            else
//            {
//                MessageBox.Show("Невірні значення потужності або ваги.");
//            }
//        }

//        private void buttonCancel_Click(object sender, EventArgs e)
//        {
//            DialogResult = DialogResult.Cancel;
//        }

//        // Порожні обробники подій для текстових полів (щоб уникнути помилок)
//        private void textBoxName_TextChanged(object sender, EventArgs e) { }
//        private void textBoxPower_TextChanged(object sender, EventArgs e) { }
//        private void textBoxWeight_TextChanged(object sender, EventArgs e) { }
//        private void textBoxInterface_TextChanged(object sender, EventArgs e) { }
//        private void textBoxResolution_TextChanged(object sender, EventArgs e) { }
//    }
//}




using System;
using System.Windows.Forms;

namespace course
{
    public partial class DeviceForm : Form
    {
        public DisplayDevice Device { get; private set; }

        public DeviceForm(DisplayDevice device)
        {
            InitializeComponent();
            Device = device;

            // Заповнюємо поля
            textBoxInterface.Text = Device.Interface;
            textBoxPower.Text = Device.Power.ToString();
            textBoxWeight.Text = Device.Weight.ToString();
            textBoxDiagonal.Text = Device.Diagonal.ToString();
            textBoxResolution.Text = Device.Resolution;
            textBoxDescription.Text = Device.Description;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxPower.Text, out double power) ||
                !double.TryParse(textBoxWeight.Text, out double weight) ||
                !double.TryParse(textBoxDiagonal.Text, out double diagonal))
            {
                MessageBox.Show("Некоректні числові значення.");
                return;
            }

            Device.Interface = textBoxInterface.Text;
            Device.Power = power;
            Device.Weight = weight;
            Device.Diagonal = diagonal;
            Device.Resolution = textBoxResolution.Text;
            Device.Description = textBoxDescription.Text;

            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBoxDiagonal_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

