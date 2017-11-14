using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;

namespace PowerSourceControlApp
{
    class PowerSource
    {
        public string Server { get; }
        public List<Chanel> ChanelList;

        public PowerSource(string server)
        {
            Server = server;
            ChanelList = new List<Chanel>();

            var connectionString = new MySqlConnectionStringBuilder
            {
                Server = Server,
                UserID = "root",
                Password = "123",
                Database = "local_data_storage"
            };

            using (var connection = GetConnection(connectionstring: connectionString))
            {
                connection.Open();
                var chanels = connection.GetList<PowerSourceSettings>();
                connection.Close();

                foreach (var chanel in chanels)
                {
                    ChanelList.Add(new Chanel(chanel.Id, connectionString));
                }
            }

            foreach (var chanel in ChanelList)
            {
                chanel.Init();
            }
        }

        private static MySqlConnection GetConnection(MySqlConnectionStringBuilder connectionstring)
        {
            var connection = new MySqlConnection(connectionstring.ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return connection;
        }
    }
}