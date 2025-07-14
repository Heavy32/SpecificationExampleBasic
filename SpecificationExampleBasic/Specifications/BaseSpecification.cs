using System.Linq.Expressions;

namespace SpecificationExampleBasic.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<T, object>> include)
            => Includes.Add(include);

        public ISpecification<T> And(ISpecification<T> specification)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var left = Criteria.Body;
            var right = specification.Criteria.Body;
            
            // Replace parameters in the right expression to match the left
            var visitor = new ParameterReplacer(specification.Criteria.Parameters[0], parameter);
            var rightBody = visitor.Visit(right);
            
            var combined = Expression.AndAlso(left, rightBody);
            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            
            return new CombinedSpecification<T>(lambda, this, specification);
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var left = Criteria.Body;
            var right = specification.Criteria.Body;
            
            // Replace parameters in the right expression to match the left
            var visitor = new ParameterReplacer(specification.Criteria.Parameters[0], parameter);
            var rightBody = visitor.Visit(right);
            
            var combined = Expression.OrElse(left, rightBody);
            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            
            return new CombinedSpecification<T>(lambda, this, specification);
        }

        public ISpecification<T> Not()
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var body = Expression.Not(Criteria.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            
            return new CombinedSpecification<T>(lambda, this, null);
        }
    }

    // Helper class to replace parameters in expressions
    public class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParameter ? _newParameter : base.VisitParameter(node);
        }
    }

    // Concrete specification for combining multiple specifications
    public class CombinedSpecification<T> : BaseSpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public CombinedSpecification(Expression<Func<T, bool>> criteria, ISpecification<T> left, ISpecification<T> right) 
            : base(criteria)
        {
            _left = left;
            _right = right;
            
            // Combine includes from both specifications
            if (left != null)
                Includes.AddRange(left.Includes);
            if (right != null)
                Includes.AddRange(right.Includes);
        }
    }
}
