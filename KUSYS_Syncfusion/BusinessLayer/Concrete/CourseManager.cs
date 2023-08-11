using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CourseManager : ICourseService
    {
        ICourseDal _courseDal;

        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }

        public Course GetById(int id)
        {
            return _courseDal.GetByID(id);
        }

        public List<Course> GetList()
        {
            return _courseDal.GetListAll();
        }

        public void CourseAdd(Course course)
        {
            _courseDal.Insert(course);
        }

        public void CourseDelete(Course course)
        {

            _courseDal.Delete(course);
        }

        public void CourseUpdate(Course course)
        {
            _courseDal.Update(course);
        }
    }
}