using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Viuw;

[CustomEditor(typeof(ViuwManager))]
public class ABUploaderEditor : Editor
{

	private List<string> assetBundleObjectIds = new List<string>();

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		ViuwManager viuwManager = (ViuwManager)target;
		if (GUILayout.Button("Upload objects"))
		{
			assetBundleObjectIds.Clear();//Clear the list of assetBundleObjectIds
			BuildAssetBundle(viuwManager);
		}
	}

	void BuildAssetBundle(ViuwManager viuwManager)
	{


		//Create a temp directory to house scene object uploads, if doesn't exist.
		//If exists, set asset bundle to null
		string tempDirPath = "Assets/ViuwObjects";
		if(!Directory.Exists(tempDirPath))
		{
			AssetDatabase.CreateFolder("Assets", "ViuwObjects");
		} else {
			Directory.Delete(tempDirPath, true);
			AssetDatabase.CreateFolder("Assets", "ViuwObjects");
		}


		//Assign the temp directory to a new asset bundle
		string bundleName = Guid.NewGuid().ToString();
		//Debug.Log("Bundle name: " + bundleName);
		AssetImporter.GetAtPath(tempDirPath).SetAssetBundleNameAndVariant(bundleName, "");


		//Add upload object names to object id array
		//Copy each of the given upload objects to the temp directory
		for (int i = 0; i < viuwManager.sceneObjects.Count; i++)
		{

			if (viuwManager.sceneObjects[i].uploadObject == null) {
				Debug.Log("VIUW: One of your upload objets is null. Skipping it. Please review your upload objects.");
				continue;
			}

			SceneObject sceneObject = viuwManager.sceneObjects[i];

			//Add game object name to assetBundleObjectIds. Session object ids will be checked against these to validate that the session objects reflect asset bundle
			string assetName = sceneObject.uploadObject.name;
			string assetPath = AssetDatabase.GetAssetPath(sceneObject.uploadObject);
			assetBundleObjectIds.Add(assetName);

			//Set asset bundle to none
			//TODO Catch error from following code. Occurs when dragging object from hierarchy into upload object
			AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(null, "");

			//Copy asset into temp dir
			string tempAssetPath = tempDirPath + '/' + assetName + ".prefab";
			AssetDatabase.CopyAsset(assetPath, tempAssetPath);

		}



		string assetBundleDirectory = "Assets/AssetBundles";
		//Check that asset bundle directory exists
		if(!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}

		//Build the asset bundle
		Debug.Log("VIUW: Compiling scene objects. Please wait.");
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);

		string assetBundlePath = "Assets/AssetBundles/" + bundleName;
		byte[] assetBundleData = File.ReadAllBytes(assetBundlePath);

		//Delete the temp ViuwObjects directory
		Directory.Delete(tempDirPath, true);
		File.Delete(String.Format("{0}.meta", tempDirPath));

		//Call ABUploader via ViuwManager
		ABUploader abUploader = viuwManager.gameObject.GetComponent<ABUploader>();
		if (abUploader == null) {abUploader = viuwManager.gameObject.AddComponent<ABUploader>();}
		//abUploader.OnUploadProgressChange += UploadProgressDidChange; //Link delegate
		abUploader.StartAssetBundleUpload(assetBundleData, assetBundleObjectIds, viuwManager.apiKey, viuwManager.projectId, viuwManager.sceneId);

		GUIUtility.ExitGUI();
	}

	/*
	void UploadProgressDidChange(float progressAsPercent){
		Debug.Log("progress retrieved: " + progressAsPercent);
	}
	*/

}
