// code starts on line 104
/*
🥂🎓👆

My code made SoloLearn's Code of the Day!
(Tuesday, February 20, 2018)

Update: Tuesday, February 05, 2019 - This code is now the 2nd most popular C# code on SoloLearn!

Update: Thursday, December 27, 2018 - This code is tied for 2nd most popular C# code on SoloLearn!

As of Monday, November 26, 2018 this code is the 3rd most popular C# code on SoloLearn and quickly approaching 2nd most popular C# code!

This code was also put on SoloLearn user // Zohir ( https://www.sololearn.com/Profile/6015460 ) list "TOP : 10 Calculators with correct code" ( https://www.sololearn.com/post/31302 ) and his top 10 list was recognized by SoloLearn ( https://www.sololearn.com/post/31617 ).

This code was recognized by SoloLearn ( https://www.sololearn.com/Profile/1 ). See "Calculator mini-projects to save and edit." ( https://www.sololearn.com/post/75408 ).
*/
 
/*
Try your own expression or copy and paste one of the following expressions into the input (note: do not include an equal sign and leaving the input empty will run a regression test):
-12 * 1 + -721 * (15 +51)
(5+3)*6
(((2+3))*(4+2))
2*3/5
2^3*2
3-3+3*3/3^3
2^2^3
--5
-(-5)
4(5)
(5)6
100 v 10
p
π
e
5×6÷10x3X2
e^π
-(5+8)
(-42) + (+24) + 12 + 8 - (-4)
21 + 40 - (+9) + 413 + (-21) + 4 + 3
5.92 - 27 + 19 - 37.1 + 27 - 25
6 ÷ 2 (1+2)
1+(2*(2+1)+2-(3*2)+1)
*/

/*
Learn more (and also read my comments on these lessons):
https://www.sololearn.com/learn/704
https://www.sololearn.com/learn/642
https://www.sololearn.com/learn/668
https://www.sololearn.com/learn/1151
https://www.sololearn.com/learn/1143
https://www.sololearn.com/Discuss/1089276
*/


/*
If I've missed something please respond to this post. If you like my work, please consider upvoting this code and comment. If you'd like to see more, follow me (Aaron Becker) ;)
*/

/*
Recognized unary operators
+
-

Recognized binary operators (with equivalents)
^
v (V)
* (×xX)
%
/ (÷)
+
-

Recognized precedence operators
(
)

Recognized constants (with equivalents)
e
π (p)

This code can deduce and perform implicit multiplication!

This code works for any digit number (not just one digit i.e. 0-9) and also negative numbers.

I encourage you to run the code, the result appears on the last line of the console, scrolling may be necessary. The console lines above the solution show the algorithm at work.

Notes:
To take a Logarithm use my custom binary operator 'v'. To write log10 100, instead write 100 v 10. To write ln 2, instead write 2 v e.

To take the nth root of a number x, use x^(1/n).

Implicit multiplication is performed when a number or a subexpression (i.e. an expression inside Parentheses) appears next to (left or right) another number or subexpression without a binary operator.

The use of double comes with a lots of precision issues that this code doesn't address and is out of the scope for this code. Rounding errors may result.

Limitations:

Upon encountering bad input, "error" is written to the console.

*/

using System;
using System.Collections.Generic;
using System.Linq;

//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Console_Calculator {
    internal class Operands {
        private enum Constants {
            Euler = 'e',
            Pi = 'π',
            PiAlt = 'p'
        }

        internal static bool IsEulerConst(string c) => c == ((char)Constants.Euler).ToString();

        internal static bool IsPiConst(string c) => 
            c == ((char)Constants.Pi).ToString() ||
            c == ((char)Constants.PiAlt).ToString();
        
        private readonly Stack<double> _operands;
        private readonly bool _verbose;
        
        internal Operands(int capacity, bool verbose = false){
            _operands = new Stack<double>(capacity);
            _verbose = verbose;
        }
        
        internal double Peek() => _operands.Peek();
        
        internal double Pop(){
            var op = _operands.Pop();
            if (_verbose) { Console.WriteLine($"Operands stack popped: {op}"); }
            return op;
        }
        
        internal void Push(double _operand){
            _operands.Push(_operand);
            if (_verbose) { Console.WriteLine($"Operands stack pushed: {_operands.Peek()}"); }
        }
        
        internal bool CanPerformBinaryOperation(){
            return _operands.Count >= 2;
        }
    }
    
    internal class Operators {
        // Important: add alias operators to this enumeration by using the same base name.
        // i.e. they must begin with the same string
        // e.g. Multiplication & MultiplicationAlt
        private enum Ops {
            OpenParenthesis = '(',
            CloseParenthesis = ')',
            Exponentiation = '^',
            Logarithm = 'v',
            LogarithmAlt = 'V',
            Multiplication = '*',
            MultiplicationAlt = '×',
            MultiplicationAlt0 = 'x',
            MultiplicationAlt1 = 'X',
            Division = '/',
            DivisionAlt = '÷',
            Modulo = '%',
            Addition = '+',
            Subtraction = '-',
            Positive = Addition,
            Negative = Subtraction
        }

        internal static bool IsOpenParenthesisOp(string op) => op == ((char)Ops.OpenParenthesis).ToString();

        internal static bool IsCloseParenthesisOp(string op) => op == ((char)Ops.CloseParenthesis).ToString();

        internal static bool IsExponentiationOp(string op) => op == ((char)Ops.Exponentiation).ToString();

        internal static bool IsLogarithmOp(string op) =>
            op == ((char)Ops.Logarithm).ToString() ||
            op == ((char)Ops.LogarithmAlt).ToString();

        internal static bool IsMultiplicationOp(string op) =>
                op == ((char)Ops.Multiplication).ToString() ||
                op == ((char)Ops.MultiplicationAlt).ToString() ||
                op == ((char)Ops.MultiplicationAlt0).ToString() ||
                op == ((char)Ops.MultiplicationAlt1).ToString();
        
        internal static bool IsDivisionOp(string op) =>
                op == ((char)Ops.Division).ToString() ||
                op == ((char)Ops.DivisionAlt).ToString();
        
        internal static bool IsModuloOp(string op) => op == ((char)Ops.Modulo).ToString();
        
        internal static bool IsAdditionOp(string op) => op == ((char)Ops.Addition).ToString();
        
        internal static bool IsSubtractionOp(string op) => op == ((char)Ops.Subtraction).ToString();
        
        internal static bool IsPositiveOp(string op) => op == ((char)Ops.Positive).ToString();
        
        internal static bool IsNegativeOp(string op) => op == ((char)Ops.Negative).ToString();
        
        internal static bool IsBinaryOp(string op) =>
                IsExponentiationOp(op) ||
                IsLogarithmOp(op) ||
                IsMultiplicationOp(op) ||
                IsModuloOp(op) ||
                IsDivisionOp(op) ||
                IsAdditionOp(op) ||
                IsSubtractionOp(op);
        
        internal static string OpenParenthesisOp() => ((char)Ops.OpenParenthesis).ToString();
        
        internal static string CloseParenthesisOp() => ((char)Ops.CloseParenthesis).ToString();
        
        internal static string MultiplicationOp() => ((char)Ops.Multiplication).ToString();
        
        private static readonly Dictionary<string, int> OpPrecedence = new Dictionary<string, int>();
        
        private readonly Stack<string> _operators;
        private readonly Stack<bool> _makeNegatives;
        
        private readonly bool _verbose;
        
        internal Operators(int capacity, bool verbose){
            _operators = new Stack<string>(capacity);
            _makeNegatives = new Stack<bool>(capacity);
            _verbose = verbose;
            
            // fill OpPrecedence dictionary with operator character as the key and their corresponding precedence as values
            
            // Below, P(EL)(MoDM)(AS) is the extension of PEDMAS, where Logarithm and Modulo are explicitly used
            // Inverse or complimentary operations share the same precedence e.g. addition and subtraction
            // default evaluation direction is left to right, except for Exponentiation 
            // evaluation direction is determined elsewhere
            
            if (OpPrecedence.Count > 0) { return; }
            
            var opType = typeof(Ops);
            Array names = Enum.GetNames(opType);
            // precedence value is based on the values for the C specification
            // To limit the scope of the precedence variable but avoid its repeated instantiation,
            // precedence is declared outside the foreach loop but within a scope that contains
            // both the declaration followed by the foreach loop
            { int precedence;
                foreach (string name in names)
                {
                    precedence = int.MinValue;
                    if (
                        name.StartsWith(Ops.Addition.ToString()) ||
                        name.StartsWith(Ops.Subtraction.ToString())
                    ) {
                        precedence = 12;
                    }
                    else if (
                        name.StartsWith(Ops.Modulo.ToString()) ||
                        name.StartsWith(Ops.Division.ToString()) ||
                        name.StartsWith(Ops.Multiplication.ToString())
                    ) {
                        precedence = 13;
                    }
                    else if (
                        name.StartsWith(Ops.Exponentiation.ToString()) ||
                        name.StartsWith(Ops.Logarithm.ToString())
                    ) {
                        precedence = 14;
                    }
                    else if (
                        name.StartsWith(Ops.Positive.ToString()) ||
                        name.StartsWith(Ops.Negative.ToString())
                    ) {
                        precedence = 16;
                    }
                    else if (
                        name.StartsWith(Ops.OpenParenthesis.ToString()) ||
                        name.StartsWith(Ops.CloseParenthesis.ToString())
                    ) {
                        precedence = 17;
                    }

                    if (precedence < 0) { continue; } // only non-negative values for precedence are in the specification

                    var op = (Ops) Enum.Parse(opType, name);
                    OpPrecedence.Add(((char) op).ToString(), precedence);
            }} // precedence goes out of scope
        }
        
        internal string Peek() => _operators.Peek();
        
        internal string Pop(){
            var op = _operators.Pop();
            if (_verbose) { Console.WriteLine($"Operators stack popped: {op}"); }
            return op;
        }
        
        internal bool Push(string _operator){
            if (
                _operator != null &&
                !IsCloseParenthesisOp(_operator) && // don't push close parenthesis, instead evaluate expression until a open parenthesis is popped
                (
                    _operators.Count() == 0 || // any operator can be pushed into an empty stack
                    IsOpenParenthesisOp(Peek()) || // any operator can be pushed after an open parenthesis
                    (
                        IsExponentiationOp(Peek()) &&
                        IsExponentiationOp(_operator)
                    ) || // exponentiation is evaluated from right to left, if new operator and one on top of stack are both exponentiation then push
                    HasGreaterPrecedence(_operator)
                )
            ) {
                _operators.Push(_operator);
                if (_verbose) { Console.WriteLine($"Operators stack pushed: {Peek()}"); }
                return true;
            }
            
            if (_verbose) { Console.WriteLine($"could not push {_operator} onto Operators stack"); }
            return false;
        }
        
        private bool HasGreaterPrecedence(string newOperator){
            if (
                IsOpenParenthesisOp(newOperator) ||
                IsBinaryOp(newOperator)
            ) {
                var topOperator = Peek();
                if (
                    OpPrecedence[newOperator] > OpPrecedence[topOperator]
                ) {
                    if (_verbose) { Console.WriteLine($"{newOperator} > {topOperator}"); }
                    return true;
                }
                    
                if (_verbose) { Console.WriteLine($"{newOperator} <= {topOperator}"); }

                return false;
            } else
            if (
                IsCloseParenthesisOp(newOperator)
            ) {
                Console.WriteLine($"{CloseParenthesisOp()} has a high precedence but should not be pushed, instead evaluate using the operator stack until a '{OpenParenthesisOp()}'");
                return false;
            } else {
                Console.WriteLine($"operator {newOperator} is not valid");
                return false;
            }

        }
        
        internal bool IsEmpty(){
            var res = _operators.Count == 0;

            if (_verbose)
            {
                Console.WriteLine(
                    res ?
                        "Operators stack is empty" :
                        "Operators stack is NOT empty"
                    );
            }

            return res;
        }
        
        internal void ToggleNegation(){
            var makeNegative = MakeNegative();
            _makeNegatives.Push(!makeNegative);
        }
        
        internal bool MakeNegative() =>
                _makeNegatives.Count > 0 &&
                _makeNegatives.Pop();
    }
    
    public class ExpressionEvaluator{
        private readonly Operands _operands;
        private readonly Operators _operators;
        
        private readonly string _expression;
        private readonly bool _verbose;
        
        public ExpressionEvaluator(string _expression, bool verbose = false){
            this._expression = _expression.Replace(" ", ""); // space is just formatting
            _verbose = verbose;
            if (_verbose){ Console.WriteLine($"Expression: {_expression}"); }
            
            _operands = new Operands(this._expression.Length, _verbose);
            _operators = new Operators(this._expression.Length, _verbose);
        }
        
        private double Subtraction(){
            var right = _operands.Pop();
            var left = _operands.Pop();
            var ret = left - right;
            if (_verbose) { Console.WriteLine($"{left} {_operators.Peek()} {right} = {ret}"); }
            return ret;
        }
        
        private double Addition(){
            var right = _operands.Pop();
            var left = _operands.Pop();
            var ret = left + right;
            if (_verbose) { Console.WriteLine($"{left} {_operators.Peek()} {right} = {ret}"); }
            return ret;
        }
        
        private double Modulo(){
            var right = _operands.Pop();
            var left = _operands.Pop();
            var ret = left % right;
            if (_verbose) { Console.WriteLine($"{left} {_operators.Peek()} {right} = {ret}"); }
            return ret;
        }
        
        private double Division(){
            var right = _operands.Pop();
            var left = _operands.Pop();
            var ret = left / right;
            if (_verbose) { Console.WriteLine($"{left} {_operators.Peek()} {right} = {ret}"); }
            return ret;
        }
        
        private double Multiplication(){
            var right = _operands.Pop();
            var left = _operands.Pop();
            var ret = left * right;
            var op = !_operators.IsEmpty() ? _operators.Peek() : Operators.MultiplicationOp(); // deal with implicit multiplication
            if (_verbose) { Console.WriteLine($"{left} {op} {right} = {ret}"); }
            return ret;
        }
        
        private double Logarithm(){
            var right = _operands.Pop();
            var left = _operands.Pop();
            var ret = Math.Log(left, right);
            if (_verbose) { Console.WriteLine($"{left} {_operators.Peek()} {right} = {ret}"); }
            return ret;
        }
        
        private double Exponentiation(){
            var right = _operands.Pop();
            var left = _operands.Pop();
            var ret = Math.Pow(left, right);
            if (_verbose) { Console.WriteLine($"{left} {_operators.Peek()} {right} = {ret}"); }
            return ret;
        }
        
        private int EvaluatePart(string op = null) {
            var pushedOperator = false;

            // try to push operator onto operator stack
            if (
                op != null &&
                !Operators.IsCloseParenthesisOp(op)
            ) {
                pushedOperator = _operators.Push(op);
                if (pushedOperator) {    // pushed the operator
                    return 0;
                }
            }

            // topOperator and result have the same effective scope as being inside the while loop without
            // multiple instantiations
            { string topOperator = null; double result;
            while (
                !_operators.IsEmpty() &&
                (
                    (op != null && !Operators.IsCloseParenthesisOp(op) && !pushedOperator) || // evaluate until parameter operator is pushed
                    Operators.IsCloseParenthesisOp(op) || // evaluate expression until an open parenthesis
                    op == null // evaluate the rest of the expression
                )
            ) {
                topOperator = _operators.Peek();

                if (Operators.IsOpenParenthesisOp(topOperator)) {
                    var matching = Operators.IsCloseParenthesisOp(op);
                    var prefix = !matching ? "no " : "";
                    if (_verbose) { Console.WriteLine($"'{Operators.OpenParenthesisOp()}' found with {prefix}matching '{Operators.CloseParenthesisOp()}'"); }
                    _operators.Pop();
                    return !matching ? -1 : 0;
                }
                
                if (!_operands.CanPerformBinaryOperation()) {
                    return -1;
                }
                
                if (
                    Operators.IsSubtractionOp(topOperator)
                ) {
                    result = Subtraction();
                } else
                if (
                    Operators.IsAdditionOp(topOperator)
                ) {
                    result = Addition();
                } else
                if (
                    Operators.IsModuloOp(topOperator)
                ) {
                    if (_operands.Peek() == 0) {
                        Console.WriteLine("avoided divide by zero");
                        return -1;
                    }
                    result = Modulo();
                } else
                if (
                    Operators.IsDivisionOp(topOperator)
                ) {
                    if (_operands.Peek() == 0) {
                        Console.WriteLine("avoided divide by zero");
                        return -1;
                    }
                    result = Division();
                } else
                if (
                    Operators.IsMultiplicationOp(topOperator)
                ) {
                    result = Multiplication();
                } else
                if (
                    Operators.IsLogarithmOp(topOperator)
                ) {
                    result = Logarithm();
                } else
                if (
                    Operators.IsExponentiationOp(topOperator)
                ) {
                    result = Exponentiation();
                } else {
                    Console.WriteLine($"top operator {topOperator} is not valid");
                    return -1;
                }
                
                _operators.Pop();
                _operands.Push(result);

                if (
                    op != null &&
                    !Operators.IsCloseParenthesisOp(op)
                ) {
                    pushedOperator = _operators.Push(op);
                }
            }} // topOperator and result go out of scope
            
            if (Operators.IsCloseParenthesisOp(op)) {
                Console.WriteLine($"'{Operators.CloseParenthesisOp()}' found with no matching '{Operators.OpenParenthesisOp()}'");
                return -1;
            }
            return 0;
        }
        
        private bool EvaluateUntilOperatorPush(string op) {
            return EvaluatePart(op) == 0;
        }
        
        private bool EvaluateUntilOpenParenthesis() {
            return EvaluatePart(Operators.CloseParenthesisOp()) == 0;
        }
        
        private bool EvaluateUntilDone(){
            return EvaluatePart() == 0;
        }
        
        public double? Evaluate(){
            if (String.IsNullOrWhiteSpace(_expression)) {
                Console.WriteLine("expression invalid: empty");
                return null;
            }
            
            bool pushedOperator = true;
            for (int i = 0; i < _expression.Length; ++i) {
                string c = _expression[i].ToString();
                if (_verbose) { Console.WriteLine($"Evaluate: {c}"); }
                if (
                    pushedOperator &&
                    Operators.IsPositiveOp(c) &&
                    i + 1 < _expression.Length
                ) {   // plus unary is not needed, skip to next character
                    continue;
                } else
                if (
                    pushedOperator &&
                    Operators.IsNegativeOp(c) &&
                    i + 1 < _expression.Length
                ) {
                    _operators.ToggleNegation();
                } else
                if (
                    Operands.IsEulerConst(c)
                ) {
                    pushedOperator = false;
                    _operands.Push(Math.E);
                } else
                if (
                    Operands.IsPiConst(c)
                ) {
                    pushedOperator = false;
                    _operands.Push(Math.PI);
                } else
                if (
                    Char.GetNumericValue(c[0]) != -1.0 ||
                    c == "."
                ) {    // this character represents the beginning of a number
                    var parsedNum = false;

                    for (var j = _expression.Length - i; j > 0; --j) { // try to find the end of the number by starting to parse the rest of the expression as a number, shorten the attempted parsed substring until success
                        if (
                            !(parsedNum = double.TryParse(_expression.Substring(i, j), out var num))
                        ) {
                            continue;
                        } 
                        
                        // we found the longest possible number (e.g. 12 instead of 1)
                        i += j - 1;
                        pushedOperator = false;
                        num = _operators.MakeNegative() ? -num : num;
                        _operands.Push(num);
                        break;
                    }
                    
                    if (!parsedNum) {
                        return null;
                    }
                } else
                if (
                    pushedOperator &&
                    Operators.IsOpenParenthesisOp(c)
                ) {    // binary expression whose second operand is a sub expression, don't treat as implicit multiplication, unless sub expression is negated
                    if (_operators.MakeNegative()) {
                        _operands.Push(-1);
                        _operators.Push(Operators.MultiplicationOp());
                    }
                    pushedOperator = EvaluateUntilOperatorPush(c);
                    if (!pushedOperator) {
                        return null;
                    }
                } else
                if (!pushedOperator) {    // character is a binary operator
                    if (Operators.IsOpenParenthesisOp(c)) { // treat as implicit multiplication
                            if (!EvaluateUntilOperatorPush(Operators.MultiplicationOp())) {
                                return null;
                            }
                            
                            pushedOperator = EvaluateUntilOperatorPush(c);
                            if (!pushedOperator) {
                                return null;
                            }
                    } else
                    if (
                        Operators.IsBinaryOp(c)
                    ) {
                        pushedOperator = EvaluateUntilOperatorPush(c);
                        if (!pushedOperator) {
                            return null;
                        }
                    } else
                    if (
                        Operators.IsCloseParenthesisOp(c)
                    ) {
                        pushedOperator = false;
                        if (!EvaluateUntilOpenParenthesis()) {
                            return null;
                        }
                    } else {
                        Console.WriteLine($"character {c} is not a valid binary operator");
                        return null;
                    }
                }
                else {
                    Console.WriteLine($"character {c} is not a valid operand or valid unary operator");
                    return null;
                }
            }
            
            var done = EvaluateUntilDone();
            if (!done) {    // there was error evaluating the expression
                return null;
            }
            
            while (_operands.CanPerformBinaryOperation()) {    // reduce operand stack to a single value by implicitly multiplying stack items together
              _operands.Push(Multiplication());
            } 
            
            return _operands.Peek();
        }
        
        public void Print(){
            Console.WriteLine($"{_expression} = {_operands.Peek()}");
        }
    }
    
    public class Program{
        private const bool Test = false;
        // private static string[] _inputs;
        private static string _expression;
        private static double? _expected = null;

        public static void Main(string[] args){
            // _inputs = String.Split(new char[] {','}, StringSplitOptions.None);
            _expression = Console.ReadLine();
            double parsedExpected = 0;
            var bParsed = false;
            if (
                Test ||
                // _inputs.Length != 1
                string.IsNullOrWhiteSpace(_expression) ||
                (bParsed = double.TryParse(Console.ReadLine(), out parsedExpected))
            ) {
                _expected = bParsed ? (double?)parsedExpected : null;
                RegressionTest();
            } else {
                Calculate();
            }

            Console.ReadLine();
        }

        private static void Calculate(){
            var ex = new ExpressionEvaluator(_expression.Trim(), !Test);
            var res = ex.Evaluate();
            if (res != null) {
                ex.Print();
            }
            else {
                Console.WriteLine("error while evaluating expression");
            }
        }

        // below is regression testing code

        private static void RegressionTest(){
            Console.WriteLine("Begin regression test...");

            // To add to the regression testing, add a new tuple to the testData list. The first member of the tuple is the string expression to be evaluated and the second member of the tuple is expected double sized floating point value of evaluating the expression.
            var testData = new List<Tuple<string, double?>> {
                new Tuple<string, double?>("-12 * 1 + -721 * (15 +51)", -47598),
                new Tuple<string, double?>("(5+3)*6", 48),
                new Tuple<string, double?>("(((2+3))*(4+2))", 30),
                new Tuple<string, double?>("2*3/5", 1.2),
                new Tuple<string, double?>("2^3*2", 16),
                new Tuple<string, double?>("3-3+3*3/3^3", 1.0 / 3.0),
                new Tuple<string, double?>("2^2^3", 256),
                new Tuple<string, double?>("--5", 5),
                new Tuple<string, double?>("-(-5)", 5),
                new Tuple<string, double?>("4(5)", 20),
                new Tuple<string, double?>("(5)6", 30),
                new Tuple<string, double?>("100 v 10", 2),
                new Tuple<string, double?>("p", Math.PI/*3.14159265358979*/),
                new Tuple<string, double?>("π", Math.PI/*3.14159265358979*/),
                new Tuple<string, double?>("e", Math.E/*2.71828182845905*/),
                new Tuple<string, double?>("5×6÷10x3X2", 18),
                new Tuple<string, double?>("e^π", Math.Pow(Math.E, Math.PI)/*23.1406926327793*/),
                new Tuple<string, double?>("-(5+8)", -13),
                new Tuple<string, double?>("(-42) + (+24) + 12 + 8 - (-4)", 6),
                new Tuple<string, double?>("21 + 40 - (+9) + 413 + (-21) + 4 + 3", 451),
                new Tuple<string, double?>("5.92 - 27 + 19 - 37.1 + 27 - 25", -37.18),
                new Tuple<string, double?>("6 ÷ 2 (1+2)", 9),
                new Tuple<string, double?>("1+(2*(2+1)+2-(3*2)+1)", 4)
            };

            if (
                !string.IsNullOrWhiteSpace(_expression) &&
                _expected != null
            ) {
                testData.Add(new Tuple<string, double?>(_expression, _expected));
            }
            
            var passed = 0;
            var failed = 0;
            foreach (var (expression, expected) in testData) {
                var result = TestCase(expression, expected);
                if (result) { ++passed; }
                else { ++failed; }
            }
            
            Console.WriteLine("... End regression test");
            Console.WriteLine("Report:");
            Console.WriteLine($"Total: {passed + failed}");
            if (passed > 0) { Console.WriteLine($"Passed: {passed}"); }
            if (failed > 0) { Console.WriteLine($"Failed: {failed}"); }
        }

        private static bool TestCase(string expression, double? expected){
            var actual = new ExpressionEvaluator(expression).Evaluate();
            
            Console.WriteLine($"{expression} = {actual}");
            //if (NearEqual(expected, actual)) {
            if (expected != actual) {
                Console.WriteLine($"failed, expected: {_expected}");
                return false;
            }

            Console.WriteLine("passed");
            return true;
        }

        // modified from:
        // https://stackoverflow.com/questions/3874627/floating-point-comparison-functions-for-c-sharp
        private static bool NearEqual(double? expected, double? actual, double epsilon = double.Epsilon/*0.0000000000000009*/){ // epsilon determined by trial/error
            if (expected == null && actual == null) { return true; }
            if (expected == null || actual == null) { return false; }
            
            // var expected = (double) _expected;
            // var actual = (double) _actual;
            
            var absExpected = Math.Abs((double)expected);
            var absActual = Math.Abs((double)actual);
            var magnitude = Math.Abs((double)(expected - actual));
        
            if (expected == actual) { return true; } // shortcut, handles infinities
            if (
                expected == 0 || // expected is zero
                actual == 0 || // actual is zero
                magnitude < double.Epsilon // expected and actual values are essentially equal
            ) { return magnitude < epsilon; }  // relative error is less meaningful here
            return magnitude / (absExpected + absActual) < epsilon; // use relative error
        }
    }
}

