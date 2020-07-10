using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows;

namespace TODOit
{
    class DB
    {
        private List<Task> tasks = new List<Task>();
        public List<Task> Tasks { get => tasks; }
        private DataTable table; 
        private string GetTableSqlQuery = "SELECT * FROM [Tasks]";
        private string TextChangeSqlQuery1 = "UPDATE [Tasks] SET text = \"", TextChangeSqlQuery2 = "\" WHERE id = ";
        private string AddTaskSqlQuery = "INSERT INTO [Tasks] ('text') values ('";
        private SQLiteCommand command = new SQLiteCommand();
        private SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.db3;Version=3;");
        private SQLiteDataAdapter adapter;
        private void ConnectionOpen()
        {
            try
            {
                connection.Open();
            }
            catch(SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GetTable()
        {
            try
            {
                command.Connection = connection;
                command.CommandText = GetTableSqlQuery;
                adapter = new SQLiteDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void TaskChange(int id, string text)
        {
            if(tasks.Count - 1 < id)
            {
                return;
            }
            if(tasks[id].Text == text)
            {
                return;
            }
            tasks[id].Text = text;
            string SQLQuery = TextChangeSqlQuery1 + text + TextChangeSqlQuery2 + (id+1).ToString();
            try
            {
                command.CommandText = SQLQuery;
                command.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CompletedChange(int id, bool b)
        {
            tasks[id].IsCompleted = b;
            string SQLQuery = "UPDATE [Tasks] SET isCompleted = ";
            if (b)
                SQLQuery += 1;
            else
                SQLQuery += 0;
            SQLQuery += " WHERE id = " + (id + 1).ToString();
            command.CommandText = SQLQuery;
            try
            {
                command.ExecuteNonQuery();
            }
            catch(SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DeleteTask(int id)
        {
            tasks.RemoveAt(id);
            string SQLQuery = "DELETE FROM [Tasks] WHERE id = " + (id + 1).ToString();
            command.CommandText = SQLQuery;
            try
            {
                command.ExecuteNonQuery();
                for (int i = id + 2; i < tasks.Count + 2; i++)
                {
                    SQLQuery = "UPDATE [Tasks] SET id = " + (i - 1).ToString() + " WHERE id = " + i.ToString();
                    command.CommandText = SQLQuery;
                    command.ExecuteNonQuery();
                }
            }
            catch(SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void AddTask(string text)
        {
            tasks.Add(new Task()
            {
                Id = Tasks.Count,
                IsCompleted = false,
                Text = text
            });
            command.CommandText = AddTaskSqlQuery + text + "')";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TasksFill()
        {
            Task task;
            bool b;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][2].ToString() == "0")
                {
                    b = false;
                }
                else
                {
                    b = true;
                }
                task = new Task
                {
                    Id = i,
                    Text = table.Rows[i][1].ToString(),
                    IsCompleted = b
                };
                tasks.Add(task);
            }
        }
        public DB()
        {
            ConnectionOpen();
            GetTable();
            TasksFill();
        }
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}
