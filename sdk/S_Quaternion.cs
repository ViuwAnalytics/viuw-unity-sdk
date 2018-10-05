using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Viuw {

  [System.Serializable]
  public struct S_Quaternion{ // A serializable quaternion class
      public float x;
      public float y;
      public float z;
      public float w;

      public S_Quaternion(Quaternion q) {
        this.x = q.x;
        this.y = q.y;
        this.z = q.z;
        this.w = q.w;
      }

      public void ToLeftHanded()
      {
        this.y *= -1;
        this.w *= -1;
      }
  }
}
