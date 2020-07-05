namespace MediaControlGUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.leftContainer = new System.Windows.Forms.TableLayoutPanel();
            this.panelPrev = new System.Windows.Forms.Panel();
            this.panelPause = new System.Windows.Forms.Panel();
            this.panelNext = new System.Windows.Forms.Panel();
            this.rightContainer = new System.Windows.Forms.Panel();
            this.gestureDisplay = new System.Windows.Forms.Panel();
            this.labelGestureText = new System.Windows.Forms.Label();
            this.buttonRecord = new System.Windows.Forms.Button();
            this.panelPrevContainer = new System.Windows.Forms.Panel();
            this.comboBoxPrev = new System.Windows.Forms.ComboBox();
            this.panelPrevPreview = new System.Windows.Forms.Panel();
            this.panelPauseContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxPause = new System.Windows.Forms.ComboBox();
            this.panelPausePreview = new System.Windows.Forms.Panel();
            this.comboBoxNext = new System.Windows.Forms.ComboBox();
            this.panelNextPreview = new System.Windows.Forms.Panel();
            this.leftContainer.SuspendLayout();
            this.rightContainer.SuspendLayout();
            this.gestureDisplay.SuspendLayout();
            this.panelPrevContainer.SuspendLayout();
            this.panelPauseContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftContainer
            // 
            this.leftContainer.ColumnCount = 2;
            this.leftContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.leftContainer.Controls.Add(this.panelPrev, 0, 0);
            this.leftContainer.Controls.Add(this.panelPause, 0, 1);
            this.leftContainer.Controls.Add(this.panelNext, 0, 2);
            this.leftContainer.Controls.Add(this.panelPrevContainer, 1, 0);
            this.leftContainer.Controls.Add(this.panelPauseContainer, 1, 1);
            this.leftContainer.Controls.Add(this.panel1, 1, 2);
            this.leftContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftContainer.Location = new System.Drawing.Point(0, 0);
            this.leftContainer.Name = "leftContainer";
            this.leftContainer.RowCount = 3;
            this.leftContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.leftContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.leftContainer.Size = new System.Drawing.Size(266, 386);
            this.leftContainer.TabIndex = 0;
            // 
            // panelPrev
            // 
            this.panelPrev.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("panelPrev.BackgroundImage")));
            this.panelPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrev.Location = new System.Drawing.Point(3, 3);
            this.panelPrev.Name = "panelPrev";
            this.panelPrev.Size = new System.Drawing.Size(127, 119);
            this.panelPrev.TabIndex = 0;
            // 
            // panelPause
            // 
            this.panelPause.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("panelPause.BackgroundImage")));
            this.panelPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPause.Location = new System.Drawing.Point(3, 128);
            this.panelPause.Name = "panelPause";
            this.panelPause.Size = new System.Drawing.Size(127, 119);
            this.panelPause.TabIndex = 2;
            // 
            // panelNext
            // 
            this.panelNext.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("panelNext.BackgroundImage")));
            this.panelNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNext.Location = new System.Drawing.Point(3, 253);
            this.panelNext.Name = "panelNext";
            this.panelNext.Size = new System.Drawing.Size(127, 130);
            this.panelNext.TabIndex = 3;
            // 
            // rightContainer
            // 
            this.rightContainer.Controls.Add(this.gestureDisplay);
            this.rightContainer.Controls.Add(this.buttonRecord);
            this.rightContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightContainer.Location = new System.Drawing.Point(269, 0);
            this.rightContainer.Name = "rightContainer";
            this.rightContainer.Size = new System.Drawing.Size(285, 386);
            this.rightContainer.TabIndex = 1;
            // 
            // gestureDisplay
            // 
            this.gestureDisplay.Controls.Add(this.labelGestureText);
            this.gestureDisplay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gestureDisplay.Location = new System.Drawing.Point(0, 191);
            this.gestureDisplay.Name = "gestureDisplay";
            this.gestureDisplay.Size = new System.Drawing.Size(285, 195);
            this.gestureDisplay.TabIndex = 1;
            // 
            // labelGestureText
            // 
            this.labelGestureText.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelGestureText.Location = new System.Drawing.Point(0, 0);
            this.labelGestureText.Name = "labelGestureText";
            this.labelGestureText.Size = new System.Drawing.Size(285, 23);
            this.labelGestureText.TabIndex = 0;
            this.labelGestureText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonRecord
            // 
            this.buttonRecord.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRecord.Location = new System.Drawing.Point(0, 0);
            this.buttonRecord.Name = "buttonRecord";
            this.buttonRecord.Size = new System.Drawing.Size(285, 38);
            this.buttonRecord.TabIndex = 0;
            this.buttonRecord.Text = "Record new gesture";
            this.buttonRecord.UseVisualStyleBackColor = false;
            // 
            // panelPrevContainer
            // 
            this.panelPrevContainer.Controls.Add(this.panelPrevPreview);
            this.panelPrevContainer.Controls.Add(this.comboBoxPrev);
            this.panelPrevContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrevContainer.Location = new System.Drawing.Point(136, 3);
            this.panelPrevContainer.Name = "panelPrevContainer";
            this.panelPrevContainer.Size = new System.Drawing.Size(127, 119);
            this.panelPrevContainer.TabIndex = 6;
            // 
            // comboBoxPrev
            // 
            this.comboBoxPrev.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxPrev.FormattingEnabled = true;
            this.comboBoxPrev.Location = new System.Drawing.Point(0, 0);
            this.comboBoxPrev.Name = "comboBoxPrev";
            this.comboBoxPrev.Size = new System.Drawing.Size(127, 21);
            this.comboBoxPrev.TabIndex = 0;
            // 
            // panelPrevPreview
            // 
            this.panelPrevPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrevPreview.Location = new System.Drawing.Point(0, 21);
            this.panelPrevPreview.Name = "panelPrevPreview";
            this.panelPrevPreview.Size = new System.Drawing.Size(127, 98);
            this.panelPrevPreview.TabIndex = 1;
            // 
            // panelPauseContainer
            // 
            this.panelPauseContainer.Controls.Add(this.panelPausePreview);
            this.panelPauseContainer.Controls.Add(this.comboBoxPause);
            this.panelPauseContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPauseContainer.Location = new System.Drawing.Point(136, 128);
            this.panelPauseContainer.Name = "panelPauseContainer";
            this.panelPauseContainer.Size = new System.Drawing.Size(127, 119);
            this.panelPauseContainer.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelNextPreview);
            this.panel1.Controls.Add(this.comboBoxNext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(136, 253);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 130);
            this.panel1.TabIndex = 8;
            // 
            // comboBoxPause
            // 
            this.comboBoxPause.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxPause.FormattingEnabled = true;
            this.comboBoxPause.Location = new System.Drawing.Point(0, 0);
            this.comboBoxPause.Name = "comboBoxPause";
            this.comboBoxPause.Size = new System.Drawing.Size(127, 21);
            this.comboBoxPause.TabIndex = 0;
            // 
            // panelPausePreview
            // 
            this.panelPausePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPausePreview.Location = new System.Drawing.Point(0, 21);
            this.panelPausePreview.Name = "panelPausePreview";
            this.panelPausePreview.Size = new System.Drawing.Size(127, 98);
            this.panelPausePreview.TabIndex = 1;
            // 
            // comboBoxNext
            // 
            this.comboBoxNext.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxNext.FormattingEnabled = true;
            this.comboBoxNext.Location = new System.Drawing.Point(0, 0);
            this.comboBoxNext.Name = "comboBoxNext";
            this.comboBoxNext.Size = new System.Drawing.Size(127, 21);
            this.comboBoxNext.TabIndex = 0;
            // 
            // panelNextPreview
            // 
            this.panelNextPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNextPreview.Location = new System.Drawing.Point(0, 21);
            this.panelNextPreview.Name = "panelNextPreview";
            this.panelNextPreview.Size = new System.Drawing.Size(127, 109);
            this.panelNextPreview.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(554, 386);
            this.Controls.Add(this.rightContainer);
            this.Controls.Add(this.leftContainer);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.leftContainer.ResumeLayout(false);
            this.rightContainer.ResumeLayout(false);
            this.gestureDisplay.ResumeLayout(false);
            this.panelPrevContainer.ResumeLayout(false);
            this.panelPauseContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button buttonRecord;
        private System.Windows.Forms.ComboBox comboBoxNext;
        private System.Windows.Forms.ComboBox comboBoxPause;
        private System.Windows.Forms.ComboBox comboBoxPrev;
        private System.Windows.Forms.Panel gestureDisplay;
        private System.Windows.Forms.Label labelGestureText;
        private System.Windows.Forms.TableLayoutPanel leftContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelNext;
        private System.Windows.Forms.Panel panelNextPreview;
        private System.Windows.Forms.Panel panelPause;
        private System.Windows.Forms.Panel panelPauseContainer;
        private System.Windows.Forms.Panel panelPausePreview;
        private System.Windows.Forms.Panel panelPrev;
        private System.Windows.Forms.Panel panelPrevContainer;
        private System.Windows.Forms.Panel panelPrevPreview;
        private System.Windows.Forms.Panel rightContainer;

        #endregion
    }
}