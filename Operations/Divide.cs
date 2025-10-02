namespace dotnet_console_calculator.Operations
{
    public class Divide : IOperable
    {
        public double Calculate(double x, double y) =>
            y != 0 ? x / y : throw new DivideByZeroException("Делить на ноль нельзя!");
    }
}