namespace Core.Filters
{
    public class ProjectFilter :  IFilter
    {
        public string? Name { get; set; }    
        public string? Description { get; set; }
        public long? MemberId { get; set; }  
        
    }
}
