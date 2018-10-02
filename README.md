Viuw Analytics Unity SDK
viuw.io


#Description
  Viuw Analytics enables augmented reality app developers to understand user engagement by delivering spatial analytics of user behavior.
  b. Picture of visualizer doing its thing

#Preface
  Our Viuw Dashboard is currently in open beta. We are actively seeking developer feedback, suggestions, and support. We have a dedicated team standing by to provide developer support if any issues are encountered on the Viuw Dashboard or in the Viuw SDK.
  <CONTACT INFO HERE>

#Table of contents

#Supported AR Frameworks
  The Viuw Unity SDK is compatible with:
  1. ARKit Plugin for Unity (link to unityarkitplugin)
  2. ARCore Plugin for Unity (link to unityarcoreplugin)

  IMPORTANT: For user spatial tracking to be enabled, your app must have one of the above plugins integrated.
#How it works
  a. Create and register a scene at www.viuw.io. Create a scene to view your dashboard.
  b. Generate API key, ProjectId, and SceneId
  c. Integrate the SDK to view scene reconstruction and user movement heat mapping

#SDK Integration
  a. Download or clone this [HOW TO PREVENT PEOPLE FROM WRITING TO IT????] repository here
  b. In your Unity scene, add the Viuw SDK folder to your Unity project files. No need to move into Plugins folder. //TODO: check

# Usage
  c. Create an empty game object in your scene: SCREENSHOT
  d. Add ViuwSession script as a component
  e. Specify your API, projectId, and scene Id from the viuw dashboard

  This is where you can specify how many objects you want the Viuw SDK to track.
  a. Enter the number of objects you want the Viuw SDK to track [probably need explanation or intro to object tracking]
## Upload an object
  b. In order for an object to be heatmapped in your Viuw Dashboard, you must upload your prefabs to the Viuw Dashboard.
  c. For each object, drag your prefab into the "Upload Object" field of the scene object
  IMPORTANT: THESE OBJECTS MUST COME FROM YOUR PROJECT FILES AND NOT FROM THE SCENE HIERARCHY.
  d. When you are ready to upload, press 'Upload Objects'
  e. Upload may take up to several minutes based upon the number and size of selected prefabs. You will receive a success response on successful upload. If you receive an error message, follow the instructions in the message and try again.

## Register an object for in-scene tracking
  a. In order for an object to be tracked in-scene, you must drag the game object into the 'Game Object' field of a Scene Object.
  IMPORTANT: UNLIKE UPLOAD, THESE OBJECTS MUST COME FROM YOUR SCENE HIERARCHY AND NOT FROM PROJECT FILES

  You are all set. To confirm your installation and setup, build and run your app in iOS or Android, and check the 'Individual' dropdown to see that sessions are being logged.


# Using the dashboard
The viuw dashboard displays individual and aggregate playback of user sessions of your app. You can toggle between individual and aggregate. If you choose individual, select a session to see. SCREENSHOT OF DROPDOWN.

# Roadmap
  Tell us what frameworks and hardware you would like to see supported: admin@viuw.io, other social media, comments section of github?

  Viuw is currently building support for Vuforia, Hololens, and Meta 2.

  Tell us what frameworks and hardware you would like to see supported: admin@viuw.io, other social media, comments section of github?

# Feedback and Support
  The viuw dashboard is currently in beta and we would love to hear your feedback. On the viuw dashboard, select 'Feedback' on the left toolbar. You can also drop us a line at admin@viuw.io, <other social media here>



#Usage/Implementation
#Caveats/Warnings
#Support
