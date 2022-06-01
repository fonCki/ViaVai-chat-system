namespace BlazorApp.Services.View; 

public class View {
    public  bool Compress { get; set; } = false;
    public bool Expand { get; set; } = true;

    public void ChangeView() {
        Compress = !Compress;
        Expand = !Compress;
    }
}