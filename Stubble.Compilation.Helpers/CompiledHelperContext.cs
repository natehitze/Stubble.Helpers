using System;
using System.Linq.Expressions;
using Stubble.Compilation.Contexts;

namespace Stubble.Compilation.Helpers
{
    public class CompiledHelperContext
    {
        private readonly CompilerContext _context;

        public CompiledHelperContext(CompilerContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            _context = context;
            //RendererSettings = new HelperRendererSettings(_context.RendererSettings);
        }

        /// <summary>
        /// Gets the render settings for the context
        /// </summary>
        //public RenderSettings RenderSettings => _context.RenderSettings;

        /// <summary>
        /// Gets the renderer settings for the context
        /// </summary>
        //public HelperRendererSettings RendererSettings { get; }

        /// <summary>
        /// Looks up a value by name from the context
        /// </summary>
        /// <param name="name">The name of the value to lookup</param>
        /// <exception cref="StubbleDataMissException">If ThrowOnDataMiss set then thrown on value not found</exception>
        /// <returns>The value if found or null if not</returns>
        public Expression Lookup(string name)
            => _context.Lookup(name);
    }
}
