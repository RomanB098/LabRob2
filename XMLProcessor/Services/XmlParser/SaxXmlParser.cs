using System.Xml;
using XMLProcessor.Models;

namespace XMLProcessor.Services.XmlParser
{
    public class SaxXmlParser : IXmlParser
    {
        public List<Scientist> Parse(string filePath, Filter filter)
        {
            var scientists = new List<Scientist>();
            string currentFaculty = null;
            string currentDepartment = null;

            try
            {
                using (var reader = XmlReader.Create(filePath))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "Faculty")
                            {
                                currentFaculty = reader.GetAttribute("name");
                            }
                            else if (reader.Name == "Department")
                            {
                                currentDepartment = reader.GetAttribute("name");
                            }
                            else if (reader.Name == "Scientist")
                            {
                                int id = int.Parse(reader.GetAttribute("id") ?? "0");
                                string position = reader.GetAttribute("position");
                                int salary = int.Parse(reader.GetAttribute("salary") ?? "0");
                                int yearsOnPosition = int.Parse(reader.GetAttribute("yearsOnPosition") ?? "0");
                                string firstName = reader.GetAttribute("firstName");
                                string lastName = reader.GetAttribute("lastName");
                                string middleName = reader.GetAttribute("middleName");

                                if ((!string.IsNullOrEmpty(filter.Faculty) && filter.Faculty != currentFaculty) ||
                                    (!string.IsNullOrEmpty(filter.Department) && filter.Department != currentDepartment) ||
                                    (!string.IsNullOrEmpty(filter.Position) && filter.Position != position) ||
                                    (filter.MinSalary.HasValue && salary < filter.MinSalary.Value) ||
                                    (filter.MaxSalary.HasValue && salary > filter.MaxSalary.Value) ||
                                    (filter.MinYearsOnPosition.HasValue && yearsOnPosition < filter.MinYearsOnPosition.Value) ||
                                    (filter.MaxYearsOnPosition.HasValue && yearsOnPosition > filter.MaxYearsOnPosition.Value))
                                {
                                    continue;
                                }

                                scientists.Add(new Scientist
                                {
                                    Id = id,
                                    Position = position,
                                    Salary = salary,
                                    YearsOnPosition = yearsOnPosition,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    MiddleName = middleName,
                                    Department = currentDepartment,
                                    Faculty = currentFaculty
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing XML using SAX: {ex.Message}");
            }

            return scientists;
        }
    }
}
