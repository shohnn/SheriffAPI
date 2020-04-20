using System;
using Sheriff.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sheriff.Infrastructure.Repositories
{
    public class SheriffContext: DbContext
    {
        public SheriffContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Bandit> Bandits { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Score> Scorings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<BandMember> BandMembers { get; set; }
        public DbSet<RoundMember> RoundMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bandit>(ConfigureBandits);
            modelBuilder.Entity<Band>(ConfigureBands);
            modelBuilder.Entity<Round>(ConfigureRounds);
            modelBuilder.Entity<Score>(ConfigureScorings);
            modelBuilder.Entity<Notification>(ConfigureNotifications);
            modelBuilder.Entity<Invitation>(ConfigureInvitations);
            modelBuilder.Entity<BandMember>(ConfigureBandMembers);
            modelBuilder.Entity<RoundMember>(ConfigureRoundMembers);
        }

        private void ConfigureBandits(EntityTypeBuilder<Bandit> entityBuilder)
        {
            entityBuilder
                .HasKey(b => b.Id);

            entityBuilder
                .HasOne(b => b.Scoring)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            entityBuilder
                .HasMany(b => b.Notifications)
                .WithOne(n => n.Bandit)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }

        private void ConfigureBands(EntityTypeBuilder<Band> entityBuilder)
        {
            entityBuilder
                .HasKey(b => b.Id);

            entityBuilder
                .HasOne(b => b.Boss)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }

        private void ConfigureRounds(EntityTypeBuilder<Round> entityBuilder)
        {
            entityBuilder
                .HasKey(r => r.Id);

            entityBuilder
                .HasOne(r => r.Sheriff)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }

        private void ConfigureScorings(EntityTypeBuilder<Score> entityBuilder)
        {
            entityBuilder
                .HasKey(s => s.Id);
        }

        private void ConfigureNotifications(EntityTypeBuilder<Notification> entityBuilder)
        {
            entityBuilder
                .HasKey(n => n.Id);
        }

        private void ConfigureInvitations(EntityTypeBuilder<Invitation> entityBuilder)
        {
            entityBuilder
                .HasKey(n => n.Id);

            entityBuilder
                .HasOne(i => i.Guest)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entityBuilder
                .HasOne(i => i.Band)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entityBuilder
                .HasOne(i => i.Handler)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }

        private void ConfigureBandMembers(EntityTypeBuilder<BandMember> entityBuilder)
        {
            entityBuilder
                .HasKey(bm => bm.Id);

            entityBuilder
                .HasOne(bm => bm.Scoring)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            entityBuilder
                .HasOne(bm => bm.Bandit)
                .WithMany(b => b.Bands)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entityBuilder
                .HasOne(bm => bm.Band)
                .WithMany(b => b.Members)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }

        private void ConfigureRoundMembers(EntityTypeBuilder<RoundMember> entityBuilder)
        {
            entityBuilder
                .HasKey(rm => rm.Id);

            entityBuilder
                .HasOne(rm => rm.Scoring)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            entityBuilder
                .HasOne(rm => rm.Member)
                .WithMany(bm => bm.Rounds)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entityBuilder
                .HasOne(rm => rm.Round)
                .WithMany(r => r.Members)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}