using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Viuw
{
  public class DeviceTracker: MonoBehaviour
  {

    Type arFramework;
    object arFrameworkInstance;
    MethodInfo GetPos;
    MethodInfo GetRot;

    private Viuw_Vuforia viuw_vuforia;
    private int frames = 0;

    public List<S_Vector3> s_positions = new List<S_Vector3>();
    public List<S_Quaternion> s_rotations = new List<S_Quaternion>();

    //TEST
    private float updateTime = 0;
    //TEST


    void Start()
    {
      //Set arFramework and arFrameworkInstance depending on the AR framework
      //running on the device
      switch (ViuwManager.Instance.platform)
      {
        case Platform.ARKitPlugin:
          arFramework = Type.GetType("Viuw.ARKit");
          break;
        case Platform.ARCorePlugin:
          arFramework = Type.GetType("Viuw.ARCore");
          break;
        case Platform.Vuforia:
          viuw_vuforia = new Viuw_Vuforia();
          break;
      }


      //Using a try-catch because if platform is vuforia,
      //arFramework is null, and CreateInstance will throw
      try {
        arFrameworkInstance = Activator.CreateInstance(arFramework,null);
        GetPos = arFramework.GetMethod("GetPosition");
        GetRot = arFramework.GetMethod("GetRotation");
      }
      catch (Exception e)
      {
        //Debug.Log(e);
      }

      StartCoroutine(UpdateTime());

    }

    void Update()
    {

      /*
      frames++;
      if (frames == 60/ViuwManager.trackingRate)
      {
        frames = 0;
        Track();

      }*/

      /*
      updateTime += Time.deltaTime;
      if (updateTime >= ViuwManager.testTrackingRate)
      {
        updateTime = 0;
        Track();
      }*/
    }

    public IEnumerator UpdateTime()
    {

      while (enabled)
      {
        yield return new WaitForSeconds(ViuwManager.trackingRate);
        Track();
      }
    }

    public void Track()
    {
      object positionObj = GetPosition();
      object rotationObj = GetRotation();

      if (positionObj != null && rotationObj != null)
      {
        var position = (Vector3)positionObj;
        var rotation = (Quaternion)rotationObj;

        if (position != null && rotation != null)
        {
          var s_position = new S_Vector3(position);
          var s_rotation = new S_Quaternion(rotation);

          s_positions.Add(s_position);
          s_rotations.Add(s_rotation);
        }
      }
    }

    public object GetPosition()
    {
      //TODO fix for next supported framework
      if (ViuwManager.Instance.platform == Platform.Vuforia)
      {
        object pos = viuw_vuforia.GetPosition();
        return pos;
      }

      object position = GetPos.Invoke(arFrameworkInstance, null);
      return position;
    }

    public object GetRotation()
    {
      //TODO fix for next supported framework
      if (ViuwManager.Instance.platform == Platform.Vuforia)
      {
        object rot =  viuw_vuforia.GetRotation();
        return rot;
      }

      object rotation = GetRot.Invoke(arFrameworkInstance, null);
      return rotation;
    }

    public void Clear()
    {
      s_positions.Clear();
      s_rotations.Clear();
    }
  }
}
