// filename BuildPostProcessor.cs
// put it in a folder Assets/Editor/
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class BuildPostProcessor
{

    [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath) {
            if(buildTarget == BuildTarget.iOS) {
             
                ChangeXcodePlist(buildTarget,buildPath);
             
                // So PBXProject.GetPBXProjectPath returns wrong path, we need to construct path by ourselves instead
                // var projPath = PBXProject.GetPBXProjectPath(buildPath);
                var projPath = buildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
                var proj = new PBXProject();
                proj.ReadFromFile(projPath);
 
                var targetGuid = proj.GetUnityFrameworkTargetGuid();
 
                UnityEngine.Debug.Log("targetGuid: " + targetGuid);
 
                //// Configure build settings
                proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
 
                proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "UnityFramework-Swift.h");
                proj.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");
                proj.SetBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");
 
                proj.WriteToFile(projPath);
 
                ProjectCapabilityManager projCapability = new ProjectCapabilityManager(projPath, "Unity-iPhone/mmk.entitlements", "Unity-iPhone");
 
                projCapability.AddHealthKit();
                projCapability.WriteToFile();
            }
        }
    
    public static void ChangeXcodePlist(BuildTarget buildTarget, string path)
    {

        if (buildTarget == BuildTarget.iOS)
        {
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            PlistElementDict rootDict = plist.root;

            Debug.Log(">> Automation, plist ... <<");

            //    PlistElementArray barray = rootDict.CreateArray("Bonjour services");
            //    barray.AddString("_bonjourmirror._tcp");

            // example of changing a value:
            rootDict.SetString("Privacy - Local Network Usage Description", "Use local network");

            // example of adding a boolean key...
            // < key > ITSAppUsesNonExemptEncryption </ key > < false />
            //  rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);

            File.WriteAllText(plistPath, plist.WriteToString());



        }
    }
}
