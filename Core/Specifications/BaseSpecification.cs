using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria )
        {
            Criteria=criteria;
        }
        public Expression<Func<T, bool>> Criteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;}=new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get;private set;}

        public Expression<Func<T, object>> OrderByDescending{get;private set;}

        public int Take {get;private set;}

        public int Skip {get;private set;}

        public bool IsPagingenabled {get;private set;}

        protected void AddInclude(Expression<Func<T,object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }
        protected void AddOrderBy(Expression<Func<T,object>> oredrByExpression)
        {
            OrderBy=oredrByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T,object>> oredrByDescExpression)
        {
             OrderByDescending=oredrByDescExpression;
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip=skip;
            Take=take;
            IsPagingenabled=true;
        }
    }
}