namespace SequenceNM
{
    public class Item
    {
        public Item(int value, Item prev = null)
        {
            this.Value = value;
            this.Previous = prev;
        }

        public int Value { get; private set; }

        public Item Previous { get; set; }
    }
}
