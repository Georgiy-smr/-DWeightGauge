using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.Extensions
{
    public static class RandomExtensions
    {
        public static T NextItem<T>(this Random rnd, params T[] items) =>
            items[rnd.Next(items.Length)];
    }
}
