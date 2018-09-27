using System;

namespace OpenHack
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string LocationName { get; set; }
        public int Rating { get; set; }
        public string UserNotes { get; set; }
    }
}
