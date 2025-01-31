namespace AtomicDynamics
{
    public class Interaction(Elementary elementary1, Elementary elementary2)
    {
        public Elementary Elementary1 = elementary1;
        public Elementary Elementary2 = elementary2;

        public override string ToString()
        {
            return $"{Elementary1}-{Elementary2}";
        }
    }
}