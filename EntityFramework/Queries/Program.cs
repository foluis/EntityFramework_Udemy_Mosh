using System.Linq;
using System;
using System.Data.Entity;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            //Section 7
            ExplicitLoading();
            EagerLoading();
            LazyLoading();
            //SEction 6
            //Class_51_DeferredExecution();
            //Class_50();
            //Class_49_LinqExtensionMethods();
            //Class_48();

        }

        private static void ExplicitLoading()
        {
            var context = new PlutoContext();

            //ExplicitLoading
            var author = context.Authors.Single(a => a.Id == 1);

            //MSDN Way, only for single result on the original query
            context.Entry(author).Collection(e => e.Courses).Load();
            context.Entry(author).Collection(e => e.Courses).Query().Where(c => c.FullPrice == 0).Load();

            //Better way
            context.Courses.Where(c => c.AuthorId == author.Id).Load();
            context.Courses.Where(c => c.AuthorId == author.Id && c.FullPrice == 0).Load();

            foreach (var course in author.Courses)
            {
                Console.WriteLine($"{course.Name}");
            }

            //EagerLoading
            var author2 = context.Authors.Include(a => a.Courses).Single(a => a.Id == 1);
             
            foreach (var course in author2.Courses)
            {
                Console.WriteLine($"{course.Name}");
            }


        }

        private static void EagerLoading()
        {
            //using System.Data.Entity;

            var context = new PlutoContext();

            //var courses = context.Courses.Include("Author").ToList(); //Bad practice
            var courses = context.Courses.Include(c => c.Author).ToList(); //Good practice

            foreach (var c in courses)
            {
                Console.WriteLine($"{c.Name} by {c.Author.Name}");
            }
        }

        private static void LazyLoading()
        {
            var context = new PlutoContext();

            //Ejemplo 1
            var course = context.Courses.Single(c=> c.Id == 2); //Firs query to Database (Load Courses)

            foreach (var tag in course.Tags)//Second query to Database (Load Tags)
            {
                Console.WriteLine(tag.Name);
            }

            //ejemplo 2
            var courses = context.Courses.ToList(); //Firs query to Database (Load Courses)

            foreach (var c in courses)
            {
                Console.WriteLine($"{c.Name} by {c.Author.Name}"); //For each iteration will query the database
            }
        }

        private static void Class_51_DeferredExecution()
        {
            var context = new PlutoContext();

            var courses = context.Courses;

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }

        private static void Class_50()
        {
            var context = new PlutoContext();

            //Particioning Seccion 6 - Clase 50 Minuto 00:03

            //Paginado por ejemplo paginas de 10 en 10
            //Pagina 2 

            var courses = context.Courses.Skip(10).Take(10);

            //Element Operator Seccion 6 - Clase 50 Minuto 00:37
            //Get a single onject or de first one

            //First element of list, if empty will get an exception
            //var course = context.Courses.OrderBy(c => c.Level).First();

            //FirstOrDefault element of list, if empty will return null
            //var course = context.Courses.OrderBy(c => c.Level).FirstOrDefault();
            //var course = context.Courses.OrderBy(c => c.Level).FirstOrDefault(c => c.FullPrice > 100);

            //Single element of list, if empty will return an exception, if multiple will get an exception
            //var course = context.Courses.OrderBy(c => c.Level).Single();

            //Single element of list, if empty will return null, if multiple elements will return an exception
            //var course = context.Courses.OrderBy(c => c.Level).SingleOrDefault();

            //Quantifying
            var areAllAbove10Dollars = context.Courses.All(c => c.FullPrice > 10);

            var isAnyInLevel1 = context.Courses.Any(c => c.Level == 1);

            //Agregating
            var qty = context.Courses.Count();

            var expensiveCourse = context.Courses.Max(c => c.FullPrice);

            var shipestCourse = context.Courses.Min(c => c.FullPrice);

            var avaragePrice = context.Courses.Average(c => c.FullPrice);
        }

        private static void Class_49_LinqExtensionMethods()
        {
            var context = new PlutoContext();

            //Restriction

            //var courses = context.Courses.Where(c => c.Level == 1);

            //Ordering

            //var courses = context.Courses
            //    .Where(c => c.Level == 1)
            //    .OrderByDescending(c => c.Name)
            //    .ThenBy(c => c.Level);

            //Projection

            //var courses = context.Courses
            //    .Where(c => c.Level == 1)
            //    .OrderByDescending(c => c.Name)
            //    .ThenBy(c => c.Level)
            //    .Select(c => new { CourseName = c.Name, AuthorName = c.Author.Name });

            //var courses = context.Courses
            //  .Where(c => c.Level == 1)
            //  .OrderByDescending(c => c.Name)
            //  .ThenBy(c => c.Level)
            //  .Select(c => c.Tags);

            //foreach (var course in courses)
            //{
            //    foreach (var tag in course)
            //    {
            //        Console.WriteLine(tag.Name);
            //    }
            //}

            ////Flaten result
            //var tags = context.Courses
            //.Where(c => c.Level == 1)
            //.OrderByDescending(c => c.Name)
            //.ThenBy(c => c.Level)
            //.SelectMany(c => c.Tags);

            //foreach (var t in tags)
            //{
            //    Console.WriteLine(t.Name);
            //}

            //Set Operators

            //var tags = context.Courses
            //.Where(c => c.Level == 1)
            //.OrderByDescending(c => c.Name)
            //.ThenBy(c => c.Level)
            //.SelectMany(c => c.Tags)
            //.Distinct(); ;

            //foreach (var t in tags)
            //{
            //    Console.WriteLine(t.Name);
            //}

            //Grouping

            //var courses = context.Courses.GroupBy(g => g.Level);

            //foreach (var group in courses)
            //{
            //    Console.WriteLine("Key: " + group.Key);

            //    foreach (var course in group)
            //    {
            //        Console.WriteLine("\t" + course.Name);
            //    }
            //}

            //Joins Seccion 6 - Clase 49 Minuto 7:30

            //context.Courses.Join(context.Authors,
            //    c => c.AuthorId,
            //    a => a.Id,
            //    (course, author) => new
            //    {
            //        CourseName = course.Name,
            //        AuthorName = author.Name
            //    });

            //Group Joins Seccion 6 - Clase 49 Minuto 11:43

            //context.Authors.GroupJoin(context.Courses,a => a.Id,c =>c.AuthorId,(author,courses) => new
            //{
            //    AuthorName = author.Name,
            //    Courses = courses
            //});

            //Cross Joins Seccion 6 - Clase 49 Minuto 14:52

            //context.Authors.SelectMany(a => context.Courses, (author,course) => new
            //{
            //    AuthorName = author.Name,
            //    CourseName = course.Name                
            //});

            

        }

        private static void Class_48()
        {
            var context = new PlutoContext();

            //Restriction

            //var query =
            //    from c in context.Courses
            //    where c.Level == 1          //---> using System.Linq;
            //        && c.AuthorId == 1
            //    select c;

            //OrderBY

            //var query =
            //    from c in context.Courses
            //    where c.AuthorId == 1
            //    orderby c.Level descending,c.Name
            //    select c;

            //projection

            //var query =
            //    from c in context.Courses
            //    where c.AuthorId == 1
            //    orderby c.Level descending, c.Name
            //    select new {Name = c.Name , Author = c.Author.Name};

            //Grouping

            //var query =
            //    from c in context.Courses
            //    group c by c.Level 
            //    into g
            //    select g;

            //foreach (var group in query)
            //{
            //    Console.WriteLine(group.Key);

            //    foreach (var course in group)
            //    {
            //        Console.WriteLine("\t{0}",course.Name);
            //    }
            //}

            //Grouping with agregate function

            //var query =
            //    from c in context.Courses
            //    group c by c.Level
            //    into g
            //    select g;

            //foreach (var group in query)
            //{
            //    Console.WriteLine("{0} ({1})",group.Key,group.Count());
            //}

            //Joining: Inner Join, Group Join, Cross Join
            //Inner Join
            //var query =
            //    from c in context.Courses
            //    join a in context.Authors on c.AuthorId equals a.Id
            //    select new { CourseName = c.Name, AuthorName = a.Name};

            //Group Join

            var query =
                from a in context.Authors
                join c in context.Courses on a.Id equals c.AuthorId into g
                select new { AuthorName = a.Name, Courses = g.Count() };

            foreach (var x in query)
            {
                Console.WriteLine("{0} ({1})", x.AuthorName, x.Courses);
            }
        }

        public void Class_47()
        {
            var context = new PlutoContext();

            //LINQ Syntax

            var query =
                from c in context.Courses
                where c.Name.Contains("C#")
                orderby c.Name
                select c;

            //foreach (var course in query)
            //{
            //    Console.WriteLine(course.Name);
            //}

            //Extension Method

            var courses = context.Courses
                .Where(c => c.Name.Contains("C#"))
                .OrderBy(c => c.Name);

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }
    }
}
