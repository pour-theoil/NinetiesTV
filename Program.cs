using System;
using System.Collections.Generic;
using System.Linq;

namespace NinetiesTV
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Show> shows = DataLoader.GetShows();

            Print("All Names", Names(shows));
            Print("Alphabetical Names", NamesAlphabetically(shows));
            Print("Ordered by Popularity", ShowsByPopularity(shows));
            Print("Shows with an '&'", ShowsWithAmpersand(shows));
            Print("Latest year a show aired", MostRecentYear(shows));
            Print("Average Rating", AverageRating(shows));
            Print("Shows only aired in the 90s", OnlyInNineties(shows));
            Print("Top Three Shows", TopThreeByRating(shows));
            Print("Shows starting with 'The'", TheShows(shows));
            Print("All But the Worst", AllButWorst(shows));
            Print("Shows with Few Episodes", FewEpisodes(shows));
            Print("Shows Sorted By Duration", ShowsByDuration(shows));
            Print("Comedies Sorted By Rating", ComediesByRating(shows));
            Print("More Than One Genre, Sorted by Start", WithMultipleGenresByStartYear(shows));
            Print("Most Episodes", MostEpisodes(shows));
            Print("Ended after 2000", EndedFirstAfterTheMillennium(shows));
            Print("Best Drama", BestDrama(shows));
            Print("All But Best Drama", AllButBestDrama(shows));
            Print("Good Crime Shows", GoodCrimeShows(shows));
            Print("Long-running, Top-rated", FirstLongRunningTopRated(shows));
            Print("Most Words in Title", WordieastName(shows));
            Print("All Names", AllNamesWithCommas(shows));
            Print("All Names with And", AllNamesWithCommasPlsAnd(shows));

            // Challenges
            Print("All Genres from the eighties", Alltheeightsgenres(shows));
            Print("Start years", ShowsStartedPerYear(shows));

            Print("All Unique Genres", AllUniqueGenres(shows));
            Print("Highest average imdb", YearWithHighestIMDB(shows));
            Print("Length to watch all shows", LengthtoWatchAll(shows));
        }

        /**************************************************************************************************
         The Exercises

         Above each method listed below, you'll find a comment that describes what the method should do.
         Your task is to write the appropriate LINQ code to make each method return the correct result.

        **************************************************************************************************/

        // 1. Return a list of each of show names.
        static List<string> Names(List<Show> shows)
        {
            return shows.Select(s => s.Name).ToList(); // Looks like this one's already done!
        }

        // 2. Return a list of show names ordered alphabetically.
        static List<string> NamesAlphabetically(List<Show> shows)
        {
            return shows.Select(s => s.Name).OrderBy(s => s).ToList();
        }

        // 3. Return a list of shows ordered by their IMDB Rating with the highest rated show first.
        static List<Show> ShowsByPopularity(List<Show> shows)
        {
            return shows.OrderByDescending(s => s.ImdbRating).ToList();
        }

        // 4. Return a list of shows whose title contains an & character.
        static List<Show> ShowsWithAmpersand(List<Show> shows)
        {
            return shows.Where(s => s.Name.Contains("&")).ToList();

        }

        // 5. Return the most recent year that any of the shows aired.
        static int MostRecentYear(List<Show> shows)
        {
            return shows.Max(s => s.EndYear);
        }

        // 6. Return the average IMDB rating for all the shows.
        static double AverageRating(List<Show> shows)
        {
            return shows.Average(s => s.ImdbRating);
        }

        // 7. Return the shows that started and ended in the 90s.
        static List<Show> OnlyInNineties(List<Show> shows)
        {
            List<Show> answer = (
                from s in shows
                where s.StartYear >= 1990 && s.EndYear < 2000
                select s
            ).ToList();
            return answer;
        }

        // 8. Return the top three highest rated shows.
        static List<Show> TopThreeByRating(List<Show> shows)
        {
            return shows.OrderByDescending(s => s.ImdbRating).Take(3).ToList();
        }

        // 9. Return the shows whose name starts with the word "The".
        static List<Show> TheShows(List<Show> shows)
        {
            return shows.Where(s => s.Name.StartsWith("The")).Select(s => s).ToList();
        }

        // 10. Return all shows except for the lowest rated show.
        static List<Show> AllButWorst(List<Show> shows)
        {
            return shows.OrderBy(s => s.ImdbRating).Skip(1).ToList();
        }

        // 11. Return the names of the shows that had fewer than 100 episodes.
        static List<string> FewEpisodes(List<Show> shows)
        {
            List<string> name = (
                from s in shows
                where s.EpisodeCount > 100
                select s.Name
            ).ToList();
            return name;
        }

        // 12. Return all shows ordered by the number of years on air.
        //     Assume the number of years between the start and end years is the number of years the show was on.
        static List<Show> ShowsByDuration(List<Show> shows)
        {
            List<Show> duration = (
                from s in shows
                orderby (s.EndYear - s.StartYear) descending
                select s
            ).ToList();
            return duration;
        }

        // 13. Return the names of the comedy shows sorted by IMDB rating.
        static List<string> ComediesByRating(List<Show> shows)
        {
            List<string> comedies = (
                from s in shows
                where s.Genres.Contains("Comedy")
                orderby s.ImdbRating descending
                select s.Name
            ).ToList();
            return comedies;
        }

        // 14. Return the shows with more than one genre ordered by their starting year.
        static List<Show> WithMultipleGenresByStartYear(List<Show> shows)
        {
            List<Show> multigenre = (
                from s in shows
                where s.Genres.Count > 1
                orderby s.StartYear
                select s
            ).ToList();
            return multigenre;
        }

        // 15. Return the show with the most episodes.
        static Show MostEpisodes(List<Show> shows)
        {
            int mostshows = shows.Max(s => s.EpisodeCount);
            return shows.FirstOrDefault(s => s.EpisodeCount == mostshows);
        }

        // 16. Order the shows by their ending year then return the first 
        //     show that ended on or after the year 2000.
        static Show EndedFirstAfterTheMillennium(List<Show> shows)
        {
            int yearnear = shows.Where(s => s.EndYear > 2000).Min(s => s.EndYear);
            return shows.Where(s => s.EndYear > 2000).OrderBy(s => s.EndYear).FirstOrDefault(s => s.EndYear == yearnear);
        }

        // 17. Order the shows by rating (highest first) 
        //     and return the first show with genre of drama.
        static Show BestDrama(List<Show> shows)
        {
            return shows.OrderByDescending(s => s.ImdbRating).FirstOrDefault(s => s.Genres.Contains("Drama"));
        }

        // 18. Return all dramas except for the highest rated.
        static List<Show> AllButBestDrama(List<Show> shows)
        {
            List<Show> drama = (
                from s in shows
                where s.Genres.Contains("Drama")
                orderby s.ImdbRating descending
                select s
            ).Skip(1).ToList();
            return drama;
        }

        // 19. Return the number of crime shows with an IMDB rating greater than 7.0.
        static int GoodCrimeShows(List<Show> shows)
        {
            int numcrime = (
                from s in shows
                where s.Genres.Contains("Crime") && s.ImdbRating > 7
                select s
            ).Count();
            return numcrime;
        }

        // 20. Return the first show that ran for more than 10 years 
        //     with an IMDB rating of less than 8.0 ordered alphabetically.
        static Show FirstLongRunningTopRated(List<Show> shows)
        {
            List<Show> running = shows.Where(s => (s.EndYear - s.StartYear) > 10 && s.ImdbRating < 8).OrderBy(s => s.Name).ToList();

            return running[0];
        }

        // 21. Return the show with the most words in the name.
        static Show WordieastName(List<Show> shows)
        {
            int wordy = shows.Max(s => s.Name.Count(Char.IsWhiteSpace));
            return shows.FirstOrDefault(s => s.Name.Count(char.IsWhiteSpace) == wordy);

        }

        // 22. Return the names of all shows as a single string seperated by a comma and a space.
        static string AllNamesWithCommas(List<Show> shows)
        {


            string names = String.Join(", ", shows.Select(s => s.Name).ToArray());
            return names;
        }

        // 23. Do the same as above, but put the word "and" between the second-to-last and last show name.
        static string AllNamesWithCommasPlsAnd(List<Show> shows)
        {
            string names = String.Join(", ", shows.Select(s => s.Name).Take(shows.Count - 1).ToArray());
            List<string> last = shows.Select(s => s.Name).Skip(shows.Count - 1).ToList();
            return names + ", and " + last[0];
        }


        /**************************************************************************************************
         CHALLENGES

         These challenges are very difficult and may require you to research LINQ methods that we haven't
         talked about. Such as:

            GroupBy()
            SelectMany()
            Count()

        **************************************************************************************************/

        // 1. Return the genres of the shows that started in the 80s.
        static List<string> Alltheeightsgenres(List<Show> shows)
        {
            return shows.Where(s => s.StartYear >= 1980 && s.StartYear < 1990).Select(s => string.Join(", ", s.Genres.ToArray())).ToList();

        }
        // 2. Print a unique list of genres.
        static List<string> AllUniqueGenres(List<Show> shows)
        {
            var Genres = (
               from s in shows
               select s.Genres
           );
            List<string> genres = new List<string>();
            foreach (var genre in Genres)
            {
                genres.AddRange(genre);
            }
            return genres.Distinct().ToList();

        }

        // 3. Print the years 1987 - 2018 along with the number of shows that started in each year (note many years will have zero shows)
        static List<string> ShowsStartedPerYear(List<Show> shows)
        {
            var yearstarted = (
                from s in shows
                group s by s.StartYear into newyear
                orderby newyear.Key
                select new { name = newyear.Key, count = newyear.Count() }
            );
            List<string> years = new List<string>();
            for (int i = 1987; i < 2019; i++)
            {
                years.Add($"{i}: 0");
                foreach (var newyear in yearstarted)
                {

                    if (i == newyear.name)
                    {
                        years[years.Count() - 1] = $"{i}: {newyear.count} ";
                    }
                }

            }

            return years;
        }
        // 4. Assume each episode of a comedy is 22 minutes long and each episode of a show that isn't a comedy is 42 minutes. How long would it take to watch every episode of each show?
        static int LengthtoWatchAll(List<Show> shows)
        {

            var comedies = shows.Where(s => s.Genres.Contains("Comedy")).Select(s => s.EpisodeCount).Sum();
            var others = shows.Select(s => s.EpisodeCount).Sum() - comedies;
            return comedies*22+others*42;
        }
        // 5. Assume each show ran each year between its start and end years (which isn't true), which year had the highest average IMDB rating.
        static int YearWithHighestIMDB(List<Show> shows)
        {
            Dictionary<int, double> yearlyavg = new Dictionary<int, double>();
            for (int i = 1987; i < 2019; i++)
            {
                double yearavr;
                var yearvalues = (
                    from s in shows
                    where i > s.StartYear && i < s.EndYear
                    select s.ImdbRating
                ).ToList();
                if (yearvalues.Count() == 0)
                {
                    yearavr = 0;    
                }
                else
                {
                    yearavr = yearvalues.Average();
                }
                yearlyavg.Add(i, yearavr);
            }

            var sorted = (
                from s in yearlyavg
                orderby s.Value descending
                select s.Key
            ).ToList();
 
            return sorted[0];
        }


        /**************************************************************************************************
         There is no code to write or change below this line, but you might want to read it.
        **************************************************************************************************/

        static void Print(string title, List<Show> shows)
        {
            PrintHeaderText(title);
            foreach (Show show in shows)
            {
                Console.WriteLine(show);
            }

            Console.WriteLine();
        }

        static void Print(string title, List<string> strings)
        {
            PrintHeaderText(title);
            foreach (string str in strings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }

        static void Print(string title, Show show)
        {
            PrintHeaderText(title);
            Console.WriteLine(show);
            Console.WriteLine();
        }

        static void Print(string title, string str)
        {
            PrintHeaderText(title);
            Console.WriteLine(str);
            Console.WriteLine();
        }

        static void Print(string title, int number)
        {
            PrintHeaderText(title);
            Console.WriteLine(number);
            Console.WriteLine();
        }

        static void Print(string title, double number)
        {
            PrintHeaderText(title);
            Console.WriteLine(number);
            Console.WriteLine();
        }

        static void PrintHeaderText(string title)
        {
            Console.WriteLine("============================================");
            Console.WriteLine(title);
            Console.WriteLine("--------------------------------------------");
        }
    }
}