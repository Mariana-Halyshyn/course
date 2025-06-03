using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace course
{
    public class DeviceContainer
    {
        private List<DisplayDevice> devices = new List<DisplayDevice>();
        private readonly string filePath;

        public DeviceContainer(string filePath = "info.txt")
        {
            this.filePath = filePath;
            Load();
        }

        public List<DisplayDevice> GetAll() => devices.ToList();

        public void Add(DisplayDevice device)
        {
            devices.Add(device);
            Save();
        }

        public void Update(int index, DisplayDevice device)
        {
            if (index >= 0 && index < devices.Count)
            {
                devices[index] = device;
                Save();
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < devices.Count)
            {
                devices.RemoveAt(index);
                Save();
            }
        }

        private void Load()
        {
            devices.Clear();

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл {filePath} не знайдено!");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            int lineNumber = 0;

            foreach (var line in lines)
            {
                lineNumber++;
                try
                {
                    var device = DisplayDevice.ParseFromFileLine(line, lineNumber);
                    devices.Add(device);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка у рядку {lineNumber}: {line}\n{ex.Message}");
                    // Можна також записати у лог файл
                }
            }
        }



        private void Save()
        {
            File.WriteAllLines(filePath, devices.Select(d => d.ToFileString()));
        }
    }
}
