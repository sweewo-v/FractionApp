namespace FractionApp.Console
{
    class Program
    {
        static void Main()
        {
            Fraction f1 = new Fraction(1, 2);
            Fraction f2 = new Fraction(2, 4);
            Fraction f3 = new Fraction(3.5);

            System.Console.WriteLine($"{f1} + {f2} = {f1 + f2}");
            System.Console.WriteLine($"{f1} - {f2} = {f1 - f2}");
            System.Console.WriteLine($"{f1} * {f2} = {f1 * f2}");
            System.Console.WriteLine($"{f1} / {f2} = {f1 / f2}");
            System.Console.WriteLine($"{f1} > {f3} = {f1 > f3}");
            System.Console.WriteLine($"{f1} == {f2} = {f1 == f2}");
            System.Console.WriteLine($"2 == !1/2 = {new Fraction(2) == !new Fraction(1, 2)}");
            System.Console.WriteLine($"(double){f3} = {(double)f3}");
            System.Console.WriteLine($"(double)((Fraction)2.00009 + 3.00001) = {(double)((Fraction)2.00009 + 3.00001)}");
            System.Console.WriteLine("new Fraction(2, -2)= " + new Fraction(2, -2));

            System.Console.ReadKey();
        }
    }
}
