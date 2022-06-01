namespace BlazorApp.Config.DropDownMenu; 

public class 
    DropDownMenuSettings {
    public string Icon { get; set; }
    public string Name { get; set; }
    public ICollection<DropDownItem> DropDownItems { get; set; }

    public DropDownMenuSettings(string icon, string name, ICollection<DropDownItem> dropDownItems) {
        Icon = icon;
        Name = name;
        DropDownItems = dropDownItems;
    }

    public DropDownMenuSettings(string name, ICollection<DropDownItem> dropDownItems) {
        Icon = "";
        Name = name;
        DropDownItems = dropDownItems;
    }
}