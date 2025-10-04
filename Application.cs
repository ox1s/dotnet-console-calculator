
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
                double numberOne = ReadNumberWithValidation("Введите первое число (или 'q' для выхода): ", out shouldExit);
                if (shouldExit) break;

                string selectedOperation = DisplayOperations();

                double numberTwo = ReadNumberWithValidation("Введите второе число: ", out _);

                double resultOfOperation = _calculator.Calculate(selectedOperation, numberOne, numberTwo);

                WriteInColor("\nРезультат: ", ConsoleColor.Gray);
                WriteLineInColor($"{AddBrackets(numberOne)} {selectedOperation} {AddBrackets(numberTwo)} = {resultOfOperation}", ConsoleColor.Green);
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

        string availableOperations = string.Join("   ", _calculator.GetAvailableOperations());
        Console.WriteLine(availableOperations);
        Console.WriteLine();

        while (true)
        {
            string? operationSymbol = ReadUserInput("Выберите операцию: ");

            if (operationSymbol != null && _calculator.GetAvailableOperations().Contains(operationSymbol))
            {
                return operationSymbol;
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

