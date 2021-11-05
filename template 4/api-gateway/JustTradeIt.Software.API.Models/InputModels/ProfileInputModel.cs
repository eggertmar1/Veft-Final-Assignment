using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ProfileInputModel
    {
        [MinLength(3)]
        public string Fullname { get; set; }
        
        public IFormFile ProfileImage { get; set; }
    }
}