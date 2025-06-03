//using System;

//namespace course
//{
//    public class DisplayDevice
//    {
//        public string Name { get; set; }
//        public double Power { get; set; }
//        public double Weight { get; set; }
//        public string Interface { get; set; }
//        public string Resolution { get; set; }

//        public override string ToString()
//        {
//            return $"{Name}, {Power}W, {Weight}kg, {Interface}, {Resolution}";
//        }

//        public string ToFileString()
//        {
//            return $"{Name}|{Power}|{Weight}|{Interface}|{Resolution}";
//        }

//        public static DisplayDevice FromFileString(string line)
//        {
//            var parts = line.Split('|');
//            return new DisplayDevice
//            {
//                Name = parts[0],
//                Power = double.Parse(parts[1]),
//                Weight = double.Parse(parts[2]),
//                Interface = parts[3],
//                Resolution = parts[4]
//            };
//        }
//    }
//}
using System;
using System.Linq;

namespace course
{
    public class DisplayDevice
    {
        public int Number { get; set; } // Порядковий номер
        public string Interface { get; set; }
        public double Power { get; set; }
        public double Weight { get; set; }
        public double Diagonal { get; set; }
        public string Resolution { get; set; }
        public string Description { get; set; }

        public DisplayDevice() { }

        // Основний метод для парсингу рядка з файлу
        public static DisplayDevice ParseFromFileLine(string line, int number)
        {
            // Розбиваємо по пробілах (усі слова)
            var words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length < 6)
                throw new FormatException("Невірний формат рядка - замало даних.");

            // Interface — перше слово
            string deviceInterface = words[0];

            // Наступні числа: Потужність, Вага, Діагональ, Роздільна здатність — потрібно їх "склеїти" відповідно, бо деякі містять пробіли.

            // Потужність (words[1]) - це одне число (наприклад 45 або 220)
            if (!double.TryParse(words[1], out double power))
                throw new FormatException("Невірний формат потужності.");

            // Вага - може містити 1 або 2 слова (наприклад: 1 500 або 600)
            // Для надійності спробуємо прочитати 1 або 2 слова, поки не вийде число
            int weightIndex = 2;
            string weightStr = words[weightIndex];
            if (weightIndex + 1 < words.Length && double.TryParse(weightStr + words[weightIndex + 1], out _))
            {
                // Можливо потрібно склеїти два слова з пробілом
                weightStr = words[weightIndex] + words[weightIndex + 1];
                weightIndex++;
            }
            else if (weightIndex + 1 < words.Length && double.TryParse(weightStr + " " + words[weightIndex + 1], out double weightTest))
            {
                // Також можна пробувати з пробілом
                weightStr = weightStr + " " + words[weightIndex + 1];
                weightIndex++;
            }
            // Спрощено — спробуємо просто склеїти 1 або 2 слова з пробілом і замінити пробіли всередині
            // В твоєму файлі "1 500" — треба прибрати пробіли для конвертації в число
            string weightClean = weightStr.Replace(" ", "");
            if (!double.TryParse(weightClean, out double weight))
                throw new FormatException("Невірний формат ваги.");

            // Діагональ - наступне слово
            int diagonalIndex = weightIndex + 1;
            if (!double.TryParse(words[diagonalIndex], out double diagonal))
                throw new FormatException("Невірний формат діагоналі.");

            // Роздільна здатність - теж може бути з пробілами (наприклад: 1 600)
            int resolutionIndex = diagonalIndex + 1;
            string resolutionStr = words[resolutionIndex];
            if (resolutionIndex + 1 < words.Length)
            {
                // Спробуємо склеїти два слова для роздільної здатності
                string testRes = resolutionStr + words[resolutionIndex + 1];
                if (double.TryParse(testRes, out _) == false)
                {
                    // Якщо не вийшло, склеюємо з пробілом
                    resolutionStr = resolutionStr + " " + words[resolutionIndex + 1];
                    resolutionIndex++;
                }
            }
            string resolutionClean = resolutionStr.Replace(" ", "");

            // Тепер опис — все, що залишилось після resolutionIndex
            int descriptionStart = resolutionIndex + 1;
            string description = string.Join(" ", words.Skip(descriptionStart));

            return new DisplayDevice
            {
                Number = number,
                Interface = deviceInterface,
                Power = power,
                Weight = weight,
                Diagonal = diagonal,
                Resolution = resolutionClean,
                Description = description
            };
        }





        // Перетворення об'єкта в рядок для збереження у файл
        public string ToFileString()
        {
            return $"{Interface}, {Power}, {Weight}, {Diagonal}, {Resolution}, {Description}";
        }

        // Перетворення об'єкта у зручний формат для відображення
        public override string ToString()
        {
            return $"№{Number}: Інтерфейс: {Interface}, Потужність: {Power} Вт, Вага: {Weight} кг, " +
                   $"Діагональ: {Diagonal} дюймів, Роздільна здатність: {Resolution}, Опис: {Description}";
        }
    }
}
