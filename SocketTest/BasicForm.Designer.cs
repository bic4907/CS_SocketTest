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
            this.clientList = new System.Windows.Forms.ListBox();
            this.buttonStatusStrip = new System.Windows.Forms.StatusStrip();
            this.bottomServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(511, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "서버열기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // clientList
            // 
            this.clientList.FormattingEnabled = true;
            this.clientList.ItemHeight = 12;
            this.clientList.Location = new System.Drawing.Point(22, 16);
            this.clientList.Name = "clientList";
            this.clientList.Size = new System.Drawing.Size(215, 232);
            this.clientList.TabIndex = 1;
            // 
            // buttonStatusStrip
            // 
            this.buttonStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bottomServerStatus});
            this.buttonStatusStrip.Location = new System.Drawing.Point(0, 251);
            this.buttonStatusStrip.Name = "buttonStatusStrip";
            this.buttonStatusStrip.Size = new System.Drawing.Size(598, 22);
            this.buttonStatusStrip.TabIndex = 0;
            this.buttonStatusStrip.Text = "상태 메세지";
            // 
            // bottomServerStatus
            // 
            this.bottomServerStatus.Name = "bottomServerStatus";
            this.bottomServerStatus.Size = new System.Drawing.Size(55, 17);
            this.bottomServerStatus.Text = "서버상태";
            // 
            // BasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 273);
            this.Controls.Add(this.buttonStatusStrip);
            this.Controls.Add(this.clientList);
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
        public System.Windows.Forms.ListBox clientList;
        public System.Windows.Forms.StatusStrip buttonStatusStrip;
        public System.Windows.Forms.ToolStripStatusLabel bottomServerStatus;
    }
}

