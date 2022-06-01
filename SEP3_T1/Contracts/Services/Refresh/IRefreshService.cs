namespace Contracts.Services.Refresh; 

public interface IRefreshService {
    public event Action RefreshRequested;
    public void CallRequestRefresh();
    
}