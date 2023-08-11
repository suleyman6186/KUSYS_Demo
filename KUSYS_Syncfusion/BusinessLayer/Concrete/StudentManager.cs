using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class StudentManager : IStudentService
    {
        IStudentDal _studentDal;

        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        public Student GetById(int id)
        {
            return _studentDal.GetByID(id);
        }

        public List<Student> GetList()
        {
            return _studentDal.GetListAll();
        }

        public List<Student> GetStudentByStudentID(string id)
        {
            return _studentDal.GetListAll(x => x.StudentId == id).ToList();
        }

        public void StudentAdd(Student student)
        {
            _studentDal.Insert(student);
        }

        public void StudentDelete(Student student)
        {

            _studentDal.Delete(student);
        }

        public void StudentUpdate(Student student)
        {
            _studentDal.Update(student);
        }
    }
}