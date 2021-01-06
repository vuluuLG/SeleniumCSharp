using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Test.Common.Helper
{
    public static class ValidationHelper
    {
        [ThreadStatic]
        public static List<KeyValuePair<string, bool?>> validations;

        public static void AddValidation(KeyValuePair<string, bool?> addedValidations)
        {
            validations.Add(addedValidations);
        }

        public static void AddValidation(List<KeyValuePair<string, bool?>> addedValidations)
        {
            if (addedValidations != null)
            {
                foreach (var item in addedValidations)
                {
                    addedValidations.Add(item);
                }
            }
        }

        public static void AssertAll()
        {
            Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
            validations.Should().OnlyContain(validations => validations.Value ?? true).Equals(bool.TrueString);
        }

        public static string[] GetFailedValidations()
        {
            return validations.Where(x => x.Value != true).Select(x => $"[{x.Key}]").ToArray();
        }

        public static string ReportFailureOfValidationPoints(string verifiedPoint, string expectedValue, string actualValue)
        {
            string outMessage = verifiedPoint + " - Expected Value: " + expectedValue + " , Actual Value: " + actualValue;
            return outMessage;
        }

        public static string ReportExceptionInValidation(string verifiedPoint, Exception e)
        {
            string outMessage;
            if (e != null)
            {
                outMessage = verifiedPoint + " Failed With Exception - " + e.Message + " " + e.StackTrace;
            }
            else
            {
                outMessage = verifiedPoint + " Failed With Exception - Unknown";
            }
            return outMessage;
        }
    }
}
