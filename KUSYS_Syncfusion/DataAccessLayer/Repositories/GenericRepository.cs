using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using (var db = new Context())
            {
                db.Remove(t);
                db.SaveChanges();
            }
        }

        public T GetByID(int id)
        {
            using (var db = new Context())
            {
                return db.Set<T>().Find(id);
            }
        }

        public T GetByStudentID(string StudentId)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListAll()
        {
            using (var db = new Context())
            {
                return db.Set<T>().ToList();
            }
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            using (var db = new Context())
            {
                return db.Set<T>().Where(filter).ToList();
            }
        }

        public void Insert(T t)
        {
            using (var db = new Context())
            {
                db.Add(t);
                db.SaveChanges();
            }
        }

        public void Update(T t)
        {
            using (var db = new Context())
            {
                db.Update(t);
                db.SaveChanges();
            }
        }
    }
}
