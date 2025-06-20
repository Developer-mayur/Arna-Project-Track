

using System.ComponentModel.DataAnnotations.Schema;

namespace Arna_Project_Track.Models
    {
        public class ActiveUser
        {
            public int ActiveUserId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
            public DateTime LoginTime { get; set; }
            public DateTime? LastActiveTime { get; set; }
            public bool IsOnline { get; set; }

            public virtual User User { get; set; }
      
    }
    }


