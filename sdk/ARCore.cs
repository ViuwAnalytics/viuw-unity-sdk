
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Viuw
{
  public class ARCore
  {

    Type frameType;
    object arcoreInstance;
    PropertyInfo poseProperty;

    public ARCore() {
      frameType = Type.GetType("GoogleARCore.Frame");

      if (frameType != null){

        arcoreInstance = Activator.CreateInstance(frameType, null);
        poseProperty = frameType.GetProperty("Pose");

      }

    }


    public object GetPosition()
    {

      if (frameType == null || arcoreInstance == null){return null;}

      object arcorePose = poseProperty.GetValue(arcoreInstance, null);
      UnityEngine.Pose unityPose = (UnityEngine.Pose)arcorePose;
      object position = (object)unityPose.position;
      return position;
    }

    public object GetRotation()
    {
      if (frameType == null || arcoreInstance == null){return null;}

      object arcorePose = poseProperty.GetValue(arcoreInstance, null);
      UnityEngine.Pose unityPose = (UnityEngine.Pose)arcorePose;
      object rotation = (object)unityPose.rotation;
      return rotation;
    }
  }
}
