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
    [Authorize(Roles = "Admin")]
    public class CoursesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        CourseManager cm = new CourseManager(new EfCourseRepository());

        public CoursesController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("courses")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Insert([FromBody] CRUDModel<Course> param)
        {
            cm.CourseAdd(param.Value);
            return Json(param);
        }

        public IActionResult Update([FromBody] CRUDModel<Course> param)
        {
            cm.CourseUpdate(param.Value);
            return Json(param);
        }

        public IActionResult Remove([FromBody] CRUDModel param)
        {
            int CourseID = Int32.Parse(param.Key.ToString());
            var student = cm.GetById(CourseID);
            if (student != null)
            {
                cm.CourseDelete(student);
            }
            return Json(UrlDatasource(new DataManagerRequest()));
        }

        public object UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = cm.GetList(); //Utils.DataTableToJson(dt); //Here dt is the dataTable
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
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
}