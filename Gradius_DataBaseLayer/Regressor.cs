using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gradius_DataBaseLayer
{
    public class Regressor
    {
        public string Id { get; set; }
        public string RegressorId { get; set; }
        public string Filter { get; set; }
        public string AppId { get; set; }

        public void LoadByFilter(string Filter)
        {
            Base.Init();
            var sql = "select * from regressor where filter = '" + Filter + "'";
         
            MySqlDataReader myReader = Base.select(sql);
            if (myReader.Read())
            {
                this.Id = Convert.ToString(myReader.GetValue(0));
                this.RegressorId = Convert.ToString(myReader.GetValue(1));
                this.Filter = myReader.GetString(2);
                this.AppId = myReader.GetString(3);
               
            }
            myReader.Close();
            Base.connection.Close();


        }

        public void LoadByHash(string Hash)
        {
            Base.Init();
            var sql = "select * from regressor where regressor_id = '" + Hash + "'";

            MySqlDataReader myReader = Base.select(sql);
            if (myReader.Read())
            {
                this.Id = Convert.ToString(myReader.GetValue(0));
                this.RegressorId = Convert.ToString(myReader.GetValue(1));
                this.Filter = myReader.GetString(2);
                this.AppId = myReader.GetString(3);

            }
            myReader.Close();
            Base.connection.Close();
        }

        public void Insert()
        {
            Base.Init();
            var sql = "INSERT INTO `regressor` (`id`, `regressor_id`, `filter`, `app_id`) VALUES (NULL, '" + this.RegressorId + "', '" + this.Filter + "', '" + this.AppId + "')";
            Base.sqlCommand(sql);
        }
    }
}
