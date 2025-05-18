namespace GamesHub
{
    partial class Form5
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
            this.btnviewrentals = new System.Windows.Forms.Button();
            this.btnrentgame = new System.Windows.Forms.Button();
            this.btnviewgame = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnlogout = new System.Windows.Forms.Button();
            this.gamedataGrid = new System.Windows.Forms.DataGridView();
            this.rentaldatagrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gamedataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentaldatagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnviewrentals
            // 
            this.btnviewrentals.BackColor = System.Drawing.Color.Green;
            this.btnviewrentals.Location = new System.Drawing.Point(982, 357);
            this.btnviewrentals.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnviewrentals.Name = "btnviewrentals";
            this.btnviewrentals.Size = new System.Drawing.Size(174, 51);
            this.btnviewrentals.TabIndex = 38;
            this.btnviewrentals.Text = "View rentals";
            this.btnviewrentals.UseVisualStyleBackColor = false;
            this.btnviewrentals.Click += new System.EventHandler(this.btnviewrentals_Click);
            // 
            // btnrentgame
            // 
            this.btnrentgame.BackColor = System.Drawing.Color.Green;
            this.btnrentgame.Location = new System.Drawing.Point(18, 37);
            this.btnrentgame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnrentgame.Name = "btnrentgame";
            this.btnrentgame.Size = new System.Drawing.Size(174, 51);
            this.btnrentgame.TabIndex = 37;
            this.btnrentgame.Text = "Rent Game";
            this.btnrentgame.UseVisualStyleBackColor = false;
            this.btnrentgame.Click += new System.EventHandler(this.btnrentgame_Click);
            // 
            // btnviewgame
            // 
            this.btnviewgame.BackColor = System.Drawing.Color.Green;
            this.btnviewgame.Location = new System.Drawing.Point(982, 18);
            this.btnviewgame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnviewgame.Name = "btnviewgame";
            this.btnviewgame.Size = new System.Drawing.Size(174, 51);
            this.btnviewgame.TabIndex = 36;
            this.btnviewgame.Text = "view game";
            this.btnviewgame.UseVisualStyleBackColor = false;
            this.btnviewgame.Click += new System.EventHandler(this.btnviewgame_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Forte", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(398, 14);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(316, 45);
            this.label5.TabIndex = 40;
            this.label5.Text = "GamesRentalSys";
            // 
            // btnlogout
            // 
            this.btnlogout.BackColor = System.Drawing.Color.Red;
            this.btnlogout.ForeColor = System.Drawing.Color.Black;
            this.btnlogout.Location = new System.Drawing.Point(18, 602);
            this.btnlogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnlogout.Name = "btnlogout";
            this.btnlogout.Size = new System.Drawing.Size(174, 51);
            this.btnlogout.TabIndex = 39;
            this.btnlogout.Text = "log out";
            this.btnlogout.UseVisualStyleBackColor = false;
            this.btnlogout.Click += new System.EventHandler(this.button7_Click);
            // 
            // gamedataGrid
            // 
            this.gamedataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gamedataGrid.Location = new System.Drawing.Point(534, 104);
            this.gamedataGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gamedataGrid.Name = "gamedataGrid";
            this.gamedataGrid.RowHeadersWidth = 62;
            this.gamedataGrid.Size = new System.Drawing.Size(622, 231);
            this.gamedataGrid.TabIndex = 41;
            // 
            // rentaldatagrid
            // 
            this.rentaldatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rentaldatagrid.Location = new System.Drawing.Point(534, 429);
            this.rentaldatagrid.Name = "rentaldatagrid";
            this.rentaldatagrid.RowHeadersWidth = 62;
            this.rentaldatagrid.RowTemplate.Height = 28;
            this.rentaldatagrid.Size = new System.Drawing.Size(622, 242);
            this.rentaldatagrid.TabIndex = 42;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.rentaldatagrid);
            this.Controls.Add(this.gamedataGrid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnlogout);
            this.Controls.Add(this.btnviewrentals);
            this.Controls.Add(this.btnrentgame);
            this.Controls.Add(this.btnviewgame);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form5";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gamedataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentaldatagrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnviewrentals;
        private System.Windows.Forms.Button btnrentgame;
        private System.Windows.Forms.Button btnviewgame;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnlogout;
        private System.Windows.Forms.DataGridView gamedataGrid;
        private System.Windows.Forms.DataGridView rentaldatagrid;
    }
}