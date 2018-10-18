using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Viuw
{

  public enum Platform {
    ARKitPlugin, ARCorePlugin, Vuforia
  }

  public class ViuwManager : MonoBehaviour
  {

    //Platform
    public Platform platform = Platform.ARKitPlugin; //set default value

    //Ids
    public string apiKey;
    public string projectId = "<YOUR PROJECT ID>";
    public string sceneId = "<YOUR SCENE ID>";
    private string sessionId = Guid.NewGuid().ToString(); //Set session id

    //API
    private Webservice webservice;

    //Tracking
    private DeviceTracker deviceTracker;
    public List<SceneObject> sceneObjects; //Scene objects to track
    //public static int trackingRate = 20;
    public static float trackingRate = 0.05f;
    private int frames = 0;

    public static ViuwManager Instance {get; private set;}

    private void Awake()
  	{

  		if (Instance == null)
  		{
  			Instance = this;
  		}
  		else
  		{
  			Destroy(gameObject);
  		}
  	}

    void Start() {

      webservice = gameObject.AddComponent<Webservice>();
      deviceTracker = gameObject.AddComponent<DeviceTracker>();
      ValidateSceneObjects();
      ConfigureSceneObjects();
      PostInitialSessionData();
      InvokeRepeating("PostSessionUpdate", 5f, 10f);
    }

    void Update()
    {}


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
      var data = new InitialSessionData(apiKey, sceneId, sessionId, getTime(), deviceTracker.s_positions,deviceTracker.s_rotations, sceneObjects, platform, trackingRate, Time.time);
      string jsonString = JsonUtility.ToJson(data);
      webservice.PostInitialSessionData(JsonUtility.ToJson(data), apiKey);
    }

    /// <summary>
    /// Post session updates
    /// </summary>
    public void PostSessionUpdate() {
      Debug.Log("SHAHIN: Total data points: " + deviceTracker.s_positions.Count);
      Debug.Log("SHAHIN: Total time: " + Time.time);
      var data = new SessionUpdate(sceneId, sessionId, apiKey, deviceTracker.s_positions, deviceTracker.s_rotations, sceneObjects, Time.time);
      string jsonString = JsonUtility.ToJson(data);
      webservice.PostSessionUpdate(JsonUtility.ToJson(data), this.apiKey);
      //Clear user and scene object data
      ClearSessionData();

    }

    public void ClearSessionData()
    {
      deviceTracker.Clear();
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
