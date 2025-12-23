using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using RazorPages.Models;
using System.Data;
using System.Text.Json;

namespace RazorPages.Pages.ProfileForms
{
    public class KendoGridModel : PageModel
    {
        private readonly string connectionString = "Data Source=Data/testDataBase.db";
        
        public IList<Form> Profiles { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Profiles = new List<Form>();
            
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            
            var selectSql = "SELECT Id, firstName, age, lastName, job FROM Profiles";
            using var command = new SqliteCommand(selectSql, connection);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                Profiles.Add(new Form
                {
                    Id = reader.GetInt32("Id"),
                    firstName = reader.GetString("firstName"),
                    age = reader.GetString("age"),
                    lastName = reader.GetString("lastName"),
                    job = reader.GetString("job")
                });
            }
        }

        public async Task<IActionResult> OnGetDataAsync()
        {
            var profiles = new List<Form>();
            
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            
            var selectSql = "SELECT Id, firstName, age, lastName, job FROM Profiles";
            using var command = new SqliteCommand(selectSql, connection);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                profiles.Add(new Form
                {
                    Id = reader.GetInt32("Id"),
                    firstName = reader.GetString("firstName"),
                    age = reader.GetString("age"),
                    lastName = reader.GetString("lastName"),
                    job = reader.GetString("job")
                });
            }
            
            return new JsonResult(profiles);
        }
    }
}