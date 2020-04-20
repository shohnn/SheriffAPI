using System;

namespace Sheriff
{
    public static class Strings
    {
        public const string UnrecognizedFormat = "{0} is not in a recognized format.";
        public const string NullOrEmpty = "{0} can't be null or empty.";
        public const string ContainsNull = "{0} contains a null value.";
        public const string ShouldByInFuture = "{0} should be in the future.";
        public const string ShouldBeInSameBand = "All bandits must be from the same band.";
        public const string CantCreateRound = "{0} can't create a round in this moment.";
        public const string CantScoreRound = "{0} can't score the round because is the sheriff.";
        public const string AlreadyScored = "{0} already scored the round.";
        public const string CantInviteToJoin = "{0} can't invite to {1} to join the band {2}.";
        public const string CantRequestToJoin = "{0} can't request to join the band {1}.";
        public const string JoinAppTitle = "Join Sheriff App";
        public const string JoinAppBody = "{0} has invited you to join the Sheriff app.";
        public const string JoinBandTitle = "Join Band";
        public const string InviteJoinBandBody = "{0} has invited you to join to the band {1}.";
        public const string RequestJoinBandBody = "{0} has requested to join to the band {1}.";
        public const string EmailAlreadyUsed = "There is already a user with email {0}.";
        public const string NotFound = "{0} with Id {1} not found";
        public const string BandScores = "Band {0} Score Table: \n";
        public const string BandScoresTuple = "Member: {0} - Score : {1}";
        public const string RoundResultTitle = "Results of the Round {0}";
        public const string RoundResultBody = "The Round with Name {0} that took place at {1} belonging to the Band: {2} Has now the next result: Band Score table: {3}. Sheriff Scoring for the assault {4}";
    }
}