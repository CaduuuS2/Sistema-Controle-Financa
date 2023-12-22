namespace ControleFacil.Api.Contract
{
    public class ModelErrorContract
    {
        public string Title {get; set;} = string.Empty;
        public int StatusCode {get; set;}
        public string Message {get; set;} = string.Empty;
        public DateTime DateTime {get; set;}
    }
}