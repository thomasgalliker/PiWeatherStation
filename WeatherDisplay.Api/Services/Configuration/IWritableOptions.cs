using System.Linq.Expressions;
using Microsoft.Extensions.Options;

namespace WeatherDisplay.Api.Services.Configuration
{
    public interface IWritableOptions<T> : IOptionsSnapshot<T> where T : class, new()
    {
        void UpdateProperty<TValue>(Expression<Func<T, TValue>> propertySelector, TValue value);

        void Update(Action<T> applyChanges);

        void Update(Func<T, T> applyChanges);
    }
}