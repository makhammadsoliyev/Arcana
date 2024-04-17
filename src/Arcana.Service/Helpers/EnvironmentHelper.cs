namespace Arcana.Service.Helpers;

public static class EnvironmentHelper
{
    public static string WebRootPath { get; set; }

    static EnvironmentHelper()
    {
        WebRootPath = Path.GetFullPath("wwwroot");
    }
}
