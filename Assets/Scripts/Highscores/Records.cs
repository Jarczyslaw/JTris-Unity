using System.Collections.Generic;
using System;

[Serializable]
public class Records
{
    [Serializable]
    public class Score : IComparable<Score>
    {
        public long date;
        public int score;

        public Score(int score, DateTime date)
        {
            this.score = score;
            this.date = date.ToFileTimeUtc();
        }

        public int CompareTo(Score other)
        {
            if (score < other.score)
                return 1;
            else if (score > other.score)
                return -1;
            else
                return -date.CompareTo(other.date);
        }

        public override string ToString()
        {
            return string.Format("Points: {0}, date: {1}", score, GetDate().ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public DateTime GetDate()
        {
            return DateTime.FromFileTimeUtc(date);
        }
    }

    public readonly int scoresLimit = 5;

    public List<Score> scores = new List<Score>();

    public bool Add(int score)
    {
        if (score > 0)
        {
            Score newScore = new Score(score, DateTime.Now);
            scores.Add(newScore);
            Sort();
            if (scores.Count > scoresLimit)
                scores.RemoveAt(scores.Count - 1);
            return scores.Contains(newScore);
        }
        else
            return false;
    }

    private void Sort()
    {
        scores.Sort((x, y) => x.CompareTo(y));
    }
}
