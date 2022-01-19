// a program that counts vowel words in sentence
string choise = "y";
char[] wovelWords = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'İ', 'O', 'U'};

while (choise == "y")
{
    List<char> vowels = new List<char>();
    Console.WriteLine("Type a sentence: ");
    string sentence = Console.ReadLine();
    if (sentence.Length < 0)
        break;
    string[] letters = sentence.Split(" ");
    foreach (string letter in letters)
    {
        foreach (var word in letter)
        {
            if (wovelWords.Contains(word) && !vowels.Contains(word))
                vowels.Add(word);
        }
    }
    if (vowels.Count < 1)
    {
        Console.WriteLine("There are no vowels");
    }
    else
    {
        Console.WriteLine("Vowels: ");
        foreach (char ch in vowels)
        {
            Console.WriteLine(ch);
        }
    }
    
    Console.WriteLine(@"Do you want to continue: (y/n)");
    choise = Console.ReadLine();
    while (choise != "y" && choise != "n")
    {
        Console.WriteLine("Please type 'y' or 'n' ");
        choise = Console.ReadLine();
    }
}

