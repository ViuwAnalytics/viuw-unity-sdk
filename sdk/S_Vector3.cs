using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct S_Vector3 { //A serializable vector3 class
  public float x;
  public float y;
  public float z;


  public S_Vector3(Vector3 v)
  {
    this.x = v.x;
    this.y = v.y;
    this.z = v.z;
  }

  public void ToLeftHanded()
  {
    this.x *= -1;
    this.z *= -1;
  }

}
