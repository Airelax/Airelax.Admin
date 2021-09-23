using System;
using System.Linq.Expressions;

namespace Airelax.Domain.DomainObject
{
    public class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _leftSpecification;
        private readonly Specification<T> _rightSpecification;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _leftSpecification = left;
            _rightSpecification = right;
        }

        public override bool IsSatisfy(T o)
        {
            return _leftSpecification.IsSatisfy(o)
                   && _rightSpecification.IsSatisfy(o);
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _leftSpecification.ToExpression();
            var rightExpression = _rightSpecification.ToExpression();

            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            exprBody = (BinaryExpression) new ParameterReplacer(paramExpr).Visit(exprBody);

            return Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);
        }
    }
}