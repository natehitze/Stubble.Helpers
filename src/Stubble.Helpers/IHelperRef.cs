using System;
using System.Collections.Immutable;

namespace Stubble.Helpers
{
    public interface IHelperRef
    {
        ImmutableArray<Type> ArgumentTypes { get; }
    }
}
