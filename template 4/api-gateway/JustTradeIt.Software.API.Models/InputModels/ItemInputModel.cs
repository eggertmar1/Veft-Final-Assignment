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
        //TODO: Validate condition code [MINT, GOOD, USED, BAD, DAMAGED]
        public string ConditionCode{ get; set; }

        [Url]
        public IEnumerable<string> ItemImages { get; set; }
    }
}