using dotnet_console_calculator.Operations;

namespace dotnet_console_calculator
{
    public class Calculator
    {
        private readonly IReadOnlyDictionary<string, IOperable> _operations;

        public Calculator()
        {
            _operations = new Dictionary<string, IOperable>
            {
                { "+", new Sum() },
                { "-", new Sub() },
                { "/", new Divide() },
                { "*", new Multiply() }
            };
        }
        public IEnumerable<string> GetAvailableOperations()
        {
            return _operations.Keys;
        }
        public double Calculate(string operationSymbol, double x, double y)
        {
            if (_operations.TryGetValue(operationSymbol, out IOperable? operation))
                return operation.Calculate(x, y);
            throw new ArgumentException("Неизвестная операция");
        }
    }

}