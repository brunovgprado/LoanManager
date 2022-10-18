using System;

namespace LoanManager.Infrastructure.CrossCutting.Helpers
{
    public static class GuardClauses
    {
        /// <summary>
        /// Validates if an argument is null
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void IsNotNull(object argument, string argumentName)
        {
            if(argument is null)
            {
                throw new ArgumentNullException($"The argument {argumentName} is in a null state");
            }
        }
    }
}
