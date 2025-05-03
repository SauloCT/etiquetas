using System;


namespace VendaERP.Core
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class KeyAttribute : Attribute
    {
        private string[] _cols;

        public KeyAttribute(params string[] columns)
        {
            this._cols = columns;
        }

        public virtual string[] Columns
        {
            get { return this._cols; }
        }
    }
}
