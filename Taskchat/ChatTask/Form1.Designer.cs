namespace ChatTask
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Listchat = new ListBox();
            ListTask = new ListBox();
            textBox1 = new TextBox();
            Send = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnClear = new Button();
            SuspendLayout();
            // 
            // Listchat
            // 
            Listchat.FormattingEnabled = true;
            Listchat.ItemHeight = 15;
            Listchat.Location = new Point(12, 67);
            Listchat.Name = "Listchat";
            Listchat.Size = new Size(749, 274);
            Listchat.TabIndex = 0;
            // 
            // ListTask
            // 
            ListTask.FormattingEnabled = true;
            ListTask.ItemHeight = 15;
            ListTask.Location = new Point(850, 67);
            ListTask.Name = "ListTask";
            ListTask.Size = new Size(368, 379);
            ListTask.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(36, 384);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(148, 23);
            textBox1.TabIndex = 2;
            // 
            // Send
            // 
            Send.Location = new Point(204, 384);
            Send.Name = "Send";
            Send.Size = new Size(75, 23);
            Send.TabIndex = 3;
            Send.Text = "Send";
            Send.UseVisualStyleBackColor = true;
            Send.Click += btnSend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 26);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 4;
            label1.Text = "Chat Convo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(976, 26);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 5;
            label2.Text = "Task Saved";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(620, 26);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 6;
            label3.Text = "Chat Heading";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(301, 384);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear Chat";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 548);
            Controls.Add(btnClear);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Send);
            Controls.Add(textBox1);
            Controls.Add(ListTask);
            Controls.Add(Listchat);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox Listchat;
        private ListBox ListTask;
        private TextBox textBox1;
        private Button Send;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnClear;
    }
}
