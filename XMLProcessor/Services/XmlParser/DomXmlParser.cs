using System.Xml;
using XMLProcessor.Models;


namespace XMLProcessor.Services.XmlParser
{
    public class DomXmlParser : IXmlParser
    {
        public List<Scientist> Parse(string filePath, Filter filter)
        {
            var scientists = new List<Scientist>();

            try
            {

                XmlDocument document = new XmlDocument();
                document.Load(filePath);


                string xPath = XPathBuilder.XPathBuilder.BuildXPath(filter);

                XmlNodeList scientistNodes = document.SelectNodes(xPath);

                if (scientistNodes != null)
                {
                    foreach (XmlNode scientistNode in scientistNodes)
                    {
                        XmlNode departmentNode = scientistNode.ParentNode;
                        XmlNode facultyNode = departmentNode.ParentNode;

                        var scientist = new Scientist
                        {
                            Id = int.Parse(scientistNode.Attributes["id"]?.Value ?? "0"),
                            Position = scientistNode.Attributes["position"]?.Value,
                            Salary = int.Parse(scientistNode.Attributes["salary"]?.Value ?? "0"),
                            YearsOnPosition = int.Parse(scientistNode.Attributes["yearsOnPosition"]?.Value ?? "0"),
                            FirstName = scientistNode.Attributes["firstName"]?.Value,
                            LastName = scientistNode.Attributes["lastName"]?.Value,
                            MiddleName = scientistNode.Attributes["middleName"]?.Value,
                            Department = departmentNode.Attributes["name"]?.Value,
                            Faculty = facultyNode.Attributes["name"]?.Value
                        };

                        scientists.Add(scientist);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing XML: {ex.Message}");
            }

            return scientists;
        }
    }
}
