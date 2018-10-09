using UnityEngine;
using System.Collections.Generic;

namespace Viuw {
[System.Serializable]
public class ObjectTracker: MonoBehaviour
{

  public string assetBundlePath;
  public List<S_Vector3> s_positions = new List<S_Vector3>();
  public List<S_Quaternion> s_rotations = new List<S_Quaternion>();
  public List<S_Vector3> s_scales = new List<S_Vector3>();

  //private Platform platform;
  private int frames = 0;
  private int trackingRate = 20;

  void Awake() {
    //Store the asset bundle paths, which are simply the asset paths within the project files
    this.assetBundlePath = gameObject.name;
  }
  void Start() {}


  void Update()
  {
    frames++;
    if (frames == 60/trackingRate)
    {
      frames = 0;
      var s_position = new S_Vector3(transform.position);
      var s_rotation = new S_Quaternion(transform.rotation);
      var s_scale = new S_Vector3(transform.localScale);

      s_positions.Add(s_position);
      s_rotations.Add(s_rotation);
      s_scales.Add(s_scale);

    }
  }

  public S_SceneObject getData() {
    return new S_SceneObject(assetBundlePath, s_positions, s_rotations, s_scales);
  }

  public void Clear()
  {
    s_positions.Clear();
    s_rotations.Clear();
    s_scales.Clear();
  }


}

}
