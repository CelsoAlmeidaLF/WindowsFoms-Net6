using System.Data;
using System.Data.SqlClient;

namespace Almeida.Data.Interface
{
    public interface IDAO
    {
        SqlDataAdapter ExecDataAdapter(string sql, SqlParameter[] parametes = null, 
            CommandType commandType = CommandType.Text);
        SqlDataAdapter ExecDataAdapter(SqlCommand cmmd);
        SqlDataReader ExecDataReader(string sql, SqlParameter[] parametes = null, 
            CommandType commandType = CommandType.Text);
        SqlDataReader ExecDataReader(SqlCommand cmmd);
        DataSet ExecDataSet(string sql, SqlParameter[] parametes = null, 
            CommandType commandType = CommandType.Text);
        DataSet ExecDataSet(SqlCommand cmmd);
        DataTable ExecDataTable(string sql, SqlParameter[] parametes = null, 
            CommandType commandType = CommandType.Text);
        DataTable ExecDataTable(SqlCommand cmmd);
        object ExecDataScalar(string sql, SqlParameter[] parametes = null, 
            CommandType commandType = CommandType.Text);
        object ExecScalar(SqlCommand cmmd);

        SqlConnection Open();
        void Close();
    }
}