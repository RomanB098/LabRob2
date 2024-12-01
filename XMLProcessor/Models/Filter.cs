namespace XMLProcessor.Models
{
    public class Filter
    {
        public string Faculty { get; set; }        
        public string Department { get; set; }     
        public string Position { get; set; }      
        public int? MinSalary { get; set; }        
        public int? MaxSalary { get; set; }        
        public int? MinYearsOnPosition { get; set; }
        public int? MaxYearsOnPosition { get; set; }
    }

}
