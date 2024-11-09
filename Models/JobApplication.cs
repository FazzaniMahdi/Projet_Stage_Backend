using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace jobHosting.Models
{
    public class JobApplication
    {
        /*Id(Primary Key)
        ApplicantId(Foreign Key to Users table)
        FileName(e.g., NVARCHAR(255)) - Stores the original name of the file.
        FileType(e.g., NVARCHAR(50)) - Stores the file type, like "application/pdf" or "image/jpeg."
        FileData(e.g., VARBINARY(MAX)) - Stores the actual binary data of the file.*/
    }
}
