using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using RazorPages.Models;

namespace RazorPages.Pages.ProfileForms
{
    public class EditModel : PageModel
    {
        private readonly string connectionString = "Data Source=Data/testDataBase.db";

        [BindProperty]
        public Form Form { get; set; } = new Form();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var selectSql = "SELECT Id, firstName, age, lastName, job FROM Profiles WHERE Id = @id";
            using var cmd = new SqliteCommand(selectSql, connection);
            cmd.Parameters.AddWithValue("@id", id.Value);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                Form = new Form
                {
                    Id = reader.GetInt32("Id"),
                    firstName = reader.GetString("firstName"),
                    age = reader.GetString("age"),
                    lastName = reader.GetString("lastName"),
                    job = reader.GetString("job")
                };
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var updateSql = "UPDATE Profiles SET firstName = @firstName, age = @age, lastName = @lastName, job = @job WHERE Id = @id";
            using var cmd = new SqliteCommand(updateSql, connection);
            cmd.Parameters.AddWithValue("@firstName", Form.firstName ?? string.Empty);
            cmd.Parameters.AddWithValue("@age", Form.age ?? string.Empty);
            cmd.Parameters.AddWithValue("@lastName", Form.lastName ?? string.Empty);
            cmd.Parameters.AddWithValue("@job", Form.job ?? string.Empty);
            cmd.Parameters.AddWithValue("@id", Form.Id);

            var rows = await cmd.ExecuteNonQueryAsync();

            return RedirectToPage("./Index");
        }
    }
}
