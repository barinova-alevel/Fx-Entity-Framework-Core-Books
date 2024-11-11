using Books.BussinessLogicLayer.Services;
using Books.DataAccessLayer;
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
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
            var pathes = new List<string>();

            while (true)
            {
                Console.WriteLine("Would you like to add books? (yes/no)");
                string userResponce = Console.ReadLine().ToLower();

                if (userResponce == "no")
                {
                    Environment.Exit(1);
                }
                else if (userResponce != "yes")
                {
                    Log.Information("Invalid input. Please enter 'yes' or 'no'.");
                }
                else if (userResponce == "yes")
                {
                    Console.WriteLine("Please provide a file path to books list:");
                    string userInput = ReadConsoleInput();
                    string filePath = GetFilePath(userInput);
                    var csvService = new CsvService();
                    var bookRepository = new BookRepository(new ApplicationContext());

                    if (!IsFileWasRun(filePath, pathes))
                    {
                        var records = csvService.ParseCsv(filePath);
                        var books = csvService.GetBooksFromFile(records);
                        await bookRepository.AddRangeAsync(books);
                        Log.Information($"{books.Count} books have been added to db.");
                    }
                    else
                    {
                        Log.Information($"Books from {filePath} have already been retrieved.");
                        var records = csvService.ParseCsv(filePath);
                        var books = csvService.GetBooksFromFile(records);
                        await AddUniqueBooksAsync(books, bookRepository);
                    }
                    pathes.Add(filePath);
                }
             }
            //ensure the date in the CSV file is formatted correctly
        }
        
        public async Task AddUniqueBooksAsync(List<Book> fileBooks, BookRepository bookRepository)
        {
            List<Book> uniqueBooks = new List<Book>();
            using var context = new ApplicationContext();
            Log.Information("Checking presence of books that are not previously added to db.");

            foreach (var book in fileBooks)
            {
                bool exists = await context.Books
                    .Include(b => b.Author)
                    .AnyAsync(b => b.Title.ToLower().Trim() == book.Title.ToLower().Trim()
                        && b.Author.Name.ToLower().Trim() == book.Author.Name.ToLower().Trim());

                if (!exists)
                {
                    Log.Information($"{book.Title}, {book.Author.Name}");
                    uniqueBooks.Add(book);
                }
            }
            Log.Information($"{uniqueBooks.Count} books is adding.");
            await bookRepository.AddRangeAsync(uniqueBooks);
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

        private bool IsFileWasRun(string filePath, List<string> runPathes)
        {
            if (runPathes != null)
            {
                foreach (var runPath in runPathes)
                {
                    if (runPath.Equals(filePath))
                    {
                        return true;
                    }
                }
            }
            return false;
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
