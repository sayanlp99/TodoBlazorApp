using System.Data;
using System.Data.SqlClient;
using TodoBlazorApp.Components.Models;

namespace TodoBlazorApp.Components.Services
{
    public static class TodoService
    {
        static string connectionString = "Server=192.168.10.10;Database=TodoDb;User Id=developer2;Password=Developer@2;";

        public static void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public static List<Todo> GetAllTodos() 
        {
            List<Todo> todos = new List<Todo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetAllTodos", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Todo todo = new Todo();
                    todo.id = Convert.ToInt16(reader["id"]);
                    todo.title = Convert.ToString(reader["title"]);
                    todo.description = Convert.ToString(reader["description"]);
                    todo.isCompleted = Convert.ToBoolean(reader["isCompleted"]);
                    todo.dateTime = Convert.ToDateTime(reader["dateTime"]);

                    todos.Add(todo);
                }
            }
            return todos;
        }
        public static void InsertTodo(Todo todo)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@title", todo.title),
                new SqlParameter("@description", todo.description),
                new SqlParameter("@isCompleted", todo.isCompleted),
                new SqlParameter("@dateTime", todo.dateTime)
            };
            ExecuteStoredProcedure("InsertTodo", parameters);
            Console.WriteLine("Todo inserted successfully.");
        }

        public static void DeleteTodo(int todoId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@id", todoId)
            };
            ExecuteStoredProcedure("DeleteTodo", parameters);
            Console.WriteLine("Todo deleted successfully.");
        }

        public static void ToggleTodo(int todoId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@id", todoId)
            };
            ExecuteStoredProcedure("ToggleTodo", parameters);
            Console.WriteLine("Todo toggled successfully.");
        }

    }
}

