namespace CodeFirstExercise
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ExerciseModel : DbContext
    {
        // Your context has been configured to use a 'ExerciseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CodeFirstExercise.ExerciseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ExerciseModel' 
        // connection string in the application configuration file.
        public ExerciseModel()
            : base("name=ExerciseModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        public Classification Classification { get; set; }
    }

    public class Genre
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public ICollection<Video> Videos { get; set; }
    }

    public enum Classification
    {
        Silver = 1,
        Gold = 2,
        Platinum= 3
    }

   
}