using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices
{
    public interface IGenericService
    {
        string? GetImagePath(string? newImgBase64, string? oldImgUrl, string folderName);
        string? GetImageFormat(string? filePath);
        bool ValidateEmail(string emailAddress);
        bool ValidatePhoneNumber(string phoneNumber);
        bool ValidatePostalCode(string postalCode);
        bool ValidateTime(string start, string end);
        bool ValidateDay(string day);
        int GetDayNumber(string day);
        bool ValidateDate(string start, string end);
        string GeneratePassword();
    }
}
