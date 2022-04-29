namespace BlazorApp.Config.DropDownMenu; 

public class DropDownItem {
    public string Name { get; set; }
    public Action Action { get; set; }

    public DropDownItem(string name, Action action) {
        Name = name;
        Action = action;
    }
}