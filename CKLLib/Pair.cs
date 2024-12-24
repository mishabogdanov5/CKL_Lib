namespace CKLLib
{
    public class Pair
    {
        public object FirstValue { get; set; }
        public object SecondValue { get; set; }

        public Pair(object firstValue, object secondValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Pair) return false;

            Pair? pair = obj as Pair;
            if (pair == null) return false;

            return FirstValue.Equals(pair.FirstValue) && SecondValue.Equals(pair.SecondValue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstValue, SecondValue);
        }

        public override string ToString()
        {
            return $"({FirstValue.ToString()};{SecondValue.ToString()})";
        }
    }
}
