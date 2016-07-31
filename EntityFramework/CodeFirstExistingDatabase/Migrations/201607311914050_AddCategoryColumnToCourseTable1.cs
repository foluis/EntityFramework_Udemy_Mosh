namespace CodeFirstExistingDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryColumnToCourseTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Courses SET Category_Id = 1");
        }
        
        public override void Down()
        {
        }
    }
}
