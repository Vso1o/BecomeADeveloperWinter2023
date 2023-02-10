using BecomeADeveloperWinter2023.TextHandler;

TextHandler textHandler = new TextHandler();

string filePath = Directory.GetCurrentDirectory() +  "\\TextInput.txt";

if (!File.Exists(filePath))
{
    Console.WriteLine("Wait for an exception!");
}

string text = File.ReadAllText(filePath);

textHandler.Text = text;

Console.WriteLine(textHandler.SolveTheProblem());