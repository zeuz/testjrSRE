using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using System.Text;
using System.Security.Cryptography;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {

        private ContosoUniversityContext db = new ContosoUniversityContext();

        private int[,] constVals = new int[8, 5] { {3000,4000,5000,6000,8000 },
                                        {401,311,543,126,223},
                                        {226,456,152,543,333 },
                                        {226,118,453,301,543 },
                                        {109,237,311,421,543 },
                                        {614,239,314,141,435 },
                                        {276,337,111,589,443 },
                                        {12,37,31,01,43 }
                                };

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                   group student by student.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult test(string sid)
        {
            string ret = "";
            try
            {
                int id = Int32.Parse(sid);
                if (id > 0)
                {
                    calc(getBase(id));
                    ret = "OK";
                }
            }catch(Exception ex)
            {
                return Json($"Error", JsonRequestBehavior.AllowGet);
            }
            return Json(ret, JsonRequestBehavior.AllowGet); 
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        private int getBase(int id)
        {
            int ret = 1;
            if (id >= 1000)
            {
                int b = id / 1000;
                id = id % 1000;
                ret = constVals[b, id];
            }
            else
            {
                ret = constVals[7, id];
            }
            
            return ret;
        }
            
        private string calc(int id)
        {
            String cadToEncrypt = "";
            for (int y = 0; y < id; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    Guid guid = Guid.NewGuid();
                    cadToEncrypt += guid.ToString();
                }

                SHA256 mySHA256 = SHA256.Create();
                byte[] hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(cadToEncrypt));
            }
            return "";
        }
        

    }
}