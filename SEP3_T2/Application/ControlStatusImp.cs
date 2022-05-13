using Contracts.Services;
using Entities.Model;

namespace Application; 

public class ControlStatusImp : IControlStatusUser{

    public Dictionary<string, Guid>? Users { get; set; }
    
    public ICollection<Guid> OnlineUsers { get; set; }

    public ControlStatusImp() {
        Users = new Dictionary<string, Guid>();
        OnlineUsers = new List<Guid>();
    }

    public async Task InsertOnlineUser(string connectionId, Guid RUI) {
        try {
             Users.Add(connectionId, RUI);
        }
        catch (Exception e) {
            Console.WriteLine("Element already added");
        }
    }
    
    public async Task RemoveConnection(string connectionId) {
        Users.Remove(connectionId);
    }

    public async Task RemoveUserOnline(Guid RUI) {
        foreach (var item in Users!.Where(pair => pair.Value.Equals(RUI)).ToList()) {
            Users!.Remove(item.Key);
        }
    } 
    
    public async Task<int> GetAmountUsersOnline() {
        return GetOnlineList().Result.Count;
    }

    public async Task<ICollection<Guid>> GetOnlineList() {
        return Users!.Values.Distinct().ToList();
    }
    
    public async Task<int> GetAmountOfConnections() {
        return Users.Count;
    }

    public async Task<bool> isOnline(Guid RUI) {
        return Users!.Any(x => x.Value.Equals(RUI));
    }

    public async Task<ICollection<string>> GetAndClearKnewConnections(Guid RUI) {
        ICollection<string> tempConnections = new List<string>();
        foreach (var keyValuePair in Users.Where(x=> x.Value.Equals(RUI))) {
            tempConnections.Add(keyValuePair.Key);
            Users.Remove(keyValuePair.Key);
        }

        return tempConnections;
    }

    public async Task<Guid> GiveMeUser(string connectionId) {
        return Users[connectionId];
    }

}