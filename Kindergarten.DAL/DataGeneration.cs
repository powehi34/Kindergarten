namespace Kindergarten.DAL
{
    public static class DataGeneration
    {
        private static string[] _firstNames = new string[] { "Александр", "Иван", "Максим", "Артем", "Анастасия", "Даниил", "Анна", "Ксения", "Юлия", "Владислав", "Виктория" };
        private static string[] _lastNames = new string[] { "Смирнов", "Иванов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов", "Новиков", "Федоров" };
        private static string[] _middleNames = new string[] { "Александрович", "Алексеевич", "Андреевич", "Евгеньевич", "Сергеевич", "Александровна", "Александровна", "Алексеевна", "Андреевна" };

        public static string GetRandomFullName(Random random)
        {
            var firstname = _firstNames[random.Next(0, _firstNames.Length)];
            var lastname = _lastNames[random.Next(0, _lastNames.Length)];
            var middlename = _middleNames[random.Next(0, _middleNames.Length)];

            return $"{firstname} {lastname} {middlename}";
        }

        public static string GetRandomPhoneNumber(Random random)
        {
            string phoneNumber = "+375";
            // Код оператора
            int operatorCode = random.Next(29, 34);
            phoneNumber += operatorCode.ToString();
            // Оставшиеся цифры
            for (int i = 0; i < 7; i++)
            {
                phoneNumber += random.Next(0, 10).ToString();
            }

            return phoneNumber;
        }

        public static DateTime GetRandomDate(int minYear, int maxYear)
        {
            Random random = new Random();
            int year = DateTime.Now.Year - random.Next(minYear, maxYear + 1);
            int month = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            DateTime date = new DateTime(year, month, day);
            return date;
        }

        public static string GetRandomAddress(Random random, string city)
        {
            string[] streets = { "ул. Пушкина", "ул. Ленина", "ул. Гагарина", "ул. Советская", "ул. Молодежная" };
            string street = streets[random.Next(streets.Length)];
            int houseNumber = random.Next(1, 101);
            int apartmentNumber = random.Next(1, 201);
            return $"{city}, {street}, д. {houseNumber}, кв. {apartmentNumber}";
        }
    }
}
