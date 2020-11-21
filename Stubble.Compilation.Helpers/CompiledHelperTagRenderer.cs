using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Stubble.Compilation.Contexts;
using Stubble.Compilation.Renderers;
using Stubble.Helpers;

namespace Stubble.Compilation.Helpers
{
    public class CompiledHelperTagRenderer : ExpressionObjectRenderer<HelperToken>
    {
        private readonly ImmutableDictionary<string, CompiledHelperRef> _helperCache;

        public CompiledHelperTagRenderer(ImmutableDictionary<string, CompiledHelperRef> helperCache)
        {
            _helperCache = helperCache;
        }

        protected override void Write(CompilationRenderer renderer, HelperToken obj, CompilerContext context)
        {
            if (renderer is null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (_helperCache.TryGetValue(obj.Name, out var helper))
            {
                var helperContext = new CompiledHelperContext(context);
                var args = obj.Args;

                var argumentTypes = helper.ArgumentTypes;
                if ((argumentTypes.Length - 1) == args.Length)
                {
                    var arr = new Expression[args.Length + 1];
                    // TODO Context isn't really helpful here but tag parser expects it to be an argument
                    arr[0] = Expression.Constant(null, typeof(CompiledHelperContext));
                    //arr[0] = helperContext;

                    for (var i = 0; i < args.Length; i++)
                    {
                        var arg = args[i].ShouldAttemptContextLoad
                            ? context.Lookup(args[i].Value)
                            : Expression.Constant(args[i].Value);

                        // TODO
                        //arg = TryConvertTypeIfRequired(arg, args[i].Value, argumentTypes[i + 1]);

                        arr[i + 1] = arg;
                    }

                    // TODO: grab html encoding/escaping block from Stubble.Compilation's InterpolationTokenRenderer
                    // TODO: also the indent block?

                    var invocation = Expression.Invoke(helper.Expression, arr);
                    var appendMethod = typeof(StringBuilder).GetMethod("Append", new[] { invocation.Type });
                    var append = Expression.Call(renderer.Builder, appendMethod, invocation);
                    renderer.AddExpressionToScope(append);

                    //var result = helper.Delegate.Method.Invoke(helper.Delegate.Target, arr);
                    //if (result is string str)
                    //{
                    //    renderer.Write(str);
                    //}
                    //else if (result is object)
                    //{
                    //    renderer.Write(Convert.ToString(result, context.RenderSettings.CultureInfo));
                    //}
                }
            }
        }

        protected override Task WriteAsync(CompilationRenderer renderer, HelperToken obj, CompilerContext context)
        {
            Write(renderer, obj, context);
            return Task.CompletedTask;
        }

//        private static Expression TryConvertTypeIfRequired(object value, string arg, Type type)
//        {
//            if (value is null && !type.IsValueType)
//            {
//                return null;
//            }
//            else if (value is null)
//            {
//                // When lookup is null and type is not a string we should try convert since may be a constant integer or float.
//                value = arg;
//            }

//            var lookupType = value.GetType();

//            if (lookupType == type)
//            {
//                return value;
//            }

//            if (type.IsAssignableFrom(lookupType))
//            {
//                return value;
//            }

//            try
//            {
//                return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
//            }
//#pragma warning disable CA1031 // Do not catch general exception types
//            catch
//#pragma warning restore CA1031 // Do not catch general exception types
//            {
//            }

//            return null;
//        }
    }
}
