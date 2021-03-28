using System;
using System.IO;
using Gtk;
using System.Text;

public partial class MainWindow : Gtk.Window
{
    public int status = 0;
    MessageDialog messageDialog = null;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnBtnRunClicked(object sender, EventArgs e)
    {
        if (status != 0)
        {
            btnRun.Label = "Run Progress Bar";

            PrgBar1.Adjustment.Lower = 0;
            PrgBar1.PulseStep = 1;
            PrgBar1.Adjustment.Upper = 0;

            PrgBar2.Adjustment.Lower = 0;
            PrgBar2.PulseStep = 1;
            PrgBar2.Adjustment.Upper = 0;

            status = 0;
        }
        else
        {
            string valueOne = TxtLoopVal1.Buffer.Text;
            string valueTwo = TxtLoopVal2.Buffer.Text;

            try
            {
                int numberOne = (int)Convert.ToUInt32(valueOne, null);
                int numberTwo = (int)Convert.ToUInt32(valueTwo, null);

                btnRun.Label = "Stop It";
                status = 1;

                PrgBar1.Adjustment.Lower = 0;
                PrgBar1.PulseStep = 1;
                PrgBar1.Adjustment.Upper = numberOne;

                PrgBar2.Adjustment.Lower = 0;
                PrgBar2.PulseStep = 1;
                PrgBar2.Adjustment.Upper = numberTwo;

                for (int i = 0; i <= numberOne; i++) {
                    PrgBar1.Pulse();
                    PrgBar1.Fraction = i/numberOne;
                    PrgBar1.Adjustment.Value = i;
                    Main.IterationDo(false);
                    for (int j = 0; j <= numberTwo; j++) {
                        PrgBar2.Pulse();
                        PrgBar2.Fraction = j/numberTwo;
                        PrgBar2.Adjustment.Value = j;
                        Main.IterationDo(false);
                    }
                }
				
            } catch (Exception exception)
            {
                string error = exception.ToString();
                messageDialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.None, null)
                {
                    Title = "Error",
                    Text = error
                };               
                messageDialog.Show();
            }

        }
    }
    protected void OnRunApplicationActionActivated(object sender, EventArgs e)
    {
        OnBtnRunClicked (sender, e);
    }
        
    protected void OnExitActionActivated (object sender, EventArgs e)
    {
        Application.Quit ();
    }
}
