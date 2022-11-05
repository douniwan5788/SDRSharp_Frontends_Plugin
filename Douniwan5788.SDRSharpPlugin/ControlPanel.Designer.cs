namespace Douniwan5788.SDRSharpPlugin
{
    partial class ControlPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.someButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // someButton
            // 
            this.someButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.someButton.Location = new System.Drawing.Point(36, 62);
            this.someButton.Name = "someButton";
            this.someButton.Size = new System.Drawing.Size(75, 23);
            this.someButton.TabIndex = 0;
            this.someButton.Text = "Click me!";
            this.someButton.UseVisualStyleBackColor = true;
            this.someButton.Click += new System.EventHandler(this.someButton_Click);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.someButton);
            this.Name = "ControlPanel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button someButton;
    }
}
