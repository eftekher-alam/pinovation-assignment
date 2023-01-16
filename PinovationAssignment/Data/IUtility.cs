namespace PinovationAssignment.Data
{
    public interface IUtility
    {
        public int GetEvenId();
        public int GetOddId();
        public int GetPrimeId();
        public bool IsPrime(int number);
    }
}
