using System;

namespace Framework.Test.Common.Helper
{
    public static class ParameterValidator
    {
        public static void ValidateNotNull([ValidatedNotNull]object toCheck, string name)
        {
            if (toCheck is null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class ValidatedNotNullAttribute: Attribute
    {
    }
}
