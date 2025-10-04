
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
                WriteInColor($"{AddBrackets(numberOne)} {selectedOperation} {AddBrackets(numberTwo)} = {resultOfOperation}", ConsoleColor.Green, true);

            }
            catch (FormatException)
            {
                WriteInColor("Ошибка: Неверный формат числа.", ConsoleColor.Red, true);
            }
            catch (Exception ex)
            {
                WriteInColor($"\nПроизошла ошибка: {ex.Message}", ConsoleColor.Red, true);
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
            WriteInColor(message, ConsoleColor.Yellow);
            string input = Console.ReadLine() ?? string.Empty;

            if (input != null && input.ToLower() == "q")
            {
                shouldExit = true;
                return 0;
            }

            if (double.TryParse(input, out double number))
            {
                return number;
            }
            WriteInColor("Ошибка: Неверный формат числа. Попробуйте еще раз.", ConsoleColor.Red, true);
        }
    }

    private string DisplayOperations()
    {
        Console.WriteLine();
        WriteInColor("Доступные операции:", ConsoleColor.Gray, true);

        var availableOperations = _calculator.GetAvailableOperations().ToList();
        Console.WriteLine(string.Join("   ", availableOperations));
        Console.WriteLine();

        while (true)
        {
            WriteInColor("Выберите операцию: ", ConsoleColor.Yellow);
            string operationSymbol = Console.ReadLine() ?? string.Empty;

            if (operationSymbol != null && availableOperations.Contains(operationSymbol))
            {
                return operationSymbol;
            }
            WriteInColor("Ошибка: неизвестная операция. Попробуйте еще раз.", ConsoleColor.Red, true);
        }
    }

    private void WriteInColor(string message, ConsoleColor color, bool newLine = false)
    {
        var previous = Console.ForegroundColor;
        Console.ForegroundColor = color;
        if (newLine) Console.WriteLine(message);
        else Console.Write(message);
        Console.ForegroundColor = previous;
    }


}

