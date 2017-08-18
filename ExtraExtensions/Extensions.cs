﻿namespace ExtraExtensions
{
    using System;
    using System.Drawing;
    using System.Net.Http;
    using System.Net;

    public static class Extensions
    {
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

        public static string GetSource(this string url)
        {
            if (!url.Contains("http://") || !url.Contains("https://") || !url.Contains("ftp://") || !url.Contains("ftps://"))
                return null;

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    return response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
