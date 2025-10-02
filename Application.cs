namespace dotnet_console_calculator
{
    public class Application
    {
        private readonly Calculator _calculator;

        public Application(Calculator calculator) => _calculator = calculator;
        public void Run()
        {
            while (true)
            {
                bool shouldExit = false;
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(@"
 ██████╗ █████╗ ██╗      ██████╗██╗   ██╗██╗      █████╗ ████████╗ ██████╗ ██████╗ 
██╔════╝██╔══██╗██║     ██╔════╝██║   ██║██║     ██╔══██╗╚══██╔══╝██╔═══██╗██╔══██╗
██║     ███████║██║     ██║     ██║   ██║██║     ███████║   ██║   ██║   ██║██████╔╝
██║     ██╔══██║██║     ██║     ██║   ██║██║     ██╔══██║   ██║   ██║   ██║██╔══██╗
╚██████╗██║  ██║███████╗╚██████╗╚██████╔╝███████╗██║  ██║   ██║   ╚██████╔╝██║  ██║
 ╚═════╝╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝
                                                                                   
");
                    Console.ResetColor();
                    double num1 = ReadNumberWithValidation("Введите первое число (или 'q' для выхода): ", out shouldExit);
                    if (shouldExit) break;

                    string selectedOperation = DisplayOperations();

                    double num2 = ReadNumberWithValidation("Введите второе число: ", out _);

                    double result = _calculator.Calculate(selectedOperation, num1, num2);

                    WriteInColor("\nРезультат: ", ConsoleColor.Gray);
                    WriteLineInColor($"{AddBrackets(num1)} {selectedOperation} {AddBrackets(num2)} = {result}", ConsoleColor.Green);
                }
                catch (FormatException)
                {
                    WriteLineInColor("Ошибка: Неверный формат числа.", ConsoleColor.Red);
                }
                catch (Exception ex)
                {
                    WriteLineInColor($"\nПроизошла ошибка: {ex.Message}", ConsoleColor.Red);
                }
                finally
                {
                    if (!shouldExit)
                    {
                        Console.WriteLine("\n------------------------------------");
                        Console.WriteLine("Нажмите любую клавишу для следующего вычисления...");
                        Console.ReadKey();
                    }
                }
            }

        }

        private string AddBrackets(double n) => n < 0 ? $"({n})" : $"{n}";
        private double ReadNumberWithValidation(string message, out bool shouldExit)
        {
            shouldExit = false;
            while (true)
            {
                string? input = ReadUserInput(message);

                if (input != null && input.ToLower() == "q")
                {
                    shouldExit = true;
                    return 0;
                }

                if (double.TryParse(input, out double number))
                {
                    return number;
                }
                WriteLineInColor("Ошибка: Неверный формат числа. Попробуйте еще раз.", ConsoleColor.Red);
            }
        }
        private string? ReadUserInput(string message)
        {
            WriteInColor(message, ConsoleColor.Yellow);
            return Console.ReadLine();
        }

        private string DisplayOperations()
        {
            Console.WriteLine();
            WriteLineInColor("Доступные операции:", ConsoleColor.Gray);

            string availableOps = string.Join("   ", _calculator.GetAvailableOperations());
            Console.WriteLine(availableOps);
            Console.WriteLine();

            while (true)
            {
                string? symbol = ReadUserInput("Выберите операцию: ");

                if (symbol != null && _calculator.GetAvailableOperations().Contains(symbol))
                {
                    return symbol;
                }
                WriteLineInColor("Ошибка: неизвестная операция. Попробуйте еще раз.", ConsoleColor.Red);
            }
        }

        private void WriteInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        private void WriteLineInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }

}