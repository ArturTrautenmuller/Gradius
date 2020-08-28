using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gradius_DataBaseLayer
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }

        public List<App> Apps { get; set; }

        public void LoadApps()
        {
            this.Apps = new List<App>();
            Base.Init();
            var sql = "select * from app where user_id = '" + this.Id + "'";
            MySqlDataReader myReader = Base.select(sql);
            while (myReader.Read())
            {
                App app = new App();
                app.Id = Convert.ToString(myReader.GetValue(0));
                app.UserId = myReader.GetString(1);
                app.Nome = myReader.GetString(2);

                this.Apps.Add(app);

            }

            myReader.Close();
            Base.connection.Close();
        }

        public void Load(string Id)
        {
            Base.Init();
            var sql = "select * from usuario where id = '" + Id + "'";
            MySqlDataReader myReader = Base.select(sql);
            if (myReader.Read())
            {
                this.Id = Convert.ToString(myReader.GetValue(0));
                this.Email = myReader.GetString(1);
                this.Senha = myReader.GetString(2);
                this.Nome = myReader.GetString(3);


                myReader.Close();
                Base.connection.Close();

            }

            myReader.Close();
            Base.connection.Close();

        }

        public void Auth(string Email, string Senha)
        {
            Base.Init();
            var sql = "select * from usuario where email = '" + Email + "' and senha = '" + Senha + "'";
            MySqlDataReader myReader = Base.select(sql);
            if (myReader.Read())
            {
                this.Id = Convert.ToString(myReader.GetValue(0));
                this.Email = myReader.GetString(1);
                this.Senha = myReader.GetString(2);
                this.Nome = myReader.GetString(3);


                myReader.Close();
                Base.connection.Close();

            }

            myReader.Close();
            Base.connection.Close();
        }

        public bool Existis()
        {
            Base.Init();
            var sql = "select * from usuario where email = '" + this.Email + "'";
            MySqlDataReader myReader = Base.select(sql);
            if (myReader.Read())
            {


                myReader.Close();
                Base.connection.Close();
                return true;

            }

            myReader.Close();
            Base.connection.Close();

            return false;
        }

        public void Insert()
        {
            Base.Init();
            var sql = "INSERT INTO `usuario` (`id`, `email`, `senha`,`nome`) VALUES (NULL, '" + this.Email + "', '" + this.Senha + "','"+this.Nome+"')";
            Base.sqlCommand(sql);
        }
    }
}
