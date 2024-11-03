using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TodoApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Web.WebPages.Html;
using HtmlHelper = System.Web.Mvc.HtmlHelper;
using System.Web.Mvc.Html;

namespace TodoApplication.Helpers
{
    public static class TodoHelpers
    {
        public static IHtmlString ItemTableRow(this HtmlHelper htmlHelper, IEnumerable<TodoItem> records)
        {
            TagBuilder myTableBody = new TagBuilder("tbody");

            foreach (var item in records)
            {
                TagBuilder myTableRow = new TagBuilder("tr");

                // Creating cells for each property of TodoItem
                TagBuilder tableData1 = new TagBuilder("td");
                tableData1.InnerHtml = item.Task;
                tableData1.Attributes.Add("align","left");
                myTableRow.InnerHtml += tableData1.ToString(); // Adding the cell to the row

                TagBuilder tableData2 = new TagBuilder("td");
                tableData2.InnerHtml = item.IsCompleted ? "Yes" : "No";
                myTableRow.InnerHtml += tableData2.ToString(); // Adding the cell to the row

                TagBuilder tableData3 = new TagBuilder("td");
                tableData3.InnerHtml = item.TaskCreated.ToString();
                myTableRow.InnerHtml += tableData3.ToString(); // Adding the cell to the row

                TagBuilder tableData4 = new TagBuilder("td");
                tableData4.InnerHtml = item.TaskCompletionDate.ToString();
                myTableRow.InnerHtml += tableData4.ToString(); // Adding the cell to the row

                // Adding operations column with ActionLinks
                TagBuilder tableData5 = new TagBuilder("td");

                if (item.IsCompleted)
                {
                    //tableData5.InnerHtml += " " + htmlHelper.ActionLink("Task Done", "TaskStatus", new { id = item.Id }, new { @class = "completed btn btn-warning", @id = "taskCompBtn" }).ToString();
                    tableData5.InnerHtml += " " + "<span class='btn bg-primary text-light'>Edit</span>";
                }
                else
                {
                    tableData5.InnerHtml += " " + htmlHelper.ActionLink("Edit", "Edit", new { id = item.Id }, new { @class = "btn btn-primary" }).ToString();
                }

                if (item.IsCompleted)
                {
                    //tableData5.InnerHtml += " " + htmlHelper.ActionLink("Task Done", "TaskStatus", new { id = item.Id }, new { @class = "completed btn btn-warning", @id = "taskCompBtn" }).ToString();
                    tableData5.InnerHtml += " " + "<span class='completed btn bg-warning text-dark'>Task Done</span>";
                }
                else
                {
                    tableData5.InnerHtml += " " + htmlHelper.ActionLink("Task Completed", "TaskStatus", new { id = item.Id }, new { @class = "incompleted btn btn-success", @id = "taskCompBtn", data_id = item.Id }).ToString();
                }

                tableData5.InnerHtml += " " + htmlHelper.ActionLink("Delete", "Delete", new { id = item.Id }, new { @class = "btn btn-danger" }).ToString();

                myTableRow.InnerHtml += tableData5.ToString(); // Adding the cell to the row

                myTableBody.InnerHtml += myTableRow.ToString();

            }

            return new MvcHtmlString(myTableBody.ToString());
        }

        public static IHtmlString ItemTableHeader()
        {
            TagBuilder myTableHeaderRow = new TagBuilder("tr");

            // Creating cells for each property of TodoItem
            TagBuilder tableHead1 = new TagBuilder("th");
            tableHead1.InnerHtml = "Your Task";
            tableHead1.Attributes.Add("align", "left");
            myTableHeaderRow.InnerHtml += tableHead1.ToString(); // Adding the cell to the row

            TagBuilder tableHead2 = new TagBuilder("th");
            tableHead2.InnerHtml = "Completion Status";
            myTableHeaderRow.InnerHtml += tableHead2.ToString(); // Adding the cell to the row

            TagBuilder tableHead3 = new TagBuilder("th");
            tableHead3.InnerHtml = "Created On";
            myTableHeaderRow.InnerHtml += tableHead3.ToString(); // Adding the cell to the row

            TagBuilder tableHead4 = new TagBuilder("th");
            tableHead4.InnerHtml = "Completed On";
            myTableHeaderRow.InnerHtml += tableHead4.ToString(); // Adding the cell to the row

            TagBuilder tableHead5 = new TagBuilder("th");
            tableHead5.InnerHtml = "Operations";
            tableHead5.Attributes["colspan"] = "3";
            myTableHeaderRow.AddCssClass("bg-dark text-light");
            myTableHeaderRow.InnerHtml += tableHead5.ToString(); // Adding the cell to the row


            return new MvcHtmlString(myTableHeaderRow.ToString());
        }

    }
}