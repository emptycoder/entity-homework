using System;
using Homework.Models;
using Homework.Models.Links;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Homework
{
    public sealed class HomeworkContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        // Link tables
        public DbSet<FacultyGroup> FacultyGroups { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlite(@"Data Source=c:\test\test.db;");
        }

        public async Task InitializeDb()
        {
            await Database.EnsureCreatedAsync();
            
            if (Students.Any() || Groups.Any() || Faculties.Any())
            {
                return;
            }
            
            await using var transaction = await Database.BeginTransactionAsync();

            try
            {
                var students = new[]
                {
                    new Student {Email = "student1@mail.com", Name = "StudentName1", Surname = "StudentSurname1"},
                    new Student {Email = "student2@mail.com", Name = "StudentName2", Surname = "StudentSurname2"},
                    new Student {Email = "student3@mail.com", Name = "StudentName3", Surname = "StudentSurname3"},
                    new Student {Email = "student4@mail.com", Name = "StudentName4", Surname = "StudentSurname4"},
                    new Student {Email = "student5@mail.com", Name = "StudentName5", Surname = "StudentSurname5"},
                    new Student {Email = "student6@mail.com", Name = "StudentName6", Surname = "StudentSurname6"}
                };
                await Students.AddRangeAsync(students);
                await SaveChangesAsync();

                var groups = new[]
                {
                    new Group {Name = "PS-12-2"},
                    new Group {Name = "PS-12-1"},
                    new Group {Name = "PE-13-1"},
                    new Group {Name = "PE-13-2"},
                    new Group {Name = "PP-13-1"},
                    new Group {Name = "PP-13-2"}
                };
                await Groups.AddRangeAsync(groups);
                await SaveChangesAsync();

                var faculties = new[]
                {
                    new Faculty {Name = "Applied math"},
                    new Faculty {Name = "Philosophy"},
                    new Faculty {Name = "Ecology"}
                };
                await Faculties.AddRangeAsync(faculties);
                await SaveChangesAsync();
                
                await FacultyGroups.AddRangeAsync(new[] {
                    new FacultyGroup { GroupId = groups[0].Id, FacultyId = faculties[0].Id },
                    new FacultyGroup { GroupId = groups[1].Id, FacultyId = faculties[0].Id },
                    new FacultyGroup { GroupId = groups[2].Id, FacultyId = faculties[2].Id },
                    new FacultyGroup { GroupId = groups[3].Id, FacultyId = faculties[2].Id },
                    new FacultyGroup { GroupId = groups[4].Id, FacultyId = faculties[1].Id },
                    new FacultyGroup { GroupId = groups[5].Id, FacultyId = faculties[1].Id }
                });
                await SaveChangesAsync();
                
                await GroupStudents.AddRangeAsync(new[] {
                    new GroupStudent { GroupId = groups[0].Id, StudentId = students[0].Id},
                    new GroupStudent { GroupId = groups[1].Id, StudentId = students[1].Id},
                    new GroupStudent { GroupId = groups[2].Id, StudentId = students[2].Id},
                    new GroupStudent { GroupId = groups[3].Id, StudentId = students[3].Id},
                    new GroupStudent { GroupId = groups[4].Id, StudentId = students[4].Id},
                    new GroupStudent { GroupId = groups[5].Id, StudentId = students[5].Id}
                });
                
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}