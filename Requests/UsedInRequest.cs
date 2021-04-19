using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WikiBrowser.Requests {
    internal class UsedInRequest : StatRequest {
        private const string Title = "Items that can be crafted with";

        protected override Task<string> Get(Item item) {
            return System.Threading.Tasks.Task.Run(() => GetUsedForCrafting(item));
        }


        private static string GetUsedForCrafting(Item item) {
            var sb = new StringBuilder();
            var finder = new RecipeFinder();

            sb.AppendFormat("{0} {1}&\n", Title, item.Name);
            finder.AddIngredient(item.type);
            foreach (var recipe in finder.SearchRecipes()) {
                sb.AppendRecipe(recipe);
            }

            if (sb.Length > (Title.Length + 43)) {
                sb.Remove(sb.Length - 43, 43);
            }

            sb.Append("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾[END]‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾\n");
            return sb.ToString();
        }
    }
}