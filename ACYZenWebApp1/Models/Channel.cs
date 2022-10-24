namespace ACYZenWebApp1.Models;

public class Channel
{
    public int ChannelId { get; set; }
   public string Login { get; set; } // префикс Логина
    public string ChannnelName { get; set; } // Название канала
    public string ChanelUrl { get; set; } // адрес канала
    
    public int ZenActionId { get; set; } // ссылка на связанное действие 
    public ZenAction ZenAction { get; set; }
}