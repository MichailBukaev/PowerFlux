using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace PowerFlux.UnitTests.DbSetMoqing
{
  internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
  {
    public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
      : base(enumerable)
    {
    }

    public TestDbAsyncEnumerable(Expression expression)
      : base(expression)
    {
    }

    public IDbAsyncEnumerator<T> GetAsyncEnumerator()
    {
      return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() => GetAsyncEnumerator();

    IQueryProvider IQueryable.Provider => new TestDbAsyncQueryProvider<T>(this);
  }
}