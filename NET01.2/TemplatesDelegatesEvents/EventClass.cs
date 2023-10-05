namespace TemplatesDelegatesEvents
{
    class EventMatrixElementChangedArgs<T> : EventArgs
    {
        public int Row { get; }
        public int Col {  get; }
        public T OldValue { get; }
        public T NewValue { get; }

        public EventMatrixElementChangedArgs(int row, int col, T oldValue, T newValue)
        {
            Row = row;
            Col = col;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
