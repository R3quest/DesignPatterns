using lljubici1_zadaca_2.Composite;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Iterator
{
    public class ConcreateIterator : IIterator
    {
        //https://www.dofactory.com/net/iterator-design-pattern

        private List<IRasporedProgramaComponent> _collection;
        private int _current = 0;
        private int _step = 1;

        // Constructor

        public ConcreateIterator(List<IRasporedProgramaComponent> collection)
        {
            this._collection = collection;
        }

        // Gets first item

        public IRasporedProgramaComponent First()
        {
            _current = 0;
            return _collection[_current] as IRasporedProgramaComponent;
        }

        // Gets next item

        public IRasporedProgramaComponent Next()
        {
            _current += _step;
            if (!IsDone)
                return _collection[_current] as IRasporedProgramaComponent;
            else

                return null;
        }

        // Gets or sets stepsize

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }

        // Gets current iterator item

        public IRasporedProgramaComponent CurrentItem
        {
            get { return _collection[_current] as IRasporedProgramaComponent; }
        }

        // Gets whether iteration is complete

        public bool IsDone
        {
            get { return _current >= _collection.Count; }
        }
    }
}
