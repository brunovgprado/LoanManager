namespace LoanManager.Api.Models.Request
{
    public class CreateGameRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
    }
}
