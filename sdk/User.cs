using System.Collections.Generic;
using UnityEngine;

namespace Viuw
{
  public class User
  {
    public List<S_Vector3> s_positions = new List<S_Vector3>();
    public List<S_Quaternion> s_rotations = new List<S_Quaternion>();
    private DeviceTracker deviceTracker = new DeviceTracker();

    public void Track()
    {
    
      object positionObj = deviceTracker.GetPosition();
      object rotationObj = deviceTracker.GetRotation();

      if (positionObj != null && rotationObj != null)
      {
        var position = (Vector3)positionObj;
        var rotation = (Quaternion)rotationObj;

        if (position != null && rotation != null)
        {

          var s_position = new S_Vector3(position);
          var s_rotation = new S_Quaternion(rotation);

          //Convert coordinate systems. ARCore and ARKit are both right handed. Convert to left handed.
          //s_position.ToLeftHanded();
          //s_rotation.ToLeftHanded();

          s_positions.Add(s_position);
          s_rotations.Add(s_rotation);
        }
      }
    }

    public void Clear()
    {
      s_positions.Clear();
      s_rotations.Clear();
    }

  }
}
