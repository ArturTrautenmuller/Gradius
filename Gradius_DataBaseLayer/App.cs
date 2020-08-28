using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gradius_DataBaseLayer
{
    public class App
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Nome { get; set; }
        public List<Regressor> Regressors { get; set; }

        public void LoadRegressors()
        {
            this.Regressors = new List<Regressor>();
            Base.Init();
            var sql = "select * from regressor where app_id = '" + this.Id + "'";

            MySqlDataReader myReader = Base.select(sql);
            while (myReader.Read())
            {
                Regressor regressor = new Regressor();
                regressor.Id = Convert.ToString(myReader.GetValue(0));
                regressor.RegressorId = Convert.ToString(myReader.GetValue(1));
                regressor.Filter = myReader.GetString(2);
                regressor.AppId = myReader.GetString(3);

                this.Regressors.Add(regressor);

            }

            myReader.Close();
            Base.connection.Close();
        }

        public void Insert()
        {
            Base.Init();
            var sql = "INSERT INTO `app` (`id`, `user_id`, `nome`) VALUES (NULL, '"+this.UserId+"', '"+this.Nome+"')";
            Base.sqlCommand(sql);
        }
    }
}
