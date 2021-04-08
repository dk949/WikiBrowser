using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace WikiBrowser.CustomUIElement {
    public class PagedString {
        private const int PageLength = 2000; // characters
        private const int LineLength = 300;
        private IEnumerable<string> _pages;
        public int CurrentPage;

        public PagedString(string body) {
            _pages = FromString(body);
        }


        public string Pages {
            get => _pages.ToString();
            set => _pages = FromString(value);
        }


        public string this[int i] => _pages.ElementAt(i); // handle errors on consumer side


        private static IEnumerable<string> FromString(string body) {
            var lines = body.Section(LineLength, " ");
            var joinedLines = string.Join("\n", lines);
            var pages = joinedLines.Section(PageLength, " ");
            return pages;
        }

        public string GetPage() {
            ModContent.GetInstance<WikiBrowser>().Logger.Info("Current page = " + CurrentPage);
            ModContent.GetInstance<WikiBrowser>().Logger.Info("_pages.Count() = " + _pages.Count());
            if (_pages == null) ModContent.GetInstance<WikiBrowser>().Logger.Info("_pages has not been initialized");

            return !_pages.Any() ? "" : _pages.ElementAt(CurrentPage);
        }

        public int Count() {
            return _pages.Count();
        }

        public IEnumerator GetEnumerator() {
            return _pages.GetEnumerator();
        }
    }

    internal static class StringExtension {
        public static IEnumerable<string> Section(this string text, int charsPerPage, string breakChar) {
            var count = 0;
            var start = 0;
            while (count < text.Length) {
                count = Math.Min(text.Length, count + charsPerPage);
                if (count == text.Length) {
                    yield return text.Substring(start, count - start);
                } else {
                    var nextBreak = text.IndexOf(breakChar, count, StringComparison.Ordinal);
                    if (nextBreak == -1) {
                        yield return text.Substring(start, count - start);
                        start = count + breakChar.Length;
                    } else {
                        yield return text.Substring(start, nextBreak - start);
                        start = nextBreak + breakChar.Length;
                    }
                }
            }
        }
    }
}