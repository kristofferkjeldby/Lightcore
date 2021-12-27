namespace Lightcore
{
    partial class LightcoreForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ViewPictureBox = new System.Windows.Forms.PictureBox();
            this.RenderButton = new System.Windows.Forms.Button();
            this.CancelRenderButton = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AnimateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPictureBox)).BeginInit();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewPictureBox
            // 
            this.ViewPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewPictureBox.Location = new System.Drawing.Point(12, 12);
            this.ViewPictureBox.Name = "ViewPictureBox";
            this.ViewPictureBox.Size = new System.Drawing.Size(1213, 875);
            this.ViewPictureBox.TabIndex = 0;
            this.ViewPictureBox.TabStop = false;
            // 
            // RenderButton
            // 
            this.RenderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderButton.Location = new System.Drawing.Point(1242, 12);
            this.RenderButton.Name = "RenderButton";
            this.RenderButton.Size = new System.Drawing.Size(75, 23);
            this.RenderButton.TabIndex = 1;
            this.RenderButton.Text = "Render";
            this.RenderButton.UseVisualStyleBackColor = true;
            // 
            // CancelRenderButton
            // 
            this.CancelRenderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelRenderButton.Location = new System.Drawing.Point(1242, 70);
            this.CancelRenderButton.Name = "CancelRenderButton";
            this.CancelRenderButton.Size = new System.Drawing.Size(75, 23);
            this.CancelRenderButton.TabIndex = 2;
            this.CancelRenderButton.Text = "Cancel";
            this.CancelRenderButton.UseVisualStyleBackColor = true;
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripStatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 899);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1329, 22);
            this.StatusStrip.TabIndex = 3;
            this.StatusStrip.Text = "StatusStrip";
            // 
            // StripStatusLabel
            // 
            this.StripStatusLabel.Name = "StripStatusLabel";
            this.StripStatusLabel.Size = new System.Drawing.Size(91, 17);
            this.StripStatusLabel.Text = "StripStatusLabel";
            // 
            // AnimateButton
            // 
            this.AnimateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnimateButton.Location = new System.Drawing.Point(1242, 41);
            this.AnimateButton.Name = "AnimateButton";
            this.AnimateButton.Size = new System.Drawing.Size(75, 23);
            this.AnimateButton.TabIndex = 4;
            this.AnimateButton.Text = "Animate";
            this.AnimateButton.UseVisualStyleBackColor = true;
            // 
            // LightcoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 921);
            this.Controls.Add(this.AnimateButton);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.CancelRenderButton);
            this.Controls.Add(this.RenderButton);
            this.Controls.Add(this.ViewPictureBox);
            this.Name = "LightcoreForm";
            this.Text = "Lightcore";
            this.Load += new System.EventHandler(this.LightcoreForm_Load);
            this.Shown += new System.EventHandler(this.LightcoreForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ViewPictureBox)).EndInit();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ViewPictureBox;
        private System.Windows.Forms.Button RenderButton;
        private System.Windows.Forms.Button CancelRenderButton;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusLabel;
        private System.Windows.Forms.Button AnimateButton;
    }
}

