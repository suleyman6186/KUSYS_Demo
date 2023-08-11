using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IStudentService
    {
        void StudentAdd(Student student);
        void StudentDelete(Student student);
        void StudentUpdate(Student student);
        List<Student> GetList();
        Student GetById(int id);
    }
}