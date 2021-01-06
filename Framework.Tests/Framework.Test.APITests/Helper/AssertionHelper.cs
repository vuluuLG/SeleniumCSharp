using FluentAssertions;
using Framework.Test.Common.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Framework.Test.APITests.Helper
{
    public static class AssertionHelper
    {
        public static void ShouldBeEquivalentTo(this string actual, string expected, string because = "")
        {
            try
            {
                try
                {
                    JToken actualJson = JToken.Parse(actual);
                    JToken expectedJson = JToken.Parse(expected);
                    actualJson.Should().BeEquivalentTo(expectedJson, because);
                }
                catch (JsonReaderException)
                {
                    // if valie is not in JSON format, compare with origin string value
                    actual.Should().BeEquivalentTo(expected, because);
                }
            }
            catch (AssertFailedException ex)
            {
                throw new AssertFailedException(FormatErrorMessage(ex.Message));
            }
        }

        public static void ShouldBeEquivalentTo(this object actual, object expected, string because = "")
        {
            try
            {
                try
                {
                    JToken actualJson = JToken.Parse(actual.ToString());
                    JToken expectedJson = JToken.Parse(expected.ToString());
                    actualJson.Should().BeEquivalentTo(expectedJson, because);
                }
                catch (JsonReaderException)
                {
                    // if valie is not in JSON format, compare with origin object format
                    actual.Should().BeEquivalentTo(expected, because);
                }
            }
            catch (AssertFailedException ex)
            {
                throw new AssertFailedException(FormatErrorMessage(ex.Message));
            }
        }

        public static void ShouldBeEquivalentTo<T>(this string actual, object expected, string because = "")
        {
            T actualObject = JsonHelper.Default.Deserialize<T>(actual);
            actualObject.Should().BeEquivalentTo(expected, because);
        }

        public static void ShouldHaveItemBeEquivalentTo(this string actual, string expected, string itemName, string because = "")
        {
            JObject actualJson = actual.ToJObject();
            string actualValue = actualJson.SelectToken($"$..{itemName}").ToString();
            actualValue.Should().BeEquivalentTo(expected, because);
        }

        public static void ShouldHaveArrayItemBeEquivalentTo(this string actual, string[] expected, string itemName, string because = "")
        {
            JObject actualJson = actual.ToJObject();
            string[] actualValue = Array.Empty<string>();
            if (actualJson.SelectToken($"$..{itemName}") != null)
            {
                actualValue = actualJson.SelectToken($"$..{itemName}").ToObject<string[]>();
            }
            actualValue.Should().BeEquivalentTo(expected, because);
        }

        public static void ShouldHaveItemContain(this string actual, string expected, string itemName, string because = "")
        {
            JObject actualJson = actual.ToJObject();
            string actualValue = actualJson.SelectToken($"$..{itemName}").ToString();
            actualValue.Should().Contain(expected, because);
        }

        public static void ShouldHaveItemMatchRegex(this string actual, string regularExpression, string itemName, string because = "")
        {
            JObject actualJson = actual.ToJObject();
            string actualValue = actualJson.SelectToken($"$..{itemName}").ToString();
            actualValue.Should().MatchRegex(regularExpression, because);
        }

        public static void ShouldHaveItemNotBeNullOrEmpty(this string actual, string itemName, string because = "")
        {
            JObject actualJson = actual.ToJObject();
            string actualValue = actualJson.SelectToken($"$..{itemName}").ToString();
            actualValue.Should().NotBeNullOrEmpty(because);
        }

        public static string FormatErrorMessage(string errorMessage)
        {
            string formattedString = errorMessage;
            if (formattedString != null && formattedString.StartsWith("Expected JSON document") && formattedString.Contains("to be equivalent to"))
            {
                // format actual json
                formattedString = formattedString.Replace("Expected JSON document", "Expected the Actual response");
                // format expected json
                formattedString = formattedString.Replace("to be equivalent to", "to be equivalent to the Expected response");
            }
            return formattedString;
        }
    }
}
