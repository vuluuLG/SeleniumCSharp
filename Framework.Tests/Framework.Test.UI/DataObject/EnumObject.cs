using System.ComponentModel;


namespace Framework.Test.UI.DataObject
{
    public enum VinStatus
    {
        Invalid,
        Valid,
        Unknown
    }

    public enum OrderMVRStatus
    {
        Invalid,
        Valid,
        Unknown
    }

    public enum WebControl
    {
        [Description("ele")]
        Element,
        [Description("txt")]
        Textbox,
        [Description("drp")]
        Dropdown,
        [Description("chx")]
        Checkbox,
        [Description("btn")]
        Button,
        [Description("lbl")]
        Label,
    }
}
