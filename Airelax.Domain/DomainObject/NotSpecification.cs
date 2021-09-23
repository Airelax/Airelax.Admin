using System;
using System.Linq.Expressions;

namespace Airelax.Domain.DomainObject
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }

        public override bool IsSatisfy(T o)
        {
            return !_specification.IsSatisfy(o);
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = _specification.ToExpression();
            return Expression.Lambda<Func<T, bool>>(
                Expression.Not(expression.Body), expression.Parameters[0]);
        }
    }
}