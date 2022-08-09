using Newtonsoft.Json;

namespace ACYZenWebApp1.Controllers.BLZenAutomation.BasicOperation;
public class ResponseGetPhoneNomber
{
    [JsonProperty("tel")]
    public long TelNomber { get; set; }
        
    [JsonProperty("idNum")]
    public string IdNum { get; set; }
}
public class ResponseGetSmsCode
{
    /*[JsonProperty("smsCode")]
    public int SMSCode { get; set; }*/
        
    [JsonProperty("smsCode")]
    public string SmsCode { get; set; }
}
    
public class TelStatusGetNewSms
{
    [JsonProperty("status")]
    public string Status { get; set; }
}