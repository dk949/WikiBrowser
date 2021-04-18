using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Main = Terraria.Main;

namespace WikiBrowser.Requests {
    public class StatRequest : Request {
        protected override string GetBody(string res) {
            return res.Split('&')[1];
        }

        protected override string GetTitle(string res) {
            return res.Split('&')[0];
        }

        public override void GetItem(Item item) {
            Task = Get(item);
        }

        public override void GetItem(string item) {
            // how to convert a string to an item??
            throw new System.NotImplementedException();
        }


        // Why?
        public Task<string> Get(Item item) {
            return System.Threading.Tasks.Task.Run(() => GetCrafting(item));
        }


        private static string GetUsedForCrafting(Item item) {
            var sb = new StringBuilder();
            var finder = new RecipeFinder();

            sb.AppendFormat("Items that can be crafted with {0}&\n", item.Name);
            finder.AddIngredient(item.type);
            foreach (var recipe in finder.SearchRecipes()) {
                sb.AppendRecipe(recipe);
            }

            if (sb.Length > 43) {
                sb.Remove(sb.Length - 43, 43);
            }

            sb.Append("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾[END]‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾\n");
            return sb.ToString();
        }

        private static string GetCrafting(Item item) {
            var sb = new StringBuilder();
            sb.AppendFormat("How to craft {0}&\n", item.Name);

            var finder = new RecipeFinder();
            finder.SetResult(item.type);
            foreach (var recipe in finder.SearchRecipes()) {
                sb.AppendRecipe(recipe);
            }

            if (sb.Length > 43) {
                sb.Remove(sb.Length - 43, 43);
            }

            sb.Append("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾[END]‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾\n");
            return sb.ToString();
        }
    }
}