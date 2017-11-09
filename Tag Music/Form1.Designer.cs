namespace Tag_Music
{
    partial class MusicTextbox
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
            this.components = new System.ComponentModel.Container();
            this.Play = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.MusicList = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SubmitMusic = new System.Windows.Forms.Button();
            this.SubmitTag = new System.Windows.Forms.Button();
            this.TagTextbox = new System.Windows.Forms.TextBox();
            this.Test = new System.Windows.Forms.Button();
            this.Test2 = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(24, 202);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 0;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.button1_Click);
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(105, 202);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 23);
            this.Next.TabIndex = 2;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.button2_Click);
            // 
            // MusicList
            // 
            this.MusicList.FormattingEnabled = true;
            this.MusicList.ItemHeight = 12;
            this.MusicList.Location = new System.Drawing.Point(24, 12);
            this.MusicList.Name = "MusicList";
            this.MusicList.Size = new System.Drawing.Size(541, 184);
            this.MusicList.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 258);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(330, 21);
            this.textBox1.TabIndex = 4;
            // 
            // SubmitMusic
            // 
            this.SubmitMusic.Location = new System.Drawing.Point(571, 258);
            this.SubmitMusic.Name = "SubmitMusic";
            this.SubmitMusic.Size = new System.Drawing.Size(75, 23);
            this.SubmitMusic.TabIndex = 5;
            this.SubmitMusic.Text = "Submit";
            this.SubmitMusic.UseVisualStyleBackColor = true;
            // 
            // SubmitTag
            // 
            this.SubmitTag.Location = new System.Drawing.Point(571, 231);
            this.SubmitTag.Name = "SubmitTag";
            this.SubmitTag.Size = new System.Drawing.Size(75, 23);
            this.SubmitTag.TabIndex = 7;
            this.SubmitTag.Text = "Submit";
            this.SubmitTag.UseVisualStyleBackColor = true;
            // 
            // TagTextbox
            // 
            this.TagTextbox.Location = new System.Drawing.Point(24, 231);
            this.TagTextbox.Name = "TagTextbox";
            this.TagTextbox.Size = new System.Drawing.Size(330, 21);
            this.TagTextbox.TabIndex = 6;
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(571, 12);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(75, 23);
            this.Test.TabIndex = 8;
            this.Test.Text = "button1";
            this.Test.UseVisualStyleBackColor = true;
            // 
            // Test2
            // 
            this.Test2.Location = new System.Drawing.Point(571, 41);
            this.Test2.Name = "Test2";
            this.Test2.Size = new System.Drawing.Size(75, 23);
            this.Test2.TabIndex = 9;
            this.Test2.Text = "button1";
            this.Test2.UseVisualStyleBackColor = true;
            this.Test2.Click += new System.EventHandler(this.Test2_Click);
            // 
            // bindingSource1
            // 
            this.bindingSource1.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bindingSource1_ListChanged);
            // 
            // MusicTextbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 296);
            this.Controls.Add(this.Test2);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.SubmitTag);
            this.Controls.Add(this.TagTextbox);
            this.Controls.Add(this.SubmitMusic);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MusicList);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Play);
            this.Name = "MusicTextbox";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.ListBox MusicList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button SubmitMusic;
        private System.Windows.Forms.Button SubmitTag;
        private System.Windows.Forms.TextBox TagTextbox;
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.Button Test2;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}

