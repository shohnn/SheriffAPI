using System;
using System.Linq;
using Sheriff.Application.DTOs;
using Models = Sheriff.Domain.Models;

namespace Sheriff
{
    static class BanditExtensions
    {
        public static Bandit ToDto(this Models.Bandit bandit)
        {
            return new Bandit
            {
                Id = bandit.Id,
                Name = bandit.Name,
                Scoring = bandit.Scoring.ToDto()
            };
        }

        public static BanditDetails ToDtoDetails(this Models.Bandit bandit)
        {
            return new BanditDetails
            {
                Id = bandit.Id,
                Name = bandit.Name,
                Email = bandit.Email.Address,
                Scoring = bandit.Scoring.ToDto(),
                Bands = bandit.Bands
                    .Select(b => b.ToDtoBand())
                    .ToArray()
            };
        }
    }

    static class BandExtensions
    {
        public static Band ToDto(this Models.Band band)
        {
            return new Band
            {
                Id = band.Id,
                Name = band.Name,
                Rounds = band.Rounds.Count
            };
        }

        public static BandDetails ToDtoDetails(this Models.Band band)
        {
            return new BandDetails
            {
                Id = band.Id,
                Name = band.Name,
                Members = band.Members
                    .Select(m => m.ToDtoMember())
                    .ToArray(),
                Rounds = band.Rounds
                    .Select(r => r.ToDto())
                    .ToArray()
            };
        }
    }

    static class RoundExtensions
    {
        public static Round ToDto(this Models.Round round)
        {
            return new Round
            {
                Id = round.Id,
                Name = round.Name,
                Place = round.Place,
                DateTime = round.DateTime
            };
        }

        public static RoundDetails ToDtoDetails(this Models.Round round)
        {
            return new RoundDetails
            {
                Id = round.Id,
                Name = round.Name,
                Place = round.Place,
                DateTime = round.DateTime,
                Sheriff = round.Sheriff.ToDtoMember(),
                Members = round.Members
                    .Select(m => m.ToDtoMember())
                    .ToArray()
            };
        }
    }

    static class BandMemberExtensions
    {
        public static BandMember ToDtoBand(this Models.BandMember band)
        {
            return new BandMember
            {
                Id = band.Band.Id,
                Band = band.Band.Name,
                Scoring = band.Scoring.ToDto()
            };
        }

        public static MemberBand ToDtoMember(this Models.BandMember member)
        {
            return new MemberBand
            {
                Id = member.Bandit.Id,
                Member = member.Bandit.Name,
                Scoring = member.Scoring.ToDto()
            };
        }
    }

    static class RoundMemberExtensions
    {
        public static MemberRound ToDtoMember(this Models.RoundMember member)
        {
            return new MemberRound
            {
                Id = member.Member.Bandit.Id,
                Member = member.Member.Bandit.Name,
                Scoring = member.Scoring.ToDto()
            };
        }
    }

    static class ScoringExtensions
    {
        public static Score ToDto(this Models.Score score)
        {
            return new Score
            {
                LootSize = score.LootSize,
                LootValue = score.LootValue,
                Service = score.Service,
                Price = score.Price
            };
        }

        public static Models.Score ToModel(this Score score)
        {
            return new Models.Score(
                lootSize: score.LootSize,
                lootValue: score.LootValue,
                service: score.Service,
                price: score.Price
            );
        }
    }

    static class InvitationExtensions
    {
        public static Invitation ToDto(this Models.Invitation invitation)
        {
            return new Invitation
            {
                Id = invitation.Id,
                Guest = invitation.Guest.ToDto(),
                Band = invitation.Band.ToDto()
            };
        }
    }

    static class NotificationExtensions
    {
        public static Notification ToDto(this Models.Notification notification)
        {
            return new Notification
            {
                Id = notification.Id,
                To = notification.Bandit.Name,
                Title = notification.Title,
                Content = notification.Body
            };
        }
    }
}