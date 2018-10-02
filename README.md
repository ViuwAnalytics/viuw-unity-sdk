# Viuw Analytics Unity SDK

Viuw Analytics enables augmented reality app developers to understand user engagement by delivering spatial analytics of user behavior.

Our Viuw Dashboard is currently in open beta. We are actively seeking developer feedback and suggestions. We have a dedicated team standing by to provide developer support if any issues are encountered on the Viuw Dashboard or in the Viuw SDK.

## Supported AR Frameworks
The Viuw Unity SDK is compatible with:
1. [ARKit Plugin for Unity](https://bitbucket.org/Unity-Technologies/unity-arkit-plugin/src)
2. [ARCore Plugin for Unity](https://github.com/google-ar/arcore-unity-sdk)

## Getting started
  1. Create and register a scene at www.viuw.io. Create a scene to view your dashboard.
  1. From your dashboard, generate an API key, Project ID, and Scene ID <SCREENSHOT>
  1. Integrate the SDK to view scene reconstruction and analytics

## SDK Integration
1. Download or clone this repository.
1. In your Unity scene, add the Viuw SDK folder to your Unity project files.

# Usage
1. Create an empty game object in your scene hierarchy.

![](https://s3.us-east-2.amazonaws.com/viuw-sdk/addEmptyGameObject.png)

2. Add the ```ViuwSession``` script as a component.

![](https://s3.us-east-2.amazonaws.com/viuw-sdk/addViuwManagerComponent.gif)

3. Specify your API, projectId, and scene Id from the Viuw Dashboard.

4. Next, you will configure your scene objects.

## Understanding Scene Objects

![](https://s3.us-east-2.amazonaws.com/viuw-sdk/scene-object-diagram+(1).png)

## Upload your objects
In order for an object to be be analyzed in your Viuw Dashboard, you must upload your prefabs to the Viuw Dashboard.

1. For each object, drag your prefab into the "Upload Object" field of the scene object. **These objects must come from your project files, not from the scene hierarchy.**
1. When you are ready to upload, press 'Upload Objects'
1. Upload may take up to several minutes based upon the number and size of selected prefabs. You will receive a success response on successful upload. 
1. If you receive an error message, follow the instructions in the message and try again. <SCREENSHOT>

## Track your objects
In order for an object to be tracked in-scene, you must drag the game object into the 'Game Object' field of a Scene Object. **These objects must come from your scene hierarchy, not from your project window.**

## Confirm your integration 
You are all set. To confirm your installation and setup, build and run your app in iOS or Android, and check the 'Individual' dropdown to see that sessions are being logged.

# Roadmap
Viuw is currently building support for Vuforia, Hololens, and Meta 2. Tell us what frameworks and hardware you would like to see supported: admin@viuw.io.

# Feedback and Support
The viuw dashboard is currently in beta and we would love to hear your feedback. On the Viuw Dashboard, select 'Feedback' on the left toolbar. You can also drop us a line at admin@viuw.io.
