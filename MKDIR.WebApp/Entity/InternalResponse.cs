namespace MKDIR.WebApp.Entity
{
    public class InternalResponse
    {
        public bool success { get; set; }
        public DateTime operationDate { get; set; }
        public bool hasErrors { get; set; }
        public object? errors { get; set; }
        public object? data { get; set; }
        public int dataCountToPaging { get; set; }

    }
}
