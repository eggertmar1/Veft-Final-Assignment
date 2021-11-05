namespace JustTradeIt.Software.API.Models.Dtos
{
    public class UserDto
    {
        public string Identifier { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public int TokenId { get; set; } // Can be JSON ignored, 
                                        // so it won't turn out in response to end-user
    } 
}