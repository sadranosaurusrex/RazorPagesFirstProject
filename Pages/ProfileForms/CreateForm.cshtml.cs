using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Models;

namespace RazorPages.Pages.ProfileForms
{
    public class CreateFormModel : PageModel
    {
        [BindProperty]        
        public Form Form { get; set; } = default!;

        private readonly string connectionString = "Data Source=Data/testDataBase.db";
        
        private readonly string createTableSql = @"
        CREATE TABLE IF NOT EXISTS Profiles (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        firstName TEXT NOT NULL,
        age TEXT NOT NULL,
        lastName TEXT NOT NULL,
        job TEXT NOT NULL
        );";
        
        private readonly string insertSql = @"
        INSERT INTO Profiles(firstName, age, lastName, job)
        VALUES (@firstName, @age, @lastName, @job);";

        public CreateFormModel()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(createTableSql, connection);
            command.ExecuteNonQuery();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@firstName", Form.firstName ?? "");
            command.Parameters.AddWithValue("@age", Form.age ?? "");
            command.Parameters.AddWithValue("@lastName", Form.lastName ?? "");
            command.Parameters.AddWithValue("@job", Form.job ?? "");
            
            await command.ExecuteNonQueryAsync();

            return RedirectToPage("./Index");
        }
    }
}