using XMLProcessor.Models;

namespace XMLProcessor.Services.XmlParser
{
    public interface IXmlParser
    {
        List<Scientist> Parse(string filePath, Filter filter);
    }
}
