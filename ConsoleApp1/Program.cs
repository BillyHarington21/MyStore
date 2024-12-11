using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Строка подключения
        string connectionString = "Server=WIN-7L0UJ4EFJQC\\MSSQLSERVER2022;Database=MyStoreDb;Trusted_Connection=True;";

        // Проверка подключения
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Открытие подключения
                Console.WriteLine("Подключение к базе данных успешно установлено.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при подключении к базе данных: {ex.Message}");
        }
    }
}
