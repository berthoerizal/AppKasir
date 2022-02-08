//3.
using System.Data.SqlClient;
using System.Data;
namespace KoneksiDBSQLServer
{
    public partial class Form1 : Form
    {

        //4.
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader dr;
        private DataSet ds;
        //5.
        Koneksi Konn = new Koneksi();

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void inputEnableFalse()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            comboBox1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button5.Visible = true;
        }

        void inputEnableTrue()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            comboBox1.Enabled = true;
            button1.Visible = true;
            button5.Visible = false;
        }

        void Bersihkan()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            comboBox1.Text = "";
            textBox7.Text = "";
        }

        void TampilBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBL_BARANG", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);

                da.Fill(ds, "TBL_BARANG");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_BARANG";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TampilBarang();
            Bersihkan();
            ComboBoxSatuan();
            KodeBarangOtomatis();
            inputEnableFalse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == ""
                || textBox2.Text.Trim() == ""
                || textBox3.Text.Trim() == ""
                || textBox4.Text.Trim() == ""
                || textBox5.Text.Trim() == ""
                || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Data Belum Lengkap!");
            }
            else
            {
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("insert into TBL_BARANG values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "')", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Tambah Data Berhasil!");
                    TampilBarang();
                    Bersihkan();
                    KodeBarangOtomatis();
                    inputEnableFalse();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == ""
                || textBox2.Text.Trim() == ""
                || textBox3.Text.Trim() == ""
                || textBox4.Text.Trim() == ""
                || textBox5.Text.Trim() == ""
                || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Data Belum Lengkap!");
            }
            else
            {
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("update TBL_BARANG set NamaBarang='" + textBox2.Text + "', HargaBeli='" + textBox3.Text + "', HargaJual='" + textBox4.Text + "', JumlahBarang='" + textBox5.Text + "', SatuanBarang='" + comboBox1.Text + "' where KodeBarang='" + textBox1.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Edit Data Berhasil!");
                    TampilBarang();
                    Bersihkan();
                    KodeBarangOtomatis();
                    inputEnableFalse();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                inputEnableTrue();
                textBox1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = false;
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["KodeBarang"].Value.ToString();
                textBox2.Text = row.Cells["NamaBarang"].Value.ToString();
                textBox3.Text = row.Cells["HargaBeli"].Value.ToString();
                textBox4.Text = row.Cells["HargaJual"].Value.ToString();
                textBox5.Text = row.Cells["JumlahBarang"].Value.ToString();
                comboBox1.Text = row.Cells["SatuanBarang"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin akan menghapus data Barang: " + textBox1.Text + "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("delete from TBL_BARANG where KodeBarang='" + textBox1.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Hapus Data Berhasil!");
                    TampilBarang();
                    Bersihkan();
                    KodeBarangOtomatis();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TampilBarang();
            Bersihkan();
            KodeBarangOtomatis();
            inputEnableFalse();
        }

        void CariBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBL_BARANG where KodeBarang like '%" + textBox7.Text + "%' or NamaBarang like '%" + textBox7.Text + "%'", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);

                da.Fill(ds, "TBL_BARANG");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_BARANG";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            CariBarang();
        }

        private void ComboBoxSatuan()
        {
            comboBox1.Items.Add("PCS");
            comboBox1.Items.Add("BOX");
            comboBox1.Items.Add("PAK");
            comboBox1.Items.Add("BOTOL");
            comboBox1.Items.Add("UNIT");
        }

        private void KodeBarangOtomatis()
        {
            long hitung;
            string urutan;
            SqlConnection conn = Konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select KodeBarang from TBL_BARANG where KodeBarang in(select max(KodeBarang) from TBL_BARANG) order by KodeBarang DESC", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            //jika data ada
            if (dr.HasRows)
            {
                hitung = Convert.ToInt64(dr[0].ToString().Substring(dr["KodeBarang"].ToString().Length - 3, 3)) + 1;
                string kodeurutan = "000" + hitung;
                urutan = "BRG" + kodeurutan.Substring(kodeurutan.Length - 3, 3);
            }
            //jika data tidak ada
            else
            {
                urutan = "BRG001";
            }
            dr.Close();
            textBox1.Enabled = false;
            textBox1.Text = urutan;
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            inputEnableTrue();

            button1.Enabled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
    }
}