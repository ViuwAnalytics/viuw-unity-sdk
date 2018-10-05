using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Viuw {
  public class Webservice : MonoBehaviour
  {

      private const string postInitialUrl = "https://op4yyxpfkc.execute-api.us-east-2.amazonaws.com/prod/sessions";
      private const string postUpdateUrl = "https://op4yyxpfkc.execute-api.us-east-2.amazonaws.com/prod/sessions/update";


      void Start () {}


      public void PostInitialSessionData(string jsonString, string apiKey) {
          //Debug.Log("SHAHIN: Posting initial session data json string: " + jsonString);
          var formData = System.Text.Encoding.UTF8.GetBytes(jsonString);

          WWW request = new WWW(postInitialUrl, formData, GetApiKeyHeader(apiKey));
          StartCoroutine(OnResponse(request));

      }

      public void PostSessionUpdate(string jsonString, string apiKey)
      {
        //Debug.Log("SHAHIN: Posting session update json string: " + jsonString);

        var formData = System.Text.Encoding.UTF8.GetBytes(jsonString);
        WWW request = new WWW(postUpdateUrl, formData, GetApiKeyHeader(apiKey));
        StartCoroutine(OnResponse(request));
      }

      public IEnumerator OnResponse(WWW request) {

        yield return request;
        if (!string.IsNullOrEmpty(request.error)) {
          Debug.Log(request.error);
          if (request.error == "403 Forbidden"){
            Debug.Log("VIUW: Could not post session data due to invalid API key. Please check your API key, which can be found on your Viuw Dashboard.");
          } else {
            Debug.Log("VIUW: Could not post session data due to unknown error. Please check your ViuwSession component settings. Ensure that each gameObject in your Viuw Session SceneObjects are coming from your scene hierarchy.");
          }
        } else Debug.Log(request.text);
      }

      public static Dictionary<string, string> GetApiKeyHeader(string apiKey)
    	{
    		Dictionary<string, string> headers = new Dictionary<string,string>();
        headers.Add("x-api-key", apiKey);
    		return headers;
    	}

  }
}
