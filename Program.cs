using System;

namespace second_program
{
    internal class Program
    {
        // главная функция программы
        static void Main(string[] args)
        {
            // вызов метода для запуска игры
            StartGameLoop();
        }

        // метод для управления циклом игры и статистикой
        static void StartGameLoop()
        {
            // объявление переменных для статистики
            int min = 0;
            int max = 0;
            int totalAttempts = 0;
            int gamesCount = 0;
            Random rnd = new Random();
            char answer = 'y';

            // цикл для повторной игры
            do
            {
                // запуск одной партии и получение количества попыток
                int attempts = PlayMatch(rnd);

                // проверка, завершена ли игра успешно
                if (attempts > 0)
                {
                    // расчет минимального количества попыток
                    if (min == 0 || min > attempts) min = attempts;
                    // расчет максимального количества попыток
                    max = max < attempts ? attempts : max;
                    // суммирование всех попыток
                    totalAttempts += attempts;
                    // увеличение счетчика игр
                    gamesCount++;
                }

                // вывод вопроса о продолжении
                Console.WriteLine("Do you want play game? (y/n)");
                // считывание ответа пользователя
                answer = Console.ReadKey().KeyChar;
                Console.WriteLine();

            } while (answer == 'y');

            // вывод итоговой статистики на консоль
            Console.WriteLine($"min = {min} max = {max} avg = {(double)totalAttempts / gamesCount}");
        }

        // метод для проведения одной партии игры (принцип SOLID - одна задача)
        static int PlayMatch(Random rnd)
        {
            // инициализация счетчика попыток
            int counter = 0;
            // генерация случайного числа от 1 до 100
            int number = rnd.Next(1, 101);

            // вывод информации на консоль
            Console.WriteLine("Try guess number?");

            // цикл процесса угадывания
            while (true)
            {
                // увеличение счетчика попыток
                counter++;
                // получение проверенного числа от пользователя
                int userNumber = GetSafeInput();

                // сравнение чисел и вывод подсказок
                if (userNumber > number)
                    Console.WriteLine("Your number is greater");
                else if (userNumber < number)
                    Console.WriteLine("Your number is less");
                else
                {
                    // вывод сообщения о победе
                    Console.WriteLine("You are win!!!");
                    // возврат количества затраченных попыток
                    return counter;
                }
            }
        }

        // метод для безопасного ввода числа с проверкой (валидация)
        static int GetSafeInput()
        {
            int input = 0;
            // цикл на 3 попытки ввода
            for (int i = 0; i < 3; i++)
            {
                // вывод инструкции пользователю
                Console.WriteLine("Input number from [1;100]");
                // попытка считать число и проверка диапазона
                if (int.TryParse(Console.ReadLine(), out input) && input >= 1 && input <= 100)
                {
                    // возврат корректного числа
                    return input;
                }

                // проверка на последнюю попытку ввода
                if (i == 2)
                {
                    // вывод сообщения об ошибке
                    Console.WriteLine("Too many errors.");
                    // завершение работы программы
                    Environment.Exit(0);
                }
            }
            return input;
        }
    }
}