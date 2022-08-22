using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Postieri.Models
{
    public class ExportData
    {
        SqlConnection connectDb = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=postieri_SampleDB;User ID=postieri_SampleDB;Password=DBSamplePW;Persist Security Info=True;");
        public DataTable GetRecord()
        {
            SqlCommand dbCommand = new SqlCommand("SELECT * FROM Orders", connectDb);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
