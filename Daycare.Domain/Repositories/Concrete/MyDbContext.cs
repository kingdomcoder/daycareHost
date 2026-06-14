using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Entities.Daycare.Chat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Concrete {
    public class MyDbContext : DbContext {
        public MyDbContext() { }
        public MyDbContext(DbContextOptions<MyDbContext> myOptions) : base(myOptions) { }

        public DbSet<Activity> Activity { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecord {get;set;}
        public DbSet<Child> Child { get; set; }
        public DbSet<CommentRecord> CommentRecord { get; set; }
        public DbSet<GrowthRecord> GrowthRecord { get; set; }
        public DbSet<MealRecord> MealRecord { get; set; }
        public DbSet<NapRecord> NapRecord { get; set; }
        public DbSet<PottyRecord> PottyRecord { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<ChatUser> ChatUser { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<DeviceToken> DeviceToken { get; set; }
        public DbSet<Photo> Photo { get; set; }
    }
}
