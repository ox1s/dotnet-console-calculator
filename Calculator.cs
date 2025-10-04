public class Calculator
{
    private readonly IReadOnlyDictionary<string, IOperable> _operations;

    public Calculator()
    {
        _operations = new Dictionary<string, IOperable>
            {
                { "+", new Summation() },
                { "-", new Subtraction() },
                { "/", new Division() },
                { "*", new Multiplication() }
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

