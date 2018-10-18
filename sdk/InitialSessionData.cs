using System;
using System.Collections.Generic;
using UnityEngine;
using Viuw;

//Model for initial data to be sent to server on launch
[System.Serializable]
class InitialSessionData {

  //DB keys
  public string apiKey;
  public string sceneId;
  public string sessionId;
  public Platform platform; //TODO: must change this in the visualizer
  public float trackingRate;
  public int timestamp;
  public float sessionLength;
  public List<S_Vector3> userPositions;
  public List<S_Quaternion> userRotations;
  public List<S_SceneObject> sceneObjects = new List<S_SceneObject>();


  public InitialSessionData(string apiKey, string sceneId, string sessionId, int timestamp, List<S_Vector3> userPositions, List<S_Quaternion> userRotations, List<SceneObject> sceneObjects, Platform platform, float trackingRate, float sessionLength) {
    this.apiKey = apiKey;
    this.sceneId = sceneId;
    this.sessionId = sessionId;
    this.platform = platform;
    this.timestamp = timestamp;
    this.userPositions = userPositions;
    this.userRotations = userRotations;
    this.trackingRate = trackingRate;
    this.sessionLength = sessionLength;

    foreach (var obj in sceneObjects)
    {
      this.sceneObjects.Add(obj.gameObject.GetComponent<ObjectTracker>().getData());
    }

  }
}
