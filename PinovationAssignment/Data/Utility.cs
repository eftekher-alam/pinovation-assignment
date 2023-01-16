namespace PinovationAssignment.Data
{
    public class Utility : IUtility
    {
        private readonly IApplicationDbContext _context;

        public Utility(IApplicationDbContext context)
        {
            _context = context;
        }


        public int GetEvenId()
        {
            int Id = 0;
            int currentId = _context.GetMaxUserId();
        again:
            if (currentId % 2 == 0)
                Id = currentId + 2;
            else
                Id = currentId + 1;

            if (IsPrime(Id))
            {
                currentId = Id;
                goto again;
            }

            return Id;
        }


        public int GetOddId()
        {
            int Id = 0;
            int currentId = _context.GetMaxUserId();
        again:
            if (currentId % 2 == 0)
                Id = currentId + 1;
            else
                Id = currentId + 2;

            if (IsPrime(Id))
            {
                currentId = Id;
                goto again;
            }

            return Id;
        }


        public int GetPrimeId()
        {
            int Id = 0;
            int currentId = _context.GetMaxUserId();
        again:
            Id = currentId + 1;
            if (!IsPrime(Id))
            {
                currentId = Id;
                goto again;
            }

            return Id;
        }


        public bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            if (number == 2)
                return true;

            if (number % 2 == 0)
                return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
