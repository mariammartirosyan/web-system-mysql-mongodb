using System;
namespace Infrastructure.Helpers
{
	public class RandomDateTimeGenerator
	{
		public static DateTime Generate(DateTime startDate, int maxDays)
		{
			var rnd = new Random();
			var rndDateTime = startDate.AddDays(rnd.Next(0, maxDays))
				.AddHours(rnd.Next(24))
				.AddMinutes(rnd.Next(60));
			return rndDateTime;

        }
	}
}

