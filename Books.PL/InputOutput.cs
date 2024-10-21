using Serilog;

namespace Books.PresentationLayer
{
    public class InputOutput : IInputOutput
    {
        public void DisplayResult()
        {
            throw new NotImplementedException();
        }

        public void SaveResult()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            Console.WriteLine("Please provide a file path to books list:");
            string userInput = ReadConsoleInput();
            string filePath = GetFilePath(userInput);
        }

        public string GetFilePath(string userInput)
        {
            if (!IsValidPath(userInput))
            {
                Log.Information("The path is not valid. Would you like to try again? (yes/no)");
                string userResonse = ReadConsoleInput();

                if (userResonse == "no")
                {
                    Environment.Exit(1);
                }
                else
                {
                    return ProvidePathAgain();
                }

            }

            if (!File.Exists(userInput))
            {
                Log.Information("The file is not existing. Would you like to try again? (yes/no)");
                string userResonse = ReadConsoleInput();

                if (userResonse == "no")
                {
                    Environment.Exit(1);
                }
                else
                {
                    return ProvidePathAgain();
                }
            }

            Log.Information($"The path is valid {userInput}");
            return userInput;
        }

        private bool IsValidPath(string path)
        {
            bool isValid = false;

            if (Path.IsPathRooted(path) && !string.IsNullOrEmpty(Path.GetFileName(path)))
            {
                isValid =  true;
            }
            
            return isValid;
        }

        private string ReadConsoleInput()
        {
            string userInput = Console.ReadLine().ToLower();
            return userInput;
        }

        private string ProvidePathAgain()
        {
            Console.WriteLine("Please provide a file path to books list:");
            string newFilePath = ReadConsoleInput();
            return GetFilePath(newFilePath);
        }
    }
}
