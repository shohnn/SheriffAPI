using System;
using System.Threading.Tasks;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Notifications
{
    public abstract class Notifications
    {
        public async Task Send(Message message, Bandit to)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            
            if (to == null)
                throw new ArgumentNullException(nameof(to));

            var sender = CreateSender();
            await sender.Send(message, to);
        }

        public static Message JoinApp(string hostName)
        {   
            if (string.IsNullOrEmpty(hostName))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(hostName)), nameof(hostName));

            return new Message
            {
                Title = Strings.JoinAppTitle,
                Body = Strings.JoinAppBody.Format(hostName)
            };
        }

        public static Message InviteJoinBand(string hostName, string bandName)
        {
            if (string.IsNullOrEmpty(hostName))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(hostName)), nameof(hostName));

            if (string.IsNullOrEmpty(bandName))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(bandName)), nameof(bandName));

            return new Message
            {
                Title = Strings.JoinBandTitle,
                Body = Strings.InviteJoinBandBody.Format(hostName, bandName)
            };
        }

        public static Message RequestJoinBand(string guestName, string bandName)
        {
            if (string.IsNullOrEmpty(guestName))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(guestName)), nameof(guestName));

            if (string.IsNullOrEmpty(bandName))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(bandName)), nameof(bandName));

            return new Message
            {
                Title = Strings.JoinBandTitle,
                Body = Strings.RequestJoinBandBody.Format(guestName, bandName)
            };
        }

        public static Message RoundResults(Band band, Round round)
        {
            if (band == null)
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(band)), nameof(band));

            if (round == null)
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(round)), nameof(round));
            
            return new Message
            {
                Title = Strings.RoundResultTitle.Format(round.Name),
                Body = Strings.RoundResultBody.Format(round.Name, round.Place, band.Name, band.GetBandScoreTable(), round.Sheriff.Scoring.GetFormatedScoreValue())
            };
        }

        protected abstract ISender CreateSender();
    }
}