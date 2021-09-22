using Lab4_Control.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_Control
{
    public partial class Form1 : Form
    {
        StudentManagement sm;

        private bool _checked = false;

        public Form1()
        {
            InitializeComponent();
            sm = new StudentManagement();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Dialog";// "Add Photos";
            dlg.Multiselect = true;
            dlg.Filter = "Image Files (JPEG, GIF, BMP, etc.)|"
            + "*.jpg;*.jpeg;*.gif;*.bmp;"
            + "*.tif;*.tiff;*.png|"
            + "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
            + "GIF files (*.gif)|*.gif|"
            + "BMP files (*.bmp)|*.bmp|"
            + "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
            + "PNG files (*.png)|*.png|"
            + "All files (*.*)|*.*";

            dlg.InitialDirectory = Environment.CurrentDirectory;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pbxAvatar.SizeMode = PictureBoxSizeMode.Zoom;
                txtPic.Text = dlg.FileName;
                pbxAvatar.Load(dlg.FileName);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            Default();
        }

        public void Default()
        {
            mtxtID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddr.Text = "";
            txtPic.Text = "";
            dtpBirthDay.Value = DateTime.Now;
            rbnMale.Checked = true;
            mtxtPhoneNum.Text = "";
            pbxAvatar.Image = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _checked = true;
            if (mtxtID.Text == ""|| txtName.Text == "")
            {
                MessageBox.Show("Không được để trống MSSV và tên","Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            Student student = new Student() {
                ID = mtxtID.Text.Trim(),
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddr.Text.Trim(),
                Character = txtPic.Text.Trim(),
                BirthDay = dtpBirthDay.Value.Date,
                Sex = rbnMale.Checked == true ? "Nam" : "Nữ",
                Class = cbClass.SelectedItem.ToString(),
                PhoneNum = mtxtPhoneNum.Text
            };
            if (sm.Save(student))
            {
                MessageBox.Show("Đã thêm","Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
                try
                {
                    pbxAvatar.Load(student.Character);
                }
                catch
                {
                }
            }
            else
            {
                var oke = MessageBox.Show($"đã có MSSV {student.ID} Bạn mún cập nhật hem", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (oke == DialogResult.Yes)
                {
                    sm.Update(student);
                }
            }
            LoadStudentList();
        }

        private void LoadStudentList()
        {
            lvLStudentList.Items.Clear();
            foreach (Student st in sm.Students)
            {
                ListViewItem item = new ListViewItem(st.ID);
                item.SubItems.Add(st.Name);
                item.SubItems.Add(st.Sex);
                item.SubItems.Add(st.BirthDay.ToString());
                item.SubItems.Add(st.Class);
                item.SubItems.Add(st.PhoneNum);
                item.SubItems.Add(st.Email);
                item.SubItems.Add(st.Address);
                item.SubItems.Add(st.Character);
                lvLStudentList.Items.Add(item);
            }
        }

        private void lvLStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvLStudentList.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem item = this.lvLStudentList.SelectedItems[0];
                mtxtID.Text =   item.SubItems[0].Text;
                txtName.Text = item.SubItems[1].Text;
                txtEmail.Text = item.SubItems[6].Text;
                txtAddr.Text = item.SubItems[7].Text;
                txtPic.Text = item.SubItems[8].Text;
                dtpBirthDay.Value = DateTime.Parse(item.SubItems[3].Text);
                if (item.SubItems[2].Text == "Nam") rbnMale.Checked = true;
                else rbnFermale.Checked = true;
                mtxtPhoneNum.Text = item.SubItems[5].Text;
                cbClass.SelectedItem = item.SubItems[4].Text;
                try
                {
                    pbxAvatar.Load(item.SubItems[8].Text);
                }
                catch 
                {
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rbnMale.Checked = true;
            cbClass.SelectedIndex = 0;
            LoadStudentList();
        }

        private void reloadDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sm.Reload();
            LoadStudentList();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _checked = true;
            int count = this.lvLStudentList.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem item = this.lvLStudentList.SelectedItems[0];
                sm.Delete(item.SubItems[0].Text);
            }
            LoadStudentList();
            Default();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_checked)
            {
                var s = MessageBox.Show("Bạn có muốn lưu hem >_< (Nhấn yes để lưu O_O )", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (s == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                if (s == DialogResult.Yes)
                {
                    sm.SaveList();
                }
            }
        }
    }
}