using System.Xml.Linq;
using System.Xml.XPath;
using XMLProcessor.Models;

namespace XMLProcessor.Services.XmlParser
{
    public class LinqXmlParser : IXmlParser
    {
        public List<Scientist> Parse(string filePath, Filter filter)
        {
            var scientists = new List<Scientist>();

            try
            {
                XDocument document = XDocument.Load(filePath);

                string xPath = XPathBuilder.XPathBuilder.BuildXPath(filter);

                IEnumerable<XElement> scientistElements = document.XPathSelectElements(xPath);

                scientists = scientistElements
                    .Select(s => new Scientist
                    {
                        Id = (int?)s.Attribute("id") ?? 0,
                        Position = (string)s.Attribute("position"),
                        Salary = (int?)s.Attribute("salary") ?? 0,
                        YearsOnPosition = (int?)s.Attribute("yearsOnPosition") ?? 0,
                        FirstName = (string)s.Attribute("firstName"),
                        LastName = (string)s.Attribute("lastName"),
                        MiddleName = (string)s.Attribute("middleName"),
                        Department = s.Ancestors("Department").FirstOrDefault()?.Attribute("name")?.Value,
                        Faculty = s.Ancestors("Faculty").FirstOrDefault()?.Attribute("name")?.Value
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing XML using LINQ with XPath: {ex.Message}");
            }

            return scientists;
        }
    }
}
