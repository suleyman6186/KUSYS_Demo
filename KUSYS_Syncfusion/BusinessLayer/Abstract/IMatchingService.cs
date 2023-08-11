using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IMatchingService
    {
        void MatchingAdd(Matching matching);
        void MatchingDelete(Matching matching);
        void MatchingUpdate(Matching matching);
        List<Matching> GetList();
        Matching GetById(int id);
    }
}