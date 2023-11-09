using CSharpFunctionalExtensions;

var multiplier = 2;

GenerateNumber()                            .Keep(out var start)
    .BindTry(n => Multiplie(n, multiplier)) .Keep(out var multiplication)
    .BindTry(n => Add(n, start.Value))
    .Match(
        result => Print(start.Value, multiplier, multiplication.Value, result),
        failure => Console.WriteLine(failure)
    );

static Result<int> GenerateNumber()
{
    var number = Random.Shared.Next(100);

    if (number < 20)
        throw new Exception($"Generated number {number} is less than 20 which is not a valid number.");

    return number;
}

static Result<decimal> Multiplie(int value, decimal multiplier)
{
    return value * multiplier;
}

static Result<decimal> Add(decimal left, decimal right)
{
    return left + right;
}

static void Print(int start, decimal multiplier, decimal multiplicationResult, decimal finalResult)
{
    Console.WriteLine($"Started from {start}, then multiplied by {multiplier} which made {multiplicationResult}.");
    Console.WriteLine($"And finally added the starting value to it {multiplicationResult} + {start} = {finalResult}.");
}

public static class KeepExtensions
{
    public static T Keep<T>(this T value, out T result)
    {
        result = value;
        return result;
    }
}