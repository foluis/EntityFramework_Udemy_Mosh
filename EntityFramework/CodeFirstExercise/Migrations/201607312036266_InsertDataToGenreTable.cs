namespace CodeFirstExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertDataToGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Action')");
            Sql("INSERT INTO Genres (Name) VALUES ('Romance')");
        }
        
        public override void Down()
        {
            Sql("DELETE Genres");
        }
    }
}
