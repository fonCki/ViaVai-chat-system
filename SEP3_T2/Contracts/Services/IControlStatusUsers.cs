namespace Contracts.Services; 

public interface IControlStatusUser {
    
    public ICollection<Guid> OnlineUsers { get; set; }
    public Task InsertOnlineUser(string connectionId, Guid RUI);
    public Task RemoveConnection(string connectionId);
    public Task RemoveUserOnline(Guid RUI);
    public Task<int> GetAmountUsersOnline();
    public Task<ICollection<Guid>> GetOnlineList();
    public Task<int> GetAmountOfConnections();
    public Task<bool> isOnline(Guid RUI);
    public Task<ICollection<string>> GetAndClearKnewConnections(Guid RUI);
    public Task<Guid> GiveMeUser(string connectionId);

}