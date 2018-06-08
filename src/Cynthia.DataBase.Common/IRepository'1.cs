using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cynthia.DataBase.Common
{
    public interface IRepository<TModel> : IRepository, IQueryable<TModel>, ICollection<TModel>
    {
        TModel this[string id] { get; set; }
        void Add(IEnumerable<TModel> items);
        int Remove(Expression<Func<TModel, bool>> predicate);
        bool Updata(TModel item);
        int Updata(IEnumerable<TModel> items);
        int Updata(Expression<Func<TModel, bool>> predicate, TModel item);
    }
}