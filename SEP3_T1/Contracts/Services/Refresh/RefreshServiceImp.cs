namespace Contracts.Services.Refresh; 

public class RefreshServiceImp : IRefreshService {
    
    public event Action? RefreshRequested;
    
    public void CallRequestRefresh() {
        RefreshRequested?.Invoke();
    }
}