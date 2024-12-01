using System.Xml;
using XMLProcessor.Models;

namespace XMLProcessor.Services.XmlGenerator
{
    public static class XmlGenerator
    {
        public static XmlDocument GenerateXml(IEnumerable<Scientist> scientists)
        {
            var xmlDocument = new XmlDocument();
            var xmlRoot = xmlDocument.CreateElement("Scientists");
            xmlDocument.AppendChild(xmlRoot);

            foreach (var scientist in scientists)
            {
                var scientistNode = xmlDocument.CreateElement("Scientist");

                void AddAttribute(string name, string value)
                {
                    var attribute = xmlDocument.CreateAttribute(name);
                    attribute.Value = value ?? string.Empty;
                    scientistNode.Attributes.Append(attribute);
                }

                AddAttribute("Id", scientist.Id.ToString());
                AddAttribute("FirstName", scientist.FirstName);
                AddAttribute("LastName", scientist.LastName);
                AddAttribute("Position", scientist.Position);
                AddAttribute("Salary", scientist.Salary.ToString());
                AddAttribute("YearsOnPosition", scientist.YearsOnPosition.ToString());
                AddAttribute("Department", scientist.Department);
                AddAttribute("Faculty", scientist.Faculty);

                xmlRoot.AppendChild(scientistNode);
            }

            return xmlDocument;
        }
    }
}
