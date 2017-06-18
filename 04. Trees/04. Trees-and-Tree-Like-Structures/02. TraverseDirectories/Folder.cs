using System;
using System.Linq;
using System.Text;

namespace _02TraverseDirectories
{
    public class Folder
    {
        public Folder(string name)
        {
            this.Name = name;
            this.Files = null;
            this.Folders = null;
        }

        public string Name { get; private set; }

        public File[] Files { get; set; }

        public Folder[] Folders { get; set; }

        public void Print(int indent)
        {
            var indentString = new string(' ', indent);
            Console.WriteLine("{0}{1}", indentString, this.Name);
            foreach (var file in this.Files)
            {
                Console.WriteLine(indentString + " " + file.ToString());
            }
            foreach (var folder in this.Folders)
            {
                folder.Print(indent + 1);
            }
        }

        public long CalculateSize()
        {
            var totalSize = this.Files.Sum(x=>x.Size);
            foreach (var folder in this.Folders)
            {
                totalSize += folder.CalculateSize();
            }
            return totalSize;
        }
    }
}
