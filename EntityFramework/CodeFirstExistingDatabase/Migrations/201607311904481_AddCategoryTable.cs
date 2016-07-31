namespace CodeFirstExistingDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("INSERT INTO CATEGORIES (id,Name) VALUES (1,'Web Development')");
            Sql("INSERT INTO CATEGORIES (id,Name) VALUES (2,'Programing Lenguajes')");

        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
