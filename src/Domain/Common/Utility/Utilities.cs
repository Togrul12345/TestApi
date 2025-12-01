using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Security.Claims;
namespace Domain.Common.Utility;
public static class Utilities
{
    public static string GetCultureLanguageName() => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

    public static int GetRandomInt(int start, int end)
    {
        Random rnd = new Random();
        return rnd.Next(start, end);
    }

    public static DateTime GetDateTimeNowByEnvironment()
    {
        bool isServer = Environment.GetEnvironmentVariable("IS_SERVER") == "true" || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
        return isServer ? DateTime.Now.AddHours(4) : DateTime.Now;
    }

    public static Guid GetCurrentUserId()
    {
        var httpContext = new HttpContextAccessor().HttpContext;
        if (httpContext?.User == null)
            return Guid.Empty;

        var userIdClaim = httpContext.User?.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            return Guid.Empty;
        return userId;
    }

    public static string GetAbsoluteUrl(this HttpRequest request, string relativePath)
    {
        return $"{request.Scheme}://{request.Host}{relativePath}";
    }

    public static async Task<string> SaveFileAsync(IFormFile file, string folder, string webRootPath)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var folderPath = Path.Combine(webRootPath, folder);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public static void DeleteFile(string fileName, string folderName, string webRootPath)
    {
        var filePath = Path.Combine(webRootPath, folderName, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
