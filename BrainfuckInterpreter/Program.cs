if (args.Length == 0)
{
    throw new ArgumentException("Must provide path to program file as the first argument.");
}

var sourcePath = args[0];

var source = File.OpenText(sourcePath);
var buffer = source.ReadToEnd();

var memory = new int[30000];
var pointer = 0;

for (var i = 0; i < buffer.Length; i++)
{
    var token = buffer[i];

    int innerBrackets;
    switch (token)
    {
        case '>':
            pointer++;
            break;
            
        case '<':
            pointer--;
            break;
            
        case '+':
            memory[pointer]++;
            break;
            
        case '-':
            memory[pointer]--;
            break;
            
        case '.':
            var text = (char)memory[pointer];
            Console.Write(text);
            break;
            
        case ',':
            var input = Console.ReadLine() ?? string.Empty;
            var character = input.ToCharArray().ElementAtOrDefault(0);
            memory[pointer] = character;
            break;
            
        case '[':
            if (memory[pointer] != 0) break;
            innerBrackets = 0;
            for (var j = i + 1; j < buffer.Length; j++)
            {
                if (buffer[j] == ']' && innerBrackets == 0)
                {
                    i = j;
                    break;
                }
                else if (buffer[j] == ']' && innerBrackets > 0)
                {
                    innerBrackets--;
                }
                else if (buffer[j] == '[')
                {
                    innerBrackets++;
                }
            }
            break;
        
        case ']':
            if (memory[pointer] == 0) break;
            innerBrackets = 0;
            for (var j = i - 1; j >= 0; j--)
            {
                if (buffer[j] == '[' && innerBrackets == 0)
                {
                    i = j;
                    break;
                }
                else if (buffer[j] == '[' && innerBrackets > 0)
                {
                    innerBrackets--;
                }
                else if (buffer[j] == ']')
                {
                    innerBrackets++;
                }
            }
            break;
    }
}
