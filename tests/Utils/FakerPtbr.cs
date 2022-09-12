using Bogus;

namespace LoanManager.Tests.Utils
{
    public class FakerPtbr
    {
        public static Faker CreateFaker()
        {
            return new Faker("pt_BR");
        }
    }
}
