using System.Collections.Immutable;

namespace Stubble.Helpers
{
    public class HelperTagParser : HelperTagParserBase<HelperRef>
    {
        public HelperTagParser(ImmutableDictionary<string, HelperRef> helperMap) : base(helperMap)
        {
        }
    }
}
