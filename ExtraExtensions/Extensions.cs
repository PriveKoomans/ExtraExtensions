namespace ExtraExtensions
{
    using System;
    using System.Drawing;
    using System.Net.Http;
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;

    public static class Extensions
    {
       
        /// <summary>
        /// Function to invert colors in the given bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        public static void Invert(this Bitmap bitmap)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    pixelColor = bitmap.GetPixel(x, y);
                    A = (byte)(255 - pixelColor.A);
                    R = pixelColor.R;
                    G = pixelColor.G;
                    B = pixelColor.B;
                    bitmap.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
        }

        /// <summary>
        /// Function to convert byte size to readable string
        /// </summary>
        /// <param name="size"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static string ToReadableString(this long size, int decimals)
        {
            string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            string sizeTemplate = "{0} {1}";
            int pos = 0;

            while (size >= 1024 && pos <= sizeSuffixes.Length)
            {
                pos++;
                size /= 1024;
            }

            return string.Format(sizeTemplate, Math.Round((double)size, decimals));
        }

        /// <summary>
        /// Function to initialize timespan class from given ticks
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this long ticks)
        {
            return TimeSpan.FromTicks(ticks);
        }

        /// <summary>
        /// Function to get webpage source
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetSource(this string url)
        {
            if (!url.Contains("http://") || !url.Contains("https://") || !url.Contains("ftp://") || !url.Contains("ftps://"))
                return null;

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.OK)
                    return response.Result.Content.ReadAsStringAsync().Result;
                else
                    return null;
            }
        }

        /// <summary>
        /// Function to convert string to byte array
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string text)
        {
            return Encoding.UTF32.GetBytes(text);
        }

        /// <summary>
        /// Function to convert byte array to string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToString(this byte[] bytes)
        {
            return Encoding.UTF32.GetString(bytes);
        }

        /// <summary>
        /// Function to serialize object to json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Function to deserialize json string to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object DeserializeJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
