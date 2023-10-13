using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class PostBuildProcessor
{
    [PostProcessBuildAttribute(Int32.MaxValue)] //We want this code to run last!
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuildProject)
    {
#if UNITY_IOS
        if (buildTarget != BuildTarget.iOS) return; // Make sure its iOS build
            
        // Getting access to the xcode project file
        string projectPath = pathToBuildProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromFile(projectPath);
            
        // Getting the UnityFramework Target and changing build settings
        string target = pbxProject.GetUnityFrameworkTargetGuid();
        pbxProject.SetBuildProperty(target, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");

        // After we're done editing the build settings we save it 
        pbxProject.WriteToFile(projectPath);
#endif
    }
    
    
    [PostProcessBuild(45)]
    private static void OnPostProcessBuildCocoaPodsAdjustments(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget != BuildTarget.iOS) return;
        
        // https://stackoverflow.com/a/51416359
        var content = "\n\npost_install do |installer|\n" +
                      "installer.pods_project.targets.each do |target|\n" +
                      "  target.build_configurations.each do |config|\n" +
                      // https://stackoverflow.com/questions/72561696/xcode-14-needs-selected-development-team-for-pod-bundles
                      "    config.build_settings['ENABLE_BITCODE'] = 'YES'\n" +
                      "  end\n" +
                      " end\n" +
                      "end\n";

        using var streamWriter = File.AppendText(Path.Combine(pathToBuiltProject, "Podfile"));
        streamWriter.WriteLine(content);
        Debug.Log("ASSSS");
    }
}