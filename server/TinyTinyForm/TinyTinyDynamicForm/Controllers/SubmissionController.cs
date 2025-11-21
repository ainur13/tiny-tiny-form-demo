using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TinyTinyDynamicForm.Data;
using TinyTinyDynamicForm.Extensions;
using TinyTinyDynamicForm.Models;

namespace TinyTinyDynamicForm.Controllers;

[ApiController]
[Route("api/submissions")]
public class SubmissionController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SubmissionController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

        [HttpPost]
        public async Task<ActionResult<int>> Create()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            if(string.IsNullOrWhiteSpace(body)) 
                return BadRequest("No submision data");

            try { JsonDocument.Parse(body); }
            catch { return BadRequest("Invalid JSON format"); }

            var submission = new Submission(body);
            _dbContext.Submissions.Add(submission);
            _dbContext.SaveChanges();

            return Ok(submission.Id);
        }

    [HttpGet]
    public async Task<ActionResult<Submission[]>> Get(string? search = null)
    {
        var submissions = await _dbContext.Submissions.ToArrayAsync();
        if (string.IsNullOrWhiteSpace(search)) return Ok(submissions);

        var result = submissions.Where(x => x.RawJson.ContainsValue(search)).ToArray();
        return Ok(result);
    }
}
