using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using Stubble.Helpers;

namespace Stubble.Compilation.Helpers
{
    public readonly struct CompiledHelperRef : IEquatable<CompiledHelperRef>, IHelperRef
    {
        public CompiledHelperRef(Expression expression, IList<ParameterExpression> expressionParameters)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));

            var @params = expressionParameters ?? throw new ArgumentNullException(nameof(expressionParameters));
            var builder = ImmutableArray.CreateBuilder<Type>(@params.Count);
            foreach (var arg in @params)
            {
                builder.Add(arg.Type);
            }
            ArgumentTypes = builder.ToImmutable();
        }

        public Expression Expression { get; }

        public ImmutableArray<Type> ArgumentTypes { get; }

        public override bool Equals(object obj)
        {
            return obj is HelperRef @ref && Equals(@ref);
        }

        public bool Equals(CompiledHelperRef other)
        {
            return EqualityComparer<Expression>.Default.Equals(Expression, other.Expression) &&
                   CompareHelper.CompareImmutableArrays(ArgumentTypes, other.ArgumentTypes);
        }

        public override int GetHashCode()
        {
            var hashCode = -1973005441;
            hashCode = hashCode * -1521134295 + EqualityComparer<Expression>.Default.GetHashCode(Expression);
            foreach (var type in ArgumentTypes)
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(type);
            }
            return hashCode;
        }

        public static bool operator ==(CompiledHelperRef left, CompiledHelperRef right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CompiledHelperRef left, CompiledHelperRef right)
        {
            return !(left == right);
        }
    }
}
