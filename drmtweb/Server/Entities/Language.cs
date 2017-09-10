using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrMturhan.Server.Entities
{
    public class Language : IEntityBase
    {
        
        public int Id { get; set; }
        public string Locale { get; set; }
        public string Description { get; set; }
        public ICollection<ContentText> ContentTexts { get; set; }
    }
}
