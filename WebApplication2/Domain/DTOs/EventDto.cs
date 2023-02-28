namespace WebApplication2.Domain.DTOs
{
    public class EventCreatDto
    {
        public string title { get; set; }
    }

    public class EventReadDto 
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        
    }


}
