using UnityEngine;
using UnityEngine.UI;

/// <summary> This class registers a game object
/// to be tracked.
/// </summary>
  [System.Serializable]
  public struct SceneObject {
    public GameObject gameObject; //An ObjectTracker component gets added to this gameobject on ViuwSession Start()
    public GameObject uploadObject;
  }
