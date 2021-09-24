using Almeida.Data.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Almeida.Data
{
    public class DAO : IDAO
    {
        private SqlConnection _conn = null;
        private SqlCommand _cmmd = null;

        #region "Contrutor e Conexões"
        public DAO(string connectionString, int timeOut = 0)
        {
            _conn = new SqlConnection($@"Data Source = CELSO-DEV\SQLEXPRESS;
                                Initial Catalog = DB_ALMEIDA_DEV;
                                Integrated Security = True;
                                Connect Timeout = 30;
                                Encrypt = False;
                                TrustServerCertificate = False;
                                ApplicationIntent = ReadWrite;
                                MultiSubnetFailover = False");
            _cmmd = new SqlCommand();
            _cmmd.CommandTimeout = timeOut;
        }

        public SqlConnection Open()
        {
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            return _conn;
        }

        public void Close()
        {
            if (_conn.State == ConnectionState.Open)
                _conn.Close();
        }
        #endregion

        public SqlDataAdapter ExecDataAdapter(string sql, SqlParameter[] parameters = null, 
            CommandType commandType = CommandType.Text)
        {
            try
            {
                _cmmd.Parameters.Clear();

                _cmmd.CommandType = commandType;

                if (parameters != null)
                    _cmmd.Parameters.AddRange(parameters);

                _cmmd.CommandText = sql;

                _cmmd.Connection = Open();
                return new SqlDataAdapter(_cmmd);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }

        public SqlDataReader ExecDataReader(string sql, SqlParameter[] parameters = null,
            CommandType commandType = CommandType.Text)
        {
            try
            {
                _cmmd.Parameters.Clear();

                _cmmd.CommandType = commandType;

                if (parameters != null)
                    _cmmd.Parameters.AddRange(parameters);

                _cmmd.CommandText = sql;

                _cmmd.Connection = Open();
                return _cmmd.ExecuteReader();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }

        public SqlBulkCopy ExecSqlBulkCopy(string sql, SqlParameter[] parameters = null,
            CommandType commandType = CommandType.Text)
        {
            return null;
        }

        public DataSet ExecDataSet(string sql, SqlParameter[] parameters = null,
            CommandType commandType = CommandType.Text)
        {
            DataSet set = new DataSet();

            try
            {
                var da = ExecDataAdapter(sql, parameters, commandType).Fill(set);
                return set;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }

        public DataTable ExecDataTable(string sql, SqlParameter[] parameters = null,
            CommandType commandType = CommandType.Text)
        {
            DataTable dt = new DataTable();

            try
            {
                var da = ExecDataAdapter(sql, parameters, commandType).Fill(dt);
                return dt;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }

        public object ExecDataScalar(string sql, SqlParameter[] parameters = null,
            CommandType commandType = CommandType.Text)
        {
            try
            {
                _cmmd.Parameters.Clear();

                _cmmd.CommandType = commandType;

                if (parameters != null)
                    _cmmd.Parameters.AddRange(parameters);

                _cmmd.CommandText = sql;

                _cmmd.Connection = Open();
                return _cmmd.ExecuteScalar();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }

        #region

        public SqlDataReader ExecDataReader(SqlCommand cmmd)
        {
            cmmd.Connection = _conn;

            try
            {
                _conn.Open();
                return cmmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public DataSet ExecDataSet(SqlCommand cmmd)
        {
            DataSet Set = null;
            cmmd.Connection = _conn;

            try
            {
                _conn.Open();
                ExecDataAdapter(cmmd).Fill(Set);
                return Set;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public DataTable ExecDataTable(SqlCommand cmmd)
        {
            DataTable Dt = null;
            cmmd.Connection = _conn;

            try
            {
                _conn.Open();
                ExecDataAdapter(cmmd).Fill(Dt);
                return Dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public object ExecScalar(SqlCommand cmmd)
        {
            cmmd.Connection = _conn;

            try
            {
                _conn.Open();
                return cmmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public SqlDataAdapter ExecDataAdapter(SqlCommand cmmd)
        {
            try
            {
                return new SqlDataAdapter(cmmd);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Monta Parametro"

        protected void MontarParameter(int index, SqlParameter[] parameters,
            string parameterName, object value, SqlDbType sqlDbType)
        {
            parameters[index] = new SqlParameter(parameterName, value);
            parameters[index].SqlDbType = sqlDbType;
        }

        #endregion
    }
}