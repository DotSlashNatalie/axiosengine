using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axios.Engine.Data
{
#if USECUSTOMDATATABLE
    public class CollectionChangeEventArgs : EventArgs
    {
        private object _element;
        private CollectionChangeAction _action;
        public CollectionChangeEventArgs(CollectionChangeAction action, Object element)
        {

        }

        public object Element
        {
            get
            {
                return _element;
            }
            private set
            {
                _element = value;
            }
        }

        public CollectionChangeAction Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }
    }
#endif
}
