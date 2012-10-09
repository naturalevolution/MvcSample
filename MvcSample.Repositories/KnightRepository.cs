using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using MvcSample.Domain;

namespace MvcSample.Repositories {
    public class KnightRepository : IKnightRepository {
        private readonly MvcSampleContext _context;

        public KnightRepository(MvcSampleContext context) {
            _context = context;
        }
        
        public IEnumerable<Knight> FindAll() {
            return _context.Knights.ToList();
        }

        public IEnumerable<Knight> FindBy(Expression<Func<Knight, bool>> predicate) {
            throw new NotImplementedException();
        }

        public Knight FindById(int id) {
            if (id > 0) {
                return _context.Knights.Find(id);
            }
            return null;
        }

        public void Add(Knight knight) {
            if (knight != null) {
                _context.Knights.Add(knight);
                _context.SaveChanges();
            }
        }

        public MessageType Remove(int id) {
            return Remove(FindById(id));
        }

        public MessageType Update(Knight knight) {
            if (knight != null) {
                _context.Entry(knight).State = EntityState.Modified;
                _context.SaveChanges();
                return MessageType.Success;
            }
            return MessageType.Error;
        }

        private MessageType Remove(Knight knight) {
            if (knight != null) {
                _context.Knights.Remove(knight);
                _context.SaveChanges();
                return MessageType.Success;
            }
            return MessageType.Error;
        }
    }
}