using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;


namespace mvc_ej2.Controllers
{
    public class HomeController : Controller
    {

        public static List<OrdersDetails> orddata = new List<OrdersDetails>();
        public static List<OrdersData> pfGridData = new List<OrdersData>();


        public static List<Employee1Details> empdata = new List<Employee1Details>();
        public ActionResult Index()
        {
            if (orddata.Count() == 0)
                BindData();

            ViewBag.datasource = orddata.ToArray();
            ViewBag.PFGridData = pfGridData.ToArray();
            return View();
        }
        public void BindData()
        {
            int code = 10000;
            for (int i = 1; i < 2; i++)
            {
                orddata.Add(new OrdersDetails(code + 2, "ANATR", 3.3 * i, "Madrid"));
                orddata.Add(new OrdersDetails(code + 3, "ANTON", 4.3 * i, "Cholchester"));
                orddata.Add(new OrdersDetails(code + 4, "BLONP", 5.3 * i, "Marseille"));
                orddata.Add(new OrdersDetails(code + 5, "BOLID", 6.3 * i, "Tsawassen"));
            }
        }


        public ActionResult UrlDatasource(DataManagerRequest dm)
        {

            IEnumerable DataSource = orddata.ToList();
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
            int count = DataSource.Cast<OrdersDetails>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        public ActionResult Update(CRUDModel myObject)
        {
            var ord = myObject.Value;
            OrdersData val = pfGridData.Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            val.OrderID = ord.OrderID;            
            val.CustomerID = ord.CustomerID;
            val.Freight = ord.Freight;            
            val.ShipCity = ord.ShipCity;
            return Json(myObject.Value);

        }
        public ActionResult Insert(CRUDModel myObject)
        {
            var ord = myObject.Value;
            pfGridData.Insert(0, ord);
            return Json(myObject.Value);
        }
        public ActionResult Remove(int key)
        {
            pfGridData.Remove(pfGridData.Where(or => or.OrderID == key).FirstOrDefault());
            return Json(new { result = orddata, count = orddata.Count });
        }

        public ActionResult SrcUpdate(CRUDInstance myObject)
        {
            var ord = myObject.Value;
            OrdersDetails val = orddata.Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            val.OrderID = ord.OrderID;
            val.CustomerID = ord.CustomerID;
            val.Freight = ord.Freight;
            val.ShipCity = ord.ShipCity;
            return Json(myObject.Value);

        }
        public ActionResult SrcInsert(CRUDInstance myObject)
        {
            var ord = myObject.Value;
            orddata.Insert(0, ord);
            return Json(myObject.Value);
        }
        public ActionResult SrcRemove(int key)
        {
            orddata.Remove(orddata.Where(or => or.OrderID == key).FirstOrDefault());
            return Json(new { result = orddata, count = orddata.Count });
        }

        public ActionResult ChildDatasource(DataManagerRequest dm)
        {
            IEnumerable DataSource = Employee1Details.GetAllRecords().ToList();
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
            int count = DataSource.Cast<Employee1Details>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);

        }
        public ActionResult ChildUpdate(CRUDModel2 myObject)
        {
            var ord = myObject.Value;
            Employee1Details val = Employee1Details.GetAllRecords().Where(or => or.EmployeeID == ord.EmployeeID).FirstOrDefault();
            val.EmployeeID = ord.EmployeeID;
            val.FirstName = ord.FirstName;
            val.LastName = ord.LastName;
            val.ReportsTo = ord.ReportsTo;

            return Json(myObject.Value);

        }
        public ActionResult ChildInsert(CRUDModel2 myObject)
        {
            var ord = myObject.Value;
            Employee1Details.GetAllRecords().Insert(0, ord);
            return Json(myObject.Value);
        }
        public ActionResult ChildRemove(int key)
        {
            Employee1Details.GetAllRecords().Remove(Employee1Details.GetAllRecords().Where(or => or.EmployeeID == key).FirstOrDefault());
            var data = Employee1Details.GetAllRecords();
            return Json(new { result = data, count = data.Count });
        }

        public void PFGridInsert(List<OrdersDetails> addData)
        {

        }


        public class CRUDInstance
        {
            public List<OrdersDetails> Added { get; set; }
            public List<OrdersDetails> Changed { get; set; }
            public List<OrdersDetails> Deleted { get; set; }
            public OrdersDetails Value { get; set; }
            public int key { get; set; }
            public string action { get; set; }
        }

        public class CRUDModel
        {
            public List<OrdersData> Added { get; set; }
            public List<OrdersData> Changed { get; set; }
            public List<OrdersData> Deleted { get; set; }
            public OrdersData Value { get; set; }
            public int key { get; set; }
            public string action { get; set; }
        }
        public class CRUDModel2
        {
            public List<OrdersDetails> Added { get; set; }
            public List<OrdersDetails> Changed { get; set; }
            public List<OrdersDetails> Deleted { get; set; }
            public Employee1Details Value { get; set; }
            public int key { get; set; }
            public string action { get; set; }
        }


        public class OrdersData{

            public OrdersData() {

            }
            public OrdersData(int OrderID, string CustomerId, double Freight, string ShipCity) {
                this.OrderID = OrderID;
                this.CustomerID = CustomerId;                
                this.Freight = Freight;
                this.ShipCity = ShipCity;
            }
            public int? OrderID { get; set; }
            public string CustomerID { get; set; }            
            public double? Freight { get; set; }
            public string ShipCity { get; set; }
        }


        public class OrdersDetails
        {
            public OrdersDetails()
            {

            }
            public OrdersDetails(int OrderID, string CustomerId, double Freight,string ShipCity)
            {
                this.OrderID = OrderID;
                this.CustomerID = CustomerId;
                //this.EmployeeID = EmployeeId;
                this.Freight = Freight;
                this.ShipCity = ShipCity;
                //this.Verified = Verified;
                //this.OrderDate = OrderDate;
                //this.ShipName = ShipName;
                //this.ShipCountry = ShipCountry;
                //this.ShippedDate = ShippedDate;
                //this.ShipAddress = ShipAddress;
            }

            public int? OrderID { get; set; }
            public string CustomerID { get; set; }
            public int? EmployeeID { get; set; }
            public double? Freight { get; set; }
            public string ShipCity { get; set; }
            public bool Verified { get; set; }
            public DateTime OrderDate { get; set; }

            public string ShipName { get; set; }

            public string ShipCountry { get; set; }

            public DateTime ShippedDate { get; set; }
            public string ShipAddress { get; set; }
        }
        public class Employee1Details
        {
            public static List<Employee1Details> order = new List<Employee1Details>();
            public Employee1Details()
            {

            }
            public Employee1Details(int EmployeeId, string FirstName, string LastName, int ReportsTO)
            {
                this.EmployeeID = EmployeeId;
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.ReportsTo = ReportsTo;
            }
            public static List<Employee1Details> GetAllRecords()
            {
                if (order.Count() == 0)
                {
                    for (int i = 1; i < 2; i++)
                    {
                        order.Add(new Employee1Details(i + 0, "Nancy", "Davolio", i + 0));
                        order.Add(new Employee1Details(i + 1, "Andrew", "Fuller", i + 3));
                        order.Add(new Employee1Details(i + 2, "Janet", "Leverling", i + 2));
                        order.Add(new Employee1Details(i + 3, "Margaret", "Peacock", i + 1));
                                               
                    }
                }
                return order;
            }


            public int? EmployeeID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? ReportsTo { get; set; }
        }
    }
}