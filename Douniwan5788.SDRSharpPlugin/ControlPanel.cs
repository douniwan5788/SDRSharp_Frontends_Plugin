using System;
using System.Windows.Forms;
using SDRSharp.Common;

namespace Douniwan5788.SDRSharpPlugin
{
    public partial class ControlPanel : UserControl
    {
        private ISharpControl _control;

        public ControlPanel(ISharpControl control)
        {
            _control = control;
            InitializeComponent();
        }

        private void someButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hey!", "Messagge", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
