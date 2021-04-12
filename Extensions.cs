using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiBrowser {
    public static class Extensions {
        public static IEnumerable<string> Section(this string text, int charsPerSection, string breakChar) {
            var count = 0;
            var start = 0;
            while (count < text.Length) {
                count = Math.Min(text.Length, count + charsPerSection);
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

        // This is not very good, 10 heap allocations on a single line
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> items, int partitionSize) {
            return items.Select((item, inx) => new {item, inx})
                .GroupBy(x => x.inx / partitionSize)
                .Select(g => g.Select(x => x.item));
        }
    }
}