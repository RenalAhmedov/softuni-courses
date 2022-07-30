namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shells = context.Shells
                .Where(x => x.ShellWeight > shellWeight)
                .ToArray()
                .Select(x => new
                {
                    ShellWeight = x.ShellWeight,
                    Caliber = x.Caliber,
                    Guns = x.Guns.OrderByDescending(g => g.GunWeight)
                    .Where(x => x.GunType == GunType.AntiAircraftGun)
                    .ToArray()
                    .Select(g => new
                    {
                        GunType = g.GunType.ToString(),
                        GunWeight = g.GunWeight,
                        BarrelLength = g.BarrelLength,
                        Range = g.Range > 3000 ? "Long-range" : "Regular range"
                    })
                    .ToArray()
                })
                .OrderBy(x => x.ShellWeight)
                .ToArray();

            var result = JsonConvert.SerializeObject(shells, Formatting.Indented);

            return result;
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            var serializer = new XmlSerializer(typeof(List<ExportGunModel>), new XmlRootAttribute("Guns"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var guns = context.Guns
                .Where(x => x.Manufacturer.ManufacturerName == manufacturer)
                .Select(x => new ExportGunModel
                {
                    Manufacturer = x.Manufacturer.ManufacturerName,
                    GunType = x.GunType.ToString(),
                    GunWeight = x.GunWeight,
                    BarrelLength = x.BarrelLength,
                    Range = x.Range,
                    Countries = x.CountriesGuns
                    .Where(x => x.Country.ArmySize > 4500000)
                    .Select(c => new ExportCountryModel
                    {
                        Country = c.Country.CountryName,
                        ArmySize = c.Country.ArmySize
                    })
                    .OrderBy(c => c.ArmySize)
                    .ToList()
                })
                .OrderBy(x => x.BarrelLength)
                .ToList();

            serializer.Serialize(sw, guns, namespaces);

            return sb.ToString();
        }
    }
}