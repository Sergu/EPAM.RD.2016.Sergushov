using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            //ConstantExpression five = Expression.Constant(5, typeof(int));
            //BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
            //Expression<Func<int, bool>> lambda1 =
            //    Expression.Lambda<Func<int, bool>>(
            //        numLessThanFive,
            //        new ParameterExpression[] { numParam });
            //Console.WriteLine(numParam.ToString());
            Expression<Func<int, bool>> expr = num => num < 5;
            Func<int, bool> result = expr.Compile();
            int[] arr = new int[] { 3, 5, 6, 1 };

            Func<int, bool> del = n => n < 6;

            var r = arr.Where(result);
            var re = arr.Where(del);
            


        }
    }
}
