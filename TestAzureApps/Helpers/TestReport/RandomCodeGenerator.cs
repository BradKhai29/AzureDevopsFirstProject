using System.Text;

namespace DemoSeleniumAutomationTesting.Helpers.TestReport
{
    public class RandomCodeGenerator
    {
        public static string GetRandomCodeWithSpecifiedLength(int length)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randomIndexer = new Random();

            var randomChars = Enumerable
                .Repeat(element: characters, count: length)
                .Select(s => s[randomIndexer.Next(characters.Length - 1)])
                .ToArray();
            
            return new string(randomChars);
        }
    }
}