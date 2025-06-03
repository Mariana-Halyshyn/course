//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;

//namespace course
//{
//    public partial class Form1 : Form
//    {
//        private DeviceContainer container = new DeviceContainer();

//        public Form1()
//        {
//            InitializeComponent();
//            RefreshList();
//        }

//        private void RefreshList()
//        {
//            listBoxDevices.Items.Clear();
//            listBoxDevices.Items.AddRange(container.GetAll().ToArray());
//        }

//        private void buttonAdd_Click(object sender, EventArgs e)
//        {
//            var form = new DeviceForm();
//            if (form.ShowDialog() == DialogResult.OK)
//            {
//                container.Add(form.Device);
//                RefreshList();
//            }
//        }

//        private void buttonEdit_Click(object sender, EventArgs e)
//        {
//            int index = listBoxDevices.SelectedIndex;
//            if (index >= 0)
//            {
//                var form = new DeviceForm(container.GetAll()[index]);
//                if (form.ShowDialog() == DialogResult.OK)
//                {
//                    container.Update(index, form.Device);
//                    RefreshList();
//                }
//}
//        }

//        private void buttonDelete_Click(object sender, EventArgs e)
//        {
//            int index = listBoxDevices.SelectedIndex;
//            if (index >= 0)
//            {
//                var result = MessageBox.Show("Видалити пристрій?", "Підтвердження", MessageBoxButtons.YesNo);
//                if (result == DialogResult.Yes)
//                {
//                    container.RemoveAt(index);
//                    RefreshList();
//                }
//            }
//        }
//    }
//}



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace course
{
    public partial class Form1 : Form
    {
        private List<DisplayDevice> devices = new List<DisplayDevice>();

        public Form1()
        {
            InitializeComponent();
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            listBoxDevices.Items.Clear();
            foreach (var d in devices)
                listBoxDevices.Items.Add(d.ToString());
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (!File.Exists("info.txt"))
            {
                MessageBox.Show("Файл info.txt не знайдено!");
                return;
            }

            devices.Clear();
            var lines = File.ReadAllLines("info.txt", Encoding.UTF8);
            int number = 1;
            foreach (var line in lines)
            {
                try
                {
                    var device = DisplayDevice.ParseFromFileLine(line, number++);
                    devices.Add(device);
                }
                catch
                {
                    MessageBox.Show("Помилка в рядку:\n" + line);
                }
            }
            UpdateListBox();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var lines = devices.Select(d => d.ToFileString()).ToArray();
            File.WriteAllLines("info.txt", lines, Encoding.UTF8);
            MessageBox.Show("Дані збережено у info.txt");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newDevice = new DisplayDevice { Number = devices.Any() ? devices.Max(d => d.Number) + 1 : 1 };

            // Заміна using var на блок using
            using (var form = new DeviceForm(newDevice))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    newDevice = form.Device;
                    newDevice.Number = devices.Any() ? devices.Max(d => d.Number) + 1 : 1;
                    devices.Add(newDevice);
                    UpdateListBox();
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Заміна using var на блок using
            using (var editForm = new EditDeviceForm(devices))
            {
                editForm.ShowDialog();
            }
            UpdateListBox();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Заміна using var на блок using
            using (var deleteForm = new DeleteDeviceForm(devices))
            {
                deleteForm.ShowDialog();
            }
            UpdateListBox();
        }
    }
}
