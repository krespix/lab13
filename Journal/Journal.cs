using System;
using System.Collections.Generic;

namespace Journal
{
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
}