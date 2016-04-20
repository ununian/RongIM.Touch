using System;

using UIKit;
using Foundation;
using System.Collections.Generic;

namespace RongIM.Touch.Demo
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            RCIMInitializer.ConnectRCIM(UIApplication.SharedApplication, "vnroth0krmm2o", @"bkeO+YxOP1RSXquTpwAXtPcjXKa/97eHWljfgGUGm1J45QgCjtREOjNOZSPXRatAI3hrPNzJLpBtv6l9zo8QW3Fo3TTzDzXl");

            this.PresentViewController(new ChatListViewController(new RCConversationType[]{ RCConversationType.Private }), true, null);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }

    public class ChatListViewController : RCConversationListViewController
    {
        public ChatListViewController(RCConversationType[] types1, RCConversationType[] types2 = null)
            : base(ToNSObjectsArray(types1), ToNSObjectsArray(types2))
        {

        }

        public override void OnSelectedTableRow(RCConversationModelType conversationModelType, RCConversationModel model, NSIndexPath indexPath)
        {
            base.OnSelectedTableRow(conversationModelType, model, indexPath); 

        }

        public static NSObject[] ToNSObjectsArray(Array self)
        {
            if (self == null)
                return new NSObject[0];
            var result = new List<NSObject>();
            foreach (var obj in self)
            {
                result.Add(NSObject.FromObject(obj));
            }

            return result.ToArray();
        }
    }
}

