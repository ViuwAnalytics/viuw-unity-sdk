
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Networking;

public class ABUploader : MonoBehaviour {

  private byte[] assetBundleData;
  private string presignedUrl;
  private List<string> assetBundleObjectIds;
  private ABData abData;
  private string assetBundleId; //Retrived from presigned put
  private string apiKey;
  private string projectId;
  private string sceneId;
  private Dictionary<string, string> headers;

  /*
  //[START]: Upload delegate setup
  public delegate void ABUploaderDelegate(float progressAsPercent);
  public event ABUploaderDelegate OnUploadProgressChange;

  public float _progress = 0;
  public float progress
  {
    get
    {
      return _progress;
    }
    set
    {
      if (_progress == value) return;
        _progress = value;
      if (OnUploadProgressChange != null)
        OnUploadProgressChange(_progress);
    }
  }
  //[START]: Upload delegate setup
  */


  public void StartAssetBundleUpload(byte[] assetBundleData, List<string> assetBundleObjectIds, string apiKey, string projectId, string sceneId) {

    this.assetBundleData = assetBundleData;
    this.assetBundleObjectIds = assetBundleObjectIds;
    this.apiKey = apiKey;
    this.projectId = projectId;
    this.sceneId = sceneId;
    this.headers = Viuw.Webservice.GetApiKeyHeader(this.apiKey);

    StartCoroutine(GetPresignedUrl());
  }

  public IEnumerator GetPresignedUrl()
  {
    string URL = "https://op4yyxpfkc.execute-api.us-east-2.amazonaws.com/prod/presignedurlput/";
    WWW request = new WWW(URL, null, this.headers);
    yield return request;
    if (!string.IsNullOrEmpty(request.error))
    {
      if (request.error == "403 Forbidden"){
        Debug.Log("VIUW: Upload failed due to invalid API key. Please enter your API key, which can be found on your Viuw Dashboard.");
      } else {
        Debug.Log("VIUW: Uploaded failed due to unknown error. Please try again.");
      }
      DestroyImmediate(this);
    } else {
      PresignedUrlObject presignedUrlObject = JsonUtility.FromJson<PresignedUrlObject>(request.text);
      this.presignedUrl = presignedUrlObject.url;
      Debug.Log("Object id: " + presignedUrlObject.objectId);
      this.assetBundleId = presignedUrlObject.objectId;
      StartCoroutine(UploadToStorage());
    }
  }

  IEnumerator UploadToStorage()
  {
    UnityWebRequest request =  UnityWebRequest.Put(presignedUrl, this.assetBundleData);
    Debug.Log("VIUW: Uploading scene objects to the Viuw Dashboard. This may take up to several minutes depending on the size of your prefabs.");
    request.timeout = 120;

    //StartCoroutine(GetProgress(request));

    yield return request.SendWebRequest();


    if (request.isNetworkError || request.isHttpError)
    {
        Debug.Log("VIUW: Upload failed. Please check your internet connection and try again.");
        DestroyImmediate(this);
    }
    else
    {

        StartCoroutine(UploadToDB());

    }
  }

  /*
  IEnumerator GetProgress(UnityWebRequest request) {
    while (!request.isDone){
      progress = request.uploadProgress;
      yield return null;
    }
  }
  */

  IEnumerator UploadToDB() {
    string url = "https://op4yyxpfkc.execute-api.us-east-2.amazonaws.com/prod/assetbundle";
    ABData abData = new ABData(this.apiKey, this.projectId, this.sceneId, this.assetBundleId, this.assetBundleObjectIds);
    string jsonString = JsonUtility.ToJson(abData);
    var formData = System.Text.Encoding.UTF8.GetBytes(jsonString);
    WWW request =  new WWW(url, formData, this.headers);
    yield return request;

    if (!string.IsNullOrEmpty(request.error))
    {
      if (request.error == "403 Forbidden"){
        Debug.Log("VIUW: Upload failed due to invalid API key. Please enter your API key, which can be found on your Viuw Dashboard.");
      } else {
        Debug.Log("VIUW: Upload failed. Please check your scene ID and project ID, which can be found on your Viuw Dashboard.");
      }
      DestroyImmediate(this);
    } else {
      Debug.Log("VIUW: Scene objects successfully uploaded to the Viuw Dashboard.");
      DestroyImmediate(this);
    }
  }


}
