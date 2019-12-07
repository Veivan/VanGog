namespace VanGogDll
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.appToolbar = new System.Windows.Forms.ToolStrip();
			this.tbtnOpen = new System.Windows.Forms.ToolStripButton();
			this.tbtnSave = new System.Windows.Forms.ToolStripButton();
			this.tbtnClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.lblZoom = new System.Windows.Forms.ToolStripLabel();
			this.tcbxZoom = new System.Windows.Forms.ToolStripComboBox();
			this.tbtnZoomIn = new System.Windows.Forms.ToolStripButton();
			this.tbynZoomOut = new System.Windows.Forms.ToolStripButton();
			this.tbtnZoomDynamic = new System.Windows.Forms.ToolStripButton();
			this.tbtnZoomMarquee = new System.Windows.Forms.ToolStripButton();
			this.tbtnZoomActualSize = new System.Windows.Forms.ToolStripButton();
			this.tbtnZoomFitVisible = new System.Windows.Forms.ToolStripButton();
			this.tbtnZoomFitWidth = new System.Windows.Forms.ToolStripButton();
			this.tbtnZoomFitHeight = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tbtnDisplaySinglePage = new System.Windows.Forms.ToolStripButton();
			this.btTurnLeft = new System.Windows.Forms.ToolStripButton();
			this.btTurnRight = new System.Windows.Forms.ToolStripButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.appToolbar.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 473);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(853, 22);
			this.statusBar.TabIndex = 7;
			// 
			// appToolbar
			// 
			this.appToolbar.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.appToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnOpen,
            this.tbtnSave,
            this.tbtnClose,
            this.toolStripSeparator1,
            this.lblZoom,
            this.tcbxZoom,
            this.tbtnZoomIn,
            this.tbynZoomOut,
            this.tbtnZoomDynamic,
            this.tbtnZoomMarquee,
            this.tbtnZoomActualSize,
            this.tbtnZoomFitVisible,
            this.tbtnZoomFitWidth,
            this.tbtnZoomFitHeight,
            this.toolStripSeparator2,
            this.tbtnDisplaySinglePage,
            this.btTurnLeft,
            this.btTurnRight});
			this.appToolbar.Location = new System.Drawing.Point(0, 0);
			this.appToolbar.Name = "appToolbar";
			this.appToolbar.Size = new System.Drawing.Size(853, 39);
			this.appToolbar.TabIndex = 9;
			this.appToolbar.Text = "toolStrip1";
			this.appToolbar.Visible = false;
			// 
			// tbtnOpen
			// 
			this.tbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("tbtnOpen.Image")));
			this.tbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnOpen.Name = "tbtnOpen";
			this.tbtnOpen.Size = new System.Drawing.Size(36, 36);
			this.tbtnOpen.Text = "Open file";
			this.tbtnOpen.Visible = false;
			this.tbtnOpen.Click += new System.EventHandler(this.tbtnOpen_Click_1);
			// 
			// tbtnSave
			// 
			this.tbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnSave.Name = "tbtnSave";
			this.tbtnSave.Size = new System.Drawing.Size(23, 36);
			this.tbtnSave.Text = "Save file";
			this.tbtnSave.Visible = false;
			// 
			// tbtnClose
			// 
			this.tbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tbtnClose.Image")));
			this.tbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnClose.Name = "tbtnClose";
			this.tbtnClose.Size = new System.Drawing.Size(36, 36);
			this.tbtnClose.Text = "Закрыть";
			this.tbtnClose.Click += new System.EventHandler(this.tbtnClose_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
			// 
			// lblZoom
			// 
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(42, 36);
			this.lblZoom.Text = "Zoom:";
			this.lblZoom.Visible = false;
			// 
			// tcbxZoom
			// 
			this.tcbxZoom.Items.AddRange(new object[] {
            "25",
            "50",
            "75",
            "100",
            "125",
            "150",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900",
            "1000"});
			this.tcbxZoom.Name = "tcbxZoom";
			this.tcbxZoom.Size = new System.Drawing.Size(75, 39);
			this.tcbxZoom.Visible = false;
			// 
			// tbtnZoomIn
			// 
			this.tbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tbtnZoomIn.Image")));
			this.tbtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnZoomIn.Name = "tbtnZoomIn";
			this.tbtnZoomIn.Size = new System.Drawing.Size(36, 36);
			this.tbtnZoomIn.Text = "Zoom in";
			this.tbtnZoomIn.Click += new System.EventHandler(this.tbtnZoomIn_Click);
			// 
			// tbynZoomOut
			// 
			this.tbynZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbynZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tbynZoomOut.Image")));
			this.tbynZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbynZoomOut.Name = "tbynZoomOut";
			this.tbynZoomOut.Size = new System.Drawing.Size(36, 36);
			this.tbynZoomOut.Text = "Zoom out";
			this.tbynZoomOut.Click += new System.EventHandler(this.tbynZoomOut_Click);
			// 
			// tbtnZoomDynamic
			// 
			this.tbtnZoomDynamic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnZoomDynamic.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnZoomDynamic.Name = "tbtnZoomDynamic";
			this.tbtnZoomDynamic.Size = new System.Drawing.Size(23, 36);
			this.tbtnZoomDynamic.Text = "Dynamic zoom";
			this.tbtnZoomDynamic.Visible = false;
			// 
			// tbtnZoomMarquee
			// 
			this.tbtnZoomMarquee.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnZoomMarquee.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnZoomMarquee.Name = "tbtnZoomMarquee";
			this.tbtnZoomMarquee.Size = new System.Drawing.Size(23, 36);
			this.tbtnZoomMarquee.Text = "Marquee zoom";
			this.tbtnZoomMarquee.Visible = false;
			// 
			// tbtnZoomActualSize
			// 
			this.tbtnZoomActualSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnZoomActualSize.Image = ((System.Drawing.Image)(resources.GetObject("tbtnZoomActualSize.Image")));
			this.tbtnZoomActualSize.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnZoomActualSize.Name = "tbtnZoomActualSize";
			this.tbtnZoomActualSize.Size = new System.Drawing.Size(36, 36);
			this.tbtnZoomActualSize.Text = "Actual size";
			this.tbtnZoomActualSize.Visible = false;
			this.tbtnZoomActualSize.Click += new System.EventHandler(this.tbtnZoomActualSize_Click);
			// 
			// tbtnZoomFitVisible
			// 
			this.tbtnZoomFitVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnZoomFitVisible.Image = ((System.Drawing.Image)(resources.GetObject("tbtnZoomFitVisible.Image")));
			this.tbtnZoomFitVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnZoomFitVisible.Name = "tbtnZoomFitVisible";
			this.tbtnZoomFitVisible.Size = new System.Drawing.Size(36, 36);
			this.tbtnZoomFitVisible.Text = "Fit visible";
			this.tbtnZoomFitVisible.Visible = false;
			this.tbtnZoomFitVisible.Click += new System.EventHandler(this.tbtnZoomFitVisible_Click);
			// 
			// tbtnZoomFitWidth
			// 
			this.tbtnZoomFitWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnZoomFitWidth.Image = ((System.Drawing.Image)(resources.GetObject("tbtnZoomFitWidth.Image")));
			this.tbtnZoomFitWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnZoomFitWidth.Name = "tbtnZoomFitWidth";
			this.tbtnZoomFitWidth.Size = new System.Drawing.Size(36, 36);
			this.tbtnZoomFitWidth.Text = "Fit width";
			this.tbtnZoomFitWidth.Visible = false;
			this.tbtnZoomFitWidth.Click += new System.EventHandler(this.tbtnZoomFitWidth_Click);
			// 
			// tbtnZoomFitHeight
			// 
			this.tbtnZoomFitHeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnZoomFitHeight.Image = ((System.Drawing.Image)(resources.GetObject("tbtnZoomFitHeight.Image")));
			this.tbtnZoomFitHeight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnZoomFitHeight.Name = "tbtnZoomFitHeight";
			this.tbtnZoomFitHeight.Size = new System.Drawing.Size(36, 36);
			this.tbtnZoomFitHeight.Text = "Fit height";
			this.tbtnZoomFitHeight.Visible = false;
			this.tbtnZoomFitHeight.Click += new System.EventHandler(this.tbtnZoomFitHeight_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
			this.toolStripSeparator2.Visible = false;
			// 
			// tbtnDisplaySinglePage
			// 
			this.tbtnDisplaySinglePage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbtnDisplaySinglePage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbtnDisplaySinglePage.Name = "tbtnDisplaySinglePage";
			this.tbtnDisplaySinglePage.Size = new System.Drawing.Size(23, 36);
			this.tbtnDisplaySinglePage.Text = "Single page";
			this.tbtnDisplaySinglePage.Visible = false;
			// 
			// btTurnLeft
			// 
			this.btTurnLeft.Name = "btTurnLeft";
			this.btTurnLeft.Size = new System.Drawing.Size(23, 36);
			this.btTurnLeft.Visible = false;
			// 
			// btTurnRight
			// 
			this.btTurnRight.Name = "btTurnRight";
			this.btTurnRight.Size = new System.Drawing.Size(23, 36);
			this.btTurnRight.Visible = false;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 39);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(853, 434);
			this.panel1.TabIndex = 10;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(12, 15);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(664, 385);
			this.pictureBox1.TabIndex = 9;
			this.pictureBox1.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(853, 495);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.appToolbar);
			this.Controls.Add(this.statusBar);
			this.Name = "MainForm";
			this.Text = "СГР";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.appToolbar.ResumeLayout(false);
			this.appToolbar.PerformLayout();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.ToolStrip appToolbar;
		private System.Windows.Forms.ToolStripButton tbtnOpen;
		private System.Windows.Forms.ToolStripButton tbtnSave;
		private System.Windows.Forms.ToolStripButton tbtnClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel lblZoom;
		private System.Windows.Forms.ToolStripComboBox tcbxZoom;
		private System.Windows.Forms.ToolStripButton tbtnZoomIn;
		private System.Windows.Forms.ToolStripButton tbynZoomOut;
		private System.Windows.Forms.ToolStripButton tbtnZoomDynamic;
		private System.Windows.Forms.ToolStripButton tbtnZoomMarquee;
		private System.Windows.Forms.ToolStripButton tbtnZoomActualSize;
		private System.Windows.Forms.ToolStripButton tbtnZoomFitVisible;
		private System.Windows.Forms.ToolStripButton tbtnZoomFitWidth;
		private System.Windows.Forms.ToolStripButton tbtnZoomFitHeight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tbtnDisplaySinglePage;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolStripButton btTurnLeft;
		private System.Windows.Forms.ToolStripButton btTurnRight;
	}
}

