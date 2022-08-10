namespace ACYZenWebApp1.Models;

public class Channel
{
    public int ChannelId { get; set; }
    //public string _firstName { get; set; } // имя аккаунта
    //public string _surname { get; set; } // фамилия аккаунта
    public string _login { get; set; } // префикс Логина
    public string _channnelName { get; set; } // Название канала
    //public int _loginNuberPre { get; set; } //номер логина "с"
    //public int _loginNuberPost { get; set; } // номер логина "по"
    public string _chanelUrl { get; set; } // адрес канала
    
    public int ZenActionId { get; set; } // ссылка на связанное действие 
    public ZenAction ZenAction { get; set; }
}