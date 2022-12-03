using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace WeatherDisplay.Api.Services.Configuration
{
    internal class PropertyUpdater<TEntity, TValue>
    {
        internal static readonly ConcurrentDictionary<Func<Expression<Func<TEntity, TValue>>>, PropertyUpdater<TEntity, TValue>> Cache = new ConcurrentDictionary<Func<Expression<Func<TEntity, TValue>>>, PropertyUpdater<TEntity, TValue>>();

        private readonly PropertyInfo propertyInfo;

        public static PropertyUpdater<TEntity, TValue> GetPropertyUpdater(Func<Expression<Func<TEntity, TValue>>> expressionGetter)
        {
            return Cache.GetOrAdd(expressionGetter, x => new PropertyUpdater<TEntity, TValue>(x));
        }

        private PropertyUpdater(Func<Expression<Func<TEntity, TValue>>> expressionGetter)
        {
            var propertyExpr = (MemberExpression)expressionGetter().Body;

            this.propertyInfo = (PropertyInfo)propertyExpr.Member;
        }

        public string Name => this.propertyInfo.Name;

        public TValue UpdateValue(TEntity entity, TValue newValue)
        {
            var oldValue = (TValue)this.propertyInfo.GetValue(entity);
            this.propertyInfo.SetValue(entity, newValue);
            return oldValue;
        }
    }
}