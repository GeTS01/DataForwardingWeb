namespace DataForwardingWeb.Domain.Enum
{
    public enum NitOperationType : int
    {
        NONE,
        RECEIVE_FROM_MINING,
        TRANSIT_TO_REFACTORING,
        TRANSIT_TO_TRANSPORT,
        TRANSIT_TO_EXPORT,
        RECEIVE_FROM_TRANSPORT,
        RECEIVE_FROM_TERMINAL
    }
}
