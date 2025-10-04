public class Calculator
{
    private readonly IReadOnlyDictionary<string, Func<IOperable>> _operations;

    public Calculator()
    {
        _operations = new Dictionary<string, Func<IOperable>>
        {
            { "+", () => new Summation() },
            { "-", () => new Subtraction() },
            { "/", () => new Division() },
            { "*", () => new Multiplication() }
        };
    }

    public IEnumerable<string> GetAvailableOperations() => _operations.Keys;

    public double Calculate(string operationSymbol, double x, double y)
    {
        if (_operations.TryGetValue(operationSymbol, out var factory))
            return factory().Calculate(x, y);

        throw new ArgumentException("Неизвестная операция");
    }
}
