using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct ABData {

	public string apiKey;
	public string projectId;
	public string sceneId;
	public string assetBundleId;
	public List<string> assetBundleObjectIds;

	public ABData(string apiKey, string projectId, string sceneId, string assetBundleId, List<string> assetBundleObjectIds)
	{

		this.apiKey = apiKey;
		this.projectId = projectId;
		this.sceneId = sceneId;
		this.assetBundleId = assetBundleId;
		this.assetBundleObjectIds = assetBundleObjectIds;


	}

}
