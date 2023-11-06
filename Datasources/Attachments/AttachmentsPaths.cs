using System.IO;
namespace MRHE.Datasources.Attachments
{

        
              public static class AttachmentsPaths
        {
            public static string GetPath(string fileName)
            {
                return Path.GetFullPath(Path.Combine("Datasources", "Attachments", fileName));
            }
        }
    
    
}
