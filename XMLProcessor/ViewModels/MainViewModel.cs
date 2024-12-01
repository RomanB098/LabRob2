using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml;
using XMLProcessor.Models;
using XMLProcessor.Services.XmlFilePicker;
using XMLProcessor.Services.XmlParser;
using XMLProcessor.Services.HtmlTransformer;
using XMLProcessor.Services.XmlGenerator;


namespace XMLProcessor.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _selectedFilePath;
        private bool _isFilterAndActionVisible;
        private ObservableCollection<string> _faculties;
        private ObservableCollection<string> _departments;
        private ObservableCollection<string> _positions;
        private ObservableCollection<string> _parsingStrategies;
        private string _selectedFaculty;
        private string _selectedDepartment;
        private string _selectedPosition;
        private int? _minSalary;
        private int? _maxSalary;
        private int? _minYears;
        private int? _maxYears;
        private string _parsingStrategy;
        private ObservableCollection<Scientist> _searchResults;
        private bool _isSearchResultsVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            IsFilterAndActionVisible = false;
            Faculties = new ObservableCollection<string>();
            Departments = new ObservableCollection<string>();
            Positions = new ObservableCollection<string>();
            ParsingStrategies = new ObservableCollection<string> { "SAX", "DOM", "LINQ" };

            ParsingStrategy = "SAX";

            SelectFileCommand = new Command(async () => await SelectFile());
            SearchCommand = new Command(async () => await Search());
            TransformCommand = new Command(Transform);
            ClearCommand = new Command(ClearFields);
        }

        public ObservableCollection<string> Faculties
        {
            get => _faculties;
            set { _faculties = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Departments
        {
            get => _departments;
            set { _departments = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Positions
        {
            get => _positions;
            set { _positions = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> ParsingStrategies
        {
            get => _parsingStrategies;
            set
            {
                _parsingStrategies = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Scientist> SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }
        public bool IsFilterAndActionVisible
        {
            get => _isFilterAndActionVisible;
            set
            {
                _isFilterAndActionVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsSearchResultsVisible
        {
            get => _isSearchResultsVisible;
            set
            {
                _isSearchResultsVisible = value;
                OnPropertyChanged();
            }
        }

        public string SelectedFaculty
        {
            get => _selectedFaculty;
            set
            {
                _selectedFaculty = value;
                OnPropertyChanged();
                FetchDepartments();
                FetchPositions();
            }
        }

        public string SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged();
                FetchPositions();
            }
        }

        public string SelectedPosition
        {
            get => _selectedPosition;
            set { _selectedPosition = value; OnPropertyChanged(); }
        }

        public int? MinSalary
        {
            get => _minSalary;
            set { _minSalary = value; OnPropertyChanged(); }
        }

        public int? MaxSalary
        {
            get => _maxSalary;
            set { _maxSalary = value; OnPropertyChanged(); }
        }

        public int? MinYears
        {
            get => _minYears;
            set { _minYears = value; OnPropertyChanged(); }
        }

        public int? MaxYears
        {
            get => _maxYears;
            set { _maxYears = value; OnPropertyChanged(); }
        }

        public string ParsingStrategy
        {
            get => _parsingStrategy;
            set
            {
                if (_parsingStrategy != value)
                {
                    _parsingStrategy = value;
                    OnPropertyChanged();
                }
            }
        }


        public Command SelectFileCommand { get; }
        public Command SearchCommand { get; }
        public Command TransformCommand { get; }
        public Command ClearCommand { get; }

        private async Task SelectFile()
        {
            var filePath = await XmlFilePicker.SelectXmlFileAsync();
            if (!string.IsNullOrEmpty(filePath))
            {
                _selectedFilePath = filePath;
                IsFilterAndActionVisible = true;
                ClearFields();
            }
            else
            {
                IsFilterAndActionVisible = false;
                Console.WriteLine("No file selected.");
            }
        }

        private void FetchFaculties()
        {
            try
            {
                var document = new XmlDocument();
                document.Load(_selectedFilePath);

                var facultyNodes = document.SelectNodes("//Faculty");
                Faculties.Clear();

                if (facultyNodes != null)
                {
                    foreach (XmlNode faculty in facultyNodes)
                    {
                        var name = faculty.Attributes["name"]?.Value;
                        if (!string.IsNullOrEmpty(name) && !Faculties.Contains(name))
                        {
                            Faculties.Add(name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching faculties: {ex.Message}");
            }
        }

        private void FetchDepartments()
        {
            try
            {
                var document = new XmlDocument();
                document.Load(_selectedFilePath);

                string xPath = string.IsNullOrEmpty(SelectedFaculty)
                    ? "//Department"
                    : $"//Faculty[@name='{SelectedFaculty}']/Department";

                var departmentNodes = document.SelectNodes(xPath);
                Departments.Clear();

                if (departmentNodes != null)
                {
                    foreach (XmlNode department in departmentNodes)
                    {
                        var name = department.Attributes["name"]?.Value;
                        if (!string.IsNullOrEmpty(name) && !Departments.Contains(name))
                        {
                            Departments.Add(name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching departments: {ex.Message}");
            }

            SelectedDepartment = null;
            Positions.Clear();
        }

        private void FetchPositions()
        {
            try
            {
                var document = new XmlDocument();
                document.Load(_selectedFilePath);

                string xPath;

                if (!string.IsNullOrEmpty(SelectedFaculty) && string.IsNullOrEmpty(SelectedDepartment))
                {
                    xPath = $"//Faculty[@name='{SelectedFaculty}']/Department/Scientist";
                }
                else if (!string.IsNullOrEmpty(SelectedDepartment))
                {
                    xPath = $"//Department[@name='{SelectedDepartment}']/Scientist";
                }
                else
                {
                    xPath = "//Scientist";
                }

                var scientistNodes = document.SelectNodes(xPath);
                Positions.Clear();

                if (scientistNodes != null)
                {
                    foreach (XmlNode scientist in scientistNodes)
                    {
                        var position = scientist.Attributes["position"]?.Value;
                        if (!string.IsNullOrEmpty(position) && !Positions.Contains(position))
                        {
                            Positions.Add(position);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching positions: {ex.Message}");
            }

            SelectedPosition = null;
        }

        private async Task Search()
        {
            if (string.IsNullOrEmpty(_selectedFilePath))
            {
                Console.WriteLine("No file selected.");
                return;
            }

            try
            {
                var filter = new Filter
                {
                    Faculty = SelectedFaculty,
                    Department = SelectedDepartment,
                    Position = SelectedPosition,
                    MinSalary = MinSalary,
                    MaxSalary = MaxSalary,
                    MinYearsOnPosition = MinYears,
                    MaxYearsOnPosition = MaxYears
                };

                IXmlParser parser = ParsingStrategy switch
                {
                    "SAX" => new SaxXmlParser(),
                    "DOM" => new DomXmlParser(),
                    "LINQ" => new LinqXmlParser(),
                    _ => throw new InvalidOperationException("Invalid parsing strategy selected.")
                };

                var results = parser.Parse(_selectedFilePath, filter);

                if (results.Any())
                {
                    SearchResults = new ObservableCollection<Scientist>(results);
                    IsSearchResultsVisible = true;
                }
                else
                {
                    SearchResults.Clear();
                    IsSearchResultsVisible = false;
                    Console.WriteLine("No results found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during search: {ex.Message}");
            }
        }


        private async void Transform()
        {
            if (SearchResults == null || !SearchResults.Any())
            {
                Console.WriteLine("No search results to transform.");
                return;
            }

            try
            {
                var xmlDocument = XmlGenerator.GenerateXml(SearchResults);

                var xsltPath = Path.Combine(AppContext.BaseDirectory, "Resources", "Raw", "Transform.xslt");
                var outputFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SearchResults.html");

                HtmlTransformer.TransformToHtml(xmlDocument, xsltPath, outputFilePath);
                await Application.Current.MainPage.DisplayAlert("Success", $"HTML file has been generated and saved to {outputFilePath}.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failure", "An error occured transforming data to HTML.", "OK");
                Console.WriteLine($"Error transforming to HTML: {ex.Message}");
            }
        }




        private void ClearFields()
        {
            SelectedFaculty = null;
            SelectedDepartment = null;
            SelectedPosition = null;
            MinSalary = null;
            MaxSalary = null;
            MinYears = null;
            MaxYears = null;
            IsSearchResultsVisible = false;

            Faculties.Clear();
            Departments.Clear();
            Positions.Clear();

            FetchFaculties();
            FetchDepartments();
            FetchPositions();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
