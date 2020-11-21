using System.Collections.Immutable;
using Stubble.Helpers;

namespace Stubble.Compilation.Helpers
{
    public class CompiledHelperTagParser : HelperTagParserBase<CompiledHelperRef>
    {
        public CompiledHelperTagParser(ImmutableDictionary<string, CompiledHelperRef> helperMap) : base(helperMap)
        {
        }
    }
}
