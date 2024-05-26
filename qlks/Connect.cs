using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace qlks
{
    internal class Connect
    {
        private string connect = @"Data Source=DESKTOP-LD8D1MH\DUNGSQL;Initial Catalog=quanlykhachsan;Integrated Security=True";
        private SqlConnection sqlConnection = new SqlConnection();

        public Connect()
        {
            try
            {
                sqlConnection.ConnectionString = connect;
            }
            catch (SqlException)
            {
                MessageBox.Show("Không kết nối được với Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void ExecuteNonQuery(string query)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public int ExecuteScalar(string query)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int count = (int)sqlCommand.ExecuteScalar();
            sqlConnection.Close();
            return count;
        }

        public void QueryData(string query, DataGridView dataGridView)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            dataGridView.DataSource = dataTable;
        }

        public DataTable DataTable(string query)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
}
