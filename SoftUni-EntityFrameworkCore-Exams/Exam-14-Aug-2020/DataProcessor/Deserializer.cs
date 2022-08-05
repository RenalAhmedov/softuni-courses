namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentDtos = JsonConvert.DeserializeObject<ImportDepartmentsAndCellsJsonDto[]>(jsonString);
            var departments = new HashSet<Department>();

            var sb = new StringBuilder();

            foreach (var departmentDto in departmentDtos)
            {
                bool hasInvalidCell = false;

                if (!IsValid(departmentDto) || departmentDto.Cells.Count == 0)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var department = new Department
                {
                    Name = departmentDto.Name,
                };

                foreach (var cellDto in departmentDto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        sb.AppendLine("Invalid Data");
                        hasInvalidCell = true;
                        break;
                    }

                    var cell = new Cell
                    {
                        Department = department,
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow,
                    };

                    department.Cells.Add(cell);
                    departments.Add(department);
                }

                if (hasInvalidCell)
                {
                    continue;
                }

                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString();

        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonerDtos = JsonConvert.DeserializeObject<ImportPrisonersAndMailsJsonDto[]>(jsonString);
            var sb = new StringBuilder();
            var prisoners = new HashSet<Prisoner>();

            foreach (var prisonerDto in prisonerDtos)
            {
                if (!IsValid(prisonerDto) || !prisonerDto.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var isValidReleaseDate = DateTime.TryParseExact(prisonerDto.RealseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime releaseDate);

                var incarcerationDate = DateTime.ParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var prisoner = new Prisoner
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = isValidReleaseDate ? releaseDate : (DateTime?)null,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId,
                    Mails = prisonerDto.Mails.Select(x => new Mail
                    {
                        Description = x.Description,
                        Sender = x.Sender,
                        Address = x.Address
                    })
                    .ToArray()
                };

                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportOfficersPrisonersXmlDto>), new XmlRootAttribute("Officers"));
            var sb = new StringBuilder();

            var officerDtos = (List<ImportOfficersPrisonersXmlDto>)serializer.Deserialize(new StringReader(xmlString));
            var officers = new HashSet<Officer>();

            foreach (var officerDto in officerDtos)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                if (!Enum.IsDefined(typeof(Weapon), officerDto.Weapon) || !Enum.IsDefined(typeof(Position), officerDto.Position))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var officer = new Officer
                {
                    DepartmentId = officerDto.DepartmentId,
                    FullName = officerDto.Name,
                    Position = Enum.Parse<Position>(officerDto.Position),
                    Weapon = Enum.Parse<Weapon>(officerDto.Weapon),
                    Salary = officerDto.Money,
                    OfficerPrisoners = officerDto.Prisoners.Select(x => new OfficerPrisoner
                    {
                        PrisonerId = x.Id
                    })
                    .ToArray()
                };

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}