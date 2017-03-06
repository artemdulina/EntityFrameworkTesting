using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DAL.Helpers
{
	public static class ConverterHelper
	{
		public static double ConvertIteration(string iteration)
		{
			string[] numbers = Regex.Split(iteration, @"\d+");
			var number = String.Join(".", numbers.Skip(1).Take(2));
			double iterationNumber;
			Double.TryParse(number, out iterationNumber);

			return iterationNumber;
		}

		public static Tuple<int, int> ConvertIterationPath(string iterationToParse)
		{
			string[] parts = iterationToParse.Split('\\');
			string releaseNumber = Regex.Match(parts[parts.Length - 2], @"\d+").Value;
			string iterationNumber = Regex.Match(parts[parts.Length - 1], @"\d+").Value;

			int release = Convert.ToInt32(releaseNumber);
			int iteration = Convert.ToInt32(iterationNumber);

			return Tuple.Create(release, iteration);
		}

		public static Tuple<int, int> ConvertIterationPathAnother(string iterationToParse)
		{
			var matches = Regex.Matches(iterationToParse, @"\d+");

			if (matches.Count < 2)
			{
				return Tuple.Create(0, 0);

			}

			int release = Convert.ToInt32(matches[0].Value);
			int iteration = Convert.ToInt32(matches[1].Value);

			return Tuple.Create(release, iteration);
		}
	}
}
