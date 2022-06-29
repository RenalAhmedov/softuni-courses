using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext db = new SoftUniContext();
            string result = RemoveTown(db);

            Console.WriteLine(result);
        }

        //03->
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToArray();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }
        //<--

        //04->
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .ToArray();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} - {e.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }
        //<--

        //05->
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .ToArray();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} from Research and Development - ${e.Salary:F2}");
            }
            return sb.ToString().TrimStart();
        }
        //<--

        //06->
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address newAdress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };
            context.Addresses.Add(newAdress);
            context.SaveChanges();

            Employee nakovEmployee = context
                .Employees
                .First(e => e.LastName == "Nakov");
            nakovEmployee.Address = newAdress;
            context.SaveChanges();

            string[] allEmployeesAddr = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10)
                .ToArray();

            return String.Join(Environment.NewLine, allEmployeesAddr);
        }
        //<--

        //07->
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        Name = p.Project.Name,
                        p.Project.StartDate,
                        p.Project.EndDate
                    })
                })
                .Where(x => x.Projects.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
                .Take(10)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: " +
                    $"{employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.Projects)
                {
                    if (project.EndDate == null)
                    {
                        sb.AppendLine($"--{project.Name} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - not finished");
                    }
                    else
                    {
                        sb.AppendLine($"--{project.Name} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - " +
                            $"{project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt")}");
                    }
                }
            }

            return sb.ToString();
        }
        //<--

        //08->
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Include(a => a.Town)
                .Include(a => a.Employees)
                .OrderByDescending(x => x.Employees.Count())
                .ThenBy(x => x.Town.Name)
                .ThenBy(x => x.AddressText)
                .Take(10)
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var address in addresses)
            {
                result.AppendLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count()} employees");
            }

            return result.ToString().TrimEnd();
        }
        //<--

        //09->
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context
                 .Employees
                 .Include(e => e.EmployeesProjects)
                 .ThenInclude(ep => ep.Project)
                 .Where(e => e.EmployeeId == 147)
                 .FirstOrDefault();

            StringBuilder result = new StringBuilder();
            result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var project in employee.EmployeesProjects.OrderBy(x => x.Project.Name))
            {
                result.AppendLine($"{project.Project.Name}");
            }

            return result.ToString().TrimEnd();
        }
        //<--

        //10->
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
            .Where(x => x.Employees.Count > 5)
            .Select(d => new
            {
                DepartmentName = d.Name,
                ManagerFirstName = d.Manager.FirstName,
                ManagerLastName = d.Manager.LastName,
                Employees = d.Employees.Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle
                })
            })
            .OrderBy(x => x.Employees.Count())
            .ThenBy(x => x.DepartmentName)
            .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.DepartmentName} - {department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var employee in department.Employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return sb.ToString();
        }
        //<--

        //11->
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
               .OrderByDescending(x => x.StartDate)
               .Take(10)
               .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var project in projects.OrderBy(x => x.Name))
            {
                sb.AppendLine($"{project.Name}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{project.StartDate.ToString("M/d/yyyy h:mm:ss tt")}");
            }

            return sb.ToString().Trim();
        }
        //<--

        //12->
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees
               .Where(x => x.Department.Name == "Engineering" ||
               x.Department.Name == "Tool Design" ||
               x.Department.Name == "Marketing" ||
               x.Department.Name == "Information Services")
               .Select(x => new
               {
                   x.FirstName,
                   x.LastName,
                   Salary = x.Salary * 1.12m
               })
               .OrderBy(x => x.FirstName)
               .ThenBy(x => x.LastName)
               .ToArray();


            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }

            return sb.ToString().Trim();
        }
        //<--

        //13->
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(x => x.FirstName.StartsWith("Sa"))
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
            }

            return sb.ToString().Trim();
        }
        //<--

        //14->
        public static string DeleteProjectById(SoftUniContext context)
        {
            var projectToDelete = context.EmployeesProjects
                .FirstOrDefault(x => x.ProjectId == 2);

            StringBuilder sb = new StringBuilder();

            var delete = context.EmployeesProjects.Remove(projectToDelete);
            context.SaveChanges();

            var projects = context
                .Projects
                .Take(10)
                .ToArray();

            foreach (var project in projects)
            {
                sb.AppendLine($"{project.Name}");
            }

            return sb.ToString().Trim();
        }
        //<--

        //15->
        public static string RemoveTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Where(x => x.Town.Name == "Seattle")
                .ToArray();

            foreach (var address in addresses)
            {
                context.Addresses.Remove(address);
            }

            var town = context.Towns
                .Where(x => x.Name == "Seattle")
                .Single();

            context.Towns.Remove(town);
            context.SaveChanges();

            return $"{addresses.Length} addresses in Seattle were deleted";
        }
        //<--
    }
}
