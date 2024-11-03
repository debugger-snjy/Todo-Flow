namespace TodoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TodoItems", "Task", c => c.String(nullable: false, maxLength: 225));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TodoItems", "Task", c => c.String(nullable: false));
        }
    }
}
