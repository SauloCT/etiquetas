using System;


namespace VendaERP.Core
{
    public class IndexedColumnAttribute : Attribute
    {
        public string[] IndexedColumns { get; private set; }
        public IndexedColumnAttribute(params string[] Columns)
        {
            IndexedColumns = Columns;
        }
    }
}