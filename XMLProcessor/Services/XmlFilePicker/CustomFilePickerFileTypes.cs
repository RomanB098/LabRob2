namespace XMLProcessor.Services.XmlFilePicker
{
    public static class CustomFilePickerFileTypes
    {
        public static readonly FilePickerFileType Xml = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.xml" } },
            { DevicePlatform.Android, new[] { "application/xml" } },
            { DevicePlatform.WinUI, new[] { ".xml" } },
            { DevicePlatform.Tizen, new[] { ".xml" } }
        });
    }
}
