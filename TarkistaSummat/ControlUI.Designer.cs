namespace TarkistaSummat
{
    partial class ControlUI
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textRange = new System.Windows.Forms.TextBox();
            this.radioPair = new System.Windows.Forms.RadioButton();
            this.radioKomb = new System.Windows.Forms.RadioButton();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.Colorbtn = new System.Windows.Forms.Button();
            this.comboBoxMax = new System.Windows.Forms.ComboBox();
            this.textBoxColor = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(34, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(119, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Tarkistetaan:";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textRange
            // 
            this.textRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRange.Location = new System.Drawing.Point(159, 42);
            this.textRange.Name = "textRange";
            this.textRange.Size = new System.Drawing.Size(100, 26);
            this.textRange.TabIndex = 1;
            this.textRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radioPair
            // 
            this.radioPair.AutoSize = true;
            this.radioPair.Checked = true;
            this.radioPair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioPair.Location = new System.Drawing.Point(34, 98);
            this.radioPair.Name = "radioPair";
            this.radioPair.Size = new System.Drawing.Size(50, 20);
            this.radioPair.TabIndex = 2;
            this.radioPair.TabStop = true;
            this.radioPair.Text = "Pari";
            this.radioPair.UseVisualStyleBackColor = true;
            // 
            // radioKomb
            // 
            this.radioKomb.AutoSize = true;
            this.radioKomb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioKomb.Location = new System.Drawing.Point(34, 122);
            this.radioKomb.Name = "radioKomb";
            this.radioKomb.Size = new System.Drawing.Size(101, 20);
            this.radioKomb.TabIndex = 3;
            this.radioKomb.Text = "Kombinaatio";
            this.radioKomb.UseVisualStyleBackColor = true;
            this.radioKomb.CheckedChanged += new System.EventHandler(this.RadioKomb_CheckedChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(34, 148);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 37);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Käynnistä";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            this.buttonStart.MouseHover += new System.EventHandler(this.buttonStart_MouseHover);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(159, 148);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 37);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Peruuta";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Visible = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxStatus.Enabled = false;
            this.textBoxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStatus.Location = new System.Drawing.Point(34, 191);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(225, 22);
            this.textBoxStatus.TabIndex = 7;
            this.textBoxStatus.Text = "Status";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // Colorbtn
            // 
            this.Colorbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Colorbtn.Location = new System.Drawing.Point(35, 219);
            this.Colorbtn.Name = "Colorbtn";
            this.Colorbtn.Size = new System.Drawing.Size(100, 37);
            this.Colorbtn.TabIndex = 9;
            this.Colorbtn.Text = "Color";
            this.Colorbtn.UseVisualStyleBackColor = true;
            this.Colorbtn.Click += new System.EventHandler(this.Colorbtn_Click);
            // 
            // comboBoxMax
            // 
            this.comboBoxMax.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.comboBoxMax.Enabled = false;
            this.comboBoxMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMax.FormattingEnabled = true;
            this.comboBoxMax.Items.AddRange(new object[] {
            "10",
            "15",
            "20"});
            this.comboBoxMax.Location = new System.Drawing.Point(159, 121);
            this.comboBoxMax.Name = "comboBoxMax";
            this.comboBoxMax.Size = new System.Drawing.Size(100, 24);
            this.comboBoxMax.TabIndex = 10;
            this.comboBoxMax.SelectedIndexChanged += new System.EventHandler(this.comboBoxMax_SelectedIndexChanged);
            // 
            // textBoxColor
            // 
            this.textBoxColor.Enabled = false;
            this.textBoxColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxColor.Location = new System.Drawing.Point(159, 221);
            this.textBoxColor.Name = "textBoxColor";
            this.textBoxColor.ReadOnly = true;
            this.textBoxColor.Size = new System.Drawing.Size(38, 35);
            this.textBoxColor.TabIndex = 11;
            // 
            // ControlUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.textBoxColor);
            this.Controls.Add(this.comboBoxMax);
            this.Controls.Add(this.Colorbtn);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.radioKomb);
            this.Controls.Add(this.radioPair);
            this.Controls.Add(this.textRange);
            this.Controls.Add(this.textBox1);
            this.Name = "ControlUI";
            this.Size = new System.Drawing.Size(303, 386);
            this.Load += new System.EventHandler(this.ControlUI_Load);
            this.Enter += new System.EventHandler(this.ControlUI_Enter);
            this.MouseHover += new System.EventHandler(this.ControlUI_MouseHover);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlUI_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textRange;
        private System.Windows.Forms.RadioButton radioPair;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.RadioButton radioKomb;
        private System.Windows.Forms.Button Colorbtn;
        private System.Windows.Forms.ComboBox comboBoxMax;
        private System.Windows.Forms.TextBox textBoxColor;
        public System.Windows.Forms.TextBox textBoxStatus;
    }
}
