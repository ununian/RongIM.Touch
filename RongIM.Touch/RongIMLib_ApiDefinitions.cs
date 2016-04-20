using System;
using CoreGraphics;
using CoreLocation;
using Foundation;
using ObjCRuntime;
 
using UIKit;

namespace RongIM.Touch
{
    // @interface RCAMRDataConverter : NSObject
    [BaseType(typeof(NSObject))]
    interface RCAMRDataConverter
    {
        // +(RCAMRDataConverter *)sharedAMRDataConverter;
        [Static]
        [Export("sharedAMRDataConverter")]
        [Verify(MethodToProperty)]
        RCAMRDataConverter SharedAMRDataConverter { get; }

        // -(NSData *)decodeAMRToWAVE:(NSData *)data;
        [Export("decodeAMRToWAVE:")]
        NSData DecodeAMRToWAVE(NSData data);

        // -(NSData *)encodeWAVEToAMR:(NSData *)data channel:(int)nChannels nBitsPerSample:(int)nBitsPerSample;
        [Export("encodeWAVEToAMR:channel:nBitsPerSample:")]
        NSData EncodeWAVEToAMR(NSData data, int nChannels, int nBitsPerSample);
    }

    // @interface RCChatRoomInfo : NSObject
    [BaseType(typeof(NSObject))]
    interface RCChatRoomInfo
    {
        // @property (nonatomic, strong) NSString * targetId;
        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        // @property (assign, nonatomic) RCChatRoomMemberOrder memberOrder;
        [Export("memberOrder", ArgumentSemantic.Assign)]
        RCChatRoomMemberOrder MemberOrder { get; set; }

        // @property (nonatomic, strong) NSArray * memberInfoArray;
        [Export("memberInfoArray", ArgumentSemantic.Strong)]
        [Verify(StronglyTypedNSArray)]
        NSObject[] MemberInfoArray { get; set; }

        // @property (assign, nonatomic) int totalMemberCount;
        [Export("totalMemberCount")]
        int TotalMemberCount { get; set; }
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

    // @interface RCUserInfo : NSObject <NSCoding>
    [BaseType(typeof(NSObject))]
    interface RCUserInfo : INSCoding
    {
        // @property (nonatomic, strong) NSString * userId;
        [Export("userId", ArgumentSemantic.Strong)]
        string UserId { get; set; }

        // @property (nonatomic, strong) NSString * name;
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }

        // @property (nonatomic, strong) NSString * portraitUri;
        [Export("portraitUri", ArgumentSemantic.Strong)]
        string PortraitUri { get; set; }

        // -(instancetype)initWithUserId:(NSString *)userId name:(NSString *)username portrait:(NSString *)portrait;
        [Export("initWithUserId:name:portrait:")]
        IntPtr Constructor(string userId, string username, string portrait);
    }

    // @protocol RCMessageCoding <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCMessageCoding
    {
        // @required -(NSData *)encode;
        [Abstract]
        [Export("encode")]
        [Verify(MethodToProperty)]
        NSData Encode { get; }

        // @required -(void)decodeWithData:(NSData *)data;
        [Abstract]
        [Export("decodeWithData:")]
        void DecodeWithData(NSData data);

        // @required +(NSString *)getObjectName;
        [Static, Abstract]
        [Export("getObjectName")]
        [Verify(MethodToProperty)]
        string ObjectName { get; }
    }

    // @protocol RCMessagePersistentCompatible <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCMessagePersistentCompatible
    {
        // @required +(RCMessagePersistent)persistentFlag;
        [Static, Abstract]
        [Export("persistentFlag")]
        [Verify(MethodToProperty)]
        RCMessagePersistent PersistentFlag { get; }
    }

    // @protocol RCMessageContentView
    [Protocol, Model]
    interface RCMessageContentView
    {
        // @optional -(NSString *)conversationDigest;
        [Export("conversationDigest")]
        [Verify(MethodToProperty)]
        string ConversationDigest { get; }
    }

    // @interface RCMessageContent : NSObject <RCMessageCoding, RCMessagePersistentCompatible, RCMessageContentView>
    [BaseType(typeof(NSObject))]
    interface RCMessageContent : RCMessageCoding, RCMessagePersistentCompatible, RCMessageContentView
    {
        // @property (nonatomic, strong) RCUserInfo * senderUserInfo;
        [Export("senderUserInfo", ArgumentSemantic.Strong)]
        RCUserInfo SenderUserInfo { get; set; }

        // -(void)decodeUserInfo:(NSDictionary *)dictionary;
        [Export("decodeUserInfo:")]
        void DecodeUserInfo(NSDictionary dictionary);

        // @property (nonatomic, setter = setRawJSONData:, strong) NSData * rawJSONData;
        [Export("rawJSONData", ArgumentSemantic.Strong)]
        NSData RawJSONData { get; [Bind ("setRawJSONData:")] set; }
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

    // @interface RCConversation : NSObject <NSCoding>
    [BaseType(typeof(NSObject))]
    interface RCConversation : INSCoding
    {
        // @property (assign, nonatomic) RCConversationType conversationType;
        [Export("conversationType", ArgumentSemantic.Assign)]
        RCConversationType ConversationType { get; set; }

        // @property (nonatomic, strong) NSString * targetId;
        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        // @property (nonatomic, strong) NSString * conversationTitle;
        [Export("conversationTitle", ArgumentSemantic.Strong)]
        string ConversationTitle { get; set; }

        // @property (assign, nonatomic) int unreadMessageCount;
        [Export("unreadMessageCount")]
        int UnreadMessageCount { get; set; }

        // @property (assign, nonatomic) BOOL isTop;
        [Export("isTop")]
        bool IsTop { get; set; }

        // @property (assign, nonatomic) RCReceivedStatus receivedStatus;
        [Export("receivedStatus", ArgumentSemantic.Assign)]
        RCReceivedStatus ReceivedStatus { get; set; }

        // @property (assign, nonatomic) RCSentStatus sentStatus;
        [Export("sentStatus", ArgumentSemantic.Assign)]
        RCSentStatus SentStatus { get; set; }

        // @property (assign, nonatomic) long long receivedTime;
        [Export("receivedTime")]
        long ReceivedTime { get; set; }

        // @property (assign, nonatomic) long long sentTime;
        [Export("sentTime")]
        long SentTime { get; set; }

        // @property (nonatomic, strong) NSString * draft;
        [Export("draft", ArgumentSemantic.Strong)]
        string Draft { get; set; }

        // @property (nonatomic, strong) NSString * objectName;
        [Export("objectName", ArgumentSemantic.Strong)]
        string ObjectName { get; set; }

        // @property (nonatomic, strong) NSString * senderUserId;
        [Export("senderUserId", ArgumentSemantic.Strong)]
        string SenderUserId { get; set; }

        // @property (nonatomic, strong) NSString * senderUserName __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("senderUserName", ArgumentSemantic.Strong)]
        string SenderUserName { get; set; }

        // @property (assign, nonatomic) long lastestMessageId;
        [Export("lastestMessageId")]
        nint LastestMessageId { get; set; }

        // @property (nonatomic, strong) RCMessageContent * lastestMessage;
        [Export("lastestMessage", ArgumentSemantic.Strong)]
        RCMessageContent LastestMessage { get; set; }

        // @property (nonatomic, strong) NSDictionary * jsonDict;
        [Export("jsonDict", ArgumentSemantic.Strong)]
        NSDictionary JsonDict { get; set; }

        // @property (nonatomic, strong) NSString * lastestMessageUId;
        [Export("lastestMessageUId", ArgumentSemantic.Strong)]
        string LastestMessageUId { get; set; }

        // +(instancetype)conversationWithProperties:(NSDictionary *)json __attribute__((deprecated("已废弃，请勿使用。")));
        [Static]
        [Export("conversationWithProperties:")]
        RCConversation ConversationWithProperties(NSDictionary json);
    }

    // @interface RCCustomServiceConfig : NSObject
    [BaseType(typeof(NSObject))]
    interface RCCustomServiceConfig
    {
        // @property (nonatomic) BOOL isBlack;
        [Export("isBlack")]
        bool IsBlack { get; set; }

        // @property (nonatomic, strong) NSString * companyName;
        [Export("companyName", ArgumentSemantic.Strong)]
        string CompanyName { get; set; }

        // @property (nonatomic, strong) NSString * companyUrl;
        [Export("companyUrl", ArgumentSemantic.Strong)]
        string CompanyUrl { get; set; }
    }

    // @interface RCDiscussion : NSObject
    [BaseType(typeof(NSObject))]
    interface RCDiscussion
    {
        // @property (nonatomic, strong) NSString * discussionId;
        [Export("discussionId", ArgumentSemantic.Strong)]
        string DiscussionId { get; set; }

        // @property (nonatomic, strong) NSString * discussionName;
        [Export("discussionName", ArgumentSemantic.Strong)]
        string DiscussionName { get; set; }

        // @property (nonatomic, strong) NSString * creatorId;
        [Export("creatorId", ArgumentSemantic.Strong)]
        string CreatorId { get; set; }

        // @property (nonatomic, strong) NSArray * memberIdList;
        [Export("memberIdList", ArgumentSemantic.Strong)]
        [Verify(StronglyTypedNSArray)]
        NSObject[] MemberIdList { get; set; }

        // @property (assign, nonatomic) int inviteStatus;
        [Export("inviteStatus")]
        int InviteStatus { get; set; }

        // @property (assign, nonatomic) int conversationType __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("conversationType")]
        int ConversationType { get; set; }

        // @property (assign, nonatomic) int pushMessageNotificationStatus __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("pushMessageNotificationStatus")]
        int PushMessageNotificationStatus { get; set; }

        // -(instancetype)initWithDiscussionId:(NSString *)discussionId discussionName:(NSString *)discussionName creatorId:(NSString *)creatorId conversationType:(int)conversationType memberIdList:(NSArray *)memberIdList inviteStatus:(int)inviteStatus msgNotificationStatus:(int)pushMessageNotificationStatus;
        [Export("initWithDiscussionId:discussionName:creatorId:conversationType:memberIdList:inviteStatus:msgNotificationStatus:")]
        [Verify(StronglyTypedNSArray)]
        IntPtr Constructor(string discussionId, string discussionName, string creatorId, int conversationType, NSObject[] memberIdList, int inviteStatus, int pushMessageNotificationStatus);
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

    // @interface RCMessage : NSObject <NSCopying, NSCoding>
    [BaseType(typeof(NSObject))]
    interface RCMessage : INSCopying, INSCoding
    {
        // @property (assign, nonatomic) RCConversationType conversationType;
        [Export("conversationType", ArgumentSemantic.Assign)]
        RCConversationType ConversationType { get; set; }

        // @property (nonatomic, strong) NSString * targetId;
        [Export("targetId", ArgumentSemantic.Strong)]
        string TargetId { get; set; }

        // @property (assign, nonatomic) long messageId;
        [Export("messageId")]
        nint MessageId { get; set; }

        // @property (assign, nonatomic) RCMessageDirection messageDirection;
        [Export("messageDirection", ArgumentSemantic.Assign)]
        RCMessageDirection MessageDirection { get; set; }

        // @property (nonatomic, strong) NSString * senderUserId;
        [Export("senderUserId", ArgumentSemantic.Strong)]
        string SenderUserId { get; set; }

        // @property (assign, nonatomic) RCReceivedStatus receivedStatus;
        [Export("receivedStatus", ArgumentSemantic.Assign)]
        RCReceivedStatus ReceivedStatus { get; set; }

        // @property (assign, nonatomic) RCSentStatus sentStatus;
        [Export("sentStatus", ArgumentSemantic.Assign)]
        RCSentStatus SentStatus { get; set; }

        // @property (assign, nonatomic) long long receivedTime;
        [Export("receivedTime")]
        long ReceivedTime { get; set; }

        // @property (assign, nonatomic) long long sentTime;
        [Export("sentTime")]
        long SentTime { get; set; }

        // @property (nonatomic, strong) NSString * objectName;
        [Export("objectName", ArgumentSemantic.Strong)]
        string ObjectName { get; set; }

        // @property (nonatomic, strong) RCMessageContent * content;
        [Export("content", ArgumentSemantic.Strong)]
        RCMessageContent Content { get; set; }

        // @property (nonatomic, strong) NSString * extra;
        [Export("extra", ArgumentSemantic.Strong)]
        string Extra { get; set; }

        // @property (nonatomic, strong) NSString * messageUId;
        [Export("messageUId", ArgumentSemantic.Strong)]
        string MessageUId { get; set; }

        // -(instancetype)initWithType:(RCConversationType)conversationType targetId:(NSString *)targetId direction:(RCMessageDirection)messageDirection messageId:(long)messageId content:(RCMessageContent *)content;
        [Export("initWithType:targetId:direction:messageId:content:")]
        IntPtr Constructor(RCConversationType conversationType, string targetId, RCMessageDirection messageDirection, nint messageId, RCMessageContent content);

        // +(instancetype)messageWithJSON:(NSDictionary *)jsonData __attribute__((deprecated("已废弃，请勿使用。")));
        [Static]
        [Export("messageWithJSON:")]
        RCMessage MessageWithJSON(NSDictionary jsonData);
    }

    // @interface RCPublicServiceMenuItem : NSObject
    [BaseType(typeof(NSObject))]
    interface RCPublicServiceMenuItem
    {
        // @property (nonatomic, strong) NSString * id;
        [Export("id", ArgumentSemantic.Strong)]
        string Id { get; set; }

        // @property (nonatomic, strong) NSString * name;
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }

        // @property (nonatomic, strong) NSString * url;
        [Export("url", ArgumentSemantic.Strong)]
        string Url { get; set; }

        // @property (nonatomic) RCPublicServiceMenuItemType type;
        [Export("type", ArgumentSemantic.Assign)]
        RCPublicServiceMenuItemType Type { get; set; }

        // @property (nonatomic, strong) NSArray * subMenuItems;
        [Export("subMenuItems", ArgumentSemantic.Strong)]
        [Verify(StronglyTypedNSArray)]
        NSObject[] SubMenuItems { get; set; }

        // +(NSArray *)menuItemsFromJsonArray:(NSArray *)jsonArray __attribute__((deprecated("已废弃，请勿使用。")));
        [Static]
        [Export("menuItemsFromJsonArray:")]
        [Verify(StronglyTypedNSArray), Verify(StronglyTypedNSArray)]
        NSObject[] MenuItemsFromJsonArray(NSObject[] jsonArray);
    }

    // @interface RCPublicServiceMenu : NSObject
    [BaseType(typeof(NSObject))]
    interface RCPublicServiceMenu
    {
        // @property (nonatomic, strong) NSArray * menuItems;
        [Export("menuItems", ArgumentSemantic.Strong)]
        [Verify(StronglyTypedNSArray)]
        NSObject[] MenuItems { get; set; }

        // -(void)decodeWithJsonDictionaryArray:(NSArray *)jsonDictionary __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("decodeWithJsonDictionaryArray:")]
        [Verify(StronglyTypedNSArray)]
        void DecodeWithJsonDictionaryArray(NSObject[] jsonDictionary);
    }

    // @interface RCPublicServiceProfile : NSObject
    [BaseType(typeof(NSObject))]
    interface RCPublicServiceProfile
    {
        // @property (nonatomic, strong) NSString * name;
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }

        // @property (nonatomic, strong) NSString * introduction;
        [Export("introduction", ArgumentSemantic.Strong)]
        string Introduction { get; set; }

        // @property (nonatomic, strong) NSString * publicServiceId;
        [Export("publicServiceId", ArgumentSemantic.Strong)]
        string PublicServiceId { get; set; }

        // @property (nonatomic, strong) NSString * portraitUrl;
        [Export("portraitUrl", ArgumentSemantic.Strong)]
        string PortraitUrl { get; set; }

        // @property (nonatomic, strong) NSString * owner;
        [Export("owner", ArgumentSemantic.Strong)]
        string Owner { get; set; }

        // @property (nonatomic, strong) NSString * ownerUrl;
        [Export("ownerUrl", ArgumentSemantic.Strong)]
        string OwnerUrl { get; set; }

        // @property (nonatomic, strong) NSString * publicServiceTel;
        [Export("publicServiceTel", ArgumentSemantic.Strong)]
        string PublicServiceTel { get; set; }

        // @property (nonatomic, strong) NSString * histroyMsgUrl;
        [Export("histroyMsgUrl", ArgumentSemantic.Strong)]
        string HistroyMsgUrl { get; set; }

        // @property (nonatomic, strong) CLLocation * location;
        [Export("location", ArgumentSemantic.Strong)]
        CLLocation Location { get; set; }

        // @property (nonatomic, strong) NSString * scope;
        [Export("scope", ArgumentSemantic.Strong)]
        string Scope { get; set; }

        // @property (nonatomic) RCPublicServiceType publicServiceType;
        [Export("publicServiceType", ArgumentSemantic.Assign)]
        RCPublicServiceType PublicServiceType { get; set; }

        // @property (getter = isFollowed, nonatomic) BOOL followed;
        [Export("followed")]
        bool Followed { [Bind ("isFollowed")] get; set; }

        // @property (nonatomic, strong) RCPublicServiceMenu * menu;
        [Export("menu", ArgumentSemantic.Strong)]
        RCPublicServiceMenu Menu { get; set; }

        // @property (getter = isGlobal, nonatomic) BOOL global;
        [Export("global")]
        bool Global { [Bind ("isGlobal")] get; set; }

        // @property (nonatomic, strong) NSDictionary * jsonDict;
        [Export("jsonDict", ArgumentSemantic.Strong)]
        NSDictionary JsonDict { get; set; }

        // -(void)initContent:(NSString *)jsonContent;
        [Export("initContent:")]
        void InitContent(string jsonContent);
    }

    // @protocol RCWatchKitStatusDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCWatchKitStatusDelegate
    {
        // @optional -(void)notifyWatchKitConnectionStatusChanged:(RCConnectionStatus)status;
        [Export("notifyWatchKitConnectionStatusChanged:")]
        void NotifyWatchKitConnectionStatusChanged(RCConnectionStatus status);

        // @optional -(void)notifyWatchKitReceivedMessage:(RCMessage *)receivedMsg;
        [Export("notifyWatchKitReceivedMessage:")]
        void NotifyWatchKitReceivedMessage(RCMessage receivedMsg);

        // @optional -(void)notifyWatchKitSendMessage:(RCMessage *)message;
        [Export("notifyWatchKitSendMessage:")]
        void NotifyWatchKitSendMessage(RCMessage message);

        // @optional -(void)notifyWatchKitSendMessageCompletion:(long)messageId status:(RCErrorCode)status;
        [Export("notifyWatchKitSendMessageCompletion:status:")]
        void NotifyWatchKitSendMessageCompletion(nint messageId, RCErrorCode status);

        // @optional -(void)notifyWatchKitUploadFileProgress:(int)progress messageId:(long)messageId;
        [Export("notifyWatchKitUploadFileProgress:messageId:")]
        void NotifyWatchKitUploadFileProgress(int progress, nint messageId);

        // @optional -(void)notifyWatchKitClearConversations:(NSArray *)conversationTypeList;
        [Export("notifyWatchKitClearConversations:")]
        [Verify(StronglyTypedNSArray)]
        void NotifyWatchKitClearConversations(NSObject[] conversationTypeList);

        // @optional -(void)notifyWatchKitClearMessages:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("notifyWatchKitClearMessages:targetId:")]
        void NotifyWatchKitClearMessages(RCConversationType conversationType, string targetId);

        // @optional -(void)notifyWatchKitDeleteMessages:(NSArray *)messageIds;
        [Export("notifyWatchKitDeleteMessages:")]
        [Verify(StronglyTypedNSArray)]
        void NotifyWatchKitDeleteMessages(NSObject[] messageIds);

        // @optional -(void)notifyWatchKitClearUnReadStatus:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("notifyWatchKitClearUnReadStatus:targetId:")]
        void NotifyWatchKitClearUnReadStatus(RCConversationType conversationType, string targetId);

        // @optional -(void)notifyWatchKitCreateDiscussion:(NSString *)name userIdList:(NSArray *)userIdList;
        [Export("notifyWatchKitCreateDiscussion:userIdList:")]
        [Verify(StronglyTypedNSArray)]
        void NotifyWatchKitCreateDiscussion(string name, NSObject[] userIdList);

        // @optional -(void)notifyWatchKitCreateDiscussionSuccess:(NSString *)discussionId;
        [Export("notifyWatchKitCreateDiscussionSuccess:")]
        void NotifyWatchKitCreateDiscussionSuccess(string discussionId);

        // @optional -(void)notifyWatchKitCreateDiscussionError:(RCErrorCode)errorCode;
        [Export("notifyWatchKitCreateDiscussionError:")]
        void NotifyWatchKitCreateDiscussionError(RCErrorCode errorCode);

        // @optional -(void)notifyWatchKitAddMemberToDiscussion:(NSString *)discussionId userIdList:(NSArray *)userIdList;
        [Export("notifyWatchKitAddMemberToDiscussion:userIdList:")]
        [Verify(StronglyTypedNSArray)]
        void NotifyWatchKitAddMemberToDiscussion(string discussionId, NSObject[] userIdList);

        // @optional -(void)notifyWatchKitRemoveMemberFromDiscussion:(NSString *)discussionId userId:(NSString *)userId;
        [Export("notifyWatchKitRemoveMemberFromDiscussion:userId:")]
        void NotifyWatchKitRemoveMemberFromDiscussion(string discussionId, string userId);

        // @optional -(void)notifyWatchKitQuitDiscussion:(NSString *)discussionId;
        [Export("notifyWatchKitQuitDiscussion:")]
        void NotifyWatchKitQuitDiscussion(string discussionId);

        // @optional -(void)notifyWatchKitDiscussionOperationCompletion:(int)tag status:(RCErrorCode)status;
        [Export("notifyWatchKitDiscussionOperationCompletion:status:")]
        void NotifyWatchKitDiscussionOperationCompletion(int tag, RCErrorCode status);
    }

    // @interface RCUploadImageStatusListener : NSObject
    [BaseType(typeof(NSObject))]
    interface RCUploadImageStatusListener
    {
        // @property (nonatomic, strong) RCMessage * currentMessage;
        [Export("currentMessage", ArgumentSemantic.Strong)]
        RCMessage CurrentMessage { get; set; }

        // @property (nonatomic, strong) void (^updateBlock)(int);
        [Export("updateBlock", ArgumentSemantic.Strong)]
        Action<int> UpdateBlock { get; set; }

        // @property (nonatomic, strong) void (^successBlock)(NSString *);
        [Export("successBlock", ArgumentSemantic.Strong)]
        Action<NSString> SuccessBlock { get; set; }

        // @property (nonatomic, strong) void (^errorBlock)(RCErrorCode);
        [Export("errorBlock", ArgumentSemantic.Strong)]
        Action<RCErrorCode> ErrorBlock { get; set; }

        // -(instancetype)initWithMessage:(RCMessage *)message uploadProgress:(void (^)(int))progressBlock uploadSuccess:(void (^)(NSString *))successBlock uploadError:(void (^)(RCErrorCode))errorBlock;
        [Export("initWithMessage:uploadProgress:uploadSuccess:uploadError:")]
        IntPtr Constructor(RCMessage message, Action<int> progressBlock, Action<NSString> successBlock, Action<RCErrorCode> errorBlock);
    }

    // @protocol RCIMClientReceiveMessageDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCIMClientReceiveMessageDelegate
    {
        // @required -(void)onReceived:(RCMessage *)message left:(int)nLeft object:(id)object;
        [Abstract]
        [Export("onReceived:left:object:")]
        void Left(RCMessage message, int nLeft, NSObject @object);
    }

    // @protocol RCConnectionStatusChangeDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCConnectionStatusChangeDelegate
    {
        // @required -(void)onConnectionStatusChanged:(RCConnectionStatus)status;
        [Abstract]
        [Export("onConnectionStatusChanged:")]
        void OnConnectionStatusChanged(RCConnectionStatus status);
    }

    // @protocol RCTypingStatusDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCTypingStatusDelegate
    {
        // @required -(void)onTypingStatusChanged:(RCConversationType)conversationType targetId:(NSString *)targetId status:(NSArray *)userTypingStatusList;
        [Abstract]
        [Export("onTypingStatusChanged:targetId:status:")]
        [Verify(StronglyTypedNSArray)]
        void TargetId(RCConversationType conversationType, string targetId, NSObject[] userTypingStatusList);
    }

    // @interface RCIMClient : NSObject
    [BaseType(typeof(NSObject))]
    interface RCIMClient
    {
        // +(instancetype)sharedRCIMClient;
        [Static]
        [Export("sharedRCIMClient")]
        RCIMClient SharedRCIMClient{ get; }

        // -(void)init:(NSString *)appKey;
        [Export("init:")]
        void Init(string appKey);

        // -(void)initWithAppKey:(NSString *)appKey;
        [Export("initWithAppKey:")]
        void InitWithAppKey(string appKey);

        // -(void)setDeviceToken:(NSString *)deviceToken;
        [Export("setDeviceToken:")]
        void SetDeviceToken(string deviceToken);

        // -(void)connectWithToken:(NSString *)token success:(void (^)(NSString *))successBlock error:(void (^)(RCConnectErrorCode))errorBlock tokenIncorrect:(void (^)())tokenIncorrectBlock;
        [Export("connectWithToken:success:error:tokenIncorrect:")]
        void ConnectWithToken(string token, Action<NSString> successBlock, Action<RCConnectErrorCode> errorBlock, Action tokenIncorrectBlock);

        // -(void)disconnect:(BOOL)isReceivePush;
        [Export("disconnect:")]
        void Disconnect(bool isReceivePush);

        // -(void)disconnect;
        [Export("disconnect")]
        void Disconnect();

        // -(void)logout;
        [Export("logout")]
        void Logout();

        // -(void)setRCConnectionStatusChangeDelegate:(id<RCConnectionStatusChangeDelegate>)delegate;
        [Export("setRCConnectionStatusChangeDelegate:")]
        void SetRCConnectionStatusChangeDelegate(RCConnectionStatusChangeDelegate @delegate);

        // -(RCConnectionStatus)getConnectionStatus;
        [Export("getConnectionStatus")]
        [Verify(MethodToProperty)]
        RCConnectionStatus ConnectionStatus { get; }

        // -(RCNetworkStatus)getCurrentNetworkStatus;
        [Export("getCurrentNetworkStatus")]
        [Verify(MethodToProperty)]
        RCNetworkStatus CurrentNetworkStatus { get; }

        // @property (readonly, assign, nonatomic) RCSDKRunningMode sdkRunningMode;
        [Export("sdkRunningMode", ArgumentSemantic.Assign)]
        RCSDKRunningMode SdkRunningMode { get; }

        [Wrap("WeakWatchKitStatusDelegate")]
        RCWatchKitStatusDelegate WatchKitStatusDelegate { get; set; }

        // @property (nonatomic, strong) id<RCWatchKitStatusDelegate> watchKitStatusDelegate;
        [NullAllowed, Export("watchKitStatusDelegate", ArgumentSemantic.Strong)]
        NSObject WeakWatchKitStatusDelegate { get; set; }

        // @property (nonatomic, strong) RCUserInfo * currentUserInfo;
        [Export("currentUserInfo", ArgumentSemantic.Strong)]
        RCUserInfo CurrentUserInfo { get; set; }

        // -(void)getUserInfo:(NSString *)userId success:(void (^)(RCUserInfo *))successBlock error:(void (^)(RCErrorCode))errorBlock __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("getUserInfo:success:error:")]
        void GetUserInfo(string userId, Action<RCUserInfo> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)registerMessageType:(Class)messageClass;
        [Export("registerMessageType:")]
        void RegisterMessageType(Class messageClass);

        // -(RCMessage *)sendMessage:(RCConversationType)conversationType targetId:(NSString *)targetId content:(RCMessageContent *)content pushContent:(NSString *)pushContent success:(void (^)(long))successBlock error:(void (^)(RCErrorCode, long))errorBlock;
        [Export("sendMessage:targetId:content:pushContent:success:error:")]
        RCMessage SendMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        // -(RCMessage *)sendMessage:(RCConversationType)conversationType targetId:(NSString *)targetId content:(RCMessageContent *)content pushContent:(NSString *)pushContent pushData:(NSString *)pushData success:(void (^)(long))successBlock error:(void (^)(RCErrorCode, long))errorBlock;
        [Export("sendMessage:targetId:content:pushContent:pushData:success:error:")]
        RCMessage SendMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, string pushData, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        // -(RCMessage *)sendImageMessage:(RCConversationType)conversationType targetId:(NSString *)targetId content:(RCMessageContent *)content pushContent:(NSString *)pushContent progress:(void (^)(int, long))progressBlock success:(void (^)(long))successBlock error:(void (^)(RCErrorCode, long))errorBlock;
        [Export("sendImageMessage:targetId:content:pushContent:progress:success:error:")]
        RCMessage SendImageMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, Action<int, nint> progressBlock, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        // -(RCMessage *)sendImageMessage:(RCConversationType)conversationType targetId:(NSString *)targetId content:(RCMessageContent *)content pushContent:(NSString *)pushContent pushData:(NSString *)pushData progress:(void (^)(int, long))progressBlock success:(void (^)(long))successBlock error:(void (^)(RCErrorCode, long))errorBlock;
        [Export("sendImageMessage:targetId:content:pushContent:pushData:progress:success:error:")]
        RCMessage SendImageMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, string pushData, Action<int, nint> progressBlock, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        // -(RCMessage *)sendImageMessage:(RCConversationType)conversationType targetId:(NSString *)targetId content:(RCMessageContent *)content pushContent:(NSString *)pushContent pushData:(NSString *)pushData uploadPrepare:(void (^)(RCUploadImageStatusListener *))uploadPrepareBlock progress:(void (^)(int, long))progressBlock success:(void (^)(long))successBlock error:(void (^)(RCErrorCode, long))errorBlock;
        [Export("sendImageMessage:targetId:content:pushContent:pushData:uploadPrepare:progress:success:error:")]
        RCMessage SendImageMessage(RCConversationType conversationType, string targetId, RCMessageContent content, string pushContent, string pushData, Action<RCUploadImageStatusListener> uploadPrepareBlock, Action<int, nint> progressBlock, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        // -(RCMessage *)sendStatusMessage:(RCConversationType)conversationType targetId:(NSString *)targetId content:(RCMessageContent *)content success:(void (^)(long))successBlock error:(void (^)(RCErrorCode, long))errorBlock;
        [Export("sendStatusMessage:targetId:content:success:error:")]
        RCMessage SendStatusMessage(RCConversationType conversationType, string targetId, RCMessageContent content, Action<nint> successBlock, Action<RCErrorCode, nint> errorBlock);

        // -(RCMessage *)insertMessage:(RCConversationType)conversationType targetId:(NSString *)targetId senderUserId:(NSString *)senderUserId sendStatus:(RCSentStatus)sendStatus content:(RCMessageContent *)content;
        [Export("insertMessage:targetId:senderUserId:sendStatus:content:")]
        RCMessage InsertMessage(RCConversationType conversationType, string targetId, string senderUserId, RCSentStatus sendStatus, RCMessageContent content);

        // -(void)downloadMediaFile:(RCConversationType)conversationType targetId:(NSString *)targetId mediaType:(RCMediaType)mediaType mediaUrl:(NSString *)mediaUrl progress:(void (^)(int))progressBlock success:(void (^)(NSString *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("downloadMediaFile:targetId:mediaType:mediaUrl:progress:success:error:")]
        void DownloadMediaFile(RCConversationType conversationType, string targetId, RCMediaType mediaType, string mediaUrl, Action<int> progressBlock, Action<NSString> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)setReceiveMessageDelegate:(id<RCIMClientReceiveMessageDelegate>)delegate object:(id)userData;
        [Export("setReceiveMessageDelegate:object:")]
        void SetReceiveMessageDelegate(RCIMClientReceiveMessageDelegate @delegate, NSObject userData);

        // -(void)sendReadReceiptMessage:(RCConversationType)conversationType targetId:(NSString *)targetId time:(long long)timestamp;
        [Export("sendReadReceiptMessage:targetId:time:")]
        void SendReadReceiptMessage(RCConversationType conversationType, string targetId, long timestamp);

        // -(NSArray *)getLatestMessages:(RCConversationType)conversationType targetId:(NSString *)targetId count:(int)count;
        [Export("getLatestMessages:targetId:count:")]
        [Verify(StronglyTypedNSArray)]
        NSObject[] GetLatestMessages(RCConversationType conversationType, string targetId, int count);

        // -(NSArray *)getHistoryMessages:(RCConversationType)conversationType targetId:(NSString *)targetId oldestMessageId:(long)oldestMessageId count:(int)count;
        [Export("getHistoryMessages:targetId:oldestMessageId:count:")]
        [Verify(StronglyTypedNSArray)]
        NSObject[] GetHistoryMessages(RCConversationType conversationType, string targetId, nint oldestMessageId, int count);

        // -(NSArray *)getHistoryMessages:(RCConversationType)conversationType targetId:(NSString *)targetId objectName:(NSString *)objectName oldestMessageId:(long)oldestMessageId count:(int)count;
        [Export("getHistoryMessages:targetId:objectName:oldestMessageId:count:")]
        [Verify(StronglyTypedNSArray)]
        NSObject[] GetHistoryMessages(RCConversationType conversationType, string targetId, string objectName, nint oldestMessageId, int count);

        // -(void)getRemoteHistoryMessages:(RCConversationType)conversationType targetId:(NSString *)targetId recordTime:(long long)recordTime count:(int)count success:(void (^)(NSArray *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("getRemoteHistoryMessages:targetId:recordTime:count:success:error:")]
        void GetRemoteHistoryMessages(RCConversationType conversationType, string targetId, long recordTime, int count, Action<NSArray> successBlock, Action<RCErrorCode> errorBlock);

        // -(long long)getMessageSendTime:(long)messageId;
        [Export("getMessageSendTime:")]
        long GetMessageSendTime(nint messageId);

        // -(RCMessage *)getMessage:(long)messageId;
        [Export("getMessage:")]
        RCMessage GetMessage(nint messageId);

        // -(RCMessage *)getMessageByUId:(NSString *)messageUId;
        [Export("getMessageByUId:")]
        RCMessage GetMessageByUId(string messageUId);

        // -(BOOL)deleteMessages:(NSArray *)messageIds;
        [Export("deleteMessages:")]
        [Verify(StronglyTypedNSArray)]
        bool DeleteMessages(NSObject[] messageIds);

        // -(BOOL)clearMessages:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("clearMessages:targetId:")]
        bool ClearMessages(RCConversationType conversationType, string targetId);

        // -(BOOL)setMessageExtra:(long)messageId value:(NSString *)value;
        [Export("setMessageExtra:value:")]
        bool SetMessageExtra(nint messageId, string value);

        // -(BOOL)setMessageReceivedStatus:(long)messageId receivedStatus:(RCReceivedStatus)receivedStatus;
        [Export("setMessageReceivedStatus:receivedStatus:")]
        bool SetMessageReceivedStatus(nint messageId, RCReceivedStatus receivedStatus);

        // -(BOOL)setMessageSentStatus:(long)messageId sentStatus:(RCSentStatus)sentStatus;
        [Export("setMessageSentStatus:sentStatus:")]
        bool SetMessageSentStatus(nint messageId, RCSentStatus sentStatus);

        // -(NSArray *)getConversationList:(NSArray *)conversationTypeList;
        [Export("getConversationList:")]
        [Verify(StronglyTypedNSArray), Verify(StronglyTypedNSArray)]
        NSObject[] GetConversationList(NSObject[] conversationTypeList);

        // -(RCConversation *)getConversation:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("getConversation:targetId:")]
        RCConversation GetConversation(RCConversationType conversationType, string targetId);

        // -(int)getMessageCount:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("getMessageCount:targetId:")]
        int GetMessageCount(RCConversationType conversationType, string targetId);

        // -(BOOL)clearConversations:(NSArray *)conversationTypeList;
        [Export("clearConversations:")]
        [Verify(StronglyTypedNSArray)]
        bool ClearConversations(NSObject[] conversationTypeList);

        // -(BOOL)removeConversation:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("removeConversation:targetId:")]
        bool RemoveConversation(RCConversationType conversationType, string targetId);

        // -(BOOL)setConversationToTop:(RCConversationType)conversationType targetId:(NSString *)targetId isTop:(BOOL)isTop;
        [Export("setConversationToTop:targetId:isTop:")]
        bool SetConversationToTop(RCConversationType conversationType, string targetId, bool isTop);

        // -(NSString *)getTextMessageDraft:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("getTextMessageDraft:targetId:")]
        string GetTextMessageDraft(RCConversationType conversationType, string targetId);

        // -(BOOL)saveTextMessageDraft:(RCConversationType)conversationType targetId:(NSString *)targetId content:(NSString *)content;
        [Export("saveTextMessageDraft:targetId:content:")]
        bool SaveTextMessageDraft(RCConversationType conversationType, string targetId, string content);

        // -(BOOL)clearTextMessageDraft:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("clearTextMessageDraft:targetId:")]
        bool ClearTextMessageDraft(RCConversationType conversationType, string targetId);

        // -(int)getTotalUnreadCount;
        [Export("getTotalUnreadCount")]
        [Verify(MethodToProperty)]
        int TotalUnreadCount { get; }

        // -(int)getUnreadCount:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("getUnreadCount:targetId:")]
        int GetUnreadCount(RCConversationType conversationType, string targetId);

        // -(int)getUnreadCount:(NSArray *)conversationTypes;
        [Export("getUnreadCount:")]
        [Verify(StronglyTypedNSArray)]
        int GetUnreadCount(NSObject[] conversationTypes);

        // -(BOOL)clearMessagesUnreadStatus:(RCConversationType)conversationType targetId:(NSString *)targetId;
        [Export("clearMessagesUnreadStatus:targetId:")]
        bool ClearMessagesUnreadStatus(RCConversationType conversationType, string targetId);

        // -(void)setConversationNotificationStatus:(RCConversationType)conversationType targetId:(NSString *)targetId isBlocked:(BOOL)isBlocked success:(void (^)(RCConversationNotificationStatus))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("setConversationNotificationStatus:targetId:isBlocked:success:error:")]
        void SetConversationNotificationStatus(RCConversationType conversationType, string targetId, bool isBlocked, Action<RCConversationNotificationStatus> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)getConversationNotificationStatus:(RCConversationType)conversationType targetId:(NSString *)targetId success:(void (^)(RCConversationNotificationStatus))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("getConversationNotificationStatus:targetId:success:error:")]
        void GetConversationNotificationStatus(RCConversationType conversationType, string targetId, Action<RCConversationNotificationStatus> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)setNotificationQuietHours:(NSString *)startTime spanMins:(int)spanMins success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("setNotificationQuietHours:spanMins:success:error:")]
        void SetNotificationQuietHours(string startTime, int spanMins, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)removeNotificationQuietHours:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("removeNotificationQuietHours:error:")]
        void RemoveNotificationQuietHours(Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)getNotificationQuietHours:(void (^)(NSString *, int))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("getNotificationQuietHours:error:")]
        void GetNotificationQuietHours(Action<NSString, int> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)setConversationNotificationQuietHours:(NSString *)startTime spanMins:(int)spanMins success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("setConversationNotificationQuietHours:spanMins:success:error:")]
        void SetConversationNotificationQuietHours(string startTime, int spanMins, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)removeConversationNotificationQuietHours:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("removeConversationNotificationQuietHours:error:")]
        void RemoveConversationNotificationQuietHours(Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)setRCTypingStatusDelegate:(id<RCTypingStatusDelegate>)delegate;
        [Export("setRCTypingStatusDelegate:")]
        void SetRCTypingStatusDelegate(RCTypingStatusDelegate @delegate);

        // -(void)sendTypingStatus:(RCConversationType)conversationType targetId:(NSString *)targetId contentType:(NSString *)objectName;
        [Export("sendTypingStatus:targetId:contentType:")]
        void SendTypingStatus(RCConversationType conversationType, string targetId, string objectName);

        // -(void)addToBlacklist:(NSString *)userId success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("addToBlacklist:success:error:")]
        void AddToBlacklist(string userId, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)removeFromBlacklist:(NSString *)userId success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("removeFromBlacklist:success:error:")]
        void RemoveFromBlacklist(string userId, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)getBlacklistStatus:(NSString *)userId success:(void (^)(int))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("getBlacklistStatus:success:error:")]
        void GetBlacklistStatus(string userId, Action<int> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)getBlacklist:(void (^)(NSArray *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("getBlacklist:error:")]
        void GetBlacklist(Action<NSArray> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)createDiscussion:(NSString *)name userIdList:(NSArray *)userIdList success:(void (^)(RCDiscussion *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("createDiscussion:userIdList:success:error:")]
        [Verify(StronglyTypedNSArray)]
        void CreateDiscussion(string name, NSObject[] userIdList, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)addMemberToDiscussion:(NSString *)discussionId userIdList:(NSArray *)userIdList success:(void (^)(RCDiscussion *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("addMemberToDiscussion:userIdList:success:error:")]
        [Verify(StronglyTypedNSArray)]
        void AddMemberToDiscussion(string discussionId, NSObject[] userIdList, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)removeMemberFromDiscussion:(NSString *)discussionId userId:(NSString *)userId success:(void (^)(RCDiscussion *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("removeMemberFromDiscussion:userId:success:error:")]
        void RemoveMemberFromDiscussion(string discussionId, string userId, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)quitDiscussion:(NSString *)discussionId success:(void (^)(RCDiscussion *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("quitDiscussion:success:error:")]
        void QuitDiscussion(string discussionId, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)getDiscussion:(NSString *)discussionId success:(void (^)(RCDiscussion *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("getDiscussion:success:error:")]
        void GetDiscussion(string discussionId, Action<RCDiscussion> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)setDiscussionName:(NSString *)targetId name:(NSString *)discussionName success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("setDiscussionName:name:success:error:")]
        void SetDiscussionName(string targetId, string discussionName, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)setDiscussionInviteStatus:(NSString *)targetId isOpen:(BOOL)isOpen success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("setDiscussionInviteStatus:isOpen:success:error:")]
        void SetDiscussionInviteStatus(string targetId, bool isOpen, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)syncGroups:(NSArray *)groupList success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("syncGroups:success:error:")]
        [Verify(StronglyTypedNSArray)]
        void SyncGroups(NSObject[] groupList, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)joinGroup:(NSString *)groupId groupName:(NSString *)groupName success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("joinGroup:groupName:success:error:")]
        void JoinGroup(string groupId, string groupName, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)quitGroup:(NSString *)groupId success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock __attribute__((deprecated("已废弃，请勿使用。")));
        [Export("quitGroup:success:error:")]
        void QuitGroup(string groupId, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)joinChatRoom:(NSString *)targetId messageCount:(int)messageCount success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("joinChatRoom:messageCount:success:error:")]
        void JoinChatRoom(string targetId, int messageCount, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)joinExistChatRoom:(NSString *)targetId messageCount:(int)messageCount success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("joinExistChatRoom:messageCount:success:error:")]
        void JoinExistChatRoom(string targetId, int messageCount, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)quitChatRoom:(NSString *)targetId success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("quitChatRoom:success:error:")]
        void QuitChatRoom(string targetId, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)getChatRoomInfo:(NSString *)targetId count:(int)count order:(RCChatRoomMemberOrder)order success:(void (^)(RCChatRoomInfo *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("getChatRoomInfo:count:order:success:error:")]
        void GetChatRoomInfo(string targetId, int count, RCChatRoomMemberOrder order, Action<RCChatRoomInfo> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)searchPublicService:(RCSearchType)searchType searchKey:(NSString *)searchKey success:(void (^)(NSArray *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("searchPublicService:searchKey:success:error:")]
        void SearchPublicService(RCSearchType searchType, string searchKey, Action<NSArray> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)searchPublicServiceByType:(RCPublicServiceType)publicServiceType searchType:(RCSearchType)searchType searchKey:(NSString *)searchKey success:(void (^)(NSArray *))successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("searchPublicServiceByType:searchType:searchKey:success:error:")]
        void SearchPublicServiceByType(RCPublicServiceType publicServiceType, RCSearchType searchType, string searchKey, Action<NSArray> successBlock, Action<RCErrorCode> errorBlock);

        // -(void)subscribePublicService:(RCPublicServiceType)publicServiceType publicServiceId:(NSString *)publicServiceId success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("subscribePublicService:publicServiceId:success:error:")]
        void SubscribePublicService(RCPublicServiceType publicServiceType, string publicServiceId, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(void)unsubscribePublicService:(RCPublicServiceType)publicServiceType publicServiceId:(NSString *)publicServiceId success:(void (^)())successBlock error:(void (^)(RCErrorCode))errorBlock;
        [Export("unsubscribePublicService:publicServiceId:success:error:")]
        void UnsubscribePublicService(RCPublicServiceType publicServiceType, string publicServiceId, Action successBlock, Action<RCErrorCode> errorBlock);

        // -(NSArray *)getPublicServiceList;
        [Export("getPublicServiceList")]
        [Verify(MethodToProperty), Verify(StronglyTypedNSArray)]
        NSObject[] PublicServiceList { get; }

        // -(RCPublicServiceProfile *)getPublicServiceProfile:(RCPublicServiceType)publicServiceType publicServiceId:(NSString *)publicServiceId;
        [Export("getPublicServiceProfile:publicServiceId:")]
        RCPublicServiceProfile GetPublicServiceProfile(RCPublicServiceType publicServiceType, string publicServiceId);

        // -(void)getPublicServiceProfile:(NSString *)targetId conversationType:(RCConversationType)type onSuccess:(void (^)(RCPublicServiceProfile *))onSuccess onError:(void (^)(NSError *))onError;
        [Export("getPublicServiceProfile:conversationType:onSuccess:onError:")]
        void GetPublicServiceProfile(string targetId, RCConversationType type, Action<RCPublicServiceProfile> onSuccess, Action<NSError> onError);

        // -(UIViewController *)getPublicServiceWebViewController:(NSString *)URLString;
        [Export("getPublicServiceWebViewController:")]
        UIViewController GetPublicServiceWebViewController(string URLString);

        // -(void)recordLaunchOptionsEvent:(NSDictionary *)launchOptions;
        [Export("recordLaunchOptionsEvent:")]
        void RecordLaunchOptionsEvent(NSDictionary launchOptions);

        // -(void)recordLocalNotificationEvent:(UILocalNotification *)notification;
        [Export("recordLocalNotificationEvent:")]
        void RecordLocalNotificationEvent(UILocalNotification notification);

        // -(void)recordRemoteNotificationEvent:(NSDictionary *)userInfo;
        [Export("recordRemoteNotificationEvent:")]
        void RecordRemoteNotificationEvent(NSDictionary userInfo);

        // -(NSDictionary *)getPushExtraFromLaunchOptions:(NSDictionary *)launchOptions;
        [Export("getPushExtraFromLaunchOptions:")]
        NSDictionary GetPushExtraFromLaunchOptions(NSDictionary launchOptions);

        // -(NSDictionary *)getPushExtraFromRemoteNotification:(NSDictionary *)userInfo;
        [Export("getPushExtraFromRemoteNotification:")]
        NSDictionary GetPushExtraFromRemoteNotification(NSDictionary userInfo);

        // -(NSString *)getSDKVersion;
        [Export("getSDKVersion")]
        [Verify(MethodToProperty)]
        string SDKVersion { get; }

        // -(NSData *)decodeAMRToWAVE:(NSData *)data;
        [Export("decodeAMRToWAVE:")]
        NSData DecodeAMRToWAVE(NSData data);

        // -(NSData *)encodeWAVEToAMR:(NSData *)data channel:(int)nChannels nBitsPerSample:(int)nBitsPerSample;
        [Export("encodeWAVEToAMR:channel:nBitsPerSample:")]
        NSData EncodeWAVEToAMR(NSData data, int nChannels, int nBitsPerSample);

        // -(void)startCustomService:(NSString *)kefuId onSuccess:(void (^)(RCCustomServiceConfig *))successBlock onError:(void (^)(int, NSString *))errorBlock onModeType:(void (^)(RCCSModeType))modeTypeBlock onQuit:(void (^)(NSString *))quitBlock;
        [Export("startCustomService:onSuccess:onError:onModeType:onQuit:")]
        void StartCustomService(string kefuId, Action<RCCustomServiceConfig> successBlock, Action<int, NSString> errorBlock, Action<RCCSModeType> modeTypeBlock, Action<NSString> quitBlock);

        // -(void)stopCustomeService:(NSString *)kefuId;
        [Export("stopCustomeService:")]
        void StopCustomeService(string kefuId);

        // -(void)switchToHumanMode:(NSString *)kefuId;
        [Export("switchToHumanMode:")]
        void SwitchToHumanMode(string kefuId);

        // -(void)evaluateCustomService:(NSString *)kefuId robotValue:(BOOL)isRobotResolved suggest:(NSString *)suggest;
        [Export("evaluateCustomService:robotValue:suggest:")]
        void EvaluateCustomService(string kefuId, bool isRobotResolved, string suggest);

        // -(void)evaluateCustomService:(NSString *)kefuId knownledgeId:(NSString *)knownledgeId robotValue:(BOOL)isRobotResolved;
        [Export("evaluateCustomService:knownledgeId:robotValue:")]
        void EvaluateCustomService(string kefuId, string knownledgeId, bool isRobotResolved);

        // -(void)evaluateCustomService:(NSString *)kefuId humanValue:(int)value suggest:(NSString *)suggest;
        [Export("evaluateCustomService:humanValue:suggest:")]
        void EvaluateCustomService(string kefuId, int value, string suggest);
    }

    [Static]
    [Verify(ConstantsInterfaceAssociation)]
    partial interface Constants
    {
        // extern NSString *const RCLibDispatchReadReceiptNotification;
        [Field("RCLibDispatchReadReceiptNotification", "__Internal")]
        NSString RCLibDispatchReadReceiptNotification { get; }
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
        [Verify(StronglyTypedNSArray)]
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
        [Verify(MethodToProperty), Verify(StronglyTypedNSArray)]
        NSObject[] Participants { get; }

        // @required -(RCRealTimeLocationStatus)getStatus;
        [Abstract]
        [Export("getStatus")]
        [Verify(MethodToProperty)]
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

    // @interface RCStatusMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCStatusMessage
    {
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

    // @interface RCUnknownMessage : RCMessageContent
    [BaseType(typeof(RCMessageContent))]
    interface RCUnknownMessage
    {
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
        NSData CompressedImageAndScalingSize(UIImage image, CGSize targetSize, nfloat maxDataLen);

        // +(NSData *)compressedImageAndScalingSize:(UIImage *)image targetSize:(CGSize)targetSize percent:(CGFloat)percent;
        [Static]
        [Export("compressedImageAndScalingSize:targetSize:percent:")]
        NSData CompressedImageAndScalingSize(UIImage image, CGSize targetSize, nfloat percent);

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
        [Verify(MethodToProperty)]
        string ApplicationDocumentsDirectory { get; }

        // +(NSString *)rongDocumentsDirectory;
        [Static]
        [Export("rongDocumentsDirectory")]
        [Verify(MethodToProperty)]
        string RongDocumentsDirectory { get; }

        // +(NSString *)rongImageCacheDirectory;
        [Static]
        [Export("rongImageCacheDirectory")]
        [Verify(MethodToProperty)]
        string RongImageCacheDirectory { get; }

        // +(NSString *)currentSystemTime;
        [Static]
        [Export("currentSystemTime")]
        [Verify(MethodToProperty)]
        string CurrentSystemTime { get; }

        // +(NSString *)currentCarrier;
        [Static]
        [Export("currentCarrier")]
        [Verify(MethodToProperty)]
        string CurrentCarrier { get; }

        // +(NSString *)currentNetWork;
        [Static]
        [Export("currentNetWork")]
        [Verify(MethodToProperty)]
        string CurrentNetWork { get; }

        // +(NSString *)currentSystemVersion;
        [Static]
        [Export("currentSystemVersion")]
        [Verify(MethodToProperty)]
        string CurrentSystemVersion { get; }

        // +(NSString *)currentDeviceModel;
        [Static]
        [Export("currentDeviceModel")]
        [Verify(MethodToProperty)]
        string CurrentDeviceModel { get; }
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
}