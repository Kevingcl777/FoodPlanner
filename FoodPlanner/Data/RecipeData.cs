namespace Fooddaily.Portable.Data
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fooddaily.Portable.Models;
    using Newtonsoft.Json;
    using Fooddaily.Portable.Helpers;
    using Fooddaily.Portable.Data;

    public static class RecipeData
    {
        private static Random random;

        public static Result.Recipe GetRandomRecipe()
        {
            //var output = Newtonsoft.Json.JsonConvert.SerializeObject(Monkeys);
            return Recipes[random.Next(Data.Constants.ZERO, Recipes.Count)];
        }

        public static ObservableCollection<Grouping<string, Result.Recipe>> RecipesGrouped { get; set; }

        public static ObservableCollection<Result.Recipe> Recipes { get; set; }

        public static async Task GetRecipe(string textInput,
            ObservableCollection<Result.RecipeGroup> rgl)
        {
            App.IT.IssueLogger(Data.Constants.SEARCHTEXT + textInput);
            string response;
            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync(
                    $"https://api.edamam.com/search?q={textInput}&app_id={Constants.AppId}&app_key={Constants.AppKey}&from=0&to=35&calories=gte%20591,%20lte%20722");
            }

            var result = JsonConvert.DeserializeObject<Result.ResultRecipe>(response);

            random = new Random();

            var values = Enum.GetValues(typeof(DayOfWeek));

            var temp = new ObservableCollection<Result.RecipeBox>();
            var b = 0;

            foreach (var item in result.Hits.OrderBy(item => random.Next()))
            {

                //tmp.Add(item);
                temp.Add(new Result.RecipeBox
                {
                    DayWeek = ((DayOfWeek)values.GetValue(b)).ToString(),
                    Recipe = item.Recipe
                });
                b++;
                if (b > Data.Constants.DAYSOFWEEK || b >= result.Hits.Count)
                {
                    b = 0;
                }
            }

            for (var a = Data.Constants.ZERO; a < values.Length; a++)
            {
                var dtmp = ((DayOfWeek)values.GetValue(a)).ToString();
                var rcg = new Result.RecipeGroup(dtmp);
                foreach (var item in temp)
                {
                    if (dtmp == item.DayWeek)
                    {
                        rcg.Add(new Result.Recipe()
                        {
                            uri = item.Recipe.uri,
                            label = item.Recipe.label,
                            image = item.Recipe.image,
                            source = item.Recipe.source,
                            url = item.Recipe.url,
                            shareAs = item.Recipe.shareAs,
                            yield = item.Recipe.yield,
                            dietLabels = item.Recipe.dietLabels,
                            healthLabels = item.Recipe.healthLabels,
                            ingredientLines = item.Recipe.ingredientLines,
                            ingredients = item.Recipe.ingredients,
                            calories = item.Recipe.calories.Split('.')[Data.Constants.ZERO] + Data.Constants.KCAL,
                        });
                        
                    }
                }
                rgl.Add(rcg);
            }

        }
    }
}
