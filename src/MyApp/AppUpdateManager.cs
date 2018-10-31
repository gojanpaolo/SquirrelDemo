using Squirrel;
using System.Threading.Tasks;

namespace MyApp
{
    static class AppUpdateManager
    {
        public static async Task<UpdateManager> GitHubUpdateManager() =>
            await UpdateManager.GitHubUpdateManager("https://github.com/gojanpaolo/SquirrelDemo");
    }
}
