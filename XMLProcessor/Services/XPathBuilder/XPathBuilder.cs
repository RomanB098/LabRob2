using XMLProcessor.Models;

namespace XMLProcessor.Services.XPathBuilder
{
    public static class XPathBuilder
    {
        public static string BuildXPath(Filter filter)
        {
            var xpath = "//Scientist";
            var conditions = new List<string>();

            if (!string.IsNullOrEmpty(filter.Faculty))
            {
                conditions.Add($"ancestor::Faculty[@name='{filter.Faculty}']");
            }

            if (!string.IsNullOrEmpty(filter.Department))
            {
                conditions.Add($"ancestor::Department[@name='{filter.Department}']");
            }

            if (!string.IsNullOrEmpty(filter.Position))
            {
                conditions.Add($"@position='{filter.Position}'");
            }

            if (filter.MinSalary.HasValue)
            {
                conditions.Add($"@salary>={filter.MinSalary.Value}");
            }

            if (filter.MaxSalary.HasValue)
            {
                conditions.Add($"@salary<={filter.MaxSalary.Value}");
            }

            if (filter.MinYearsOnPosition.HasValue)
            {
                conditions.Add($"@yearsOnPosition>={filter.MinYearsOnPosition.Value}");
            }

            if (filter.MaxYearsOnPosition.HasValue)
            {
                conditions.Add($"@yearsOnPosition<={filter.MaxYearsOnPosition.Value}");
            }

            if (conditions.Count > 0)
            {
                xpath += "[" + string.Join(" and ", conditions) + "]";
            }

            return xpath;
        }
    }
}
