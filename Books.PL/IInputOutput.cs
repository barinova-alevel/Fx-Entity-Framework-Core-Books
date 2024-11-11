
namespace Books.PresentationLayer
{
    public interface IInputOutput
    {
        string GetFilePath(string userInput);
        void DisplayResult();
        void SaveResult();
    }
}
