using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace Stubble.Compilation.Helpers
{
    public class CompiledHelpers
    {
        private readonly Dictionary<string, CompiledHelperRef> _helpers
            = new Dictionary<string, CompiledHelperRef>();

        public ImmutableDictionary<string, CompiledHelperRef> HelperMap => _helpers.ToImmutableDictionary(StringComparer.OrdinalIgnoreCase);

        public CompiledHelpers Register<T>(string name, Expression<Func<CompiledHelperContext, T, object>> func) => Register(name, func, func?.Parameters); 

        //public Helpers Register(string name, Func<HelperContext, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2>(string name, Func<HelperContext, T2, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3>(string name, Func<HelperContext, T2, T3, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4>(string name, Func<HelperContext, T2, T3, T4, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5>(string name, Func<HelperContext, T2, T3, T4, T5, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6>(string name, Func<HelperContext, T2, T3, T4, T5, T6, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7, T8>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, T8, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7, T8, T9>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, T8, T9, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7, T8, T9, T10>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, T8, T9, T10, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object> func) => Register(name, (Delegate)func);
        //public Helpers Register<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string name, Func<HelperContext, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object> func) => Register(name, (Delegate)func);

        private CompiledHelpers Register(string name, Expression expression, IList<ParameterExpression> expressionParameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (expressionParameters is null)
            {
                throw new ArgumentNullException(nameof(expressionParameters));
            }

            _helpers[name.Trim()] = new CompiledHelperRef(expression, expressionParameters);
            return this;
        }
    }
}
