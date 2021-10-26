using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ProfileInputModel
    {
        [MinLength(3)]
        public string Fullname { get; set; }
        
        //TODO: an image file(See uploading an image for reference)
        public IFormFile ProfileImage { get; set; }
    }
}