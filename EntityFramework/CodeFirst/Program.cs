using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public CourseLevel Level { get; set; }

        public float FullPrice { get; set; }

        public Author Author { get; set; }

        public IList<Tag> Tags { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Course> Courses { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Course> Courses { get; set; }
    }

    public enum CourseLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advance = 3
    }

    public class PlutoContext : DbContext
    {
        public PlutoContext()
            : base("name=DefaultConnection")
        {

        }

        DbSet<Course> Courses { get; set; }

        DbSet<Author> Authors { get; set; }

        DbSet<Tag> Tags { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
