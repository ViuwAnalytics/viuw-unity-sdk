
using System;
using System.Collections.Generic;
using UnityEngine;
using Viuw;

//Model for initial data to be sent to server on launch
[System.Serializable]
class SessionUpdate {

  //Db keys
  public string sceneId;
  public string sessionId;
  public string apiKey;
  public float sessionLength;
  public List<S_Vector3> userPositions;
  public List<S_Quaternion> userRotations;
  public List<S_SceneObject> sceneObjects = new List<S_SceneObject>();

  public SessionUpdate(string sceneId, string sessionId, string apiKey, List<S_Vector3> userPositions, List<S_Quaternion> userRotations, List<SceneObject> sceneObjects, float sessionLength) {
    this.sceneId = sceneId;
    this.sessionId = sessionId;
    this.apiKey = apiKey;
    this.userPositions = userPositions;
    this.userRotations = userRotations;
    this.sessionLength = sessionLength;

    foreach (var sceneObject in sceneObjects)
    {
      this.sceneObjects.Add(sceneObject.gameObject.GetComponent<ObjectTracker>().getData());
    }
  }
}
