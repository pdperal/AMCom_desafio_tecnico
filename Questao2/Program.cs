using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public class Result
    {
        public int page { get; set; }
        public int total_pages { get; set; }
        public List<Team> data { get; set; }
    }
    public class Team
    {
        public string competition { get; set; }
        public int year { get; set; }
        public string team1 { get; set; }
        public int team1goals { get; set; }
    }

    private static Result callApi(string team, int year, int page)
    {
        var httpClient = new HttpClient();

        var result = httpClient.GetAsync(new Uri(string.Format("https://jsonmock.hackerrank.com/api/football_matches?year={0}&team1={1}&page={2}", year, team, page)))
            .GetAwaiter()
            .GetResult();

        if (result.IsSuccessStatusCode)
        {
            var data = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<Result>(data);
        }

        return default;
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int currentPage = 0;
        int goals = 0;

        while (true)
        {
            var result = callApi(team, year, currentPage);

            if (result is not null)
            {
                goals += result.data.Sum(x => x.team1goals);

                if (result.total_pages == currentPage)
                {
                    break;
                }
            }
            else
            {
                break;
            }
            
            
            currentPage++;            
        }

        return goals;
    }

}