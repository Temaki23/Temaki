using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CadastroClientes
{
    public partial class Form1 : Form
    {
     
      string connectionString = @"Server=SQLEXPRESS;Database=CJ3028224PR2;User Id=aluno;Password=aluno;";

        public Form1()
        {
            InitializeComponent();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Clientes";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewClientes.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message);
            }
        }

     

        private void LimparCampos()
        {
            txtNome.Clear();
            txtCPF.Clear();
            txtEndereco.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                MessageBox.Show("Nome e CPF são obrigatórios.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Clientes (Nome, CPF_CNPJ, Endereco, Email, Telefone) " +
                                   "VALUES (@Nome, @CPF, @Endereco, @Email, @Telefone)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@CPF", txtCPF.Text);
                    cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Cliente salvo com sucesso!");

                    LimparCampos();
                    CarregarClientes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar cliente: " + ex.Message);
            }
        }

        private void dataGridViewClientes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                
                int id = Convert.ToInt32(dataGridViewClientes.SelectedRows[0].Cells["Id"].Value);

               
                DialogResult result = MessageBox.Show("Deseja realmente excluir este registro?", "Confirmação", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                   
                    using (SqlConnection conn = new SqlConnection("Data Source=sqlexpress;Initial Catalog=CJ3028224PR2;User ID=aluno;Password=aluno"))
                    {
                        conn.Open();
                        string query = "DELETE FROM Clientes WHERE Id = @Id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }

            
                    dataGridViewClientes.Rows.RemoveAt(dataGridViewClientes.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha para excluir.");
            }
        }

        }
    }
    
