using System;

namespace _02StringEditor
{
    using Wintellect.PowerCollections;

    public class StringEditor
    {
        private BigList<char> editor;

        public StringEditor()
        {
            this.editor = new BigList<char>();
        }

        public void Append(string text)
        {
            this.editor.AddRange(text);
            Console.WriteLine("OK");
        }

        public void Insert(int index, string text)
        {
            if (index < 0 || index >= this.editor.Count)
            {
                Console.WriteLine("ERROR");
                return;
            }
            this.editor.InsertRange(index,text);
            Console.WriteLine("OK");
        }

        public void Delete(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= this.editor.Count || startIndex + count >= this.editor.Count)
            {
                Console.WriteLine("ERROR");
                return;
            }
            this.editor.RemoveRange(startIndex,count);
            Console.WriteLine("OK");
        }

        public void Replace(int startIndex, int count, string substring)
        {
            if (startIndex<0 || startIndex >= this.editor.Count || startIndex+count >= this.editor.Count)
            {
                Console.WriteLine("ERROR");
                return;
            }
            this.editor.RemoveRange(startIndex,count);
            this.editor.InsertRange(startIndex,substring);
            Console.WriteLine("OK");
        }

        public void Print()
        {
            Console.WriteLine(string.Join("", this.editor));
        }
    }
}
