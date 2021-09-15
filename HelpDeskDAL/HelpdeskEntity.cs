using System.ComponentModel.DataAnnotations;

namespace HelpDeskDAL
{
    public class HelpdeskEntity
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[] Timer { get; set; }
    }
}
