namespace SignalRChat.Models
{
    public class Message 
    {
        public int Id { get; set; }
        public virtual User Author { get; set; }

        public virtual Chat Chat { get; set; }

        public string Context { get; set; }
    }
}