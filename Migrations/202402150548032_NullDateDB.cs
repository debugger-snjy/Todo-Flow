namespace TodoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullDateDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TodoItems", "TaskUpdated", c => c.DateTime());
            AlterColumn("dbo.TodoItems", "TaskCompletionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TodoItems", "TaskCompletionDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TodoItems", "TaskUpdated", c => c.DateTime(nullable: false));
        }
    }
}
