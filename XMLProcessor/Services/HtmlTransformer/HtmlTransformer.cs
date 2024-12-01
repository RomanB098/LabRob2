using System.Xml;
using System.Xml.Xsl;

namespace XMLProcessor.Services.HtmlTransformer
{
    public static class HtmlTransformer
    {
        public static void TransformToHtml(XmlDocument xmlDocument, string xsltPath, string outputFilePath)
        {
            try
            {
                var xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                using (var writer = XmlWriter.Create(outputFilePath, new XmlWriterSettings { Indent = true }))
                {
                    xslt.Transform(xmlDocument, writer);
                }

                Console.WriteLine($"HTML file successfully saved to: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error transforming XML to HTML: {ex.Message}");
                throw;
            }
        }
    }
}
