namespace CollegeMSystem
{
    partial class preview
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
            this.print = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // print
            // 
            this.print.BackColor = System.Drawing.Color.SandyBrown;
            this.print.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.print.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold);
            this.print.ForeColor = System.Drawing.Color.Maroon;
            this.print.Location = new System.Drawing.Point(106, 14);
            this.print.Name = "print";
            this.print.ReadOnly = true;
            this.print.Size = new System.Drawing.Size(354, 500);
            this.print.TabIndex = 0;
            this.print.Text = "";
            this.print.TextChanged += new System.EventHandler(this.print_TextChanged);
            // 
            // preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(545, 560);
            this.Controls.Add(this.print);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "preview";
            this.Text = "preview";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox print;

    }
}