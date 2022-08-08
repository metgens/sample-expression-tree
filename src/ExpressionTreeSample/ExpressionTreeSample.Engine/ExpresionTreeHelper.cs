using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Aq.ExpressionJsonSerializer;
using Newtonsoft.Json;

namespace ExpressionTreeSample.Engine
{
    public static class ExpressionTreeHelper
    {

        public static Func<T, int> CallAnyMathOperation<T>(string mathExpression)
        {
            var pattern = @"(.*)([\+\-\/\*])(.*)";
            var matchCollection = new Regex(pattern).Match(mathExpression);
            var selector1 = matchCollection.Groups[1].Value;
            var selector2 = matchCollection.Groups[3].Value;
            var operation = matchCollection.Groups[2].Value;

            return CallAnyMathOperation<T>(selector1, selector2, operation).Compile();
        }

        private static Expression<Func<T, int>> CallAnyMathOperation<T>(string selector1, string selector2, string operation)
        {
            var target = Expression.Parameter(typeof(T), "data");
            var memberAccess1 = CreateMemberAccess(target, selector1);
            var memberAccess2 = CreateMemberAccess(target, selector2);
            var expressionType = ParseExpressionOperator(operation);

            return Expression.Lambda<Func<T, int>>(Expression.MakeBinary(expressionType, memberAccess1, memberAccess2), false, target);
        }

        public static string SerializeAnyMathOperation<T>(string mathExpression)
        {
            var pattern = @"(.*)([\+\-\/\*])(.*)";
            var matchCollection = new Regex(pattern).Match(mathExpression);
            var selector1 = matchCollection.Groups[1].Value;
            var selector2 = matchCollection.Groups[3].Value;
            var operation = matchCollection.Groups[2].Value;

            var expresion = CallAnyMathOperation<T>(selector1, selector2, operation);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ExpressionJsonConverter(Assembly.GetAssembly(typeof(ExpressionTreeHelper))
            ));

            var json = JsonConvert.SerializeObject(expresion, settings);
            return json;
        }


        private static ExpressionType ParseExpressionOperator(string operation)
        {

            switch (operation?.ToLower())
            {
                case "+":
                    return ExpressionType.Add;
                case "-":
                    return ExpressionType.Subtract;
                case "*":
                    return ExpressionType.Multiply;
                case "/":
                    return ExpressionType.Divide;
            }

            throw new ArgumentException("Wrong operation type provided");
        }

        private static Expression CreateMemberAccess(Expression target, string selector)
        {
            var isConstantRegex = new Regex(@"^[0-9]*$");
            if (isConstantRegex.IsMatch(selector))
            {
                return Expression.Constant(ConversionHelper.ConvertToInt(selector), typeof(int));
            }

            return selector.Split('.').Aggregate(target, (t, n) => Expression.PropertyOrField(t, n));
        }


    }
}