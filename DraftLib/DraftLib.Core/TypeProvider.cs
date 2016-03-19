using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftLib.Core
{
    public static class TypeProvider<T>
    {
        static TypeProvider()
        {
            Type type = typeof(T);
            subTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
        }
        private static readonly IEnumerable<Type> subTypes;
        public static IEnumerable<Type> Types { get { return subTypes; } }

        public static IEnumerable<Type> ChildTypes { get { return subTypes.Except(new[] { typeof(T) }); } }
       
    }
}
