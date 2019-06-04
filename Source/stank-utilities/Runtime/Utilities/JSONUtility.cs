using Newtonsoft.Json;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that handles manipulating JSON data.
    /// </summary>
    public static class JSONUtility
    {
        /// <summary>
        /// Formatting options for JSONUtility.
        /// </summary>
        public enum JSONFormatting
        {
            /// <summary>
            /// Applies no formatting to a JSON string.
            /// </summary>
            None = 0,

            /// <summary>
            /// Properly indents a JSON string.
            /// </summary>
            Indented = 1
        }

        /// <summary>
        /// Deserializes a JSON string into an object.
        /// </summary>
        /// <typeparam name="T">Type to convert JSON string to.</typeparam>
        /// <param name="json">JSON string to read.</param>
        /// <returns>Returns an object of type T from a JSON string.</returns>
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Serializes an object into a JSON string.
        /// </summary>
        /// <param name="value">Object to serialize.</param>
        /// <param name="formatting">Formatting of JSON string that will be returned.</param>
        /// <returns>Returns a JSON string that has been converted to a JSON string from an object.</returns>
        public static string SerializeObject(object value, JSONFormatting formatting = JSONFormatting.None)
        {
            // Return JSON string with indented formatting.
            if(formatting == JSONFormatting.Indented)
            {
                return JsonConvert.SerializeObject(value, Formatting.Indented);
            }
            else // Return JSON string with no formatting.
            {
                return JsonConvert.SerializeObject(value, Formatting.None);
            }
        }
    }
}
