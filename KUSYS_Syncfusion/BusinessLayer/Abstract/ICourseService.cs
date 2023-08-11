using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ICourseService
    {
        void CourseAdd(Course Course);
        void CourseDelete(Course Course);
        void CourseUpdate(Course Course);
        List<Course> GetList();
        Course GetById(int id);
    }
}