using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        this.OneButton.KeyPressEvent += new global::Gtk.KeyPressEventHandler(this.AddOne);
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void AddOne(object sender, KeyPressEventArgs a)
    {
        string value = this.MainOutput.Text;
        this.MainOutput.Text = value + sender.ToString();
    }
}
