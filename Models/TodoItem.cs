using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

// Created the TodoItem Model
namespace TodoApplication.Models
{
    public class TodoItem
    {
        [Key]  // Primary Key for the Database
        public int Id { get; set; }

        // Validations for the Task Title or Message
        [Required(ErrorMessage = "Task Details are Required")]
        [StringLength(225, ErrorMessage = "String length must be greater than or equal 5 characters.",MinimumLength = 5)]
        [DataType(DataType.MultilineText)] // For the MultiLine Text Enable
        public string Task { get; set; }

        // Parameter for Task Completion Status
        [DefaultValue(false)]
        public Boolean IsCompleted { get; set; }

        // Paramter for Task Created Datetime
        [DataType(DataType.DateTime)]
        public DateTime TaskCreated { get; set; }

        // Paramter for Task Updated Datatime
        [DataType(DataType.DateTime)]
        public DateTime? TaskUpdated { get; set; }

        // Paramter for Task Updated Datatime
        [DataType(DataType.DateTime)]
        public DateTime? TaskCompletionDate { get; set; }

        // Constructor
        public TodoItem()
        {
            TaskCreated = DateTime.Now;
        }
    }
}