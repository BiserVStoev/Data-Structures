using System;

namespace _02TraverseDirectories
{
    public class File
    {
        public File(string name, long size)
        {
            this.Name = name;
            this.Size = size;
        }

        public string Name { get; private set; }

        public long Size { get; private set; }

        public override string ToString()
        {
            return String.Format(this.Name + " - " + this.Size/1024 + " KB");
        }
    }
}
