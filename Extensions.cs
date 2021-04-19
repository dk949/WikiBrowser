using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using WikiBrowser.Requests;

namespace WikiBrowser {
    internal static class Extensions {
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


        public static void AppendRecipe(this StringBuilder sb, Recipe recipe) {
            sb.AppendFormat("{0} can be crafted [at] ", recipe.createItem.Name);
            var stationNeeded = false;
            foreach (var tile in recipe.requiredTile) {
                if (tile == -1) break;
                sb.AppendFormat("{0} + ", Helpers.TileFromId(tile));
                stationNeeded = true;
            }

            if (stationNeeded) {
                sb.Remove(sb.Length - 2, 2);
            } else {
                sb.Remove(sb.Length - 5, 5);
                sb.Append("by Hand ");
            }

            sb.Append(" :: you'll need: ");


            foreach (var ingredient in recipe.requiredItem) {
                if (ingredient.Name == "") break;

                sb.AppendFormat("{1} {0} + ", ingredient.Name, ingredient.stack.ToString());
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾\n");
        }
    }
}