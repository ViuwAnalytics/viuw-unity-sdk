using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Viuw
{


  public class ViuwManager : MonoBehaviour
  {

    //Platform
    #if (UNITY_IOS || UNITY_EDITOR)
      public static string platform = "ARKit";
    #else
      public static string platform = "ARCore";
    #endif

    //Ids
    public string apiKey;
    public string projectId = "<YOUR PROJECT ID>";
    public string sceneId = "<YOUR SCENE ID>";
    private string sessionId = Guid.NewGuid().ToString(); //Set session id


    //API
    private Webservice webservice;

    //Tracking
    private User user = new User(); //To get user position & rotation
    public List<SceneObject> sceneObjects; //Scene objects to track
    private int trackingRate = 30;
    private int frames = 0;



    void Start() {
      Debug.Log("SessionId: " + this.sessionId);
      webservice = gameObject.AddComponent(typeof(Webservice)) as Webservice;
      ValidateSceneObjects();
      ConfigureSceneObjects();
      PostInitialSessionData();
      InvokeRepeating("PostSessionUpdate", 5f, 10f);
    }

    void Update()
    {
      frames++;
      if (frames == 60/trackingRate)
      {
        frames = 0;
        user.Track(); //Track the user's device transform
      }
    }

    /// <summary>
    //Check for null game object reference in scene objects
    /// </summary>
    void ValidateSceneObjects()
    {

      var indecesToRemove = new List<int>();
      for (int i = 0; i < sceneObjects.Count; i++)
      {
        if (sceneObjects[i].gameObject == null)
        {
          Debug.Log("VIUW: One or more registered Scene Objects are missing game object references. Please stop your scene and check your Viuw Session script in the editor.");
          indecesToRemove.Add(i);
        }
      }
      //Loop becaward through indecesToRemove and remove those indeces from scene objects;
      for (int i= indecesToRemove.Count - 1; i>=0;i--)
      {
        sceneObjects.RemoveAt(indecesToRemove[i]);
      }
    }


    void ConfigureSceneObjects() {
      for (int i = 0; i < sceneObjects.Count; i++)
      {
        var sceneObject = sceneObjects[i];
        //Add object tracker to scene objects and start tracking
        sceneObject.gameObject.AddComponent(typeof(Viuw.ObjectTracker));
      }
    }


    /// <summary>
    /// Post initial session data
    /// </summary>
    public void PostInitialSessionData() {
      var data = new InitialSessionData(apiKey, sceneId, sessionId, getTime(), user.s_positions,user.s_rotations, sceneObjects, platform, trackingRate);
      string jsonString = JsonUtility.ToJson(data);
      webservice.PostInitialSessionData(JsonUtility.ToJson(data), this.apiKey);
    }


    /// <summary>
    /// Post session updates
    /// </summary>
    public void PostSessionUpdate() {
      //Debug.Log("Total update data points USER: " + user.s_positions.Count);

      var data = new SessionUpdate(sceneId, sessionId, apiKey, user.s_positions, user.s_rotations, sceneObjects);
      string jsonString = JsonUtility.ToJson(data);
      webservice.PostSessionUpdate(JsonUtility.ToJson(data), this.apiKey);
      //Clear user and scene object data
      ClearSessionData();

    }

    public void ClearSessionData()
    {
      user.Clear();
      foreach (var sceneObject in sceneObjects)
      {
        sceneObject.gameObject.GetComponent<ObjectTracker>().Clear();
      }

    }

    private int getTime() {
        DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int timestamp = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        return timestamp;
    }

  }

}
