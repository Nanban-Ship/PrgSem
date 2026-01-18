using System;
using System.Collections.Generic;

namespace MathFunctions
{
    interface IDifferentiable
    {
        string OutputDerivative();
    }

    interface IInvertible
    {
        string OutputInversion();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<MathFunction> functions = new()
            {
                new LinearFunction(2, 3),
                new LinearAbsoluteFunction(2, -1, 0),
                new QuadraticFunction(1, -2, 1),
                new LinearFractionalFunction(1, 0, 1, -2)
            };

            double x = 2;
            foreach (var f in functions)
            {
                f.PrintInfo();
                Console.WriteLine($"f({x}) = {f.Calculate(x)}\n");
                
                if (f is IDifferentiable)
                    Console.WriteLine(((IDifferentiable)f).OutputDerivative());
                
                if (f is IInvertible)
                    Console.WriteLine(((IInvertible)f).OutputInversion());
                
                Console.WriteLine(); 
            }
            Console.ReadKey();
        }
    }
    struct Interval
    {

        public double UpperBoundValue { get; }
        public double LowerBoundValue { get; }
        public char UpperBoundBracket { get; }
        public char LowerBoundBracket { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbb">Lower Bound Bracket</param>
        /// <param name="lbv">Lower Bound Value</param>
        /// <param name="ubv">Upper Bound Value</param>
        /// <param name="ubb">Upper Bound Bracket</param>
        public Interval(char lbb, double lbv, double ubv, char ubb)
        {
            UpperBoundBracket = ubb;
            LowerBoundBracket = lbb;
            UpperBoundValue = ubv;
            LowerBoundValue = lbv;
        }
        public override string ToString()
        {
            return $"{LowerBoundBracket}{LowerBoundValue},{UpperBoundValue}{UpperBoundBracket}";
        }
    }
    abstract class MathFunction
    {
        public string Name { get;  }
        public string Description { get;  }
        public Interval Domain { get; protected set; } // chtělo by to vlastně jako list intervalů... 
        public Interval Range { get; protected set; }

        public MathFunction(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract double Calculate(double x);

        public virtual void PrintInfo()
        {
            Console.WriteLine($"{Name}: {Description} na D(f) = {Domain}");
        }
    }

    class LinearFunction : MathFunction, IInvertible, IDifferentiable
    {
        private double a, b; // zapouzdření, privátní datové položky

        public LinearFunction(double a, double b) : base("Lineární funkce", $"f(x) = {a}x + {b}")
        {
            this.a = a;
            this.b = b;
            Domain = new Interval ('(',double.NegativeInfinity,double.PositiveInfinity,')');
            Range = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
        }

        public override double Calculate(double x) => a * x + b;

        public string OutputDerivative() =>  $"f'(x) = {a}";

        public string OutputInversion()
        {
            if (a == 0)
                return "Nelze, jelikož a = 0.";
            return $"f^(-1)(x) = (x - {b}) / {a} \n D(f^(-1)) = {Range}";
        }
    }

    class LinearAbsoluteFunction : MathFunction
    {
        private double a, b, c;

        public LinearAbsoluteFunction(double a, double b, double c) : base("Lineární funkce s absolutní hodnotou", $"f(x) = {a}|x + {b}| + {c}")
        {
            this.a = a;
            this.b = b;
            this.c = c;
            Domain = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
            
            if (a >= 0)
                Range = new Interval('[', c, double.PositiveInfinity, ')');
            else
                Range = new Interval('(', double.NegativeInfinity, c, ']');
        }

        public override double Calculate(double x) => a * Math.Abs(x + b) + c;
    }

    class QuadraticFunction : MathFunction, IDifferentiable
    {
        private double a, b, c;

        public QuadraticFunction(double a, double b, double c) : base("Kvadratická funkce", $"f(x) = {a}x^2 + {b}x + {c}")
        {
            this.a = a;
            this.b = b;
            this.c = c;
            Domain = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
            
            double vertexY = c - (b * b) / (4 * a);
            
            if (a > 0)
                Range = new Interval('[', vertexY, double.PositiveInfinity, ')');
            else
                Range = new Interval('(', double.NegativeInfinity, vertexY, ']');
        }

        public override double Calculate(double x) => a * x * x + b * x + c;

        public string OutputDerivative() => $"f'(x) = {2 * a}x + {b}";

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("Funkce má parabolický průběh.");
        }
    }

    class LinearFractionalFunction : MathFunction, IDifferentiable, IInvertible
    {
        private double a, b, c, d;

        public LinearFractionalFunction(double a, double b, double c, double d) : base("Lineární lomená funkce", $"f(x) = ({a}x + {b}) / ({c}x + {d})")
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            Domain = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
            Range = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
        }

        public override double Calculate(double x) => (a * x + b) / (c * x + d);

        public string OutputDerivative()
        {
            double numerator = a * d - b * c;
            return $"f'(x) = {numerator} / ({c}x + {d})^2";
        }

        public string OutputInversion()
        {
            return $"f^(-1)(x) = ({d}x - {b}) / ({a} - {c}x)";
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("Funkce má hyperbolický průběh.");
        }
    }
}