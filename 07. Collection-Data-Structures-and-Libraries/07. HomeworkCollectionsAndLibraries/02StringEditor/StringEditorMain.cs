using System;

namespace _02StringEditor
{
    public class StringEditorMain
    {
        public static void Main()
        {
            var editor = new StringEditor();
            string line = Console.ReadLine();
            while (line != "END")
            {
                string[] tokens = line.Split();
                string[] parameters = new string[0];
                switch (tokens[0])
                {
                    case "APPEND":
                        parameters = line.Split(new []{' '},2);
                        editor.Append(parameters[1]);
                        break;
                    case "INSERT":
                        parameters = line.Split(new[] { ' ' }, 3);
                        editor.Insert(int.Parse(parameters[1]), parameters[2]);
                        break;
                    case "DELETE":
                        editor.Delete(int.Parse(tokens[1]), int.Parse(tokens[2]));
                        break;
                    case "REPLACE":
                        parameters = line.Split(new[] { ' ' }, 4);
                        editor.Replace(int.Parse(parameters[1]), int.Parse(parameters[2]), parameters[3]);
                        break;
                    case "PRINT":
                        editor.Print();
                        break;
                }
                line = Console.ReadLine();
            }
        }
    }
}
