using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ItemInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^(MINT|GOOD|USED|BAD|DAMAGED)$")]
        // using regex to validate the enum value
        public string ConditionCode{ get; set; }

        public IEnumerable<string> ItemImages { get; set; }
    }
}