using System;

using MySql.Data.MySqlClient;


namespace Nicebike
{
    public static class Db
    {
        public readonly static string connectionString = "server=pat.infolab.ecam.be;port=63309;database=dbNicebike;user=projet_gl;password=root;";

        public static void Opebn()
        {
            MySqlConnection connection = new(connectionString);
            connection.Open();
        }

    }

}