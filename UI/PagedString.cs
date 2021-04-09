﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WikiBrowser.UI {
    public class PagedString {
        private const int PageLength = 15; // lines
        private const int LineLength = 50;
        private IEnumerable<string> _pages;
        public int CurrentPage;

        public PagedString(string body) {
            _pages = FromString(body);
        }

        public PagedString() {
            _pages = new List<string>();
        }


        public string Pages {
            get => _pages.ToString();
            set => _pages = FromString(value);
        }


        public string this[int i] => _pages.ElementAt(i); // handle errors on consumer side


        private static IEnumerable<string> FromString(string body) {
            var paragraphs = body.Split('\n');
            var lines = new List<string>();

            foreach (var paragraph in paragraphs) lines.AddRange(paragraph.Section(LineLength, " "));

            var parts = lines.Partition(PageLength);

            return parts.Select(part => string.Join("\n", part)).ToList();
        }

        public string GetPage() {
            if (_pages != null)
                return !_pages.Any() ? "" : _pages.ElementAt(CurrentPage);
            return "";
        }

        public int Count() {
            return _pages.Count();
        }

        public IEnumerator GetEnumerator() {
            return _pages.GetEnumerator();
        }
    }
}