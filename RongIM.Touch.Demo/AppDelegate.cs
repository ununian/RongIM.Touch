using Foundation;
using UIKit;
using System;

namespace RongIM.Touch.Demo
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }

    public class ChatUserDataSource : RCIMUserInfoDataSource
    {
         
        public override async void Completion(string userId, Action<RCUserInfo> completion)
        {
            var user = new RCUserInfo();


            user.Name = userId;

            user.UserId = userId;


            completion(user);
        }
    }


    public static class RCIMInitializer
    {
        private static bool _isInited = false;
        private static bool _isIniting = false;

        public static void DisconnectRCIM()
        {
            if (_isInited)
            {
                RCIM.SharedRCIM.Disconnect(false);
                _isInited = false;
            }
        }

        public static void ConnectRCIM(UIApplication application, string appkey, string token)
        {
            if (_isIniting || _isInited)
                return;

            _isIniting = true;
            RCIM.SharedRCIM.InitWithAppKey(appkey);
            RCIM.SharedRCIM.ReceiveMessageDelegate = new IMRMDelegate();
            RCIM.SharedRCIM.UserInfoDataSource = new ChatUserDataSource();
            if (application.IsRegisteredForRemoteNotifications)
            {
                var setting = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
                application.RegisterUserNotificationSettings(setting);
            }
            else
            {
                var myTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                application.RegisterForRemoteNotificationTypes(myTypes);
            }

            RCIM.SharedRCIM.ConnectWithToken(token,
                success =>
                {

                    System.Console.WriteLine("Connect RCIM Success");
                    _isInited = true;
                    _isIniting = false;
                },
                err =>
                {
                    _isIniting = false;

                    System.Console.WriteLine("Connect RCIM Error : " + err.ToString());
                },
                () =>
                {
                    _isIniting = false; 
                    System.Console.WriteLine("Connect RCIM Error : Auth Token Fail");
                });
        }
    }

    public class IMRMDelegate : RCIMReceiveMessageDelegate
    {
        public override void OnRCIMReceiveMessage(RCMessage message, int left)
        {

        }
    }

    public class IMCSDelegate : RCIMConnectionStatusDelegate
    {
        public override void OnRCIMConnectionStatusChanged(RCConnectionStatus status)
        {

        }
    }

}