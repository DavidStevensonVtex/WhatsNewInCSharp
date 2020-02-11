// https://github.com/dotnet/roslyn/blob/master/docs/features/refout.md

// The main purpose of the /refout option is to speed up incremental build 
// scenarios, so it is acceptable for the current implementation for this 
// flag to produce a ref assembly with more metadata than /refonly does 
// (for instance, anonymous types). This is a candidate for post-C#-7.1 
// refinement.


using System.Collections.Generic;

namespace TheColonelsSecretRecipe
{
	public class OriginalRecipeFriedChicken
    {
		private List<string> ingredients = new List<string>();
		private void AddFlour ( decimal numberOfCups )
		{
			ingredients.Add(string.Format("{0:N0} cups of flour", numberOfCups));
		}
		private void AddElevenHerbsAndSpices()
		{
			string[] ElevenHerbsAndSpices =
			{
				"2/3 Ts Salt",
				"1/2 Ts Thyme",
				"1/2 Ts Basil",
				"1/3 Ts Oregano",
				"1 Ts Celery salt",
				"1 Ts Black pepper",
				"1 Ts Dried mustard",
				"4 Ts Paprika",
				"2 Ts Garlic salt",
				"1 Ts Ground ginger",
				"3 Ts White pepper"
			};

			ingredients.AddRange(ElevenHerbsAndSpices);

		}
		public string MakeFriedChicken()
		{
			AddElevenHerbsAndSpices();
			AddFlour(2.0m);
			return "Recipe for Fried Chicken";
		}
    }
}
