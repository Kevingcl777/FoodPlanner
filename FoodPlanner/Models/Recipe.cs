namespace Fooddaily.Portable.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Result
    {
        public string Id
        {
            get => this.id;
            set => this.id = value;
        }

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        
        public string id { get; set; }
        public string name { get; set; }

        public class RecipeGroup : ObservableCollection<Recipe>
        {
            public RecipeGroup(string name)
            {
                this.Name = name;

            }

            public string Name
            {
                get;
                set;
            }


        }

        public class Recipe
        {
            public string uri { get; set; }
            public string label { get; set; }
            public string image { get; set; }
            public string source { get; set; }
            public string url { get; set; }
            public string shareAs { get; set; }
            public string yield { get; set; }
            public List<string> dietLabels { get; set; }
            public List<string> healthLabels { get; set; }
            public List<Ingredients> ingredients { get; set; }
            public List<string> ingredientLines { get; set; }
            public string calories { get; set; }
            

        }
        public class RecipeBox
        {

            public string DayWeek { get; set; }
            public Recipe Recipe { get; set; }

        }

        public class ResultRecipe
        {
            public bool More { get; set; }
            public int Count { get; set; }
            public List<RecipeBox> Hits { get; set; }//hits
        }

        public class Ingredients
        {
            public string text { get; set; }
            public string weight { get; set; }
        }
    }
}
