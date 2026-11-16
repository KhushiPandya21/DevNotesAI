using Microsoft.EntityFrameworkCore;
using DevNotesAI.Models;
using DevNotesAI.Services;
using DevNotesAI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevNotesAI.Controllers
{
    public class HomeController : Controller
    {

        private readonly GeminiService _geminiService;
        private readonly ApplicationDbContext _context;

        public HomeController(
            GeminiService geminiService,
            ApplicationDbContext context)
        {
            _geminiService = geminiService;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public async Task<IActionResult> GetAnalysisHistory()
        {
            var history = await _context.AnalysisHistories
                .OrderByDescending(h => h.CreatedAt)
                .Take(50)
                .Select(h => new
                {
                    h.HistoryId,
                    h.AnalysisType,
                    h.Language,
                    h.SourceCode,
                    h.Result,
                    h.CreatedAt
                })
                .ToListAsync();

            return Json(history);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExplainCode(
            [FromBody] CodeRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest(new
                {
                    message = "Please paste your code first."
                });
            }

            if (request.Language != "C#" &&
                request.Language != "Java" &&
                request.Language != "Python")
            {
                return BadRequest(new
                {
                    message = "Unsupported programming language."
                });
            }

            try
            {

                string explanation =
                    await _geminiService.ExplainCodeAsync(
                        request.Code,
                        request.Language);

                // Save the analysis in SQL Server
                var history = new AnalysisHistory
                {
                    AnalysisType = "Explain Code",
                    Language = request.Language,
                    SourceCode = request.Code,
                    Result = explanation
                };

                _context.AnalysisHistories.Add(history);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = explanation
                });

            }

            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Gemini error ({ex.GetType().Name}): {ex.Message}"
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AnalyzeFunctions(
            [FromBody] CodeRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest(new
                {
                    message = "Please paste your code first."
                });
            }

            if (request.Language != "C#" &&
                request.Language != "Java" &&
                request.Language != "Python")
            {
                return BadRequest(new
                {
                    message = "Unsupported programming language."
                });
            }

            try
            {

                string analysis =
                    await _geminiService.AnalyzeFunctionsAsync(
                        request.Code,
                        request.Language);

                var history = new AnalysisHistory
                {
                    AnalysisType = "Analyze Functions",
                    Language = request.Language,
                    SourceCode = request.Code,
                    Result = analysis
                };

                _context.AnalysisHistories.Add(history);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = analysis
                });

            }

            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Error ({ex.GetType().Name}): {ex.Message}"
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> GenerateDocumentation(
            [FromBody] CodeRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest(new
                {
                    message = "Please paste your code first."
                });
            }

            if (request.Language != "C#" &&
                request.Language != "Java" &&
                request.Language != "Python")
            {
                return BadRequest(new
                {
                    message = "Unsupported programming language."
                });
            }

            try
            {

                string documentation =
                    await _geminiService.GenerateDocumentationAsync(
                        request.Code,
                        request.Language);

                var history = new AnalysisHistory
                {
                    AnalysisType = "Generate Documentation",
                    Language = request.Language,
                    SourceCode = request.Code,
                    Result = documentation
                };

                _context.AnalysisHistories.Add(history);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = documentation
                });

            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Unable to generate documentation right now. Please try again."
                });
            }
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}
