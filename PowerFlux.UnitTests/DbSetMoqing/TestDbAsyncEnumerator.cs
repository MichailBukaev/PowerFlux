using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace PowerFlux.UnitTests.DbSetMoqing
{
  internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
  {
    private readonly IEnumerator<T> _inner;

    public TestDbAsyncEnumerator(IEnumerator<T> inner)
    {
      _inner = inner;
    }

    public void Dispose()
    {
      _inner.Dispose();
    }

    public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
    {
      return Task.FromResult(_inner.MoveNext());
    }

    public T Current => _inner.Current;

    object IDbAsyncEnumerator.Current => Current;
  }
}