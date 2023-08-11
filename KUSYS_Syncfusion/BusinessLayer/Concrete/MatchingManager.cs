using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class MatchingManager : IMatchingService
    {
        IMatchingDal _matchingDal;

        public MatchingManager(IMatchingDal matchingDal)
        {
            _matchingDal = matchingDal;
        }

        public Matching GetById(int id)
        {
            return _matchingDal.GetByID(id);
        }

        public List<Matching> GetList()
        {
            return _matchingDal.GetListAll();
        }

        public List<Matching> GetMatchingByStudentID(int id)
        {
            return _matchingDal.GetListAll(x => x.StudentRecordID == id).ToList();
        }

        public void MatchingAdd(Matching matching)
        {
            _matchingDal.Insert(matching);
        }

        public void MatchingDelete(Matching matching)
        {

            _matchingDal.Delete(matching);
        }

        public void MatchingUpdate(Matching matching)
        {
            _matchingDal.Update(matching);
        }
    }
}