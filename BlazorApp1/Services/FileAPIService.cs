using System.Text;
using System.Text.Json;
using BlazorApp1.Data;

namespace BlazorApp1.Services {
	public class FileAPIService { 
		
		private HttpClient httpClient;
		protected virtual string BASE_ADDR => "http://localhost:80";


        public FileAPIService() { 
			httpClient = new HttpClient();
		}


        public class StreakService
        {
            private const string FilePath = "best_streak.txt";

            public int GetBestStreak()
            {
                if (File.Exists(FilePath))
                {
                    string content = File.ReadAllText(FilePath);
                    if (int.TryParse(content, out int bestStreak))
                    {
                        return bestStreak;
                    }
                }

                return 0;
            }

            public void UpdateBestStreak(int newBestStreak)
            {
                File.WriteAllText(FilePath, newBestStreak.ToString());
            }
        }
    }
}

