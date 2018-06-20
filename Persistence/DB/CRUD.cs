using System;
using System.Data;
using System.Data.SQLite;

namespace Persistence.DB
{
    internal static class CRUD          //Create, Read, Update, Delete
    {
        private const string cs = @"Data Source=DB\UAT.db; Version=3;";

        internal static void Read(string tableName, Action<SQLiteDataReader> callback)
        {
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM " + tableName, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            callback(rdr);
                        }
                    }
                }

                con.Close();
            }
        }

        internal static int Update(string commandText)
        {
            int result = 0;

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(commandText, con))
                {
                    result = cmd.ExecuteNonQuery();
                }

                con.Close();
            }

            return result;
        }
    }
}