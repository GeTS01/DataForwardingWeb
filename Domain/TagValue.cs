using DataForwardingWeb.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("tag_values")]
    public class TagValue : PersistentObject
    {
        [Column("value")]
        public double Value { get; set; }

        [Column("TagId")]
        public long TagId { get; set; }
        public Tag Tag { get; set; }
        public DateTime Date { get; set; }

        public TagValue() { }
    }
}
