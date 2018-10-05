using System;
using System.Reflection;
using UnityEngine;

namespace Viuw
{
  public class DeviceTracker
  {

    Type arFramework;
    object arFrameworkInstance;
    MethodInfo GetPos;
    MethodInfo GetRot;

    public DeviceTracker()
    {
      //Set arFramework and arFrameworkInstance depending on the AR framework
      //running on the device
      #if (UNITY_IOS || UNITY_EDITOR)
        arFramework = Type.GetType("Viuw.ARKit");
      #elif (UNITY_ANDROID)
        arFramework = Type.GetType("Viuw.ARCore");
      #endif

      arFrameworkInstance = Activator.CreateInstance(arFramework,null);
      GetPos = arFramework.GetMethod("GetPosition");
      GetRot = arFramework.GetMethod("GetRotation");
    }

    public object GetPosition()
    {
      object position = GetPos.Invoke(arFrameworkInstance, null);
      return position;
    }

    public object GetRotation()
    {
      object rotation = GetRot.Invoke(arFrameworkInstance, null);
      return rotation;
    }
  }
}
