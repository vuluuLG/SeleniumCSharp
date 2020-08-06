using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace Framework.Test.Common.Helper
{
    public static class Utils
    {
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

        public static string GetRandomValue(string value)
        {
            if (value != null)
            {
                value = string.Format("{0}_{1}{2}", value.Replace(' ', '_'), DateTime.Now.ToString("yyyyMMddHHmmssffff"), Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                value = string.Format("Unknown_{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmssffff"), Thread.CurrentThread.ManagedThreadId);
            }

            return value;
        }

        public static string GetProjectPath(bool includeBinFolder = false)
        {
            //string path_old = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
            string path = Directory.GetCurrentDirectory() + "\\";
            string actualPath = path;
            if (!includeBinFolder)
            {
                actualPath = path.Substring(0, path.LastIndexOf("bin"));
            }
            string projectPath = new Uri(actualPath).LocalPath; // project path of your solution
            return projectPath;
        }

        public static string ImageToBase64(string imagePath)
        {
            string base64String;
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public static IMarkup MarkupJsonString(this string data)
        {
            return MarkupHelper.CreateCodeBlock(data, CodeLanguage.Json);
        }

        public static string MaskSensitiveJsonData(this string data, string[] sensitiveList, string mask = "********")
        {
            JObject jObject = JObject.Parse(data);

            if (jObject != null && sensitiveList != null)
            {
                foreach (var item in sensitiveList)
                {
                    jObject.SelectTokens($"$..{item}").ToList().ForEach(x => x.Replace(mask));
                }
                return jObject.ToString();
            }
            return data;
        }

        public static string ConvertObjectToJson(this object dataObject)
        {
            return JsonConvert.SerializeObject(dataObject);
        }

        public static string ToDateTimeString(this DateTime date, string dateFormat = "yyyy-MM-dd")
        {
            return date.ToString(dateFormat);
        }

        public static T ToEnumValue<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException($"{type} is not Enum");
            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new { Field = f, Att = a })
                            .Where(a => ((DescriptionAttribute)a.Att)
                                .Description == description).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }

        public static string ToDescription<T>(this T value)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static string ToEnumName<T>(this T value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static Dictionary<string, string> ToDictionary(this object value)
        {
            if (value != null)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                var list = value.ToJObject();
                foreach (var item in list)
                {
                    dict.Add(item.Key, item.Value.ToString());
                }
                return dict;
            }
            else return null;
        }

        public static string ToMemberDescription(this MemberInfo value)
        {
            if (value != null)
            {
                try
                {
                    return value.GetCustomAttribute<DescriptionAttribute>().Description;
                }
                catch (Exception)
                {
                    return value.Name;
                }
            }

            return null;
        }

        public static object GetPropertyValue(object data, string propertyName)
        {
            if (propertyName != null)
            {
                foreach (var prop in propertyName.Split('.').Select(s => data.GetType().GetProperty(s)))
                    data = prop.GetValue(data, null);
                return data;
            }

            return null;
        }

        public static string MarkupTestCategory(string category)
        {
            if (string.IsNullOrEmpty(category)) return category;
            return category.Replace(" ", "_");
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Substring(0, Math.Min(maxLength, value.Length));
        }

        public static string GetCurrentDateString(string dateFormat, string timeLine = null, string timeLineFormat = null)
        {
            DateTime currentDate = DateTime.Now;

            return GetTargetDateString(currentDate, dateFormat, timeLine, timeLineFormat);
        }

        public static string GetNextDateString(string dateFormat, string timeLine = null, string timeLineFormat = null)
        {
            return GetNextNDateString(dateFormat, timeLine, timeLineFormat, 1);
        }

        public static string GetNextNDateString(string dateFormat, string timeLine = null, string timeLineFormat = null, int addDays = 0)
        {
            DateTime nextDate = DateTime.Now.AddDays(addDays);

            return GetTargetDateString(nextDate, dateFormat, timeLine, timeLineFormat);
        }

        public static string GetTargetDateString(DateTime targetDate, string dateFormat, string timeLine = null, string timeLineFormat = null)
        {
            DateTime currentDate = targetDate;

            if (timeLine != null)
            {
                DateTime timeLineDate;

                if (timeLineFormat != null)
                {
                    timeLineDate = DateTime.ParseExact(timeLine, timeLineFormat, null);
                }
                else
                {
                    timeLineDate = DateTime.ParseExact(timeLine, dateFormat, null);
                }

                if (DateTime.Compare(currentDate, timeLineDate) < 0)
                {
                    return timeLineDate.ToString(dateFormat);
                }
            }

            return currentDate.ToString(dateFormat);
        }

        public static string GetNumberStringFromCurrency(string currency)
        {
            if (string.IsNullOrEmpty(currency)) return currency;
            return currency.Replace("$", "").Replace(",", "");
        }

        public static string ConvertDynamicData(object data)
        {
            if (data != null)
            {
                string convertedData = data.ToString();
                if (Regex.IsMatch(convertedData, @"@{next\d+day}"))
                {
                    do
                    {
                        var groups = (Regex.Match(convertedData, @"@{next(\d+)day}")).Groups;
                        int day = int.Parse(groups[1].Value);
                        string convertedString = DateTime.Now.AddDays(day).ToString("yyyy-MM-dd");
                        convertedData = convertedData.Replace(groups[0].Value, convertedString);
                    }
                    while (Regex.IsMatch(convertedData, @"@{next\d+day}"));
                }
                if (Regex.IsMatch(convertedData, @"@{next\d+day:timeline<.+:.+>}"))
                {
                    do
                    {
                        var groups = (Regex.Match(convertedData, @"@{next(\d+)day:timeline<(.+):(.+)>}")).Groups;
                        int day = int.Parse(groups[1].Value);
                        string timeline = groups[2].Value;
                        string timelineFormat = groups[3].Value;
                        string convertedString = GetTargetDateString(DateTime.Now.AddDays(day), "yyyy-MM-dd", timeline, timelineFormat);
                        convertedData = convertedData.Replace(groups[0].Value, convertedString);
                    }
                    while (Regex.IsMatch(convertedData, @"@{next\d+day:timeline<.+:.+>}"));
                }

                return convertedData;
            }

            return null;
        }

        public static JObject ToJObject(this object value)
        {
            if (value != null)
            {
                return JsonHelper.Default.Deserialize<JObject>(value.ToString());
            }
            return null;
        }

        public static string Item(this JObject jObject, string item)
        {
            if (jObject != null)
            {
                var token = jObject.SelectToken(item);
                if (token != null)
                    return token.ToString();
            }
            return null;
        }

        public static T Item<T>(this JObject jObject, string item)
        {
            if (jObject != null)
            {
                var token = jObject.SelectToken(item);
                if (token != null)
                    return token.ToObject<T>();
            }
            return default(T);
        }

        public static void CopyFile(string inputFilePath, string outputFilePath)
        {
            int bufferSize = 1024 * 1024;
            using (FileStream input = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (FileStream output = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    output.SetLength(input.Length);
                    int bytesRead = -1;
                    byte[] bytes = new byte[bufferSize];
                    while ((bytesRead = input.Read(bytes, 0, bufferSize)) > 0)
                    {
                        output.Write(bytes, 0, bytesRead);
                    }
                }
            }
        }

        public static string ReadAllTextFromFile(string filePath)
        {
            string contents = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    contents = sr.ReadToEnd();
                }
            }
            return contents;
        }

        public static void WriteAllTextToFile(string filePath, string contents)
        {
            if (contents != null)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(contents);
                        fs.SetLength(contents.Length);
                    }
                }
            }
        }
    }
}