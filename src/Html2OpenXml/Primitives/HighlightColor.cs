/* Copyright (C) Olivier Nizet https://github.com/onizet/html2openxml - All Rights Reserved
 * 
 * This source is subject to the Microsoft Permissive License.
 * Please see the License.txt file for more information.
 * All other rights reserved.
 * 
 * THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 */

using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace HtmlToOpenXml.Primitives
{
    /// <summary>
    /// Represents an highlight color.
    /// </summary>
    struct HighlightColor
    {
        private static Dictionary<HighlightColorValues, HtmlColor> knownColors;

        /// <summary>
        /// Parses a given color to an OpenXML HighlightColorValues
        /// </summary>
        public static HighlightColorValues Parse(String color)
        {
            if (!String.IsNullOrWhiteSpace(color))
                return HighlightColorValues.None;

            var htmlColor = HtmlColor.Parse(color);

            return Parse(htmlColor);
        }

        /// <summary>
        /// Parses a given color to an OpenXML HighlightColorValues
        /// </summary>
        public static HighlightColorValues Parse(HtmlColor htmlColor)
        {
            if (htmlColor.Equals(HtmlColor.Empty))
                return HighlightColorValues.None;

            if (knownColors == null)
                InitKnownColors();

            // Get nearest color
            int rgbDistance = int.MaxValue; // max distance of the 4 channels
            var highlightColorValue = HighlightColorValues.None;

            foreach (var highlightColor in knownColors)
            {
                var distance = CalculateRgbDistance(htmlColor, highlightColor.Value);

                if (distance < rgbDistance)
                {
                    rgbDistance = distance;
                    highlightColorValue = highlightColor.Key;
                }
            }

            return highlightColorValue;
        }

        /// <summary>
        /// Calculates the distance to a given color
        /// </summary>
        private static int CalculateRgbDistance(HtmlColor myColor, HtmlColor checkColor)
        {
            // https://stackoverflow.com/a/1847112
            return (int) (Math.Pow((myColor.R - checkColor.R) * 0.30, 2)
                    + Math.Pow((myColor.G - checkColor.G) * 0.59, 2)
                    + Math.Pow((myColor.B - checkColor.B) * 0.11, 2));
        }

        // https://www.colorhexa.com
        private static void InitKnownColors()
        {
            knownColors = new Dictionary<HighlightColorValues, HtmlColor>();
            knownColors.Add(HighlightColorValues.Black,     HtmlColor.FromArgb(0, 0, 0));
            knownColors.Add(HighlightColorValues.Blue,      HtmlColor.FromArgb(0, 0, 255));
            knownColors.Add(HighlightColorValues.Cyan,      HtmlColor.FromArgb(0, 255, 255));
            knownColors.Add(HighlightColorValues.DarkBlue,  HtmlColor.FromArgb(0, 0, 139));
            knownColors.Add(HighlightColorValues.DarkCyan,  HtmlColor.FromArgb(0, 139, 139));
            knownColors.Add(HighlightColorValues.DarkGray,  HtmlColor.FromArgb(169, 169, 169));
            knownColors.Add(HighlightColorValues.DarkGreen, HtmlColor.FromArgb(0, 100, 0));
            knownColors.Add(HighlightColorValues.DarkMagenta, HtmlColor.FromArgb(139, 0, 139));
            knownColors.Add(HighlightColorValues.DarkRed,   HtmlColor.FromArgb(139, 0, 0));
            knownColors.Add(HighlightColorValues.DarkYellow, HtmlColor.FromArgb(139, 0, 139));
            knownColors.Add(HighlightColorValues.Green,     HtmlColor.FromArgb(0, 255, 0));
            knownColors.Add(HighlightColorValues.LightGray, HtmlColor.FromArgb(211, 211, 211));
            knownColors.Add(HighlightColorValues.Magenta,   HtmlColor.FromArgb(255, 0, 255));
            knownColors.Add(HighlightColorValues.Red,       HtmlColor.FromArgb(255, 0, 0));
            knownColors.Add(HighlightColorValues.White,     HtmlColor.FromArgb(255, 255, 255));
            knownColors.Add(HighlightColorValues.Yellow,    HtmlColor.FromArgb(255, 255, 0));
        }
    }
}