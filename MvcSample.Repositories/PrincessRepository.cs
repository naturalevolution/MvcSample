using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using MvcSample.Domain;

namespace MvcSample.Repositories {
    public class PrincessRepository : IPrincessRepository {
        private readonly MvcSampleContext _context;

        public PrincessRepository(MvcSampleContext context) {
            _context = context;
        }

        public IEnumerable<Princess> FindAll() {
            return _context.Princesses.ToList();
        }

        public IEnumerable<Princess> FindBy(Expression<Func<Princess, bool>> predicate) {
            throw new NotImplementedException();
        }

        public Princess FindById(int id) {
            if (id > 0) {
                return _context.Princesses.Find(id);
            }
            return null;
        }

        public void Add(Princess princess) {
            if (princess != null) {
                _context.Princesses.Add(princess);
                _context.SaveChanges();
            }
        }

        public MessageType Remove(int id) {
            return Remove(FindById(id));
        }
        
        public MessageType Update(Princess princess) {
            if (princess != null) {
                _context.Entry(princess).State = EntityState.Modified;
                _context.SaveChanges();
                return MessageType.Success;
            }
            return MessageType.Error;
        }

        private MessageType Remove(Princess princess) {
            if (princess != null) {
                _context.Princesses.Remove(princess);
                _context.SaveChanges();
                return MessageType.Success;
            }
            return MessageType.Error;
        }
    }
}