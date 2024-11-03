using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using TodoApplication.Models;
using TodoApplication.Models.DataContext;

namespace TodoApplication.Controllers
{
    public class TodoController : Controller
    {
        // Created the Context Data Object
        ContextData _contextData = new ContextData();

        // Index or Home Page to display the Task List
        public ActionResult Index()
        {
            // Getting all the Data from the Database and sending it to the view
            var alldata = _contextData.Todos.ToList();

            return View(alldata);
        }

        // Page to Create or Add the New Task
        public ActionResult Create()
        {
            return View();
        }

        // View or Action to do on Submitting the Add New Task Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoItem newTodoItem)
        {
            // Checking the Validation of the Form
            if (ModelState.IsValid)
            {

                if (_contextData.Todos.Where(model => model.Task == newTodoItem.Task && model.IsCompleted == false).ToList().Count > 0)
                {
                    // Showing the Alert Box on Inserting the Task in the DB
                    TempData["alertMsg"] = "Task Already Exists !!";
                    TempData["alertType"] = "danger";

                    // Redirecting the User to Index of Todo Controller
                    return RedirectToAction("Index");
                }

                // Updating the Dates for the Task Completion
                newTodoItem.TaskCreated = DateTime.Now;
                newTodoItem.TaskUpdated = null;
                newTodoItem.TaskCompletionDate = null;


                _contextData.Todos.Add(newTodoItem);

                // TODO NOTE : We have to Convert the DateTime to datetime2 in the Database Manually !!
                int rowAffected = _contextData.SaveChanges();

                if (rowAffected > 0)
                {
                    // Showing the Alert Box on Inserting the Task in the DB
                    TempData["alertMsg"] = "Your Task is Added !!";
                    TempData["alertType"] = "success";

                    // Redirecting the User to Index of Todo Controller
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["alertMsg"] = "Your Task is NOT Added !!";
                    TempData["alertType"] = "danger";
                }
            }
            else
            {
                TempData["alertMsg"] = "Your Form is Invalid !!";
                TempData["alertType"] = "danger";
            }

            return View();
        }

        // Page for Deletion Process to Display Data
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                // TODO : I will add an Error Page
            }
            else
            {
                TodoItem deletedItem = _contextData.Todos.Where(model => model.Id == id).FirstOrDefault();
                return View(deletedItem);
            }
            return View();
        }

        // Action or View to Delete the Record on Delete Button
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TodoItem deletedRecord = _contextData.Todos.Find(id);
            _contextData.Todos.Remove(deletedRecord);
            int rowAffected = _contextData.SaveChanges();

            if (rowAffected > 0)
            {
                TempData["alertMsg"] = "Record Deleted Successfully";
                TempData["alertType"] = "success";

                // If the Record Inserted Successfully then Redirecting it to the Index Page of User Controller
                return RedirectToAction("Index");
            }
            else
            {
                TempData["alertMsg"] = "Record Deletion Failed";
                TempData["alertType"] = "danger";
            }

            return RedirectToAction("Index");
        }

        public ActionResult TaskStatus(int id)
        {
            TodoItem record = _contextData.Todos.Find(id);
            
            // Removing Condition for Mkaing the Completed Task Incompleted
            //if (record.IsCompleted)
            //{
            //    record.IsCompleted = false;
            //    record.TaskCompletionDate = null;
            //}
            //else
            //{
            //    record.IsCompleted = true;
            //    record.TaskCompletionDate = DateTime.Now;
            //}

            record.IsCompleted = true;
            record.TaskCompletionDate = DateTime.Now;

            int rowAffected = _contextData.SaveChanges();

            if (rowAffected > 0)
            {
                if (record.IsCompleted)
                {
                    TempData["alertMsg"] = "Congrats, Your Task is Completed";
                    TempData["alertType"] = "success";
                }
                else
                {
                    TempData["alertMsg"] = "Oops ! Your Task is Still Remaining";
                    TempData["alertType"] = "warning";
                }

                // If the Record Inserted Successfully then Redirecting it to the Index Page of User Controller
                return RedirectToAction("Index");
            }
            else
            {
                TempData["alertMsg"] = "Something Went Wrong !!";
                TempData["alertType"] = "danger";
            }

            return RedirectToAction("index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // TODO : I will add an Error Page
            }
            else
            {
                TodoItem editedItem = _contextData.Todos.Where(model => model.Id == id).FirstOrDefault();
                return View(editedItem);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TodoItem editedItem)
        {
            // Checking the Validation of the Form
            if (ModelState.IsValid)
            {

                if (editedItem.TaskCompletionDate != null)
                {
                    if (editedItem.IsCompleted) { }
                    else
                    {
                        editedItem.TaskCompletionDate = null;
                    }
                }
                else
                {
                    if (editedItem.IsCompleted)
                    {
                        editedItem.TaskCompletionDate = DateTime.Now;
                    }
                }

                _contextData.Entry(editedItem).State = EntityState.Modified;

                // TODO NOTE : We have to Convert the DateTime to datetime2 in the Database Manually !!
                int rowAffected = _contextData.SaveChanges();

                if (rowAffected > 0)
                {
                    // Showing the Alert Box on Inserting the Task in the DB
                    TempData["alertMsg"] = "Your Task is Added !!";
                    TempData["alertType"] = "success";

                    // Redirecting the User to Index of Todo Controller
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["alertMsg"] = "Your Task is NOT Added !!";
                    TempData["alertType"] = "success";
                }
            }
            else
            {
                TempData["alertMsg"] = "Your Form is Invalid !!";
                TempData["alertType"] = "success";
            }

            return View();
        }
    }
}