using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MvcSample.Domain;

namespace MvcSample.Repositories {
    public interface IRepository<T> {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindById(int id);
        void Add(T newEntity);
        MessageType Remove(int id);
        MessageType Update(T expected);
    }

    public interface IKnightRepository : IRepository<Knight> {
    }
    public interface IPrincessRepository : IRepository<Princess> {
    }


    public enum MessageType {
        Success,
        Error
    }
}
