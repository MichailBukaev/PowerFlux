using System;
using System.Linq;
using PowerFlux.Db.Entities.Interfaces;

namespace PowerFlux.Db
{
  public interface IDataService
  {
    public IQueryable<T> GetEntities<T>() where T : Entity;
  }
}
