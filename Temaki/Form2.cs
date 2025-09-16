using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temaki
{
    public partial class Form2 : Form
    {
        string connectionString = @"Server=SQLEXPRESS;Database=CJ3028224PR2;User Id=aluno;Password=aluno;";

        public Form2()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO ClientesDoAdvogado (Nome, CPF, Telefone, Email, Endereco, AdvogadoId) " +
                             "VALUES (@Nome, @CPF, @Telefone, @Email, @Endereco, @AdvogadoId)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", txtCli.Text);
                    cmd.Parameters.AddWithValue("@CPF", txtCPFCli.Text);
                    cmd.Parameters.AddWithValue("@Telefone", txtTELCli.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmailCli.Text);
                    cmd.Parameters.AddWithValue("@Endereco", txtENDCli.Text);
                    cmd.Parameters.AddWithValue("@AdvogadoId", Convert.ToInt32(txtADVCli.Text)); 

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cliente adicionado com sucesso!");
        }

       
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
