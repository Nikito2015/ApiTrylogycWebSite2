namespace CommonTrylogycWebsite.ServiceRequests.Interfaces
{
    public interface IUserAssociatesAndBalancesRequest
    {
        int UserId { get; set; }

        bool IsValid();
    }
}