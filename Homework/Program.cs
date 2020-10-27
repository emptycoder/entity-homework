using System.Threading.Tasks;

namespace Homework
{
	internal static class Program
	{
		private static async Task Main(string[] args)
		{
			await using var context = new HomeworkContext();
			await context.InitializeDb();
		}
	}
}
