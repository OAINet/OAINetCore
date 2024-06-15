namespace OAINet.Node.Network;

public enum RequestStatus
{
    NetSuccess = 1,
    NetInternalError = 2,
    NetNotFound = 3,
    NetInvalidFormat = 4,
    NetBlacklistClient = 5,
}