using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Moq;

namespace PowerFlux.UnitTests.DbSetMoqing
{
  public static class DbSetMockFactory
  {
    public static Mock<DbSet<T>> SetupDbSetMoq<T>(IQueryable<T> data) where T : class
    {
      var mock = new Mock<DbSet<T>>();

      mock.As<IDbAsyncEnumerable<T>>()
        .Setup(x => x.GetAsyncEnumerator())
        .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

      mock.As<IQueryable<T>>()
        .Setup(x => x.Provider)
        .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));

      mock.As<IQueryable<T>>()
        .Setup(x => x.Expression)
        .Returns(data.Expression);

      mock.As<IQueryable<T>>()
        .Setup(x => x.ElementType)
        .Returns(data.ElementType);

      mock.As<IQueryable<T>>()
        .Setup(x => x.GetEnumerator())
        .Returns(data.GetEnumerator());

      return mock;
    }
  }
}
