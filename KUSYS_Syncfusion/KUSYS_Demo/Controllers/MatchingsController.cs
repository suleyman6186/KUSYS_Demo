using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;
using System.Collections;

namespace KUSYS_Demo.Controllers
{
    public class MatchingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        CourseManager cm = new CourseManager(new EfCourseRepository());
        StudentManager sm = new StudentManager(new EfStudentRepository());
        MatchingManager mm = new MatchingManager(new EfMatchingRepository());

        public MatchingsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [Route("student-course-matchings/{p:int?}")]
        public async Task<IActionResult> Index(int? p)
        {
            if (p != null)
            {
                ViewBag.ddlListStudentValue = p;
                ViewBag.ddlListCourseEnable = true;
                ViewBag.CourseList = GetCourseListDataSource(p);
            }
            else
            {
                ViewBag.ddlListStudentValue = 0;
                ViewBag.ddlListCourseEnable = false;
                ViewBag.CourseList = GetCourseListDataSource(null);
            }

            ViewBag.StudentList = GetStudentListDataSource();
            ViewBag.MatchingGridDataSource = await GetMatchingGridDataSource();

            return View();
        }

        [Authorize(Roles = "User")]
        [Route("my-course-selections")]
        public async Task<IActionResult> MyMatchings()
        { 
            //Getting user info from AspNetUsers table
            var UserInfo = await _userManager.FindByNameAsync(User.Identity?.Name);
            //Getting StudentRecordID from Students table
            int StudentRecordID = sm.GetStudentByStudentID(UserInfo.UserName).Select(x => x.Id).First();
            //Getting CourseList not selected before
            ViewBag.CourseList = GetCourseListDataSource(StudentRecordID);
            return View();
        }
        public async Task<List<Matching>> GetMatchingGridDataSource()
        {
            List<Matching> data = new List<Matching>();
            //Getting user info from AspNetUsers table
            var UserInfo = await _userManager.FindByNameAsync(User.Identity?.Name);
            //Getting role info from AspNetUserRoles table
            var RoleInfo = await _userManager.GetRolesAsync(UserInfo);

            if (RoleInfo != null)
            {
                if (RoleInfo[0] == "User")
                {
                    //Getting StudentRecordID from Students table
                    int StudentRecordID = sm.GetStudentByStudentID(UserInfo.UserName).Select(x => x.Id).First();
                    //Getting Course selections of Student's his/her own
                    data = mm.GetMatchingByStudentID(StudentRecordID);
                }
                else
                {
                    data = mm.GetList();
                }
            }

            List<Matching> MatchingList = new List<Matching>();

            foreach (var item in data)
            {
                //Getting student info
                var student = sm.GetById(item.StudentRecordID);
                //Getting course info
                var course = cm.GetById(item.CourseRecordID);
                MatchingList.Add(new Matching()
                {
                    Id = item.Id,
                    CreationDate = item.CreationDate,
                    StudentDetail = student.StudentId + " - " + student.FirstName + " " + student.LastName,
                    CourseDetail = course.CourseId + " - " + course.CourseName
                });
            }
            
            return MatchingList.OrderByDescending(x => x.CreationDate).ToList();
        }

        public List<DDList> GetStudentListDataSource()
        {
            List<DDList> StudentList = new List<DDList>();

            //Getting all students list
            var data = sm.GetList();

            foreach (var item in data)
            {
                StudentList.Add(new DDList()
                {
                    text = item.StudentId + " - " + item.FirstName + " " + item.LastName,
                    value = item.Id
                });
            }

            return StudentList;
        }

        public List<DDList> GetCourseListDataSource(int? StudentRecordID)
        {
            List<DDList> CourseList = new List<DDList>();
            List<Course> data = cm.GetList();

            //if StudentRecordID not null
            if (StudentRecordID != null)
            {
                //Getting AlreadySelectedCoursesIDs from Matching table by StudentID
                var AlreadySelectedCoursesID = mm.GetMatchingByStudentID(StudentRecordID.Value).Select(x => x.CourseRecordID).ToArray();
                //Getting course list for Student not selected course/s before
                data = data.Where(f => !AlreadySelectedCoursesID.Contains(f.Id)).ToList();
            }

            foreach (var item in data)
            {
                CourseList.Add(new DDList()
                {
                    text = item.CourseId + " - " + item.CourseName,
                    value = item.Id
                });
            }

            return CourseList;
        }

        [HttpPost]
        public async Task<JsonResult> SaveMatching(MatchingInfo info)
        {
            //Getting user info from AspNetUsers table
            var UserInfo = await _userManager.FindByNameAsync(User.Identity?.Name);
            //Getting role info from AspNetUserRoles table
            var RoleInfo = await _userManager.GetRolesAsync(UserInfo);
           
            if(RoleInfo !=  null) 
            {
                if(RoleInfo[0] == "User") // If the role of the logged in user is "User"
                {
                    //Getting StudentRecordID from Students table
                    int StudentRecordID = sm.GetStudentByStudentID(UserInfo.UserName).Select(x => x.Id).First();
                    //We assigned StudentRecordID to info.StudentRecordID
                    info.StudentRecordID = StudentRecordID;
                }   
            }

            DateTime dt = DateTime.Now;

            //info.CourseRecordIDs array is sorted
            Array.Sort(info.CourseRecordIDs);

            //We added selected courses to Matchings table with foreach loop
            foreach (var item in info.CourseRecordIDs)
            {
                Matching match = new Matching();
                match.CreationDate = dt;
                match.StudentRecordID = info.StudentRecordID;
                match.CourseRecordID = item;
                mm.MatchingAdd(match);
            }

            return Json(info);
        }
        public async Task<IActionResult> Remove([FromBody] CRUDModel param)
        {
            // Getting MatchingID from param.Key
            int MatchingID = Int32.Parse(param.Key.ToString());
            //Getting matching info from table
            var matching = mm.GetById(MatchingID);
            if (matching != null) // if matching info not null
            {
                //matching record is deleting
                mm.MatchingDelete(matching);
            }
            // returning datasource after record deletion
            return Json(await UrlDatasource(new DataManagerRequest()));
        }

        public async Task<object> UrlDatasource([FromBody] DataManagerRequest dm)
        {
            //Necessary for Searching, Sorting, Filtering, Paging
            IEnumerable DataSource = await GetMatchingGridDataSource();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Searching
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            List<string> str = new List<string>();
            if (dm.Aggregates != null)
            {
                for (var i = 0; i < dm.Aggregates.Count; i++)
                    str.Add(dm.Aggregates[i].Field);
            }
            IEnumerable aggregate = operation.PerformSelect(DataSource, str);
            int count = DataSource.Cast<object>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count, aggregate = aggregate }) : Json(DataSource);
        }
    }

    public class DDList
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class MatchingInfo
    {
        public int StudentRecordID { get; set; }
        public int[] CourseRecordIDs { get; set; }
    }
}