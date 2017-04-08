﻿namespace SteeringCS
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
            this.dbPanel1 = new SteeringCS.DBPanel();
            this.lbl_ShowPath = new System.Windows.Forms.Label();
            this.lbl_ShowGoals = new System.Windows.Forms.Label();
            this.lbl_ShowGraph = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dbPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbPanel1
            // 
            this.dbPanel1.BackColor = System.Drawing.Color.White;
            this.dbPanel1.Controls.Add(this.label3);
            this.dbPanel1.Controls.Add(this.lbl_ShowPath);
            this.dbPanel1.Controls.Add(this.lbl_ShowGoals);
            this.dbPanel1.Controls.Add(this.lbl_ShowGraph);
            this.dbPanel1.Controls.Add(this.label4);
            this.dbPanel1.Controls.Add(this.label2);
            this.dbPanel1.Controls.Add(this.label1);
            this.dbPanel1.Location = new System.Drawing.Point(0, 0);
            this.dbPanel1.Name = "dbPanel1";
            this.dbPanel1.Size = new System.Drawing.Size(1050, 850);
            this.dbPanel1.TabIndex = 0;
            this.dbPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.dbPanel1_Paint);
            this.dbPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbPanel1_MouseClick);
            // 
            // lbl_ShowPath
            // 
            this.lbl_ShowPath.AutoSize = true;
            this.lbl_ShowPath.Location = new System.Drawing.Point(178, 22);
            this.lbl_ShowPath.Name = "lbl_ShowPath";
            this.lbl_ShowPath.Size = new System.Drawing.Size(40, 13);
            this.lbl_ShowPath.TabIndex = 8;
            this.lbl_ShowPath.Text = "[undef]";
            // 
            // lbl_ShowGoals
            // 
            this.lbl_ShowGoals.AutoSize = true;
            this.lbl_ShowGoals.Location = new System.Drawing.Point(178, 35);
            this.lbl_ShowGoals.Name = "lbl_ShowGoals";
            this.lbl_ShowGoals.Size = new System.Drawing.Size(40, 13);
            this.lbl_ShowGoals.TabIndex = 7;
            this.lbl_ShowGoals.Text = "[undef]";
            // 
            // lbl_ShowGraph
            // 
            this.lbl_ShowGraph.AutoSize = true;
            this.lbl_ShowGraph.Location = new System.Drawing.Point(178, 9);
            this.lbl_ShowGraph.Name = "lbl_ShowGraph";
            this.lbl_ShowGraph.Size = new System.Drawing.Size(40, 13);
            this.lbl_ShowGraph.TabIndex = 6;
            this.lbl_ShowGraph.Text = "[undef]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Goals active (press G to toggle):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Path active (press F to toggle):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Graph active (press D to toggle):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "Click anywhere on the screen to make the manager (square),\r\nmove to the closest a" +
    "vailable node to where you clicked.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 861);
            this.Controls.Add(this.dbPanel1);
            this.Name = "Form1";
            this.Text = "Steering";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.dbPanel1.ResumeLayout(false);
            this.dbPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DBPanel dbPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_ShowPath;
        private System.Windows.Forms.Label lbl_ShowGoals;
        private System.Windows.Forms.Label lbl_ShowGraph;
        private System.Windows.Forms.Label label3;
    }
}

