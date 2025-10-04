public class Division : IOperable
{
    public double Calculate(double x, double y) => y != 0
        ? x / y
        : throw new DivideByZeroException("Делить на ноль нельзя!");
}
