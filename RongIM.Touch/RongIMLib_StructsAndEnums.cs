using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace RongIM.Touch
{
    public enum Mode : uint
    {
        Mr475 = 0,
        Mr515,
        Mr59,
        Mr67,
        Mr74,
        Mr795,
        Mr102,
        Mr122,
        Mrdtx,
        NModes
    }

    [Native]
    public enum RCConnectErrorCode : long
    {
        NetNaviError = 30000,
        NetChannelInvalid = 30001,
        NetUnavailable = 30002,
        MsgRespTimeout = 30003,
        HttpSendFail = 30004,
        HttpReqTimeout = 30005,
        HttpRecvFail = 30006,
        NaviResourceError = 30007,
        NodeNotFound = 30008,
        DomainNotResolve = 30009,
        SocketNotCreated = 30010,
        SocketDisconnected = 30011,
        PingSendFail = 30012,
        PongRecvFail = 30013,
        MsgSendFail = 30014,
        ConnAckTimeout = 31000,
        ConnProtoVersionError = 31001,
        ConnIdReject = 31002,
        ConnServerUnavailable = 31003,
        ConnTokenIncorrect = 31004,
        ConnNotAuthrorized = 31005,
        ConnRedirected = 31006,
        ConnPackageNameInvalid = 31007,
        ConnAppBlockedOrDeleted = 31008,
        ConnUserBlocked = 31009,
        DisconnKick = 31010,
        QueryAckNoData = 32001,
        MsgDataIncomplete = 32002,
        ClientNotInit = 33001,
        InvalidParameter = 33003,
        InvalidArgument = -1000
    }

    [Native]
    public enum RCErrorCode : long
    {
        ErrorcodeUnknown = -1,
        RejectedByBlacklist = 405,
        ErrorcodeTimeout = 5004,
        SendMsgFrequencyOverrun = 20604,
        NotInDiscussion = 21406,
        NotInGroup = 22406,
        ForbiddenInGroup = 22408,
        NotInChatroom = 23406,
        ForbiddenInChatroom = 23408,
        KickedFromChatroom = 23409,
        RcChatroomNotExist = 23410,
        RcChatroomIsFull = 23411,
        RcChannelInvalid = 30001,
        RcNetworkUnavailable = 30002,
        ClientNotInit = 33001,
        DatabaseError = 33002,
        InvalidParameter = 33003,
        MsgRoamingServiceUnavailable = 33007,
        InvalidPublicNumber = 29201
    }

    [Native]
    public enum RCConnectionStatus : long
    {
        Unknown = -1,
        Connected = 0,
        NetworkUnavailable = 1,
        AirplaneMode = 2,
        Cellular_2G = 3,
        Cellular_3G_4G = 4,
        Wifi = 5,
        KickedOfflineByOtherClient = 6,
        LoginOnWeb = 7,
        ServerInvalid = 8,
        ValidateInvalid = 9,
        Connecting = 10,
        Unconnected = 11,
        SignUp = 12,
        TokenIncorrect = 31004,
        DisconnException = 31011
    }

    [Native]
    public enum RCNetworkStatus : long
    {
        NotReachable = 0,
        ReachableViaWiFi = 1,
        ReachableViaLTE = 2,
        ReachableVia3G = 3,
        ReachableVia2G = 4
    }

    [Native]
    public enum RCSDKRunningMode : long
    {
        Backgroud = 0,
        Foregroud = 1
    }

    [Native]
    public enum RCConversationType : long
    {
        Private = 1,
        Discussion = 2,
        Group = 3,
        Chatroom = 4,
        Customerservice = 5,
        System = 6,
        Appservice = 7,
        Publicservice = 8,
        Pushservice = 9
    }

    [Native]
    public enum RCConversationNotificationStatus : long
    {
        DoNotDisturb = 0,
        Notify = 1
    }

    [Native]
    public enum RCReadReceiptMessageType : long
    {
        RC_ReadReceipt_Conversation = 1
    }

    [Native]
    public enum RCChatRoomMemberOrder : long
    {
        Asc = 1,
        Desc = 2
    }

    [Native]
    public enum RCMessagePersistent : long
    {
        None = 0,
        Ispersisted = 1,
        Iscounted = 3,
        Status = 16
    }

    [Native]
    public enum RCMessageDirection : long
    {
        Send = 1,
        Receive = 2
    }

    [Native]
    public enum RCSentStatus : long
    {
        Sending = 10,
        Failed = 20,
        Sent = 30,
        Received = 40,
        Read = 50,
        Destroyed = 60
    }

    [Native]
    public enum RCReceivedStatus : long
    {
        Unread = 0,
        Read = 1,
        Listened = 2,
        Downloaded = 4,
        Retrieved = 8,
        Multiplereceive = 16
    }

    [Native]
    public enum RCMediaType : long
    {
        Image = 1,
        Audio = 2,
        Video = 3,
        File = 100
    }

    [Native]
    public enum RCPublicServiceType : long
    {
        AppPublicService = 7,
        PublicService = 8
    }

    [Native]
    public enum RCPublicServiceMenuItemType : long
    {
        Group = 0,
        View = 1,
        Click = 2
    }

    [Native]
    public enum RCSearchType : long
    {
        Exact = 0,
        Fuzzy = 1
    }

    [Native]
    public enum RCCSModeType : long
    {
        NoService = 0,
        RobotOnly = 1,
        HumanOnly = 2,
        RobotFirst = 3
    }

    [Native]
    public enum RCDiscussionNotificationType : long
    {
        InviteDiscussionNotification = 1,
        QuitDiscussionNotification,
        RenameDiscussionTitleNotification,
        RemoveDiscussionMemberNotification,
        SwichInvitationAccessNotification
    }

    [Native]
    public enum RCRealTimeLocationStatus : long
    {
        Idle,
        Incoming,
        Outgoing,
        Connected
    }

    [Native]
    public enum RCRealTimeLocationErrorCode : long
    {
        NotSupport,
        ConversationNotSupport,
        ExceedMaxParticipant,
        GetConversationFailure
    }
}