namespace Journal
{
    public class JournalEntry
    {
        string NameCollection { get; set; }
        string ChangeCollection { get; set; }
        object Obj { get; set; }

        public JournalEntry()
        {
            NameCollection = null;
            ChangeCollection = null;
            Obj = default;
        }

        public JournalEntry(string colName, string changetype, object p)
        {
            NameCollection = colName;
            ChangeCollection = changetype;
            Obj = p;
        }

        public override string ToString()
        {
            return "Коллекция: " + NameCollection + ", " + ChangeCollection + " следующий элемент: " + Obj.ToString();
        }
    }
}