using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using RazorPages.Models;

namespace RazorPages.Pages.ProfileForms;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly string connectionString = "Data Source=Data/testDataBase.db";
 
    public IndexModel(ILogger<IndexModel> logger)
    {
        this._logger = logger;
    }

    public IList<Form> Form { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Form = new List<Form>();
        
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        
        var selectSql = "SELECT Id, firstName, age, lastName, job FROM Profiles";
        using var command = new SqliteCommand(selectSql, connection);
        using var reader = await command.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
        {
            Form.Add(new Form
            {
                Id = reader.GetInt32("Id"),
                firstName = reader.GetString("firstName"),
                age = reader.GetString("age"),
                lastName = reader.GetString("lastName"),
                job = reader.GetString("job")
            });
        }
    }
}