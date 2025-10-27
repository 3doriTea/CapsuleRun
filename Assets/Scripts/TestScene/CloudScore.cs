using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CloudScore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GetRequest("https://3dori.yuuronacademy.com"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetRequest(string url)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
