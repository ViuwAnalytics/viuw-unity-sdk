using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Viuw
{
  public class ARKit
  {

    Type arkit;
    Type matrixOps;
    MethodInfo GetCameraPose;
    MethodInfo GetPositionFromMatrix;
    MethodInfo GetRotationFromMatrix;
    object arkitInstance;
    object matrixOpsInstance;

    public ARKit()
    {

      arkit = Type.GetType("UnityEngine.XR.iOS.UnityARSessionNativeInterface");
      matrixOps = Type.GetType("UnityEngine.XR.iOS.UnityARMatrixOps");

      if (arkit != null)
      {
        GetCameraPose = Type.GetType("UnityEngine.XR.iOS.UnityARSessionNativeInterface").GetMethod("GetCameraPose");
        GetPositionFromMatrix = Type.GetType("UnityEngine.XR.iOS.UnityARMatrixOps").GetMethod("GetPosition");
        GetRotationFromMatrix = Type.GetType("UnityEngine.XR.iOS.UnityARMatrixOps").GetMethod("GetRotation");
        arkitInstance = Activator.CreateInstance(arkit, null);
        matrixOpsInstance = Activator.CreateInstance(matrixOps, null);
      }

    }

    public object GetPosition()
    {
      if (arkit == null || arkitInstance == null)
      {
        return null;
      }

      var matrix = GetCameraPose.Invoke(arkitInstance, null);
      object[] paramArr = new object[] {matrix};
      var position = GetPositionFromMatrix.Invoke(matrixOpsInstance, paramArr);
      //Debug.Log(position);
      return position;
    }

    public object GetRotation()
    {
      if (arkit == null || arkitInstance == null)
      {
        return null;
      }

      var matrix = GetCameraPose.Invoke(arkitInstance, null);
      object[] paramArr = new object[] {matrix};
      var rotation = GetRotationFromMatrix.Invoke(matrixOpsInstance, paramArr);
      //Debug.Log(rotation);
      return rotation;
    }
  }
}
