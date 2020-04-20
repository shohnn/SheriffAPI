using System;
using System.Collections.Generic;

namespace Sheriff.Domain.Models
{
    public class Bandit
    {
        private Bandit(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = new EmailAddress(email);
            Scoring = Score.Zero;
            Bands = new List<BandMember>();
            Notifications = new List<Notification>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public Score Scoring { get; private set; }
        public List<BandMember> Bands { get; private set; }
        public List<Notification> Notifications { get; private set; }

        public static Bandit Create(string name, string email)
        {
            return Create(0, name, email);
        }

        public static Bandit Create(int id, string name, string email)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(name)), nameof(name));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(email)), nameof(email));

            return new Bandit(id, name, email);
        }

        public void UpdateScoring(Score score)
        {
            this.Scoring.Add(score);
        }
    }
}