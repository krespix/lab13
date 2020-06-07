namespace Journal
{
    public class CollectionHandlerEventArgs : System.EventArgs
    {
        public string NameCollection { get; set; }
        public string ChangeCollection { get; set; }
        public object Obj { get; set; }

        public CollectionHandlerEventArgs()
        {
            NameCollection = null;
            ChangeCollection = null;
            Obj = default;
        }

        public CollectionHandlerEventArgs(string colName, string changetype, object p)
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