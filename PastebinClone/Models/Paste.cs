using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PastebinClone.Models
{
    public class Paste
    {
        [Key]
        public string UrlID { get; private set; } = Guid.NewGuid().ToString("N")[..8];
        public string UserId { get; private set; }
        public string Text { get; private set; } = string.Empty;
        public DateTime DateCreated { get; private set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; private set; } = DateTime.UtcNow.AddDays(7);

        public Paste()
        {
            
        }

        public Paste(string text, string userId)
        {
            UserId = userId;
            Text = text;
        }

        public void ExtendExpirationDate()
        {
            ExpirationDate = DateTime.UtcNow.AddDays(7);
        }

    }
}
