
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
  public List<S_Vector3> userPositions;
  public List<S_Quaternion> userRotations;
  public List<S_SceneObject> sceneObjects = new List<S_SceneObject>();

  public SessionUpdate(string sceneId, string sessionId, string apiKey, List<S_Vector3> userPositions, List<S_Quaternion> userRotations, List<SceneObject> sceneObjects) {
    this.sceneId = sceneId;
    this.sessionId = sessionId;
    this.apiKey = apiKey;
    this.userPositions = userPositions;
    this.userRotations = userRotations;

    var blah = sceneObjects[0].gameObject.GetComponent<ObjectTracker>().getData();

    foreach (var sceneObject in sceneObjects)
    {
      this.sceneObjects.Add(sceneObject.gameObject.GetComponent<ObjectTracker>().getData());
    }
  }
}
