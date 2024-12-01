namespace XMLProcessor.Services.XmlFilePicker
{
    public static class XmlFilePicker
    {
        public static async Task<string> SelectXmlFileAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an XML File",
                    FileTypes = CustomFilePickerFileTypes.Xml
                });

                if (result != null)
                {
                    return result.FullPath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selecting file: {ex.Message}");
            }

            return null;
        }
    }
}
