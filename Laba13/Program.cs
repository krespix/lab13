using System;
using System.Collections.Generic;
using Laba11;
using Laba12;

namespace Laba13
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            NewColl col1 = new NewColl("First", 10);
            NewColl col2 = new NewColl("Second", 10);
            
            //один объект Journal подписать на события CollectionCountChanged и CollectionReferenceChanged из первой коллекции
            Journal joun1 = new Journal();
            col1.CollectionCountChanged += joun1.CollectionCountChanged;
            col1.CollectionReferenceChanged += joun1.CollectionReferenceChanged;

            //второй объект Journal подписать на события CollectionReferenceChanged из обеих коллекций. 
            Journal joun2 = new Journal();
            col2.CollectionCountChanged += joun2.CollectionCountChanged;
            col2.CollectionReferenceChanged += joun2.CollectionReferenceChanged;

            Goods temp;
            temp = new Toy();
            col1.Add(temp);
            temp = new Product();
            col1.Add(temp);

            col1.Remove();
            
            col2.Add(temp);
            
            // journals
            Console.WriteLine("________ЖУРНАЛ 1 КОЛЛЕКЦИИ__________");
            joun1.Show();
            Console.WriteLine("________ЖУРНАЛ 2 КОЛЛЕКЦИИ__________");
            joun2.Show();

        }
        
    }
    
    //Записи для журнала
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

    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args); //   ДЕЛЕГАТ

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

    //Журнал в котором сохраняются все записи об изменениях в моей коллекции
    public class Journal
    {
        private List<JournalEntry> journal = new List<JournalEntry>();

        public void CollectionCountChanged(object sourse, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.NameCollection, e.ChangeCollection, e.Obj.ToString());
            journal.Add(je);
        }

        public void CollectionReferenceChanged(object sourse, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.NameCollection, e.ChangeCollection, e.Obj.ToString());
            journal.Add(je);
        }


        public void Show()
        {
            foreach (JournalEntry item in journal)
                Console.WriteLine(item + "\n");
        }
    }

    public class NewColl: Laba12.Queue
    {
        public Laba12.Queue Queue = new Laba12.Queue();
        string Name { get; set; }

        public override string ToString()
        {
            return Queue.ToString();
        }

        public NewColl()
        {
            Name = null;
        }

        public NewColl(string colName, int size)
        {
            Name = colName;
            Queue = new Laba12.Queue(size);
        }

        public new void Add(Goods item)
        {
            Queue.Enqueue(item);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(Name, "ДОБАВЛЕН", item));
        }

        public bool Remove()
        {
            if (Queue.capacity > 0)
            {
                OnCollectionCountChanged(this,
                    new CollectionHandlerEventArgs(Name, "УДАЛЕНИЕ", Queue.Dequeue()));
                
                return true;
            }

            return false;
        }

        public new Goods this[int index]
        {
            get => Queue[index];
            set
            {
                OnCollectionReferenceChanged(this,
                    new CollectionHandlerEventArgs(this.Name,
                        $"ПРИСВОЕНО НОВОЕ ЗНАЧЕНИЕ ЭЛЕМЕНТУ ({this})", $"[{index}] - {Queue[index]}"));
                Queue[index] = value;
            }
        }


        //происходит при добавлении нового элемента или при удалении элемента из коллекции
        public event CollectionHandler CollectionCountChanged;

        //объекту коллекции присваивается новое значение       
        public event CollectionHandler CollectionReferenceChanged;


        //обработчик события CollectionCountChanged
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionCountChanged != null)
                CollectionCountChanged(source, args);
        }

        //обработчик события OnCollectionReferenceChanged
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
        }
    }
}
