using System.Net;

public class NetWorking
{
    public Liders[] Liders;
    
    public Liders[] GetLidersFromJson(string URL)
    {
        using (WebClient WebClient = new WebClient())
        {
            try
            {
                return JSONHelper.FromJson<Liders>(JSONHelper.FixJson(WebClient.DownloadString(URL)));
            }
            catch
            {
                return null;
            }
        }
    }
    
    public Liders[] GetJsonObject(string URL)
    {
        try
        {
            return GetLidersFromJson(URL);
        }
        catch
        {
            return null;
        }
    }
}