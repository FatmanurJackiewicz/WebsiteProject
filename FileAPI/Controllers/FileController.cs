using FileAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Invalid File");
        }

        var folderName = Path.Combine("Resources", "AllFiles");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (!Directory.Exists(pathToSave))
        {
            Directory.CreateDirectory(pathToSave);
        }
        var fileName = file.FileName;
        var fullPath = Path.Combine(pathToSave, fileName);
        var dbPath = Path.Combine(folderName, fileName);

        if (System.IO.File.Exists(fullPath))
        {
            return BadRequest("File already exists!");
        }

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        // Dosya URL'ini oluştur
        var urlToDownload = Url.Action("DownloadByName", "File", new { name = fileName }, Request.Scheme);

        return Ok(new { Url = urlToDownload });
    }


    [HttpPost("multipleUpload"), DisableRequestSizeLimit]
    public async Task<IActionResult> MultipleUploadFile([FromForm] MultipleUploadModel model)
    {


        var response = new Dictionary<string, string>();
        if (model.Files == null || model.Files.Count == 0)
        {
            return BadRequest("Invalid File");
        }

        foreach (var file in model.Files)
        {
            var folderName = Path.Combine("Resources", "AllFiles");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = file.FileName;
            //c://resources/repos/allfiles/fileName.jpg
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            if (!System.IO.File.Exists(fullPath))
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                await System.IO.File.WriteAllBytesAsync(fullPath, memoryStream.ToArray());
                response.Add(fileName, dbPath);
            }
            else
            {
                response.Add(fileName, "Already exists.");
            }
        }
        return Ok(new { response });
    }

    [HttpGet("download/{name}")]
    public async Task<IActionResult> DownloadByName(string name)
    {
        var folderName = Path.Combine("Resources", "AllFiles");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        var fileName = name;
        var fullPath = Path.Combine(pathToSave, fileName);

        if (!System.IO.File.Exists(fullPath))
        {
            return BadRequest("file not exists");
        }
        var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
        var fileContentResult = new FileContentResult(fileBytes, "application/octet-stream")
        {
            FileDownloadName = fileName,
        };
        return fileContentResult;

    }

    [HttpGet("fileUrl/{fileName}")]
    public IActionResult GetFileUrl(string fileName)
    {
        var folderName = "Resources/AllFiles";
        var urlToDownload = Url.Action("DownloadByName", "File", new { name = fileName }, Request.Scheme);
        return Ok(new { Url = urlToDownload });
    }

}
