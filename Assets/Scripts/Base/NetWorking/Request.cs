using System.Collections;
using UnityEngine.Networking;

public class Request
{
    public string GetURL;

    public Request(string URL)
    {
        GetURL = URL;
    }

    public IEnumerator GetRequest(string Answer)
    {
        UnityWebRequest WebRequest = new UnityWebRequest(GetURL);

        yield return WebRequest.SendWebRequest();

        if (WebRequest.isHttpError || WebRequest.isNetworkError)
            Answer = "failed request";
        else Answer = WebRequest.downloadHandler.text;
    }
}
