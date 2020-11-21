using Stubble.Compilation.Settings;
using Stubble.Core.Parser.TokenParsers;

namespace Stubble.Compilation.Helpers
{
    public static class CompilationHelperExtensions
    {
        public static CompilerSettingsBuilder AddHelpers(this CompilerSettingsBuilder builder, CompiledHelpers helpers)
        {
            if (builder is null)
            {
                throw new System.ArgumentNullException(nameof(builder));
            }

            if (helpers is null)
            {
                throw new System.ArgumentNullException(nameof(helpers));
            }

            builder.ConfigureParserPipeline(pipelineBuilder => pipelineBuilder
                .AddBefore<InterpolationTagParser>(new CompiledHelperTagParser(helpers.HelperMap)));

            builder.TokenRenderers.Add(new CompiledHelperTagRenderer(helpers.HelperMap));

            return builder;
        }
    }
}
