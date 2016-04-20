using ObjCRuntime;

[assembly: LinkWith("RongIMLib.a", LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.Arm64, SmartLink = true, ForceLoad = true,
    //IsCxx = true,
    Frameworks = "AssetsLibrary AudioToolbox AVFoundation CFNetwork CoreAudio CoreLocation CoreMedia CoreTelephony CoreVideo CoreGraphics ImageIO MapKit OpenGLES UIKit Foundation QuartzCore SystemConfiguration",
    LinkerFlags = "-lc++ -lc++abi -lsqlite3 -lstdc++ -lxml2 -lz -lstdc++ -liconv -ObjC"
)]
