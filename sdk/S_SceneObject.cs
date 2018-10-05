using Viuw;
using System.Collections.Generic;


[System.Serializable]
public struct S_SceneObject {
  public string objectId;
  public List<S_Vector3> positions;
  public List<S_Quaternion> rotations;
  public List<S_Vector3> scales;

  public S_SceneObject(string objectId, List<S_Vector3> s_positions, List<S_Quaternion> s_rotations, List<S_Vector3> s_scales)
  {
    this.objectId = objectId;
    this.positions = s_positions;
    this.rotations = s_rotations;
    this.scales = s_scales;
  }
}
