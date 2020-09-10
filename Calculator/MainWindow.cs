using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    private InputValidator inputValidator;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        inputValidator = new InputValidator();

        Build();

        this.OneButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.TwoButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.ThreeButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.FourButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.FiveButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.SixButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.SevenButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.EightButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.NineButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.ZeroButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.DotButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void AppendCharacter(object sender, EventArgs a)
    {
        string newText = inputValidator.Validate((sender as Button).Label);
        this.MainOutput.Text = newText;
    }
}
