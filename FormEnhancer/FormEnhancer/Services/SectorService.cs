using FormEnhancer.Data.Models;
using FormEnhancer.Repository;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.RegularExpressions;

namespace FormEnhancer.Services
{
    public class SectorService : ISectorService
    {
        private readonly ISectorRepository _sectorRepository;

        public SectorService(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        private const string PatternForValue = @"([""'])(?:(?=(\\?))\2.)*?\1";
        private const string PatternForName = @"\>(.+?)\<";
        private static string SectorsOptions => "<option value=\"1\">Manufacturing</option>\r\n            <option value=\"19\">&nbsp;&nbsp;&nbsp;&nbsp;Construction materials</option>\r\n            <option value=\"18\">&nbsp;&nbsp;&nbsp;&nbsp;Electronics and Optics</option>\r\n            <option value=\"6\">&nbsp;&nbsp;&nbsp;&nbsp;Food and Beverage</option>\r\n            <option value=\"342\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bakery &amp; confectionery products</option>\r\n            <option value=\"43\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Beverages</option>\r\n            <option value=\"42\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fish &amp; fish products </option>\r\n            <option value=\"40\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Meat &amp; meat products</option>\r\n            <option value=\"39\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Milk &amp; dairy products </option>\r\n            <option value=\"437\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Other</option>\r\n            <option value=\"378\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sweets &amp; snack food</option>\r\n            <option value=\"13\">&nbsp;&nbsp;&nbsp;&nbsp;Furniture</option>\r\n            <option value=\"389\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bathroom/sauna </option>\r\n            <option value=\"385\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bedroom</option>\r\n            <option value=\"390\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Children’s room </option>\r\n            <option value=\"98\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Kitchen </option>\r\n            <option value=\"101\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Living room </option>\r\n            <option value=\"392\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Office</option>\r\n            <option value=\"394\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Other (Furniture)</option>\r\n            <option value=\"341\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Outdoor </option>\r\n            <option value=\"99\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Project furniture</option>\r\n            <option value=\"12\">&nbsp;&nbsp;&nbsp;&nbsp;Machinery</option>\r\n            <option value=\"94\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Machinery components</option>\r\n            <option value=\"91\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Machinery equipment/tools</option>\r\n            <option value=\"224\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Manufacture of machinery </option>\r\n            <option value=\"97\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Maritime</option>\r\n            <option value=\"271\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Aluminium and steel workboats </option>\r\n            <option value=\"269\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Boat/Yacht building</option>\r\n            <option value=\"230\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ship repair and conversion</option>\r\n            <option value=\"93\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Metal structures</option>\r\n            <option value=\"508\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Other</option>\r\n            <option value=\"227\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Repair and maintenance service</option>\r\n            <option value=\"11\">&nbsp;&nbsp;&nbsp;&nbsp;Metalworking</option>\r\n            <option value=\"67\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Construction of metal structures</option>\r\n            <option value=\"263\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Houses and buildings</option>\r\n            <option value=\"267\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Metal products</option>\r\n            <option value=\"542\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Metal works</option>\r\n            <option value=\"75\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CNC-machining</option>\r\n            <option value=\"62\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Forgings, Fasteners </option>\r\n            <option value=\"69\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Gas, Plasma, Laser cutting</option>\r\n            <option value=\"66\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MIG, TIG, Aluminum welding</option>\r\n            <option value=\"9\">&nbsp;&nbsp;&nbsp;&nbsp;Plastic and Rubber</option>\r\n            <option value=\"54\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Packaging</option>\r\n            <option value=\"556\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Plastic goods</option>\r\n            <option value=\"559\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Plastic processing technology</option>\r\n            <option value=\"55\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Blowing</option>\r\n            <option value=\"57\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Moulding</option>\r\n            <option value=\"53\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Plastics welding and processing</option>\r\n            <option value=\"560\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Plastic profiles</option>\r\n            <option value=\"5\">&nbsp;&nbsp;&nbsp;&nbsp;Printing </option>\r\n            <option value=\"148\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Advertising</option>\r\n            <option value=\"150\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Book/Periodicals printing</option>\r\n            <option value=\"145\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Labelling and packaging printing</option>\r\n            <option value=\"7\">&nbsp;&nbsp;&nbsp;&nbsp;Textile and Clothing</option>\r\n            <option value=\"44\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Clothing</option>\r\n            <option value=\"45\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Textile</option>\r\n            <option value=\"8\">&nbsp;&nbsp;&nbsp;&nbsp;Wood</option>\r\n            <option value=\"337\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Other (Wood)</option>\r\n            <option value=\"51\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Wooden building materials</option>\r\n            <option value=\"47\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Wooden houses</option>\r\n            <option value=\"3\">Other</option>\r\n            <option value=\"37\">&nbsp;&nbsp;&nbsp;&nbsp;Creative industries</option>\r\n            <option value=\"29\">&nbsp;&nbsp;&nbsp;&nbsp;Energy technology</option>\r\n            <option value=\"33\">&nbsp;&nbsp;&nbsp;&nbsp;Environment</option>\r\n            <option value=\"2\">Service</option>\r\n            <option value=\"25\">&nbsp;&nbsp;&nbsp;&nbsp;Business services</option>\r\n            <option value=\"35\">&nbsp;&nbsp;&nbsp;&nbsp;Engineering</option>\r\n            <option value=\"28\">&nbsp;&nbsp;&nbsp;&nbsp;Information Technology and Telecommunications</option>\r\n            <option value=\"581\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Data processing, Web portals, E-marketing</option>\r\n            <option value=\"576\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Programming, Consultancy</option>\r\n            <option value=\"121\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Software, Hardware</option>\r\n            <option value=\"122\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Telecommunications</option>\r\n            <option value=\"22\">&nbsp;&nbsp;&nbsp;&nbsp;Tourism</option>\r\n            <option value=\"141\">&nbsp;&nbsp;&nbsp;&nbsp;Translation services</option>\r\n            <option value=\"21\">&nbsp;&nbsp;&nbsp;&nbsp;Transport and Logistics</option>\r\n            <option value=\"111\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Air</option>\r\n            <option value=\"114\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rail</option>\r\n            <option value=\"112\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Road</option>\r\n            <option value=\"113\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Water</option>";
        private StringBuilder Options { get; set; } = new();

        private static List<string> Sectors => SectorsOptions.Trim()
                    .Split("/option>").ToList();

        public static List<SectorEntity> SectorEntities
        {
            get
            {
                List<SectorEntity> entities = new List<SectorEntity>();
                Dictionary<int, int> previousValues = new Dictionary<int, int>();

                foreach (string item in Sectors)
                {
                    Match valueMatch = Regex.Match(item.Trim(), PatternForValue, RegexOptions.IgnoreCase);
                    Match nameMatch = Regex.Match(item.Trim(), PatternForName, RegexOptions.IgnoreCase);

                    if (valueMatch.Success && nameMatch.Success
                        && int.TryParse(valueMatch.Value.Replace("\"", ""), out int value))
                    {
                        var entity = new SectorEntity()
                        {
                            Id = value,
                            Name = nameMatch.Value.Replace("<", "").Replace(">", ""),
                        };
                        var cascadingValue = Regex.Matches(entity.Name, "&nbsp;&nbsp;&nbsp;&nbsp;").Count;

                        if (previousValues.TryGetValue(cascadingValue, out int previousId))
                        {
                            previousValues[cascadingValue] = value;
                        }
                        else
                        {
                            previousValues.Add(cascadingValue, value);
                        }

                        if (cascadingValue != 0)
                        {
                            entity.ParentId = previousValues[cascadingValue - 1];
                        }

                        entity.Name = entity.Name.Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "").Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "");

                        entities.Add(entity);
                    }
                }
                return entities;
            }
        }

        public async Task InsertSectorsToDbAsync()
        {
            await _sectorRepository.InsertSectorsAsync(SectorEntities);
        }

        public async Task<IEnumerable<SectorEntity>> GetSectorsAsync()
        {
            return await _sectorRepository.GetSectorsTreeAsync();
        }

        public string CreateTreeMarkup(SectorEntity sector)
        {
            foreach (SectorEntity subSector in sector.SubSectors)
            {
                int padding = subSector.Distance * 15;
                Options.Append($" <option value=\"{subSector.Id}\" style=\"padding-left:{@padding}px\">" +
                      $"{(MarkupString)subSector.Name} </option>");

                CreateTreeMarkup(subSector);
            }
            return Options.ToString();
        }
    }
}