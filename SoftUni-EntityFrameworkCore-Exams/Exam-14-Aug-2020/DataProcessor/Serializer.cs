namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
               .Where(x => ids.Contains(x.Id))
               .ToArray()
               .Select(x => new
               {
                   Id = x.Id,
                   Name = x.FullName,
                   CellNumber = x.Cell.CellNumber,
                   Officers = x.PrisonerOfficers.Select(o => new
                   {
                       OfficerName = o.Officer.FullName,
                       Department = o.Officer.Department.Name
                   })
                   .OrderBy(o => o.OfficerName)
                   .ToArray(),
                   TotalOfficerSalary = x.PrisonerOfficers.Sum(x => x.Officer.Salary)
               })
               .OrderBy(x => x.Name)
               .ThenBy(x => x.Id)
               .ToArray();

            var result = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var serializer = new XmlSerializer(typeof(PrisonersInboxDto[]), new XmlRootAttribute("Prisoners"));
            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            var names = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var prisoners = context.Prisoners
                .Where(x => names.Contains(x.FullName))
                .Select(x => new PrisonersInboxDto
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = x.Mails.Select(m => new EncryptedMessageDto
                    {
                        Description = new string(m.Description.Reverse().ToArray())
                    })
                    .ToArray()
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();

            serializer.Serialize(sw, prisoners, namespaces);

            return sb.ToString();
        }
    }
}