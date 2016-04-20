using System;
using CoreAnimation;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using ObjCRuntime;
using UIKit;

namespace RongIM.Touch
{


    [BaseType(typeof(NSObject))]
    interface RCUserInfo : INSCoding
    {
        [Export("userId", ArgumentSemantic.Strong)]
        string UserId { get; set; }

        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }

        [Export("portraitUri", ArgumentSemantic.Strong)]
        string PortraitUri { get; set; }

        [Export("initWithUserId:name:portrait:")]
        IntPtr Constructor(string userId, string username, string portrait);

    }

    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCMessageCoding
    {
        [Abstract]
        [Export("encode")] 
        NSData Encode { get; }

        [Abstract]
        [Export("decodeWithData:")]
        void DecodeWithData(NSData data);

        [Static, Abstract]
        [Export("getObjectName")] 
        string ObjectName { get; }

    }

    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCMessagePersistentCompatible
    {
        [Static, Abstract]
        [Export("persistentFlag")] 
        RCMessagePersistent PersistentFlag { get; }

    }

    [Protocol, Model]
    interface RCMessageContentView
    {
        [Export("conversationDigest")] 
        string ConversationDigest { get; }

    }

    [BaseType(typeof(NSObject))]
    interface RCMessageContent : RCMessageCoding, RCMessagePersistentCompatible, RCMessageContentView
    {
        [Export("senderUserInfo", ArgumentSemantic.Strong)]
        RCUserInfo SenderUserInfo { get; set; }

        [Export("decodeUserInfo:")]
        void DecodeUserInfo(NSDictionary dictionary);

        [Export("rawJSONData", ArgumentSemantic.Strong)]
        NSData RawJSONData { get; [Bind ("setRawJSONData:")] set; }

    }

    [BaseType(typeof(NSObject))]
    interface RCMessage : INSCopying, INSCoding
    {
        [Export("conversationType", ArgumentSemantic.Assign)]
        RCConversationType ConversationType { get; set; }

        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        [Export("messageId")]
        nint MessageId { get; set; }

        [Export("messageDirection", ArgumentSemantic.Assign)]
        RCMessageDirection MessageDirection { get; set; }

        [Export("senderUserId", ArgumentSemantic.Strong)]
        string SenderUserId { get; set; }

        [Export("receivedStatus", ArgumentSemantic.Assign)]
        RCReceivedStatus ReceivedStatus { get; set; }

        [Export("sentStatus", ArgumentSemantic.Assign)]
        RCSentStatus SentStatus { get; set; }

        [Export("receivedTime")]
        long ReceivedTime { get; set; }

        [Export("sentTime")]
        long SentTime { get; set; }

        [Export("objectName", ArgumentSemantic.Strong)]
        string ObjectName { get; set; }

        [Export("content", ArgumentSemantic.Strong)]
        RCMessageContent Content { get; set; }

        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }

        [Export("messageUId", ArgumentSemantic.Strong)]
        string MessageUId { get; set; }

        [Export("initWithType:targetId:direction:messageId:content:")]
        IntPtr Constructor(RCConversationType conversationType, string targetId, RCMessageDirection messageDirection, nint messageId, RCMessageContent content);

        [Static]
        [Export("messageWithJSON:")]
        RCMessage MessageWithJSON(NSDictionary jsonData);

    }

    [BaseType(typeof(NSObject))]
    interface RCPublicServiceMenuItem
    {
        [Export("id", ArgumentSemantic.Strong)]
        string Id { get; set; }

        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }

        [Export("url", ArgumentSemantic.Strong)]
        string Url { get; set; }

        [Export("type", ArgumentSemantic.Assign)]
        RCPublicServiceMenuItemType Type { get; set; }

        [Export("subMenuItems", ArgumentSemantic.Strong)] 
        NSObject[] SubMenuItems { get; set; }

        [Static]
        [Export("menuItemsFromJsonArray:")] 
        NSObject[] MenuItemsFromJsonArray(NSObject[] jsonArray);

    }

    [BaseType(typeof(NSObject))]
    interface RCPublicServiceMenu
    {
        [Export("menuItems", ArgumentSemantic.Strong)] 
        NSObject[] MenuItems { get; set; }

        [Export("decodeWithJsonDictionaryArray:")] 
        void DecodeWithJsonDictionaryArray(NSObject[] jsonDictionary);

    }

    [BaseType(typeof(NSObject))]
    interface RCPublicServiceProfile
    {
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }

        [Export("introduction", ArgumentSemantic.Strong)]
        string Introduction { get; set; }

        [Export("publicServiceId", ArgumentSemantic.Strong)]
        string PublicServiceId { get; set; }

        [Export("portraitUrl", ArgumentSemantic.Strong)]
        string PortraitUrl { get; set; }

        [Export("owner", ArgumentSemantic.Strong)]
        string Owner { get; set; }

        [Export("ownerUrl", ArgumentSemantic.Strong)]
        string OwnerUrl { get; set; }

        [Export("publicServiceTel", ArgumentSemantic.Strong)]
        string PublicServiceTel { get; set; }

        [Export("histroyMsgUrl", ArgumentSemantic.Strong)]
        string HistroyMsgUrl { get; set; }

        [Export("location", ArgumentSemantic.Strong)]
        CLLocation Location { get; set; }

        [Export("scope", ArgumentSemantic.Strong)]
        string Scope { get; set; }

        [Export("publicServiceType", ArgumentSemantic.Assign)]
        RCPublicServiceType PublicServiceType { get; set; }

        [Export("followed")]
        bool Followed { [Bind ("isFollowed")] get; set; }

        [Export("menu", ArgumentSemantic.Strong)]
        RCPublicServiceMenu Menu { get; set; }

        [Export("global")]
        bool Global { [Bind ("isGlobal")] get; set; }

        [Export("jsonDict", ArgumentSemantic.Strong)]
        NSDictionary JsonDict { get; set; }

        [Export("initContent:")]
        void InitContent(string jsonContent);

    }

    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCWatchKitStatusDelegate
    {
        [Export("notifyWatchKitConnectionStatusChanged:")]
        void NotifyWatchKitConnectionStatusChanged(RCConnectionStatus status);

        [Export("notifyWatchKitReceivedMessage:")]
        void NotifyWatchKitReceivedMessage(RCMessage receivedMsg);

        [Export("notifyWatchKitSendMessage:")]
        void NotifyWatchKitSendMessage(RCMessage message);

        [Export("notifyWatchKitSendMessageCompletion:status:")]
        void NotifyWatchKitSendMessageCompletion(nint messageId, RCErrorCode status);

        [Export("notifyWatchKitUploadFileProgress:messageId:")]
        void NotifyWatchKitUploadFileProgress(int progress, nint messageId);

        [Export("notifyWatchKitClearConversations:")] 
        void NotifyWatchKitClearConversations(NSObject[] conversationTypeList);

        [Export("notifyWatchKitClearMessages:targetId:")]
        void NotifyWatchKitClearMessages(RCConversationType conversationType, string targetId);

        [Export("notifyWatchKitDeleteMessages:")] 
        void NotifyWatchKitDeleteMessages(NSObject[] messageIds);

        [Export("notifyWatchKitClearUnReadStatus:targetId:")]
        void NotifyWatchKitClearUnReadStatus(RCConversationType conversationType, string targetId);

        [Export("notifyWatchKitCreateDiscussion:userIdList:")] 
        void NotifyWatchKitCreateDiscussion(string name, NSObject[] userIdList);

        [Export("notifyWatchKitCreateDiscussionSuccess:")]
        void NotifyWatchKitCreateDiscussionSuccess(string discussionId);

        [Export("notifyWatchKitCreateDiscussionError:")]
        void NotifyWatchKitCreateDiscussionError(RCErrorCode errorCode);

        [Export("notifyWatchKitAddMemberToDiscussion:userIdList:")] 
        void NotifyWatchKitAddMemberToDiscussion(string discussionId, NSObject[] userIdList);

        [Export("notifyWatchKitRemoveMemberFromDiscussion:userId:")]
        void NotifyWatchKitRemoveMemberFromDiscussion(string discussionId, string userId);

        [Export("notifyWatchKitQuitDiscussion:")]
        void NotifyWatchKitQuitDiscussion(string discussionId);

        [Export("notifyWatchKitDiscussionOperationCompletion:status:")]
        void NotifyWatchKitDiscussionOperationCompletion(int tag, RCErrorCode status);

    }

    [BaseType(typeof(NSObject))]
    interface RCUploadImageStatusListener
    {
        [Export("currentMessage", ArgumentSemantic.Strong)]
        RCMessage CurrentMessage { get; set; }

        [Export("updateBlock", ArgumentSemantic.Strong)]
        Action<int> UpdateBlock { get; set; }

        [Export("successBlock", ArgumentSemantic.Strong)]
        Action<NSString> SuccessBlock { get; set; }

        [Export("errorBlock", ArgumentSemantic.Strong)]
        Action<RCErrorCode> ErrorBlock { get; set; }

        [Export("initWithMessage:uploadProgress:uploadSuccess:uploadError:")]
        IntPtr Constructor(RCMessage message, Action<int> progressBlock, Action<NSString> successBlock, Action<RCErrorCode> errorBlock);

    }

    [BaseType(typeof(NSObject))]
    interface RCConversation : INSCoding
    {
        [Export("conversationType", ArgumentSemantic.Assign)]
        RCConversationType ConversationType { get; set; }

        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        [Export("conversationTitle", ArgumentSemantic.Strong)]
        string ConversationTitle { get; set; }

        [Export("unreadMessageCount")]
        int UnreadMessageCount { get; set; }

        [Export("isTop")]
        bool IsTop { get; set; }

        [Export("receivedStatus", ArgumentSemantic.Assign)]
        RCReceivedStatus ReceivedStatus { get; set; }

        [Export("sentStatus", ArgumentSemantic.Assign)]
        RCSentStatus SentStatus { get; set; }

        [Export("receivedTime")]
        long ReceivedTime { get; set; }

        [Export("sentTime")]
        long SentTime { get; set; }

        [Export("draft", ArgumentSemantic.Strong)]
        string Draft { get; set; }

        [Export("objectName", ArgumentSemantic.Strong)]
        string ObjectName { get; set; }

        [Export("senderUserId", ArgumentSemantic.Strong)]
        string SenderUserId { get; set; }

        [Export("senderUserName", ArgumentSemantic.Strong)]
        string SenderUserName { get; set; }

        [Export("lastestMessageId")]
        nint LastestMessageId { get; set; }

        [Export("lastestMessage", ArgumentSemantic.Strong)]
        RCMessageContent LastestMessage { get; set; }

        [Export("jsonDict", ArgumentSemantic.Strong)]
        NSDictionary JsonDict { get; set; }

        [Export("lastestMessageUId", ArgumentSemantic.Strong)]
        string LastestMessageUId { get; set; }

        [Static]
        [Export("conversationWithProperties:")]
        RCConversation ConversationWithProperties(NSDictionary json);

    }

    [BaseType(typeof(NSObject))]
    interface RCDiscussion
    {
        [Export("discussionId", ArgumentSemantic.Strong)]
        string DiscussionId { get; set; }

        [Export("discussionName", ArgumentSemantic.Strong)]
        string DiscussionName { get; set; }

        [Export("creatorId", ArgumentSemantic.Strong)]
        string CreatorId { get; set; }

        [Export("memberIdList", ArgumentSemantic.Strong)] 
        NSObject[] MemberIdList { get; set; }

        [Export("inviteStatus")]
        int InviteStatus { get; set; }

        [Export("conversationType")]
        int ConversationType { get; set; }

        [Export("pushMessageNotificationStatus")]
        int PushMessageNotificationStatus { get; set; }

        [Export("initWithDiscussionId:discussionName:creatorId:conversationType:memberIdList:inviteStatus:msgNotificationStatus:")] 
        IntPtr Constructor(string discussionId, string discussionName, string creatorId, int conversationType, NSObject[] memberIdList, int inviteStatus, int pushMessageNotificationStatus);

    }

    [BaseType(typeof(NSObject))]
    interface RCChatRoomInfo
    {
        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        [Export("memberOrder", ArgumentSemantic.Assign)]
        RCChatRoomMemberOrder MemberOrder { get; set; }

        [Export("memberInfoArray", ArgumentSemantic.Strong)]  
        NSObject[] MemberInfoArray { get; set; }

        [Export("totalMemberCount")]
        int TotalMemberCount { get; set; }

    }

    [BaseType(typeof(NSObject))]
    interface RCCustomServiceConfig
    {
        [Export("isBlack")]
        bool IsBlack { get; set; }

        [Export("companyName", ArgumentSemantic.Strong)]
        string CompanyName { get; set; }

        [Export("companyUrl", ArgumentSemantic.Strong)]
        string CompanyUrl { get; set; }

    }

    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCIMClientReceiveMessageDelegate
    {
        [Abstract]
        [Export("onReceived:left:object:")]
        void Left(RCMessage message, int nLeft, NSObject @object);

    }

    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCConnectionStatusChangeDelegate
    {
        [Abstract]
        [Export("onConnectionStatusChanged:")]
        void OnConnectionStatusChanged(RCConnectionStatus status);

    }

    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCTypingStatusDelegate
    {
        [Abstract]
        [Export("onTypingStatusChanged:targetId:status:")] 
        void TargetId(RCConversationType conversationType, string targetId, NSObject[] userTypingStatusList);

    }

    [BaseType(typeof(NSObject))]
    interface RCIMClient
    {
        [Static]
        [Export("sharedRCIMClient")]
        RCIMClient SharedRCIMClient{ get; }

        [Export("init:")]
        void Init(string appKey);

        [Export("initWithAppKey:")]
        void InitWithAppKey(string appKey);

        [Export("setDeviceToken:")]
        void SetDeviceToken(string deviceToken);

        [Export("connectWithToken:success:error:tokenIncorrect:")]
        void ConnectWithToken(string token, Action<NSString> successBlock, Action<RCConnectErrorCode> errorBlock, Action tokenIncorrectBlock);

        [Export("disconnect:")]
        void Disconnect(bool isReceivePush);

        [Export("disconnect")]
        void Disconnect();

        [Export("logout")]
        void Logout();

        [Export("setRCConnectionStatusChangeDelegate:")]
        void SetRCConnectionStatusChangeDelegate(RCConnectionStatusChangeDelegate @delegate);

        [Export("getConnectionStatus")] 
        RCConnectionStatus ConnectionStatus { get; }

        [Export("getCurrentNetworkStatus")] 
        RCNetworkStatus CurrentNetworkStatus { get; }

        [Export("sdkRunningMode", ArgumentSemantic.Assign)]
        RCSDKRunningMode SdkRunningMode { get; }

        [Wrap("WeakWatchKitStatusDelegate")]
        RCWatchKitStatusDelegate WatchKitStatusDelegate { get; set; }

        [NullAllowed, Export("watchKitStatusDelegate", ArgumentSemantic.Strong)]
        NSObject WeakWatchKitStatusDelegate { get; set; }

        [Export("currentUserInfo", ArgumentSemantic.Strong)]
        RCUserInfo CurrentUserInfo { get; set; }

        [Export("getUserInfo:success:error:")]
        void GetUserInfo(string userId, Action<RCUserInfo> successBlock, Action<RCErrorCode> errorBlock);

        [Export("registerMessageType:")]
        void RegisterMessageType(Class messageClass);

        [Export("sendMessage:targetId:content:pushContent:success:error:")]
        RCMessage SendMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        [Export("sendMessage:targetId:content:pushContent:pushData:success:error:")]
        RCMessage SendMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, string pushData, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        [Export("sendImageMessage:targetId:content:pushContent:progress:success:error:")]
        RCMessage SendImageMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, Action<int, nint> progressBlock, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        [Export("sendImageMessage:targetId:content:pushContent:pushData:progress:success:error:")]
        RCMessage SendImageMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, string pushData, Action<int, nint> progressBlock, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        [Export("sendImageMessage:targetId:content:pushContent:pushData:uploadPrepare:progress:success:error:")]
        RCMessage SendImageMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, string pushData, Action<RCUploadImageStatusListener> uploadPrepareBlock, Action<int, nint> progressBlock, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        [Export("sendStatusMessage:targetId:content:success:error:")]
        RCMessage SendStatusMessage(RCConversationType conversationType, string targetId, RCMessageContent content, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        [Export("insertMessage:targetId:senderUserId:sendStatus:content:")]
        RCMessage InsertMessage(RCConversationType conversationType, string targetId, string senderUserId, RCSentStatus sendStatus, RCMessageContent content);

        [Export("downloadMediaFile:targetId:mediaType:mediaUrl:progress:success:error:")]
        void DownloadMediaFile(RCConversationType conversationType, string targetId, RCMediaType mediaType, string mediaUrl, Action<int> progressBlock, Action<NSString> successBlock, Action<RCErrorCode> errorBlock);

        [Export("setReceiveMessageDelegate:object:")]
        void SetReceiveMessageDelegate(RCIMClientReceiveMessageDelegate @delegate, NSObject userData);

        [Export("sendReadReceiptMessage:targetId:time:")]
        void SendReadReceiptMessage(RCConversationType conversationType, string targetId, long timestamp);

        [Export("getLatestMessages:targetId:count:")] 
        NSObject[] GetLatestMessages(RCConversationType conversationType, string targetId, int count);

        [Export("getHistoryMessages:targetId:oldestMessageId:count:")] 
        NSObject[] GetHistoryMessages(RCConversationType conversationType, string targetId, nint oldestMessageId, int count);

        [Export("getHistoryMessages:targetId:objectName:oldestMessageId:count:")] 
        NSObject[] GetHistoryMessages(RCConversationType conversationType, string targetId, string objectName, nint oldestMessageId, int count);

        [Export("getRemoteHistoryMessages:targetId:recordTime:count:success:error:")]
        void GetRemoteHistoryMessages(RCConversationType conversationType, string targetId, long recordTime, int count, Action<NSArray> successBlock, Action<RCErrorCode> errorBlock);

        [Export("getMessageSendTime:")]
        long GetMessageSendTime(nint messageId);

        [Export("getMessage:")]
        RCMessage GetMessage(nint messageId);

        [Export("getMessageByUId:")]
        RCMessage GetMessageByUId(string messageUId);

        [Export("deleteMessages:")] 
        bool DeleteMessages(NSObject[] messageIds);

        [Export("clearMessages:targetId:")]
        bool ClearMessages(RCConversationType conversationType, string targetId);

        [Export("setMessageExtra:value:")]
        bool SetMessageExtra(nint messageId, string value);

        [Export("setMessageReceivedStatus:receivedStatus:")]
        bool SetMessageReceivedStatus(nint messageId, RCReceivedStatus receivedStatus);

        [Export("setMessageSentStatus:sentStatus:")]
        bool SetMessageSentStatus(nint messageId, RCSentStatus sentStatus);

        [Export("getConversationList:")] 
        NSObject[] GetConversationList(NSObject[] conversationTypeList);

        [Export("getConversation:targetId:")]
        RCConversation GetConversation(RCConversationType conversationType, string targetId);

        [Export("getMessageCount:targetId:")]
        int GetMessageCount(RCConversationType conversationType, string targetId);

        [Export("clearConversations:")] 
        bool ClearConversations(NSObject[] conversationTypeList);

        [Export("removeConversation:targetId:")]
        bool RemoveConversation(RCConversationType conversationType, string targetId);

        [Export("setConversationToTop:targetId:isTop:")]
        bool SetConversationToTop(RCConversationType conversationType, string targetId, bool isTop);

        [Export("getTextMessageDraft:targetId:")]
        string GetTextMessageDraft(RCConversationType conversationType, string targetId);

        [Export("saveTextMessageDraft:targetId:content:")]
        bool SaveTextMessageDraft(RCConversationType conversationType, string targetId, string content);

        [Export("clearTextMessageDraft:targetId:")]
        bool ClearTextMessageDraft(RCConversationType conversationType, string targetId);

        [Export("getTotalUnreadCount")] 
        int TotalUnreadCount { get; }

        [Export("getUnreadCount:targetId:")]
        int GetUnreadCount(RCConversationType conversationType, string targetId);

        [Export("getUnreadCount:")] 
        int GetUnreadCount(NSObject[] conversationTypes);

        [Export("clearMessagesUnreadStatus:targetId:")]
        bool ClearMessagesUnreadStatus(RCConversationType conversationType, string targetId);

        [Export("setConversationNotificationStatus:targetId:isBlocked:success:error:")]
        void SetConversationNotificationStatus(RCConversationType conversationType, string targetId, bool isBlocked, Action<RCConversationNotificationStatus> successBlock, Action<RCErrorCode> errorBlock);

        [Export("getConversationNotificationStatus:targetId:success:error:")]
        void GetConversationNotificationStatus(RCConversationType conversationType, string targetId, Action<RCConversationNotificationStatus> successBlock, Action<RCErrorCode> errorBlock);

        [Export("setNotificationQuietHours:spanMins:success:error:")]
        void SetNotificationQuietHours(string startTime, int spanMins, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("removeNotificationQuietHours:error:")]
        void RemoveNotificationQuietHours(Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("getNotificationQuietHours:error:")]
        void GetNotificationQuietHours(Action<NSString, int> successBlock, Action<RCErrorCode> errorBlock);

        [Export("setConversationNotificationQuietHours:spanMins:success:error:")]
        void SetConversationNotificationQuietHours(string startTime, int spanMins, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("removeConversationNotificationQuietHours:error:")]
        void RemoveConversationNotificationQuietHours(Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("setRCTypingStatusDelegate:")]
        void SetRCTypingStatusDelegate(RCTypingStatusDelegate @delegate);

        [Export("sendTypingStatus:targetId:contentType:")]
        void SendTypingStatus(RCConversationType conversationType, string targetId, string objectName);

        [Export("addToBlacklist:success:error:")]
        void AddToBlacklist(string userId, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("removeFromBlacklist:success:error:")]
        void RemoveFromBlacklist(string userId, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("getBlacklistStatus:success:error:")]
        void GetBlacklistStatus(string userId, Action<int> successBlock, Action<RCErrorCode> errorBlock);

        [Export("getBlacklist:error:")]
        void GetBlacklist(Action<NSArray> successBlock, Action<RCErrorCode> errorBlock);

        [Export("createDiscussion:userIdList:success:error:")] 
        void CreateDiscussion(string name, NSObject[] userIdList, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        [Export("addMemberToDiscussion:userIdList:success:error:")] 
        void AddMemberToDiscussion(string discussionId, NSObject[] userIdList, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        [Export("removeMemberFromDiscussion:userId:success:error:")]
        void RemoveMemberFromDiscussion(string discussionId, string userId, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        [Export("quitDiscussion:success:error:")]
        void QuitDiscussion(string discussionId, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        [Export("getDiscussion:success:error:")]
        void GetDiscussion(string discussionId, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        [Export("setDiscussionName:name:success:error:")]
        void SetDiscussionName(string targetId, string discussionName, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("setDiscussionInviteStatus:isOpen:success:error:")]
        void SetDiscussionInviteStatus(string targetId, bool isOpen, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("syncGroups:success:error:")] 
        void SyncGroups(NSObject[] groupList, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("joinGroup:groupName:success:error:")]
        void JoinGroup(string groupId, string groupName, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("quitGroup:success:error:")]
        void QuitGroup(string groupId, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("joinChatRoom:messageCount:success:error:")]
        void JoinChatRoom(string targetId, int messageCount, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("joinExistChatRoom:messageCount:success:error:")]
        void JoinExistChatRoom(string targetId, int messageCount, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("quitChatRoom:success:error:")]
        void QuitChatRoom(string targetId, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("getChatRoomInfo:count:order:success:error:")]
        void GetChatRoomInfo(string targetId, int count, RCChatRoomMemberOrder order, Action<RCChatRoomInfo> successBlock, Action<RCErrorCode> errorBlock);

        [Export("searchPublicService:searchKey:success:error:")]
        void SearchPublicService(RCSearchType searchType, string searchKey, Action<NSArray> successBlock, Action<RCErrorCode> errorBlock);

        [Export("searchPublicServiceByType:searchType:searchKey:success:error:")]
        void SearchPublicServiceByType(RCPublicServiceType publicServiceType, RCSearchType searchType, string searchKey, Action<NSArray> successBlock, Action<RCErrorCode> errorBlock);

        [Export("subscribePublicService:publicServiceId:success:error:")]
        void SubscribePublicService(RCPublicServiceType publicServiceType, string publicServiceId, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("unsubscribePublicService:publicServiceId:success:error:")]
        void UnsubscribePublicService(RCPublicServiceType publicServiceType, string publicServiceId, Action successBlock, Action<RCErrorCode> errorBlock);

        [Export("getPublicServiceList")] 
        NSObject[] PublicServiceList { get; }

        [Export("getPublicServiceProfile:publicServiceId:")]
        RCPublicServiceProfile GetPublicServiceProfile(RCPublicServiceType publicServiceType, string publicServiceId);

        [Export("getPublicServiceProfile:conversationType:onSuccess:onError:")]
        void GetPublicServiceProfile(string targetId, RCConversationType type, Action<RCPublicServiceProfile> onSuccess, Action<NSError> onError);

        [Export("getPublicServiceWebViewController:")]
        UIViewController GetPublicServiceWebViewController(string URLString);

        [Export("recordLaunchOptionsEvent:")]
        void RecordLaunchOptionsEvent(NSDictionary launchOptions);

        [Export("recordLocalNotificationEvent:")]
        void RecordLocalNotificationEvent(UILocalNotification notification);

        [Export("recordRemoteNotificationEvent:")]
        void RecordRemoteNotificationEvent(NSDictionary userInfo);

        [Export("getPushExtraFromLaunchOptions:")]
        NSDictionary GetPushExtraFromLaunchOptions(NSDictionary launchOptions);

        [Export("getPushExtraFromRemoteNotification:")]
        NSDictionary GetPushExtraFromRemoteNotification(NSDictionary userInfo);

        [Export("getSDKVersion")] 
        string SDKVersion { get; }

        [Export("decodeAMRToWAVE:")]
        NSData DecodeAMRToWAVE(NSData data);

        [Export("encodeWAVEToAMR:channel:nBitsPerSample:")]
        NSData EncodeWAVEToAMR(NSData data, int nChannels, int nBitsPerSample);

        [Export("startCustomService:onSuccess:onError:onModeType:onQuit:")]
        void StartCustomService(string kefuId, Action<RCCustomServiceConfig> successBlock, Action<int, NSString> errorBlock, Action<RCCSModeType> modeTypeBlock, Action<NSString> quitBlock);

        [Export("stopCustomeService:")]
        void StopCustomeService(string kefuId);

        [Export("switchToHumanMode:")]
        void SwitchToHumanMode(string kefuId);

        [Export("evaluateCustomService:robotValue:suggest:")]
        void EvaluateCustomService(string kefuId, bool isRobotResolved, string suggest);

        [Export("evaluateCustomService:knownledgeId:robotValue:")]
        void EvaluateCustomService(string kefuId, string knownledgeId, bool isRobotResolved);

        [Export("evaluateCustomService:humanValue:suggest:")]
        void EvaluateCustomService(string kefuId, int value, string suggest);

    }
    // @interface RCGroup : NSObject <NSCoding>
    [BaseType(typeof(NSObject))]
    interface RCGroup : INSCoding
    {
        // @property (nonatomic, strong) NSString * groupId;
        [Export("groupId", ArgumentSemantic.Strong)]
        string GroupId { get; set; }
        // @property (nonatomic, strong) NSString * groupName;
        [Export("groupName", ArgumentSemantic.Strong)]
        string GroupName { get; set; }
        // @property (nonatomic, strong) NSString * portraitUri;
        [Export("portraitUri", ArgumentSemantic.Strong)]
        string PortraitUri { get; set; }
        // -(instancetype)initWithGroupId:(NSString *)groupId groupName:(NSString *)groupName portraitUri:(NSString *)portraitUri;
        [Export("initWithGroupId:groupName:portraitUri:")]
        IntPtr Constructor(string groupId, string groupName, string portraitUri);

    }
    // @interface RCUserTypingStatus : NSObject
    [BaseType(typeof(NSObject))]
    interface RCUserTypingStatus
    {
        // @property (nonatomic, strong) NSString * userId;
        [Export("userId", ArgumentSemantic.Strong)]
        string UserId { get; set; }
        // @property (nonatomic, strong) NSString * contentType;
        [Export("contentType", ArgumentSemantic.Strong)]
        string ContentType { get; set; }
        // -(instancetype)initWithUserId:(NSString *)userId contentType:(NSString *)objectName;
        [Export("initWithUserId:contentType:")]
        IntPtr Constructor(string userId, string objectName);

    }
    // @interface RCTextMessage : RCMessageContent <NSCoding>
    [BaseType(typeof(RCMessageContent))]
    interface RCTextMessage : INSCoding
    {
        // @property (nonatomic, strong) NSString * content;
        [Export("content", ArgumentSemantic.Strong)]
        string Content { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)messageWithContent:(NSString *)content;
        [Static]
        [Export("messageWithContent:")]
        RCTextMessage MessageWithContent(string content);

    }
    // @interface RCImageMessage : RCMessageContent <NSCoding>
    [BaseType(typeof(RCMessageContent))]
    interface RCImageMessage : INSCoding
    {
        // @property (nonatomic, strong) NSString * imageUrl;
        [Export("imageUrl", ArgumentSemantic.Strong)]
        string ImageUrl { get; set; }
        // @property (nonatomic, strong) UIImage * thumbnailImage;
        [Export("thumbnailImage", ArgumentSemantic.Strong)]
        UIImage ThumbnailImage { get; set; }
        // @property (getter = isFull, nonatomic) BOOL full;
        [Export("full")]
        bool Full { [Bind ("isFull")] get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // @property (nonatomic, strong) UIImage * originalImage;
        [Export("originalImage", ArgumentSemantic.Strong)]
        UIImage OriginalImage { get; set; }
        // +(instancetype)messageWithImage:(UIImage *)image;
        [Static]
        [Export("messageWithImage:")]
        RCImageMessage MessageWithImage(UIImage image);
        // +(instancetype)messageWithImageURI:(NSString *)imageURI;
        [Static]
        [Export("messageWithImageURI:")]
        RCImageMessage MessageWithImageURI(string imageURI);

    }
    // @interface RCVoiceMessage : RCMessageContent <NSCoding>
    [BaseType(typeof(RCMessageContent))]
    interface RCVoiceMessage : INSCoding
    {
        // @property (nonatomic, strong) NSData * wavAudioData;
        [Export("wavAudioData", ArgumentSemantic.Strong)]
        NSData WavAudioData { get; set; }
        // @property (assign, nonatomic) long duration;
        [Export("duration")]
        nint Duration { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)messageWithAudio:(NSData *)audioData duration:(long)duration;
        [Static]
        [Export("messageWithAudio:duration:")]
        RCVoiceMessage MessageWithAudio(NSData audioData, nint duration);

    }
    // @interface RCLocationMessage : RCMessageContent <NSCoding>
    [BaseType(typeof(RCMessageContent))]
    interface RCLocationMessage : INSCoding
    {
        // @property (assign, nonatomic) CLLocationCoordinate2D location;
        [Export("location", ArgumentSemantic.Assign)]
        CLLocationCoordinate2D Location { get; set; }
        // @property (nonatomic, strong) NSString * locationName;
        [Export("locationName", ArgumentSemantic.Strong)]
        string LocationName { get; set; }
        // @property (nonatomic, strong) UIImage * thumbnailImage;
        [Export("thumbnailImage", ArgumentSemantic.Strong)]
        UIImage ThumbnailImage { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)messageWithLocationImage:(UIImage *)image location:(CLLocationCoordinate2D)location locationName:(NSString *)locationName;
        [Static]
        [Export("messageWithLocationImage:location:locationName:")]
        RCLocationMessage MessageWithLocationImage(UIImage image, CLLocationCoordinate2D location, string locationName);

    }
    // @interface RCRichContentMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCRichContentMessage
    {
        // @property (nonatomic, strong) NSString * title;
        [Export("title", ArgumentSemantic.Strong)]
        string Title { get; set; }
        // @property (nonatomic, strong) NSString * digest;
        [Export("digest", ArgumentSemantic.Strong)]
        string Digest { get; set; }
        // @property (nonatomic, strong) NSString * imageURL;
        [Export("imageURL", ArgumentSemantic.Strong)]
        string ImageURL { get; set; }
        // @property (nonatomic, strong) NSString * url;
        [Export("url", ArgumentSemantic.Strong)]
        string Url { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)messageWithTitle:(NSString *)title digest:(NSString *)digest imageURL:(NSString *)imageURL extra:(NSString *)extra;
        [Static]
        [Export("messageWithTitle:digest:imageURL:extra:")]
        RCRichContentMessage MessageWithTitle(string title, string digest, string imageURL, string extra);
        // +(instancetype)messageWithTitle:(NSString *)title digest:(NSString *)digest imageURL:(NSString *)imageURL url:(NSString *)url extra:(NSString *)extra;
        [Static]
        [Export("messageWithTitle:digest:imageURL:url:extra:")]
        RCRichContentMessage MessageWithTitle(string title, string digest, string imageURL, string url, string extra);

    }
    // @interface RCRealTimeLocationStartMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCRealTimeLocationStartMessage
    {
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)messageWithExtra:(NSString *)extra;
        [Static]
        [Export("messageWithExtra:")]
        RCRealTimeLocationStartMessage MessageWithExtra(string extra);

    }
    // @interface RCRealTimeLocationEndMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCRealTimeLocationEndMessage
    {
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)messageWithExtra:(NSString *)extra;
        [Static]
        [Export("messageWithExtra:")]
        RCRealTimeLocationEndMessage MessageWithExtra(string extra);

    }
    // @interface RCCommandMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCCommandMessage
    {
        // @property (nonatomic, strong) NSString * name;
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }
        // @property (nonatomic, strong) NSString * data;
        [Export("data", ArgumentSemantic.Strong)]
        string Data { get; set; }
        // +(instancetype)messageWithName:(NSString *)name data:(NSString *)data;
        [Static]
        [Export("messageWithName:data:")]
        RCCommandMessage MessageWithName(string name, string data);

    }
    // @interface RCCommandNotificationMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCCommandNotificationMessage
    {
        // @property (nonatomic, strong) NSString * name;
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }
        // @property (nonatomic, strong) NSString * data;
        [Export("data", ArgumentSemantic.Strong)]
        string Data { get; set; }
        // +(instancetype)notificationWithName:(NSString *)name data:(NSString *)data;
        [Static]
        [Export("notificationWithName:data:")]
        RCCommandNotificationMessage NotificationWithName(string name, string data);

    }
    // @interface RCInformationNotificationMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCInformationNotificationMessage
    {
        // @property (nonatomic, strong) NSString * message;
        [Export("message", ArgumentSemantic.Strong)]
        string Message { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)notificationWithMessage:(NSString *)message extra:(NSString *)extra;
        [Static]
        [Export("notificationWithMessage:extra:")]
        RCInformationNotificationMessage NotificationWithMessage(string message, string extra);

    }
    // @interface RCUnknownMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCUnknownMessage
    {

    }
    // @interface RCProfileNotificationMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCProfileNotificationMessage
    {
        // @property (nonatomic, strong) NSString * operation;
        [Export("operation", ArgumentSemantic.Strong)]
        string Operation { get; set; }
        // @property (nonatomic, strong) NSString * data;
        [Export("data", ArgumentSemantic.Strong)]
        string Data { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)notificationWithOperation:(NSString *)operation data:(NSString *)data extra:(NSString *)extra;
        [Static]
        [Export("notificationWithOperation:data:extra:")]
        RCProfileNotificationMessage NotificationWithOperation(string operation, string data, string extra);

    }
    // @interface RCPublicServiceCommandMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCPublicServiceCommandMessage
    {
        // @property (nonatomic, strong) NSString * command;
        [Export("command", ArgumentSemantic.Strong)]
        string Command { get; set; }
        // @property (nonatomic, strong) NSString * data;
        [Export("data", ArgumentSemantic.Strong)]
        string Data { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)messageFromMenuItem:(RCPublicServiceMenuItem *)item;
        [Static]
        [Export("messageFromMenuItem:")]
        RCPublicServiceCommandMessage MessageFromMenuItem(RCPublicServiceMenuItem item);
        // +(instancetype)messageWithCommand:(NSString *)command data:(NSString *)data;
        [Static]
        [Export("messageWithCommand:data:")]
        RCPublicServiceCommandMessage MessageWithCommand(string command, string data);

    }
    // @interface RCPublicServiceMultiRichContentMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCPublicServiceMultiRichContentMessage
    {
        // @property (nonatomic, strong) NSArray * richConents;
        [Export("richConents", ArgumentSemantic.Strong)] 
        NSObject[] RichConents { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }

    }
    // @interface RCRichContentItem : NSObject
    [BaseType(typeof(NSObject))]
    interface RCRichContentItem
    {
        // @property (nonatomic, strong) NSString * title;
        [Export("title", ArgumentSemantic.Strong)]
        string Title { get; set; }
        // @property (nonatomic, strong) NSString * digest;
        [Export("digest", ArgumentSemantic.Strong)]
        string Digest { get; set; }
        // @property (nonatomic, strong) NSString * imageURL;
        [Export("imageURL", ArgumentSemantic.Strong)]
        string ImageURL { get; set; }
        // @property (nonatomic, strong) NSString * url;
        [Export("url", ArgumentSemantic.Strong)]
        string Url { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)messageWithTitle:(NSString *)title digest:(NSString *)digest imageURL:(NSString *)imageURL extra:(NSString *)extra;
        [Static]
        [Export("messageWithTitle:digest:imageURL:extra:")]
        RCRichContentItem MessageWithTitle(string title, string digest, string imageURL, string extra);
        // +(instancetype)messageWithTitle:(NSString *)title digest:(NSString *)digest imageURL:(NSString *)imageURL url:(NSString *)url extra:(NSString *)extra;
        [Static]
        [Export("messageWithTitle:digest:imageURL:url:extra:")]
        RCRichContentItem MessageWithTitle(string title, string digest, string imageURL, string url, string extra);

    }
    // @interface RCPublicServiceRichContentMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCPublicServiceRichContentMessage
    {
        // @property (nonatomic, strong) RCRichContentItem * richConent;
        [Export("richConent", ArgumentSemantic.Strong)]
        RCRichContentItem RichConent { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }

    }
    // @interface RCDiscussionNotificationMessage : RCMessageContent <NSCoding>
    [BaseType(typeof(RCMessageContent))]
    interface RCDiscussionNotificationMessage : INSCoding
    {
        // @property (assign, nonatomic) RCDiscussionNotificationType type;
        [Export("type", ArgumentSemantic.Assign)]
        RCDiscussionNotificationType Type { get; set; }
        // @property (nonatomic, strong) NSString * operatorId;
        [Export("operatorId", ArgumentSemantic.Strong)]
        string OperatorId { get; set; }
        // @property (nonatomic, strong) NSString * extension;
        [Export("extension", ArgumentSemantic.Strong)]
        string Extension { get; set; }
        // +(instancetype)notificationWithType:(RCDiscussionNotificationType)type operator:(NSString *)operatorId extension:(NSString *)extension;
        [Static]
        [Export("notificationWithType:operator:extension:")]
        RCDiscussionNotificationMessage NotificationWithType(RCDiscussionNotificationType type, string operatorId, string extension);

    }
    // @interface RCGroupNotificationMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCGroupNotificationMessage
    {
        // @property (nonatomic, strong) NSString * operation;
        [Export("operation", ArgumentSemantic.Strong)]
        string Operation { get; set; }
        // @property (nonatomic, strong) NSString * operatorUserId;
        [Export("operatorUserId", ArgumentSemantic.Strong)]
        string OperatorUserId { get; set; }
        // @property (nonatomic, strong) NSString * data;
        [Export("data", ArgumentSemantic.Strong)]
        string Data { get; set; }
        // @property (nonatomic, strong) NSString * message;
        [Export("message", ArgumentSemantic.Strong)]
        string Message { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)notificationWithOperation:(NSString *)operation operatorUserId:(NSString *)operatorUserId data:(NSString *)data message:(NSString *)message extra:(NSString *)extra;
        [Static]
        [Export("notificationWithOperation:operatorUserId:data:message:extra:")]
        RCGroupNotificationMessage NotificationWithOperation(string operation, string operatorUserId, string data, string message, string extra);

    }
    // @interface RCContactNotificationMessage : RCMessageContent <NSCoding>
    [BaseType(typeof(RCMessageContent))]
    interface RCContactNotificationMessage : INSCoding
    {
        // @property (nonatomic, strong) NSString * operation;
        [Export("operation", ArgumentSemantic.Strong)]
        string Operation { get; set; }
        // @property (nonatomic, strong) NSString * sourceUserId;
        [Export("sourceUserId", ArgumentSemantic.Strong)]
        string SourceUserId { get; set; }
        // @property (nonatomic, strong) NSString * targetUserId;
        [Export("targetUserId", ArgumentSemantic.Strong)]
        string TargetUserId { get; set; }
        // @property (nonatomic, strong) NSString * message;
        [Export("message", ArgumentSemantic.Strong)]
        string Message { get; set; }
        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }
        // +(instancetype)notificationWithOperation:(NSString *)operation sourceUserId:(NSString *)sourceUserId targetUserId:(NSString *)targetUserId message:(NSString *)message extra:(NSString *)extra;
        [Static]
        [Export("notificationWithOperation:sourceUserId:targetUserId:message:extra:")]
        RCContactNotificationMessage NotificationWithOperation(string operation, string sourceUserId, string targetUserId, string message, string extra);

    }
    // @interface RCUtilities : NSObject
    [BaseType(typeof(NSObject))]
    interface RCUtilities
    {
        // +(NSData *)dataWithBase64EncodedString:(NSString *)string;
        [Static]
        [Export("dataWithBase64EncodedString:")]
        NSData DataWithBase64EncodedString(string @string);
        // +(NSString *)base64EncodedStringFrom:(NSData *)data;
        [Static]
        [Export("base64EncodedStringFrom:")]
        string Base64EncodedStringFrom(NSData data);
        // +(UIImage *)scaleImage:(UIImage *)image toScale:(float)scaleSize;
        [Static]
        [Export("scaleImage:toScale:")]
        UIImage ScaleImage(UIImage image, float scaleSize);
        // +(UIImage *)imageByScalingAndCropSize:(UIImage *)image targetSize:(CGSize)targetSize;
        [Static]
        [Export("imageByScalingAndCropSize:targetSize:")]
        UIImage ImageByScalingAndCropSize(UIImage image, CGSize targetSize);
        // +(NSData *)compressedImageWithMaxDataLength:(UIImage *)image maxDataLength:(CGFloat)maxDataLength;
        [Static]
        [Export("compressedImageWithMaxDataLength:maxDataLength:")]
        NSData CompressedImageWithMaxDataLength(UIImage image, nfloat maxDataLength);
        // +(NSData *)compressedImageAndScalingSize:(UIImage *)image targetSize:(CGSize)targetSize maxDataLen:(CGFloat)maxDataLen;
        [Static]
        [Export("compressedImageAndScalingSize:targetSize:maxDataLen:")]
        NSData CompressedImageAndScalingSizeMaxDataLen(UIImage image, CGSize targetSize, nfloat maxDataLen);
        // +(NSData *)compressedImageAndScalingSize:(UIImage *)image targetSize:(CGSize)targetSize percent:(CGFloat)percent;
        [Static]
        [Export("compressedImageAndScalingSize:targetSize:percent:")]
        NSData CompressedImageAndScalingSizePercent(UIImage image, CGSize targetSize, nfloat percent);
        // +(NSData *)compressedImage:(UIImage *)image percent:(CGFloat)percent;
        [Static]
        [Export("compressedImage:percent:")]
        NSData CompressedImage(UIImage image, nfloat percent);
        // +(BOOL)excludeBackupKeyForURL:(NSURL *)storageURL;
        [Static]
        [Export("excludeBackupKeyForURL:")]
        bool ExcludeBackupKeyForURL(NSUrl storageURL);
        // +(NSString *)applicationDocumentsDirectory;
        [Static]
        [Export("applicationDocumentsDirectory")]  
        string ApplicationDocumentsDirectory { get; }
        // +(NSString *)rongDocumentsDirectory;
        [Static]
        [Export("rongDocumentsDirectory")] 
        string RongDocumentsDirectory { get; }
        // +(NSString *)rongImageCacheDirectory;
        [Static]
        [Export("rongImageCacheDirectory")] 
        string RongImageCacheDirectory { get; }
        // +(NSString *)currentSystemTime;
        [Static]
        [Export("currentSystemTime")] 
        string CurrentSystemTime { get; }
        // +(NSString *)currentCarrier;
        [Static]
        [Export("currentCarrier")] 
        string CurrentCarrier { get; }
        // +(NSString *)currentNetWork;
        [Static]
        [Export("currentNetWork")] 
        string CurrentNetWork { get; }
        // +(NSString *)currentSystemVersion;
        [Static]
        [Export("currentSystemVersion")] 
        string CurrentSystemVersion { get; }
        // +(NSString *)currentDeviceModel;
        [Static]
        [Export("currentDeviceModel")] 
        string CurrentDeviceModel { get; }

    }
    // @interface RCAMRDataConverter : NSObject
    [BaseType(typeof(NSObject))]
    interface RCAMRDataConverter
    {
        // +(RCAMRDataConverter *)sharedAMRDataConverter;
        [Static]
        [Export("sharedAMRDataConverter")] 
        RCAMRDataConverter SharedAMRDataConverter { get; }
        // -(NSData *)decodeAMRToWAVE:(NSData *)data;
        [Export("decodeAMRToWAVE:")]
        NSData DecodeAMRToWAVE(NSData data);
        // -(NSData *)encodeWAVEToAMR:(NSData *)data channel:(int)nChannels nBitsPerSample:(int)nBitsPerSample;
        [Export("encodeWAVEToAMR:channel:nBitsPerSample:")]
        NSData EncodeWAVEToAMR(NSData data, int nChannels, int nBitsPerSample);

    }
    // @interface RCChatRoomMemberInfo : NSObject
    [BaseType(typeof(NSObject))]
    interface RCChatRoomMemberInfo
    {
        // @property (nonatomic, strong) NSString * userId;
        [Export("userId", ArgumentSemantic.Strong)]
        string UserId { get; set; }
        // @property (assign, nonatomic) long long joinTime;
        [Export("joinTime")]
        long JoinTime { get; set; }

    }
    // @protocol RCRealTimeLocationObserver <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCRealTimeLocationObserver
    {
        // @optional -(void)onRealTimeLocationStatusChange:(RCRealTimeLocationStatus)status;
        [Export("onRealTimeLocationStatusChange:")]
        void OnRealTimeLocationStatusChange(RCRealTimeLocationStatus status);
        // @optional -(void)onReceiveLocation:(CLLocation *)location fromUserId:(NSString *)userId;
        [Export("onReceiveLocation:fromUserId:")]
        void OnReceiveLocation(CLLocation location, string userId);
        // @optional -(void)onParticipantsJoin:(NSString *)userId;
        [Export("onParticipantsJoin:")]
        void OnParticipantsJoin(string userId);
        // @optional -(void)onParticipantsQuit:(NSString *)userId;
        [Export("onParticipantsQuit:")]
        void OnParticipantsQuit(string userId);
        // @optional -(void)onUpdateLocationFailed:(NSString *)description;
        [Export("onUpdateLocationFailed:")]
        void OnUpdateLocationFailed(string description);
        // @optional -(void)onStartRealTimeLocationFailed:(long)messageId;
        [Export("onStartRealTimeLocationFailed:")]
        void OnStartRealTimeLocationFailed(nint messageId);

    }
    // @protocol RCRealTimeLocationProxy <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCRealTimeLocationProxy
    {
        // @required -(void)startRealTimeLocation;
        [Abstract]
        [Export("startRealTimeLocation")]
        void StartRealTimeLocation();
        // @required -(void)joinRealTimeLocation;
        [Abstract]
        [Export("joinRealTimeLocation")]
        void JoinRealTimeLocation();
        // @required -(void)quitRealTimeLocation;
        [Abstract]
        [Export("quitRealTimeLocation")]
        void QuitRealTimeLocation();
        // @required -(void)addRealTimeLocationObserver:(id<RCRealTimeLocationObserver>)delegate;
        [Abstract]
        [Export("addRealTimeLocationObserver:")]
        void AddRealTimeLocationObserver(RCRealTimeLocationObserver @delegate);
        // @required -(void)removeRealTimeLocationObserver:(id<RCRealTimeLocationObserver>)delegate;
        [Abstract]
        [Export("removeRealTimeLocationObserver:")]
        void RemoveRealTimeLocationObserver(RCRealTimeLocationObserver @delegate);
        // @required -(NSArray *)getParticipants;
        [Abstract]
        [Export("getParticipants")] 
        NSObject[] Participants { get; }
        // @required -(RCRealTimeLocationStatus)getStatus;
        [Abstract]
        [Export("getStatus")] 
        RCRealTimeLocationStatus Status { get; }
        // @required -(CLLocation *)getLocation:(NSString *)userId;
        [Abstract]
        [Export("getLocation:")]
        CLLocation GetLocation(string userId);

    }
    // @interface RCRealTimeLocationManager : NSObject
    [BaseType(typeof(NSObject))]
    interface RCRealTimeLocationManager
    {
        // +(instancetype)sharedManager;
        [Static]
        [Export("sharedManager")]
        RCRealTimeLocationManager SharedManager();
        // -(void)getRealTimeLocationProxy:(RCConversationType)conversationType targetId:(NSString *)targetId success:(void (^)(id<RCRealTimeLocationProxy>))successBlock error:(void (^)(RCRealTimeLocationErrorCode))errorBlock;
        [Export("getRealTimeLocationProxy:targetId:success:error:")]
        void GetRealTimeLocationProxy(RCConversationType conversationType, string targetId, Action<RCRealTimeLocationProxy> successBlock, Action<RCRealTimeLocationErrorCode> errorBlock);

    }
    // @protocol RCIMUserInfoDataSource <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCIMUserInfoDataSource
    {
        [Abstract]
        [Export("getUserInfoWithUserId:completion:")]
        void Completion(string userId, Action<RCUserInfo> completion);

    }
    // @protocol RCIMGroupInfoDataSource <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCIMGroupInfoDataSource
    {
        [Abstract]
        [Export("getGroupInfoWithGroupId:completion:")]
        void Completion(string groupId, Action<RCGroup> completion);

    }
    // @protocol RCIMGroupUserInfoDataSource <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCIMGroupUserInfoDataSource
    {
        [Abstract]
        [Export("getUserInfoWithUserId:inGroup:completion:")]
        void InGroup(string userId, string groupId, Action<RCUserInfo> completion);

    }
    // @protocol RCIMReceiveMessageDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCIMReceiveMessageDelegate
    {
        [Abstract]
        [Export("onRCIMReceiveMessage:left:")]
        void OnRCIMReceiveMessage(RCMessage message, int left);

        [Export("onRCIMCustomLocalNotification:withSenderName:")]
        bool OnRCIMCustomLocalNotification(RCMessage message, string senderName);

        [Export("onRCIMCustomAlertSound:")]
        bool OnRCIMCustomAlertSound(RCMessage message);

    }
    // @protocol RCIMConnectionStatusDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCIMConnectionStatusDelegate
    {
        [Abstract]
        [Export("onRCIMConnectionStatusChanged:")]
        void OnRCIMConnectionStatusChanged(RCConnectionStatus status);

    }
    // @interface RCIM : NSObject
    [BaseType(typeof(NSObject))]
    interface RCIM
    {
        [Static]
        [Export("sharedRCIM")]
        RCIM SharedRCIM{ get; }

        [Export("initWithAppKey:")]
        void InitWithAppKey(string appKey);

        [Export("connectWithToken:success:error:tokenIncorrect:")]
        void ConnectWithToken(string token, Action<NSString> successBlock, Action<RCConnectErrorCode> errorBlock, Action tokenIncorrectBlock);

        [Export("disconnect:")]
        void Disconnect(bool isReceivePush);

        [Export("disconnect")]
        void Disconnect();

        [Export("logout")]
        void Logout();

        [Wrap("WeakConnectionStatusDelegate")]
        [NullAllowed]
        RCIMConnectionStatusDelegate ConnectionStatusDelegate { get; set; }

        [NullAllowed, Export("connectionStatusDelegate", ArgumentSemantic.Weak)]
        NSObject WeakConnectionStatusDelegate { get; set; }

        [Export("getConnectionStatus")] 
        RCConnectionStatus ConnectionStatus { get; }

        [Export("registerMessageType:")]
        void RegisterMessageType(Class messageClass);

        [Export("sendMessage:targetId:content:pushContent:pushData:success:error:")]
        RCMessage SendMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, string pushData, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        [Export("sendImageMessage:targetId:content:pushContent:pushData:progress:success:error:")]
        RCMessage SendImageMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, string pushData, Action<int, nint> progressBlock, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        [Export("startVoIPCallWithTargetId:")]
        void StartVoIPCallWithTargetId(string targetId);

        [Wrap("WeakReceiveMessageDelegate")]
        [NullAllowed]
        RCIMReceiveMessageDelegate ReceiveMessageDelegate { get; set; }

        [NullAllowed, Export("receiveMessageDelegate", ArgumentSemantic.Weak)]
        NSObject WeakReceiveMessageDelegate { get; set; }

        [Export("disableMessageNotificaiton")]
        bool DisableMessageNotificaiton { get; set; }

        [Export("disableMessageAlertSound")]
        bool DisableMessageAlertSound { get; set; }

        [Export("enableTypingStatus")]
        bool EnableTypingStatus { get; set; }

        [Export("enableReadReceipt")]
        bool EnableReadReceipt { get; set; }

        [Export("showUnkownMessage")]
        bool ShowUnkownMessage { get; set; }

        [Export("showUnkownMessageNotificaiton")]
        bool ShowUnkownMessageNotificaiton { get; set; }

        [Export("currentUserInfo", ArgumentSemantic.Strong)]
        RCUserInfo CurrentUserInfo { get; set; }

        [NullAllowed, Export("userInfoDataSource", ArgumentSemantic.Weak)]
        RCIMUserInfoDataSource UserInfoDataSource { get; set; }

        [NullAllowed, Export("groupInfoDataSource", ArgumentSemantic.Weak)]
        RCIMGroupInfoDataSource GroupInfoDataSource { get; set; }

        [NullAllowed, Export("groupUserInfoDataSource", ArgumentSemantic.Weak)]
        RCIMGroupUserInfoDataSource GroupUserInfoDataSource { get; set; }

        [Export("enableMessageAttachUserInfo")]
        bool EnableMessageAttachUserInfo { get; set; }

        [Export("maxVoiceDuration")]
        nuint MaxVoiceDuration { get; set; }

        [Export("refreshUserInfoCache:withUserId:")]
        void RefreshUserInfoCache(RCUserInfo userInfo, string userId);

        [Export("refreshGroupInfoCache:withGroupId:")]
        void RefreshGroupInfoCache(RCGroup groupInfo, string groupId);

        [Export("refreshGroupUserInfoCache:withUserId:withGroupId:")]
        void RefreshGroupUserInfoCache(RCUserInfo userInfo, string userId, string groupId);

        [Export("clearUserInfoCache")]
        void ClearUserInfoCache();

        [Export("clearGroupInfoCache")]
        void ClearGroupInfoCache();

        [Export("globalNavigationBarTintColor", ArgumentSemantic.Assign)]
        UIColor GlobalNavigationBarTintColor { get; set; }

        [Export("globalConversationAvatarStyle", ArgumentSemantic.Assign)]
        RCUserAvatarStyle GlobalConversationAvatarStyle { get; set; }

        [Export("globalConversationPortraitSize", ArgumentSemantic.Assign)]
        CGSize GlobalConversationPortraitSize { get; set; }

        [Export("globalMessageAvatarStyle", ArgumentSemantic.Assign)]
        RCUserAvatarStyle GlobalMessageAvatarStyle { get; set; }

        [Export("globalMessagePortraitSize", ArgumentSemantic.Assign)]
        CGSize GlobalMessagePortraitSize { get; set; }

        [Export("portraitImageViewCornerRadius")]
        nfloat PortraitImageViewCornerRadius { get; set; }

    }
    // @interface RCConversationModel : NSObject
    [BaseType(typeof(NSObject))]
    interface RCConversationModel
    {
        [Export("conversationModelType", ArgumentSemantic.Assign)]
        RCConversationModelType ConversationModelType { get; set; }

        [Export("extend", ArgumentSemantic.Strong)]
        NSObject Extend { get; set; }

        [Export("conversationType", ArgumentSemantic.Assign)]
        RCConversationType ConversationType { get; set; }

        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        [Export("conversationTitle", ArgumentSemantic.Strong)]
        string ConversationTitle { get; set; }

        [Export("unreadMessageCount")]
        nint UnreadMessageCount { get; set; }

        [Export("isTop")]
        bool IsTop { get; set; }

        [Export("receivedStatus", ArgumentSemantic.Assign)]
        RCReceivedStatus ReceivedStatus { get; set; }

        [Export("sentStatus", ArgumentSemantic.Assign)]
        RCSentStatus SentStatus { get; set; }

        [Export("receivedTime")]
        long ReceivedTime { get; set; }

        [Export("sentTime")]
        long SentTime { get; set; }

        [Export("draft", ArgumentSemantic.Strong)]
        string Draft { get; set; }

        [Export("objectName", ArgumentSemantic.Strong)]
        string ObjectName { get; set; }

        [Export("senderUserId", ArgumentSemantic.Strong)]
        string SenderUserId { get; set; }

        [Export("senderUserName", ArgumentSemantic.Strong)]
        string SenderUserName { get; set; }

        [Export("lastestMessageId")]
        nint LastestMessageId { get; set; }

        [Export("lastestMessage", ArgumentSemantic.Strong)]
        RCMessageContent LastestMessage { get; set; }

        [Export("jsonDict", ArgumentSemantic.Strong)]
        NSDictionary JsonDict { get; set; }

        [Export("init:exntend:")]
        IntPtr Constructor(RCConversationModelType conversationModelType, NSObject extend);

        [Export("init:conversation:extend:")]
        IntPtr Constructor(RCConversationModelType conversationModelType, RCConversation conversation, NSObject extend);

    }
    // @interface RCBaseViewController : UIViewController
    [BaseType(typeof(UIViewController))]
    interface RCBaseViewController
    {

    }
    // @interface RCConversationBaseCell : UITableViewCell
    [BaseType(typeof(UITableViewCell))]
    interface RCConversationBaseCell
    {
        [Export("model", ArgumentSemantic.Strong)]
        RCConversationModel Model { get; set; }

        [Export("setDataModel:")]
        void SetDataModel(RCConversationModel model);

    }
    // @interface RCConversationListViewController : RCBaseViewController <UITableViewDataSource, UITableViewDelegate>
    [BaseType(typeof(RCBaseViewController))]
    interface RCConversationListViewController : IUITableViewDataSource, IUITableViewDelegate
    {
        [Export("initWithDisplayConversationTypes:collectionConversationType:")] 
        IntPtr Constructor(NSObject[] displayConversationTypeArray, NSObject[] collectionConversationTypeArray);

        [Export("displayConversationTypeArray", ArgumentSemantic.Strong)] 
        NSObject[] DisplayConversationTypeArray { get; set; }

        [Export("collectionConversationTypeArray", ArgumentSemantic.Strong)] 
        NSObject[] CollectionConversationTypeArray { get; set; }

        [Export("setDisplayConversationTypes:")] 
        void SetDisplayConversationTypes(NSObject[] conversationTypeArray);

        [Export("setCollectionConversationType:")] 
        void SetCollectionConversationType(NSObject[] conversationTypeArray);

        [Export("isEnteredToCollectionViewController")]
        bool IsEnteredToCollectionViewController { get; set; }

        [Export("conversationListDataSource", ArgumentSemantic.Strong)]
        NSMutableArray ConversationListDataSource { get; set; }

        [Export("conversationListTableView", ArgumentSemantic.Strong)]
        UITableView ConversationListTableView { get; set; }

        [Export("isShowNetworkIndicatorView")]
        bool IsShowNetworkIndicatorView { get; set; }

        [Export("showConnectingStatusOnNavigatorBar")]
        bool ShowConnectingStatusOnNavigatorBar { get; set; }

        [Export("emptyConversationView", ArgumentSemantic.Strong)]
        UIView EmptyConversationView { get; set; }

        [Export("cellBackgroundColor", ArgumentSemantic.Assign)]
        UIColor CellBackgroundColor { get; set; }

        [Export("topCellBackgroundColor", ArgumentSemantic.Assign)]
        UIColor TopCellBackgroundColor { get; set; }

        [Export("setConversationAvatarStyle:")]
        void SetConversationAvatarStyle(RCUserAvatarStyle avatarStyle);

        [Export("setConversationPortraitSize:")]
        void SetConversationPortraitSize(CGSize size);

        [Export("onSelectedTableRow:conversationModel:atIndexPath:")]
        void OnSelectedTableRow(RCConversationModelType conversationModelType, RCConversationModel model, NSIndexPath indexPath);

        [Export("didTapCellPortrait:")]
        void DidTapCellPortrait(RCConversationModel model);

        [Export("didLongPressCellPortrait:")]
        void DidLongPressCellPortrait(RCConversationModel model);

        [Export("didDeleteConversationCell:")]
        void DidDeleteConversationCell(RCConversationModel model);

        [Export("willReloadTableData:")]
        NSMutableArray WillReloadTableData(NSMutableArray dataSource);

        [Export("willDisplayConversationTableCell:atIndexPath:")]
        void WillDisplayConversationTableCell(RCConversationBaseCell cell, NSIndexPath indexPath);

        [Export("rcConversationListTableView:cellForRowAtIndexPath:")]
        RCConversationBaseCell  CellForRowAtIndexPath(UITableView tableView, NSIndexPath indexPath);

        [Export("rcConversationListTableView:heightForRowAtIndexPath:")]
        nfloat HeightForRowAtIndexPath(UITableView tableView, NSIndexPath indexPath);

        [Export("rcConversationListTableView:commitEditingStyle:forRowAtIndexPath:")]
        void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath);

        [Export("refreshConversationTableViewIfNeeded")]
        void RefreshConversationTableViewIfNeeded();

        [Export("refreshConversationTableViewWithConversationModel:")]
        void RefreshConversationTableViewWithConversationModel(RCConversationModel conversationModel);

        [Export("didReceiveMessageNotification:")]
        void DidReceiveMessageNotification(NSNotification notification);

        [Export("notifyUpdateUnreadMessageCount")]
        void NotifyUpdateUnreadMessageCount();

        [Export("resetConversationListBackgroundViewIfNeeded")]
        void ResetConversationListBackgroundViewIfNeeded();

        [Export("networkIndicatorView", ArgumentSemantic.Strong)]
        NSObject NetworkIndicatorView { get; set; }

        [Export("updateConnectionStatusOnNavigatorBar")]
        void UpdateConnectionStatusOnNavigatorBar();

        [Export("setNavigationItemTitleView")]
        void SetNavigationItemTitleView();

    }
    // @interface RCPublicServiceListViewController : UITableViewController
    [BaseType(typeof(UITableViewController))]
    interface RCPublicServiceListViewController
    {

    }
    // @interface RCPluginBoardView : UICollectionView
    [BaseType(typeof(UICollectionView))]
    interface RCPluginBoardView
    {
        [Export("allItems", ArgumentSemantic.Strong)]
        NSMutableArray AllItems { get; set; }

        [Wrap("WeakPluginBoardDelegate")]
        [NullAllowed]
        RCPluginBoardViewDelegate PluginBoardDelegate { get; set; }

        [NullAllowed, Export("pluginBoardDelegate", ArgumentSemantic.Weak)]
        NSObject WeakPluginBoardDelegate { get; set; }

        [Export("insertItemWithImage:title:atIndex:tag:")]
        void InsertItemWithImage(UIImage image, string title, nint index, nint tag);

        [Export("insertItemWithImage:title:tag:")]
        void InsertItemWithImage(UIImage image, string title, nint tag);

        [Export("updateItemAtIndex:image:title:")]
        void UpdateItemAtIndex(nint index, UIImage image, string title);

        [Export("updateItemWithTag:image:title:")]
        void UpdateItemWithTag(nint tag, UIImage image, string title);

        [Export("removeItemAtIndex:")]
        void RemoveItemAtIndex(nint index);

        [Export("removeItemWithTag:")]
        void RemoveItemWithTag(nint tag);

        [Export("removeAllItems")]
        void RemoveAllItems();

    }
    // @protocol RCPluginBoardViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCPluginBoardViewDelegate
    {
        [Abstract]
        [Export("pluginBoardView:clickedItemWithTag:")]
        void ClickedItemWithTag(RCPluginBoardView pluginBoardView, nint tag);

    }



    // @interface RCTextView : UITextView
    [BaseType(typeof(UITextView))]
    interface RCTextView
    {
        [Export("disableActionMenu")]
        bool DisableActionMenu { get; set; }

    }



    // @interface RCChatSessionInputBarControl : UIView
    [BaseType(typeof(UIView))]
    interface RCChatSessionInputBarControl
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        RCChatSessionInputBarControlDelegate Delegate { get; set; }

        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        [NullAllowed, Export("clientView", ArgumentSemantic.Weak)]
        UIView ClientView { get; set; }

        [Export("pubSwitchButton", ArgumentSemantic.Strong)]
        UIButton PubSwitchButton { get; set; }

        [Export("robotSwitchButton", ArgumentSemantic.Strong)]
        UIButton RobotSwitchButton { get; set; }

        [Export("inputContainerView", ArgumentSemantic.Strong)]
        UIView InputContainerView { get; set; }

        [Export("menuContainerView", ArgumentSemantic.Strong)]
        UIView MenuContainerView { get; set; }

        [Export("switchButton", ArgumentSemantic.Strong)]
        UIButton SwitchButton { get; set; }

        [Export("recordButton", ArgumentSemantic.Strong)]
        UIButton RecordButton { get; set; }

        [Export("inputTextView", ArgumentSemantic.Strong)]
        RCTextView InputTextView { get; set; }

        [Export("emojiButton", ArgumentSemantic.Strong)]
        UIButton EmojiButton { get; set; }

        [Export("additionalButton", ArgumentSemantic.Strong)]
        UIButton AdditionalButton { get; set; }

        [Export("contextView", ArgumentSemantic.Assign)]
        UIView ContextView { get; }

        [Export("currentPositionY")]
        float CurrentPositionY { get; set; }

        [Export("originalPositionY")]
        float OriginalPositionY { get; set; }

        [Export("inputTextview_height")]
        float InputTextview_height { get; set; }

        [Export("publicServiceMenu", ArgumentSemantic.Strong)]
        RCPublicServiceMenu PublicServiceMenu { get; set; }

        [Export("initWithFrame:withContextView:type:style:")]
        IntPtr Constructor(CGRect frame, UIView contextView, RCChatSessionInputBarControlType type, RCChatSessionInputBarControlStyle style);

        [Export("setInputBarType:style:")]
        void SetInputBarType(RCChatSessionInputBarControlType type, RCChatSessionInputBarControlStyle style);

        [Export("dismissPublicServiceMenuPopupView")]
        void DismissPublicServiceMenuPopupView();

    }



    // @protocol RCChatSessionInputBarControlDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCChatSessionInputBarControlDelegate
    {
        [Export("keyboardWillShowWithFrame:")]
        void KeyboardWillShowWithFrame(CGRect keyboardFrame);

        [Export("keyboardWillHide")]
        void KeyboardWillHide();

        [Export("chatSessionInputBarControlContentSizeChanged:")]
        void ChatSessionInputBarControlContentSizeChanged(CGRect frame);

        [Export("didTouchKeyboardReturnKey:text:")]
        void DidTouchKeyboardReturnKey(RCChatSessionInputBarControl inputControl, string text);

        [Export("didTouchEmojiButton:")]
        void DidTouchEmojiButton(UIButton sender);

        [Export("didTouchAddtionalButton:")]
        void DidTouchAddtionalButton(UIButton sender);

        [Export("didTouchSwitchButton:")]
        void DidTouchSwitchButton(bool switched);

        [Export("didTouchPubSwitchButton:")]
        void DidTouchPubSwitchButton(bool switched);

        [Export("didTouchRobotSwitchButton:")]
        void DidTouchRobotSwitchButton(bool switched);

        [Export("didTouchRecordButon:event:")]
        void DidTouchRecordButon(UIButton sender, UIControlEvent e);

        [Export("inputTextView:shouldChangeTextInRange:replacementText:")]
        void InputTextView(UITextView inputTextView, NSRange range, string text);

        [Export("onPublicServiceMenuItemSelected:")]
        void OnPublicServiceMenuItemSelected(RCPublicServiceMenuItem selectedMenuItem);

    }



    // @protocol RCEmojiViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCEmojiViewDelegate
    {
        [Export("didTouchEmojiView:touchedEmoji:")]
        void DidTouchEmojiView(RCEmojiBoardView emojiView, string @string);

        [Export("didSendButtonEvent:sendButton:")]
        void DidSendButtonEvent(RCEmojiBoardView emojiView, UIButton sendButton);

    }



    // @interface RCEmojiBoardView : UIView <UIScrollViewDelegate>
    [BaseType(typeof(UIView))]
    interface RCEmojiBoardView : IUIScrollViewDelegate
    {
        [Export("emojiBackgroundView", ArgumentSemantic.Strong)]
        UIScrollView EmojiBackgroundView { get; set; }

        [Export("emojiLabel", ArgumentSemantic.Strong)]
        UILabel EmojiLabel { get; set; }

        [Wrap("WeakDelegate")]
        RCEmojiViewDelegate Delegate { get; set; }

        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        [Export("loadLabelView")]
        void LoadLabelView();

    }



    // @interface RCMessageModel : NSObject
    [BaseType(typeof(NSObject))]
    interface RCMessageModel
    {
        [Export("isDisplayMessageTime")]
        bool IsDisplayMessageTime { get; set; }

        [Export("isDisplayNickname")]
        bool IsDisplayNickname { get; set; }

        [Export("userInfo", ArgumentSemantic.Strong)]
        RCUserInfo UserInfo { get; set; }

        [Export("conversationType", ArgumentSemantic.Assign)]
        RCConversationType ConversationType { get; set; }

        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        [Export("messageId")]
        nint MessageId { get; set; }

        [Export("messageDirection", ArgumentSemantic.Assign)]
        RCMessageDirection MessageDirection { get; set; }

        [Export("senderUserId", ArgumentSemantic.Strong)]
        string SenderUserId { get; set; }

        [Export("receivedStatus", ArgumentSemantic.Assign)]
        RCReceivedStatus ReceivedStatus { get; set; }

        [Export("sentStatus", ArgumentSemantic.Assign)]
        RCSentStatus SentStatus { get; set; }

        [Export("receivedTime")]
        long ReceivedTime { get; set; }

        [Export("sentTime")]
        long SentTime { get; set; }

        [Export("objectName", ArgumentSemantic.Strong)]
        string ObjectName { get; set; }

        [Export("content", ArgumentSemantic.Strong)]
        RCMessageContent Content { get; set; }

        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }

        [Export("cellSize", ArgumentSemantic.Assign)]
        CGSize CellSize { get; set; }

        [Static]
        [Export("modelWithMessage:")]
        RCMessageModel ModelWithMessage(RCMessage rcMessage);

        [Export("initWithMessage:")]
        IntPtr Constructor(RCMessage rcMessage);

    }




    // @interface RCMessageCellNotificationModel : NSObject
    [BaseType(typeof(NSObject))]
    interface RCMessageCellNotificationModel
    {
        [Export("messageId")]
        nint MessageId { get; set; }

        [Export("actionName", ArgumentSemantic.Strong)]
        string ActionName { get; set; }

        [Export("progress")]
        nint Progress { get; set; }

    }



    // @protocol RCMessageCellDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCMessageCellDelegate
    {
        [Export("didTapMessageCell:")]
        void DidTapMessageCell(RCMessageModel model);

        [Export("didTapUrlInMessageCell:model:")]
        void DidTapUrlInMessageCell(string url, RCMessageModel model);

        [Export("didTapPhoneNumberInMessageCell:model:")]
        void DidTapPhoneNumberInMessageCell(string phoneNumber, RCMessageModel model);

        [Export("didTapCellPortrait:")]
        void DidTapCellPortrait(string userId);

        [Export("didLongPressCellPortrait:")]
        void DidLongPressCellPortrait(string userId);

        [Export("didLongTouchMessageCell:inView:")]
        void DidLongTouchMessageCell(RCMessageModel model, UIView view);

        [Export("didTapmessageFailedStatusViewForResend:")]
        void DidTapmessageFailedStatusViewForResend(RCMessageModel model);

        [Export("didTapCustomService:RobotResoluved:")]
        void DidTapCustomService(RCMessageModel model, bool isResolved);

    }



    // @protocol RCPublicServiceMessageCellDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCPublicServiceMessageCellDelegate
    {
        [Export("didTapPublicServiceMessageCell:")]
        void DidTapPublicServiceMessageCell(RCMessageModel model);

        [Export("didTapUrlInPublicServiceMessageCell:model:")]
        void DidTapUrlInPublicServiceMessageCell(string url, RCMessageModel model);

        [Export("didTapPhoneNumberInPublicServiceMessageCell:model:")]
        void DidTapPhoneNumberInPublicServiceMessageCell(string phoneNumber, RCMessageModel model);

        [Export("didLongTouchPublicServiceMessageCell:inView:")]
        void DidLongTouchPublicServiceMessageCell(RCMessageModel model, UIView view);

        [Export("didTapPublicServiceMessageFailedStatusViewForResend:")]
        void DidTapPublicServiceMessageFailedStatusViewForResend(RCMessageModel model);

    }



    // @interface RCAttributedLabelClickedTextInfo : NSObject
    [BaseType(typeof(NSObject))]
    interface RCAttributedLabelClickedTextInfo
    {
        [Export("textCheckingType", ArgumentSemantic.Assign)]
        NSTextCheckingType TextCheckingType { get; set; }

        [Export("text", ArgumentSemantic.Strong)]
        string Text { get; set; }

    }



    // @protocol RCAttributedDataSource <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCAttributedDataSource
    {
        [Abstract]
        [Export("attributeDictionaryForTextType:")]
        NSDictionary AttributeDictionaryForTextType(ulong textType);

        [Abstract]
        [Export("highlightedAttributeDictionaryForTextType:")]
        NSDictionary HighlightedAttributeDictionaryForTextType(NSTextCheckingType textType);

    }





    // @interface RCAttributedLabel : UILabel <RCAttributedDataSource, UIGestureRecognizerDelegate>
    [BaseType(typeof(UILabel))]
    interface RCAttributedLabel : RCAttributedDataSource, IUIGestureRecognizerDelegate
    {
        [Export("attributeDataSource", ArgumentSemantic.Strong)]
        RCAttributedDataSource AttributeDataSource { get; set; }

        [Export("attributedStrings", ArgumentSemantic.Strong)]
        NSMutableArray AttributedStrings { get; set; }

        [Wrap("WeakDelegate")]
        RCAttributedLabelDelegate Delegate { get; set; }

        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        [Export("attributeDictionary", ArgumentSemantic.Strong)]
        NSDictionary AttributeDictionary { get; set; }

        [Export("highlightedAttributeDictionary", ArgumentSemantic.Strong)]
        NSDictionary HighlightedAttributeDictionary { get; set; }

        [Export("textCheckingTypes")]
        ulong TextCheckingTypes { get; set; }

        [Export("currentTextCheckingType", ArgumentSemantic.Assign)]
        NSTextCheckingType CurrentTextCheckingType { get; }

        [Export("setText:dataDetectorEnabled:")]
        void SetText(string text, bool dataDetectorEnabled);

        [Export("textInfoAtPoint:")]
        RCAttributedLabelClickedTextInfo TextInfoAtPoint(CGPoint point);

        [Export("setTextHighlighted:atPoint:")]
        void SetTextHighlighted(bool highlighted, CGPoint point);

    }



    // @protocol RCAttributedLabelDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCAttributedLabelDelegate
    {
        [Export("attributedLabel:didSelectLinkWithURL:")]
        void DidSelectLinkWithURL(RCAttributedLabel label, NSUrl url);

        [Export("attributedLabel:didSelectLinkWithPhoneNumber:")]
        void DidSelectLinkWithPhoneNumber(RCAttributedLabel label, string phoneNumber);

        [Export("attributedLabel:didTapLabel:")]
        void DidTapLabel(RCAttributedLabel label, string content);

    }



    // @interface RCTipLabel : RCAttributedLabel
    [BaseType(typeof(RCAttributedLabel))]
    interface RCTipLabel
    {
        [Export("marginInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets MarginInsets { get; set; }

        [Static]
        [Export("greyTipLabel")]
        RCTipLabel GreyTipLabel();

    }





    // @interface RCMessageBaseCell : UICollectionViewCell
    [BaseType(typeof(UICollectionViewCell))]
    interface RCMessageBaseCell
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        RCMessageCellDelegate Delegate { get; set; }

        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        [Export("messageTimeLabel", ArgumentSemantic.Strong)]
        RCTipLabel MessageTimeLabel { get; set; }

        [Export("model", ArgumentSemantic.Strong)]
        RCMessageModel Model { get; set; }

        [Export("baseContentView", ArgumentSemantic.Strong)]
        UIView BaseContentView { get; set; }

        [Export("messageDirection", ArgumentSemantic.Assign)]
        RCMessageDirection MessageDirection { get; set; }

        [Export("isDisplayMessageTime")]
        bool IsDisplayMessageTime { get; }

        [Export("isDisplayReadStatus")]
        bool IsDisplayReadStatus { get; set; }

        [Export("initWithFrame:")]
        IntPtr Constructor(CGRect frame);

        [Export("setDataModel:")]
        void SetDataModel(RCMessageModel model);

        [Export("messageCellUpdateSendingStatusEvent:")]
        void MessageCellUpdateSendingStatusEvent(NSNotification notification);

    }



    // @interface RCConversationViewController : RCBaseViewController <UICollectionViewDelegate, UICollectionViewDataSource, UICollectionViewDelegateFlowLayout, UIGestureRecognizerDelegate, UIScrollViewDelegate>
    [BaseType(typeof(RCBaseViewController))]
    interface RCConversationViewController : IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout, IUIGestureRecognizerDelegate, IUIScrollViewDelegate
    {
        [Export("initWithConversationType:targetId:")]
        IntPtr Constructor(RCConversationType conversationType, string targetId);

        [Export("conversationType", ArgumentSemantic.Assign)]
        RCConversationType ConversationType { get; set; }

        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        [Export("userName", ArgumentSemantic.Strong)]
        string UserName { get; set; }

        [Export("conversationDataRepository", ArgumentSemantic.Strong)]
        NSMutableArray ConversationDataRepository { get; set; }

        [Export("conversationMessageCollectionView", ArgumentSemantic.Strong)]
        UICollectionView ConversationMessageCollectionView { get; set; }

        [Export("customFlowLayout", ArgumentSemantic.Strong)]
        UICollectionViewFlowLayout CustomFlowLayout { get; set; }

        [Export("displayConversationTypeArray", ArgumentSemantic.Strong)] 
        NSObject[] DisplayConversationTypeArray { get; set; }

        [Export("notifyUpdateUnreadMessageCount")]
        void NotifyUpdateUnreadMessageCount();

        [Export("enableUnreadMessageIcon")]
        bool EnableUnreadMessageIcon { get; set; }

        [Export("unReadMessage")]
        nint UnReadMessage { get; set; }

        [Export("unReadMessageLabel", ArgumentSemantic.Strong)]
        UILabel UnReadMessageLabel { get; set; }

        [Export("unReadButton", ArgumentSemantic.Strong)]
        UIButton UnReadButton { get; set; }

        [Export("enableNewComingMessageIcon")]
        bool EnableNewComingMessageIcon { get; set; }

        [Export("unReadNewMessageLabel", ArgumentSemantic.Strong)]
        UILabel UnReadNewMessageLabel { get; set; }

        [Export("chatSessionInputBarControl", ArgumentSemantic.Strong)]
        RCChatSessionInputBarControl ChatSessionInputBarControl { get; set; }

        [Export("defaultInputType", ArgumentSemantic.Assign)]
        RCChatSessionInputBarInputType DefaultInputType { get; set; }

        [Export("pluginBoardView", ArgumentSemantic.Strong)]
        RCPluginBoardView PluginBoardView { get; set; }

        [Export("emojiBoardView", ArgumentSemantic.Strong)]
        RCEmojiBoardView EmojiBoardView { get; set; }

        [Export("pluginBoardView:clickedItemWithTag:")]
        void PluginBoardViewClickWithTag(RCPluginBoardView pluginBoardView, nint tag);

        [Export("inputTextView:shouldChangeTextInRange:replacementText:")]
        void InputTextView(UITextView inputTextView, NSRange range, string text);

        [Export("setChatSessionInputBarStatus:animated:")]
        void SetChatSessionInputBarStatus(KBottomBarStatus inputBarStatus, bool animated);

        [Export("setMessageAvatarStyle:")]
        void SetMessageAvatarStyle(RCUserAvatarStyle avatarStyle);

        [Export("setMessagePortraitSize:")]
        void SetMessagePortraitSize(CGSize size);

        [Export("displayUserNameInCell")]
        bool DisplayUserNameInCell { get; set; }

        [Export("defaultHistoryMessageCountOfChatRoom")]
        int DefaultHistoryMessageCountOfChatRoom { get; set; }

        [Export("scrollToBottomAnimated:")]
        void ScrollToBottomAnimated(bool animated);

        [Export("leftBarButtonItemPressed:")]
        void LeftBarButtonItemPressed(NSObject sender);

        [Export("sendMessage:pushContent:")]
        void SendMessage(RCMessageContent messageContent, string pushContent);

        [Export("sendImageMessage:pushContent:")]
        void SendImageMessage(RCImageMessage imageMessage, string pushContent);

        [Export("resendMessage:")]
        void ResendMessage(RCMessageContent messageContent);

        [Export("sendImageMessage:pushContent:appUpload:")]
        void SendImageMessage(RCImageMessage imageMessage, string pushContent, bool appUpload);

        [Export("uploadImage:uploadListener:")]
        void UploadImage(RCMessage message, RCUploadImageStatusListener uploadListener);

        [Export("appendAndDisplayMessage:")]
        void AppendAndDisplayMessage(RCMessage message);

        [Export("deleteMessage:")]
        void DeleteMessage(RCMessageModel model);

        [Export("willSendMessage:")]
        RCMessageContent WillSendMessage(RCMessageContent messageCotent);

        [Export("didSendMessage:content:")]
        void DidSendMessage(nint stauts, RCMessageContent messageCotent);

        [Export("willAppendAndDisplayMessage:")]
        RCMessage WillAppendAndDisplayMessage(RCMessage message);

        [Export("willDisplayMessageCell:atIndexPath:")]
        void WillDisplayMessageCell(RCMessageBaseCell cell, NSIndexPath indexPath);

        [Export("willDisplayConversationTableCell:atIndexPath:")]
        void WillDisplayConversationTableCell(RCMessageBaseCell cell, NSIndexPath indexPath);

        [Export("registerClass:forCellWithReuseIdentifier:")]
        void RegisterClass(Class cellClass, string identifier);

        [Export("rcConversationCollectionView:cellForItemAtIndexPath:")]
        RCMessageBaseCell RcConversationCollectionView(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("rcConversationCollectionView:layout:sizeForItemAtIndexPath:")]
        CGSize RcConversationCollectionView(UICollectionView collectionView, UICollectionViewLayout collectionViewLayout, NSIndexPath indexPath);

        [Export("rcUnkownConversationCollectionView:cellForItemAtIndexPath:")]
        RCMessageBaseCell RcUnkownConversationCollectionView(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("rcUnkownConversationCollectionView:layout:sizeForItemAtIndexPath:")]
        CGSize RcUnkownConversationCollectionView(UICollectionView collectionView, UICollectionViewLayout collectionViewLayout, NSIndexPath indexPath);

        [Export("didTapMessageCell:")]
        void DidTapMessageCell(RCMessageModel model);

        [Export("didLongTouchMessageCell:inView:")]
        void DidLongTouchMessageCell(RCMessageModel model, UIView view);

        [Export("didTapUrlInMessageCell:model:")]
        void DidTapUrlInMessageCell(string url, RCMessageModel model);

        [Export("didTapPhoneNumberInMessageCell:model:")]
        void DidTapPhoneNumberInMessageCell(string phoneNumber, RCMessageModel model);

        [Export("didTapCellPortrait:")]
        void DidTapCellPortrait(string userId);

        [Export("didLongPressCellPortrait:")]
        void DidLongPressCellPortrait(string userId);

        [Export("onBeginRecordEvent")]
        void OnBeginRecordEvent();

        [Export("onEndRecordEvent")]
        void OnEndRecordEvent();

        [Export("enableContinuousReadUnreadVoice")]
        bool EnableContinuousReadUnreadVoice { get; set; }

        [Export("presentImagePreviewController:")]
        void PresentImagePreviewController(RCMessageModel model);

        [Export("enableSaveNewPhotoToLocalSystem")]
        bool EnableSaveNewPhotoToLocalSystem { get; set; }

        [Export("saveNewPhotoToLocalSystemAfterSendingSuccess:")]
        void SaveNewPhotoToLocalSystemAfterSendingSuccess(UIImage newImage);

        [Export("presentLocationViewController:")]
        void PresentLocationViewController(RCLocationMessage locationMessageContent);

        [Export("commentCustomerServiceAndQuit:")]
        void CommentCustomerServiceAndQuit(int serviceStatus);

        [Export("customServiceLeftCurrentViewController")]
        void CustomServiceLeftCurrentViewController();

    }



    // @interface RCPublicServiceSearchViewController : UITableViewController
    [BaseType(typeof(UITableViewController))]
    interface RCPublicServiceSearchViewController
    {

    }



    // @interface RCPublicServiceChatViewController : RCConversationViewController
    [BaseType(typeof(RCConversationViewController))]
    interface RCPublicServiceChatViewController
    {
        [Export("didTapImageTxtMsgCell:webViewController:")]
        void DidTapImageTxtMsgCell(string tapedUrl, UIViewController rcWebViewController);

    }



    // @interface RCImageMessageProgressView : UIView
    [BaseType(typeof(UIView))]
    interface RCImageMessageProgressView
    {
        [Export("label", ArgumentSemantic.Assign)]
        UILabel Label { get; set; }

        [Export("indicatorView", ArgumentSemantic.Strong)]
        UIActivityIndicatorView IndicatorView { get; set; }

        [Export("updateProgress:")]
        void UpdateProgress(nint progress);

        [Export("startAnimating")]
        void StartAnimating();

        [Export("stopAnimating")]
        void StopAnimating();

    }



    // @interface RCImagePreviewController : RCBaseViewController
    [BaseType(typeof(RCBaseViewController))]
    interface RCImagePreviewController
    {
        [Export("originalImageView", ArgumentSemantic.Strong)]
        UIImageView OriginalImageView { get; set; }

        [Export("messageModel", ArgumentSemantic.Strong)]
        RCMessageModel MessageModel { get; set; }

        [Export("rcImageProressView", ArgumentSemantic.Strong)]
        RCImageMessageProgressView RcImageProressView { get; set; }

        [Export("leftBarButtonItemPressed:")]
        void LeftBarButtonItemPressed(NSObject sender);

        [Export("rightBarButtonItemPressed:")]
        void RightBarButtonItemPressed(NSObject sender);

        [Export("imageDownloadDone")]
        void ImageDownloadDone();

    }


    // typedef void (^OnPoiSearchResult)(NSArray *, BOOL, BOOL, NSError *);
    delegate void OnPoiSearchResult(NSObject[] arg0,bool arg1,bool arg2,NSError arg3);


    // @interface RCLocationPickerViewController : RCBaseViewController <CLLocationManagerDelegate, UITableViewDataSource, UITableViewDelegate>
    [BaseType(typeof(RCBaseViewController))]
    interface RCLocationPickerViewController : ICLLocationManagerDelegate, IUITableViewDataSource, IUITableViewDelegate
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        RCLocationPickerViewControllerDelegate Delegate { get; set; }

        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        [Export("dataSource", ArgumentSemantic.Strong)]
        RCLocationPickerViewControllerDataSource DataSource { get; set; }

        [Export("mapViewContainer", ArgumentSemantic.Strong)]
        UIView MapViewContainer { get; set; }

        [Export("initWithDataSource:")]
        IntPtr Constructor(RCLocationPickerViewControllerDataSource dataSource);

        [Export("leftBarButtonItemPressed:")]
        void LeftBarButtonItemPressed(NSObject sender);

        [Export("rightBarButtonItemPressed:")]
        void RightBarButtonItemPressed(NSObject sender);

    }



    // @protocol RCLocationPickerViewControllerDataSource <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCLocationPickerViewControllerDataSource
    {
        [Export("mapView")] 
        UIView MapView { get; }

        [Export("annotationLayer")] 
        CALayer AnnotationLayer { get; }

        [Export("titleOfPlaceMark:")]
        string TitleOfPlaceMark(NSObject placeMark);

        [Export("locationCoordinate2DOfPlaceMark:")]
        CLLocationCoordinate2D LocationCoordinate2DOfPlaceMark(NSObject placeMark);

        [Export("setMapViewCenter:animated:")]
        void SetMapViewCenter(CLLocationCoordinate2D location, bool animated);

        [Export("setMapViewCoordinateRegion:animated:")]
        void SetMapViewCoordinateRegion(MKCoordinateRegion coordinateRegion, bool animated);

        [Export("userSelectPlaceMark:")]
        void UserSelectPlaceMark(NSObject placeMark);

        [Export("mapViewCenter")] 
        CLLocationCoordinate2D MapViewCenter { get; }

        [Export("setOnPoiSearchResult:")]
        void SetOnPoiSearchResult(OnPoiSearchResult poiSearchResult);

        [Export("beginFetchPoisOfCurrentLocation")]
        void BeginFetchPoisOfCurrentLocation();

        [Export("mapViewScreenShot")] 
        UIImage MapViewScreenShot { get; }

    }



    // @protocol RCLocationPickerViewControllerDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCLocationPickerViewControllerDelegate
    {
        [Abstract]
        [Export("locationPicker:didSelectLocation:locationName:mapScreenShot:")]
        void DidSelectLocation(RCLocationPickerViewController locationPicker, CLLocationCoordinate2D location, string locationName, UIImage mapScreenShot);

    }



    // @interface RCMessageBubbleTipView : UIView
    [BaseType(typeof(UIView))]
    interface RCMessageBubbleTipView
    {
        [Export("bubbleTipText")]
        string BubbleTipText { get; set; }

        [Export("bubbleTipAlignment", ArgumentSemantic.Assign)]
        RCMessageBubbleTipViewAlignment BubbleTipAlignment { get; set; }

        [Export("bubbleTipTextColor", ArgumentSemantic.Strong)]
        UIColor BubbleTipTextColor { get; set; }

        [Export("bubbleTipTextShadowOffset", ArgumentSemantic.Assign)]
        CGSize BubbleTipTextShadowOffset { get; set; }

        [Export("bubbleTipTextShadowColor", ArgumentSemantic.Strong)]
        UIColor BubbleTipTextShadowColor { get; set; }

        [Export("bubbleTipTextFont", ArgumentSemantic.Strong)]
        UIFont BubbleTipTextFont { get; set; }

        [Export("bubbleTipBackgroundColor", ArgumentSemantic.Strong)]
        UIColor BubbleTipBackgroundColor { get; set; }

        [Export("bubbleTipOverlayColor", ArgumentSemantic.Strong)]
        UIColor BubbleTipOverlayColor { get; set; }

        [Export("bubbleTipPositionAdjustment", ArgumentSemantic.Assign)]
        CGPoint BubbleTipPositionAdjustment { get; set; }

        [Export("frameToPositionInRelationWith", ArgumentSemantic.Assign)]
        CGRect FrameToPositionInRelationWith { get; set; }

        [Export("isShowNotificationNumber")]
        bool IsShowNotificationNumber { get; set; }

        [Export("initWithParentView:alignment:")]
        IntPtr Constructor(UIView parentView, RCMessageBubbleTipViewAlignment alignment);

        [Export("setBubbleTipNumber:")]
        void SetBubbleTipNumber(int msgCount);

    }



    // @interface RCConversationCell : RCConversationBaseCell
    [BaseType(typeof(RCConversationBaseCell))]
    interface RCConversationCell
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        RCConversationCellDelegate Delegate { get; set; }

        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        [Export("headerImageViewBackgroundView", ArgumentSemantic.Strong)]
        UIView HeaderImageViewBackgroundView { get; set; }

        [Export("headerImageView", ArgumentSemantic.Strong)]
        NSObject HeaderImageView { get; set; }

        [Export("conversationTitle", ArgumentSemantic.Strong)]
        UILabel ConversationTitle { get; set; }

        [Export("messageContentLabel", ArgumentSemantic.Strong)]
        UILabel MessageContentLabel { get; set; }

        [Export("messageCreatedTimeLabel", ArgumentSemantic.Strong)]
        UILabel MessageCreatedTimeLabel { get; set; }

        [Export("bubbleTipView", ArgumentSemantic.Strong)]
        RCMessageBubbleTipView BubbleTipView { get; set; }

        [Export("conversationStatusImageView", ArgumentSemantic.Strong)]
        UIImageView ConversationStatusImageView { get; set; }

        [Export("portraitStyle", ArgumentSemantic.Assign)]
        RCUserAvatarStyle PortraitStyle { get; set; }

        [Export("enableNotification")]
        bool EnableNotification { get; set; }

        [Export("isShowNotificationNumber")]
        bool IsShowNotificationNumber { get; set; }

        [Export("cellBackgroundColor", ArgumentSemantic.Assign)]
        UIColor CellBackgroundColor { get; set; }

        [Export("topCellBackgroundColor", ArgumentSemantic.Assign)]
        UIColor TopCellBackgroundColor { get; set; }

        [Export("lastSendMessageStatusView", ArgumentSemantic.Strong)]
        UIImageView LastSendMessageStatusView { get; set; }

        [Export("setHeaderImagePortraitStyle:")]
        void SetHeaderImagePortraitStyle(RCUserAvatarStyle portraitStyle);

        [Export("setDataModel:")]
        void SetDataModel(RCConversationModel model);

    }



    // @protocol RCConversationCellDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCConversationCellDelegate
    {
        [Abstract]
        [Export("didTapCellPortrait:")]
        void DidTapCellPortrait(RCConversationModel model);

        [Abstract]
        [Export("didLongPressCellPortrait:")]
        void DidLongPressCellPortrait(RCConversationModel model);

    }



    // @interface RCContentView : UIView
    [BaseType(typeof(UIView))]
    interface RCContentView
    {
        [Export("eventBlock", ArgumentSemantic.Copy)]
        Action<CGRect> EventBlock { get; set; }

        [Export("registerFrameChangedEvent:")]
        void RegisterFrameChangedEvent(Action<CGRect> eventBlock);

    }



    // @interface RCMessageCell : RCMessageBaseCell
    [BaseType(typeof(RCMessageBaseCell))]
    interface RCMessageCell
    {
        [Export("portraitImageView", ArgumentSemantic.Strong)]
        NSObject PortraitImageView { get; set; }

        [Export("nicknameLabel", ArgumentSemantic.Strong)]
        UILabel NicknameLabel { get; set; }

        [Export("messageContentView", ArgumentSemantic.Strong)]
        RCContentView MessageContentView { get; set; }

        [Export("statusContentView", ArgumentSemantic.Strong)]
        UIView StatusContentView { get; set; }

        [Export("messageFailedStatusView", ArgumentSemantic.Strong)]
        UIButton MessageFailedStatusView { get; set; }

        [Export("messageActivityIndicatorView", ArgumentSemantic.Strong)]
        UIActivityIndicatorView MessageActivityIndicatorView { get; set; }

        [Export("messageContentViewWidth")]
        nfloat MessageContentViewWidth { get; }

        [Export("portraitStyle", ArgumentSemantic.Assign)]
        RCUserAvatarStyle PortraitStyle { get; [Bind ("setPortraitStyle:")] set; }

        [Export("isDisplayNickname")]
        bool IsDisplayNickname { get; }

        [Export("messageHasReadStatusView", ArgumentSemantic.Strong)]
        UIView MessageHasReadStatusView { get; set; }

        [Export("messageSendSuccessStatusView", ArgumentSemantic.Strong)]
        UIView MessageSendSuccessStatusView { get; set; }

        [Export("setDataModel:")]
        void SetDataModel(RCMessageModel model);

        [Export("updateStatusContentView:")]
        void UpdateStatusContentView(RCMessageModel model);

    }



    // @interface RCTipMessageCell : RCMessageBaseCell
    [BaseType(typeof(RCMessageBaseCell))]
    interface RCTipMessageCell
    {
        [Export("tipMessageLabel", ArgumentSemantic.Strong)]
        RCTipLabel TipMessageLabel { get; set; }

        [Export("setDataModel:")]
        void SetDataModel(RCMessageModel model);

    }



    // @interface RCUnknownMessageCell : RCMessageBaseCell
    [BaseType(typeof(RCMessageBaseCell))]
    interface RCUnknownMessageCell
    {
        [Export("messageLabel", ArgumentSemantic.Strong)]
        RCTipLabel MessageLabel { get; set; }

        [Export("setDataModel:")]
        void SetDataModel(RCMessageModel model);

    }

    // @interface RCVoiceMessageCell : RCMessageCell
    [BaseType(typeof(RCMessageCell))]
    interface RCVoiceMessageCell
    {
        // @property (nonatomic, strong) UIImageView * bubbleBackgroundView;
        [Export("bubbleBackgroundView", ArgumentSemantic.Strong)]
        UIImageView BubbleBackgroundView { get; set; }

        // @property (nonatomic, strong) UIImageView * playVoiceView;
        [Export("playVoiceView", ArgumentSemantic.Strong)]
        UIImageView PlayVoiceView { get; set; }

        // @property (nonatomic, strong) UIImageView * voiceUnreadTagView;
        [Export("voiceUnreadTagView", ArgumentSemantic.Strong)]
        UIImageView VoiceUnreadTagView { get; set; }

        // @property (nonatomic, strong) UILabel * voiceDurationLabel;
        [Export("voiceDurationLabel", ArgumentSemantic.Strong)]
        UILabel VoiceDurationLabel { get; set; }

        // -(void)playVoice;
        [Export("playVoice")]
        void PlayVoice();
    }

    // @interface RCRichContentMessageCell : RCMessageCell
    [BaseType(typeof(RCMessageCell))]
    interface RCRichContentMessageCell
    {
        // @property (nonatomic, strong) UIImageView * bubbleBackgroundView;
        [Export("bubbleBackgroundView", ArgumentSemantic.Strong)]
        UIImageView BubbleBackgroundView { get; set; }

        // @property (nonatomic, strong) RCloudImageView * richContentImageView;
        [Export("richContentImageView", ArgumentSemantic.Strong)]
        NSObject RichContentImageView { get; set; }

        // @property (nonatomic, strong) RCAttributedLabel * digestLabel;
        [Export("digestLabel", ArgumentSemantic.Strong)]
        RCAttributedLabel DigestLabel { get; set; }

        // @property (nonatomic, strong) RCAttributedLabel * titleLabel;
        [Export("titleLabel", ArgumentSemantic.Strong)]
        RCAttributedLabel TitleLabel { get; set; }
    }

    // @interface RCImageMessageCell : RCMessageCell
    [BaseType(typeof(RCMessageCell))]
    interface RCImageMessageCell
    {
        // @property (nonatomic, strong) UIImageView * bubbleBackgroundView;
        [Export("bubbleBackgroundView", ArgumentSemantic.Strong)]
        UIImageView BubbleBackgroundView { get; set; }

        // @property (nonatomic, strong) UIImageView * pictureView;
        [Export("pictureView", ArgumentSemantic.Strong)]
        UIImageView PictureView { get; set; }

        // @property (nonatomic, strong) RCImageMessageProgressView * progressView;
        [Export("progressView", ArgumentSemantic.Strong)]
        RCImageMessageProgressView ProgressView { get; set; }
    }

    // @interface RCLocationMessageCell : RCMessageCell
    [BaseType(typeof(RCMessageCell))]
    interface RCLocationMessageCell
    {
        // @property (nonatomic, strong) UIImageView * bubbleBackgroundView;
        [Export("bubbleBackgroundView", ArgumentSemantic.Strong)]
        UIImageView BubbleBackgroundView { get; set; }

        // @property (nonatomic, strong) UIImageView * pictureView;
        [Export("pictureView", ArgumentSemantic.Strong)]
        UIImageView PictureView { get; set; }

        // @property (nonatomic, strong) UILabel * locationNameLabel;
        [Export("locationNameLabel", ArgumentSemantic.Strong)]
        UILabel LocationNameLabel { get; set; }
    }

    // @interface RCTextMessageCell : RCMessageCell <RCAttributedLabelDelegate>
    [BaseType(typeof(RCMessageCell))]
    interface RCTextMessageCell : RCAttributedLabelDelegate
    {
        // @property (nonatomic, strong) RCAttributedLabel * textLabel;
        [Export("textLabel", ArgumentSemantic.Strong)]
        RCAttributedLabel TextLabel { get; set; }

        // @property (nonatomic, strong) UIImageView * bubbleBackgroundView;
        [Export("bubbleBackgroundView", ArgumentSemantic.Strong)]
        UIImageView BubbleBackgroundView { get; set; }

        // -(void)setDataModel:(RCMessageModel *)model;
        [Export("setDataModel:")]
        void SetDataModel(RCMessageModel model);
    }

    // @interface RCKitUtility : NSObject
    [BaseType(typeof(NSObject))]
    interface RCKitUtility
    {
        // +(NSString *)ConvertMessageTime:(long long)secs;
        [Static]
        [Export("ConvertMessageTime:")]
        string ConvertMessageTime(long secs);

        // +(NSString *)ConvertChatMessageTime:(long long)secs;
        [Static]
        [Export("ConvertChatMessageTime:")]
        string ConvertChatMessageTime(long secs);

        // +(UIImage *)imageNamed:(NSString *)name ofBundle:(NSString *)bundleName;
        [Static]
        [Export("imageNamed:ofBundle:")]
        UIImage ImageNamed(string name, string bundleName);

        // +(UIImage *)createImageWithColor:(UIColor *)color;
        [Static]
        [Export("createImageWithColor:")]
        UIImage CreateImageWithColor(UIColor color);

        // +(NSString *)formatMessage:(RCMessageContent *)messageContent;
        [Static]
        [Export("formatMessage:")]
        string FormatMessage(RCMessageContent messageContent);

        // +(NSString *)localizedDescription:(RCMessageContent *)messageContent;
        [Static]
        [Export("localizedDescription:")]
        string LocalizedDescription(RCMessageContent messageContent);

        // +(NSDictionary *)getNotificationUserInfoDictionary:(RCMessage *)message;
        [Static]
        [Export("getNotificationUserInfoDictionary:")]
        NSDictionary GetNotificationUserInfoDictionary(RCMessage message);

        // +(NSDictionary *)getNotificationUserInfoDictionary:(RCConversationType)conversationType fromUserId:(NSString *)fromUserId targetId:(NSString *)targetId messageContent:(RCMessageContent *)messageContent __attribute__((deprecated("已废弃，请勿使用。")));
        [Static]
        [Export("getNotificationUserInfoDictionary:fromUserId:targetId:messageContent:")]
        NSDictionary GetNotificationUserInfoDictionary(RCConversationType conversationType, string fromUserId, string targetId, RCMessageContent messageContent);

        // +(NSDictionary *)getNotificationUserInfoDictionary:(RCConversationType)conversationType fromUserId:(NSString *)fromUserId targetId:(NSString *)targetId objectName:(NSString *)objectName;
        [Static]
        [Export("getNotificationUserInfoDictionary:fromUserId:targetId:objectName:")]
        NSDictionary GetNotificationUserInfoDictionary(RCConversationType conversationType, string fromUserId, string targetId, string objectName);
    }

    // @interface RCConversationSettingTableViewController : UITableViewController
    [BaseType(typeof(UITableViewController))]
    interface RCConversationSettingTableViewController
    {
        // @property (readonly, nonatomic, strong) NSArray * defaultCells;
        [Export("defaultCells", ArgumentSemantic.Strong)] 
        NSObject[] DefaultCells { get; }

        // @property (assign, nonatomic) BOOL headerHidden;
        [Export("headerHidden")]
        bool HeaderHidden { get; set; }

        // @property (assign, nonatomic) BOOL switch_isTop;
        [Export("switch_isTop")]
        bool Switch_isTop { get; set; }

        // @property (assign, nonatomic) BOOL switch_newMessageNotify;
        [Export("switch_newMessageNotify")]
        bool Switch_newMessageNotify { get; set; }

        // @property (nonatomic, strong) NSArray * users;
        [Export("users", ArgumentSemantic.Strong)] 
        NSObject[] Users { get; set; }

        // -(void)disableDeleteMemberEvent:(BOOL)disable;
        [Export("disableDeleteMemberEvent:")]
        void DisableDeleteMemberEvent(bool disable);

        // -(void)disableInviteMemberEvent:(BOOL)disable;
        [Export("disableInviteMemberEvent:")]
        void DisableInviteMemberEvent(bool disable);

        // -(void)onClickIsTopSwitch:(id)sender;
        [Export("onClickIsTopSwitch:")]
        void OnClickIsTopSwitch(NSObject sender);

        // -(void)onClickNewMessageNotificationSwitch:(id)sender;
        [Export("onClickNewMessageNotificationSwitch:")]
        void OnClickNewMessageNotificationSwitch(NSObject sender);

        // -(void)onClickClearMessageHistory:(id)sender;
        [Export("onClickClearMessageHistory:")]
        void OnClickClearMessageHistory(NSObject sender);

        // -(void)addUsers:(NSArray *)users;
        [Export("addUsers:")] 
        void AddUsers(NSObject[] users);

        // -(void)settingTableViewHeader:(RCConversationSettingTableViewHeader *)settingTableViewHeader indexPathOfSelectedItem:(NSIndexPath *)indexPathOfSelectedItem allTheSeletedUsers:(NSArray *)users;
        [Export("settingTableViewHeader:indexPathOfSelectedItem:allTheSeletedUsers:")] 
        void SettingTableViewHeader(RCConversationSettingTableViewHeader settingTableViewHeader, NSIndexPath indexPathOfSelectedItem, NSObject[] users);

        // -(void)deleteTipButtonClicked:(NSIndexPath *)indexPath;
        [Export("deleteTipButtonClicked:")]
        void DeleteTipButtonClicked(NSIndexPath indexPath);

        // -(void)didTipHeaderClicked:(NSString *)userId;
        [Export("didTipHeaderClicked:")]
        void DidTipHeaderClicked(string userId);
    }

    // typedef void (^clearHistory)(BOOL);
    delegate void clearHistory(bool arg0);

    // @interface RCSettingViewController : RCConversationSettingTableViewController
    [BaseType(typeof(RCConversationSettingTableViewController))]
    interface RCSettingViewController
    {
        // @property (copy, nonatomic) NSString * targetId;
        [Export("targetId")]
        string TargetId { get; set; }

        // @property (assign, nonatomic) RCConversationType conversationType;
        [Export("conversationType", ArgumentSemantic.Assign)]
        RCConversationType ConversationType { get; set; }

        // @property (copy, nonatomic) clearHistory clearHistoryCompletion;
        [Export("clearHistoryCompletion", ArgumentSemantic.Copy)]
        clearHistory ClearHistoryCompletion { get; set; }

        // @property (readonly, nonatomic, strong) NSArray * defaultCells;
        [Export("defaultCells", ArgumentSemantic.Strong)] 
        NSObject[] DefaultCells { get; }

        // @property (readonly, nonatomic, strong) UIActionSheet * clearMsgHistoryActionSheet;
        [Export("clearMsgHistoryActionSheet", ArgumentSemantic.Strong)]
        UIActionSheet ClearMsgHistoryActionSheet { get; }

        // -(void)clearHistoryMessage;
        [Export("clearHistoryMessage")]
        void ClearHistoryMessage();

        // -(void)settingTableViewHeader:(RCConversationSettingTableViewHeader *)settingTableViewHeader indexPathOfSelectedItem:(NSIndexPath *)indexPathOfSelectedItem allTheSeletedUsers:(NSArray *)users;
        [Export("settingTableViewHeader:indexPathOfSelectedItem:allTheSeletedUsers:")] 
        void SettingTableViewHeader(RCConversationSettingTableViewHeader settingTableViewHeader, NSIndexPath indexPathOfSelectedItem, NSObject[] users);

        // -(void)deleteTipButtonClicked:(NSIndexPath *)indexPath;
        [Export("deleteTipButtonClicked:")]
        void DeleteTipButtonClicked(NSIndexPath indexPath);
    }

    // @protocol RCConversationSettingTableViewHeaderDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCConversationSettingTableViewHeaderDelegate
    {
        // @optional -(void)settingTableViewHeader:(RCConversationSettingTableViewHeader *)settingTableViewHeader indexPathOfSelectedItem:(NSIndexPath *)indexPathOfSelectedItem allTheSeletedUsers:(NSArray *)users;
        [Export("settingTableViewHeader:indexPathOfSelectedItem:allTheSeletedUsers:")] 
        void SettingTableViewHeader(RCConversationSettingTableViewHeader settingTableViewHeader, NSIndexPath indexPathOfSelectedItem, NSObject[] users);

        // @optional -(void)deleteTipButtonClicked:(NSIndexPath *)indexPath;
        [Export("deleteTipButtonClicked:")]
        void DeleteTipButtonClicked(NSIndexPath indexPath);

        // @optional -(void)didTipHeaderClicked:(NSString *)userId;
        [Export("didTipHeaderClicked:")]
        void DidTipHeaderClicked(string userId);
    }

    // @interface RCConversationSettingTableViewHeader : UICollectionView <UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout>
    [BaseType(typeof(UICollectionView))]
    interface RCConversationSettingTableViewHeader : IUICollectionViewDataSource, IUICollectionViewDelegate, IUICollectionViewDelegateFlowLayout
    {
        // @property (assign, nonatomic) BOOL showDeleteTip;
        [Export("showDeleteTip")]
        bool ShowDeleteTip { get; set; }

        // @property (assign, nonatomic) BOOL isAllowedDeleteMember;
        [Export("isAllowedDeleteMember")]
        bool IsAllowedDeleteMember { get; set; }

        // @property (assign, nonatomic) BOOL isAllowedInviteMember;
        [Export("isAllowedInviteMember")]
        bool IsAllowedInviteMember { get; set; }

        [Wrap("WeakSettingTableViewHeaderDelegate")]
        [NullAllowed]
        RCConversationSettingTableViewHeaderDelegate SettingTableViewHeaderDelegate { get; set; }

        // @property (nonatomic, weak) id<RCConversationSettingTableViewHeaderDelegate> _Nullable settingTableViewHeaderDelegate;
        [NullAllowed, Export("settingTableViewHeaderDelegate", ArgumentSemantic.Weak)]
        NSObject WeakSettingTableViewHeaderDelegate { get; set; }

        // @property (nonatomic, strong) NSMutableArray * users;
        [Export("users", ArgumentSemantic.Strong)]
        NSMutableArray Users { get; set; }
    }

    // @protocol RCPublicServiceProfileViewUrlDelegate
    [Protocol, Model]
    interface RCPublicServiceProfileViewUrlDelegate
    {
        // @required -(void)gotoUrl:(NSString *)url;
        [Abstract]
        [Export("gotoUrl:")]
        void GotoUrl(string url);
    }

    // @interface RCPublicServiceProfileViewController : UITableViewController
    [BaseType(typeof(UITableViewController))]
    interface RCPublicServiceProfileViewController
    {
        // @property (nonatomic, strong) RCPublicServiceProfile * serviceProfile;
        [Export("serviceProfile", ArgumentSemantic.Strong)]
        RCPublicServiceProfile ServiceProfile { get; set; }

        // @property (nonatomic) RCUserAvatarStyle portraitStyle;
        [Export("portraitStyle", ArgumentSemantic.Assign)]
        RCUserAvatarStyle PortraitStyle { get; set; }

        // @property (nonatomic) BOOL fromConversation;
        [Export("fromConversation")]
        bool FromConversation { get; set; }
    }
}