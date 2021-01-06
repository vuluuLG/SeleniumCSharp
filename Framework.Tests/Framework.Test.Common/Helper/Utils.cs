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

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Substring(0, Math.Min(maxLength, value.Length));
        }

        public static string GetNumberStringFromCurrency(string currency, string currencyFormat = "$")
        {
            if (string.IsNullOrEmpty(currency)) return currency;
            return currency.Replace(currencyFormat, "").Replace(",", "");
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
                        string convertedString = DateHelper.GetTargetDateString(DateTime.Now.AddDays(day), "yyyy-MM-dd", timeline, timelineFormat);
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

        public static JObject[] ToJObjects(this object value)
        {
            if (value != null)
            {
                return JsonHelper.Default.Deserialize<JObject[]>(value.ToString());
            }
            return null;
        }

        public static string Item(this JToken jObject, string itemName)
        {
            if (jObject != null)
            {
                var tokens = jObject.SelectTokens($"$..{itemName}");
                if (tokens.Any())
                    return tokens.ToArray().First().ToString();
            }
            return null;
        }

        public static T Item<T>(this JToken jObject, string itemName)
        {
            if (jObject != null)
            {
                var tokens = jObject.SelectTokens($"$..{itemName}");
                if (tokens.Any())
                    return tokens.ToArray().First().ToObject<T>();
            }
            return default(T);
        }

        public static T[] Items<T>(this JToken jObject, string itemName)
        {
            if (jObject != null)
            {
                var tokens = jObject.SelectTokens($"$..{itemName}");
                if (tokens.Any())
                    return tokens.Cast<T>().ToArray();
            }
            return default(T[]);
        }

        public static bool IsNumber(string value)
        {
            return int.TryParse(value, out int number);
        }

        public static T ToType<T>(this object value, string valueName = "Value")
        {
            try
            {
                return (T)Convert.ChangeType(value?.ToString(), typeof(T));
            }
            catch (Exception)
            {
                throw new Exception($"{valueName} ({typeof(T)}) was not in a correct format");
            }
        }
    }
}