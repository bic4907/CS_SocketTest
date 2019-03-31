using System;
using System.Windows.Forms;

namespace SocketTest
{
    partial class BasicForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.buttonStatusStrip = new System.Windows.Forms.StatusStrip();
            this.bottomServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.svClientList = new System.Windows.Forms.ListView();
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.last_timestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sendMsg = new System.Windows.Forms.Button();
            this.msgTxtBox = new System.Windows.Forms.TextBox();
            this.buttonStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "서버열기";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonStatusStrip
            // 
            this.buttonStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bottomServerStatus});
            this.buttonStatusStrip.Location = new System.Drawing.Point(0, 251);
            this.buttonStatusStrip.Name = "buttonStatusStrip";
            this.buttonStatusStrip.Size = new System.Drawing.Size(498, 22);
            this.buttonStatusStrip.TabIndex = 0;
            this.buttonStatusStrip.Text = "상태 메세지";
            // 
            // bottomServerStatus
            // 
            this.bottomServerStatus.Name = "bottomServerStatus";
            this.bottomServerStatus.Size = new System.Drawing.Size(83, 17);
            this.bottomServerStatus.Text = "서버 오프라인";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(254, 112);
            this.listBox1.MultiColumn = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(215, 136);
            this.listBox1.TabIndex = 1;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(394, 12);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "서버접속";
            this.connectBtn.UseVisualStyleBackColor = true;
            // 
            // svClientList
            // 
            this.svClientList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IP,
            this.last_timestamp});
            this.svClientList.Location = new System.Drawing.Point(22, 111);
            this.svClientList.MultiSelect = false;
            this.svClientList.Name = "svClientList";
            this.svClientList.Size = new System.Drawing.Size(210, 137);
            this.svClientList.TabIndex = 2;
            this.svClientList.UseCompatibleStateImageBehavior = false;
            this.svClientList.View = System.Windows.Forms.View.Details;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 100;
            // 
            // last_timestamp
            // 
            this.last_timestamp.Text = "LastTimestamp";
            this.last_timestamp.Width = 120;
            // 
            // sendMsg
            // 
            this.sendMsg.Location = new System.Drawing.Point(115, 40);
            this.sendMsg.Name = "sendMsg";
            this.sendMsg.Size = new System.Drawing.Size(117, 23);
            this.sendMsg.TabIndex = 0;
            this.sendMsg.Text = "전체 메세지 전송";
            this.sendMsg.UseVisualStyleBackColor = true;
            this.sendMsg.Click += new System.EventHandler(this.sendMsg_Click);
            // 
            // msgTxtBox
            // 
            this.msgTxtBox.Location = new System.Drawing.Point(22, 42);
            this.msgTxtBox.Name = "msgTxtBox";
            this.msgTxtBox.Size = new System.Drawing.Size(87, 21);
            this.msgTxtBox.TabIndex = 3;
            // 
            // BasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 273);
            this.Controls.Add(this.msgTxtBox);
            this.Controls.Add(this.svClientList);
            this.Controls.Add(this.buttonStatusStrip);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.sendMsg);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "BasicForm";
            this.Text = "BasicForm";
            this.Load += new System.EventHandler(this.BasicForm_Load);
            this.buttonStatusStrip.ResumeLayout(false);
            this.buttonStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.StatusStrip buttonStatusStrip;
        public System.Windows.Forms.ToolStripStatusLabel bottomServerStatus;
        public System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.Button connectBtn;
        public System.Windows.Forms.ListView svClientList;
        private ColumnHeader IP;
        private ColumnHeader last_timestamp;
        public Button sendMsg;
        private TextBox msgTxtBox;
    }
}

