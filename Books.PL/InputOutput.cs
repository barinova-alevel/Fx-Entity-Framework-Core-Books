using Books.BussinessLogicLayer.Services;
using Books.DataAccessLayer;
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer.Repositories;
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

        public async Task Run()
        {
            Console.WriteLine("Please provide a file path to books list:");
            string userInput = ReadConsoleInput();
            string filePath = GetFilePath(userInput);

            var csvService = new CsvService();
            var records = csvService.ParseCsv(filePath);
            var authors = csvService.GetListUniqueAuthors(records);
            var authorsMap = authors.ToDictionary(_ => _.Name);
            var genres = csvService.GetListUniqueGenres(records);
            var genresMap = genres.ToDictionary(_ => _.Name);
            var publishers = csvService.GetListUniquePublishers(records);
            var publishersMap = publishers.ToDictionary(_ => _.Name);
            var books = records
                .Select(_ => new Book()
                {
                    Title = _.Title,
                    ReleaseDate = _.ReleaseDate,
                    Pages = _.Pages,
                    Author = authorsMap[_.Author],
                    Genre = genresMap[_.Genre],
                    Publisher = publishersMap[_.Publisher]
                })
                .ToList();

            var bookRepository = new BookRepository(new ApplicationContext());
            await bookRepository.AddRangeAsync(books);
            //check if program has been run with same file path, then avoid duplicated entries. 
            //ensure the date in the CSV file is formatted correctly
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
                isValid = true;
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
