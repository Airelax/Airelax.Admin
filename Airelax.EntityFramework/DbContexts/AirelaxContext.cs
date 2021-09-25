using Airelax.Domain.Comments;
using Airelax.Domain.Houses;
using Airelax.Domain.Houses.Price;
using Airelax.Domain.Members;
using Airelax.Domain.Messages;
using Airelax.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Airelax.EntityFramework.DbContexts
{
    public class AirelaxContext : DbContext
    {
        public AirelaxContext(DbContextOptions<AirelaxContext> opt) : base(opt)
        {
        }

        public DbSet<House> Houses { get; set; }
        public DbSet<HouseCategory> HouseCategories { get; set; }
        public DbSet<HouseLocation> HouseLocations { get; set; }
        public DbSet<HouseDescription> HouseDescriptions { get; set; }
        public DbSet<HouseRule> HouseRules { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<ReservationRule> ReservationRules { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<BedroomDetail> BedroomDetails { get; set; }
        public DbSet<HousePrice> HousePrices { get; set; }

        public DbSet<Member> Members { get; set; }
        public DbSet<MemberInfo> MemberInfos { get; set; }
        public DbSet<MemberLoginInfo> MemberLoginInfos { get; set; }
        public DbSet<WishList> WishLists { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderPriceDetail> OrderPriceDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Star> Stars { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigHouse(modelBuilder);
            ConfigHouseCategory(modelBuilder);
            ConfigHouseLocation(modelBuilder);
            ConfigHouseDescription(modelBuilder);
            ConfigHouseRule(modelBuilder);
            ConfigPolicy(modelBuilder);
            ConfigReservationRule(modelBuilder);
            ConfigPhoto(modelBuilder);
            ConfigSpace(modelBuilder);
            ConfigBedroomDetail(modelBuilder);
            ConfigHousePrice(modelBuilder);

            ConfigMember(modelBuilder);
            ConfigMemberLogInfo(modelBuilder);
            ConfigMemberInfo(modelBuilder);
            ConfigEmergencyContact(modelBuilder);
            ConfigWishList(modelBuilder);

            ConfigOrder(modelBuilder);
            ConfigOrderDetail(modelBuilder);
            ConfigPriceDetail(modelBuilder);
            ConfigPayment(modelBuilder);

            ConfigComment(modelBuilder);
            ConfigStar(modelBuilder);

            ConfigMessage(modelBuilder);
        }


        #region HouseConfig

        private static void ConfigHouse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>(
                builder =>
                {
                    builder.SetEntityKey<House, string>();
                    builder.SetPropMaxLength(x => x.Title, 50).IsRequired();
                    builder.SetEnumDbMapping(x => x.Status).IsRequired();
                    builder.SetEnumDbMapping(x => x.CreateState).IsRequired();
                    builder.Property(x => x.ReservationDates).HasJsonConversion();
                    builder.Property(x => x.ProvideFacilities).HasJsonConversion();
                    builder.Property(x => x.NotProvideFacilities).HasJsonConversion();

                    builder.HasOne(x => x.Member).WithMany(x => x.Houses).HasForeignKey(x => x.OwnerId);

                });
        }

        private static void ConfigHouseCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseCategory>(builder =>
            {
                builder.SetEntityKey<HouseCategory, string>();
                builder.SetEnumDbMapping(x => x.Category);
                builder.SetEnumDbMapping(x => x.HouseType);
                builder.SetEnumDbMapping(x => x.RoomCategory);
                builder.HasOne<House>().WithOne(x => x.HouseCategory).HasForeignKey<HouseCategory>(x => x.Id);
            });
        }

        private static void ConfigBedroomDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BedroomDetail>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
                builder.SetEnumDbMapping(x => x.BedType).IsRequired();
                builder.HasOne<Space>().WithMany(x => x.BedroomDetails).HasForeignKey(x => x.SpaceId);
            });
        }

        private static void ConfigSpace(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Space>(builder =>
            {
                builder.SetEntityKey<Space, string>();
                builder.SetEnumDbMapping(x => x.SpaceType).IsRequired();
                builder.HasOne<House>().WithMany(x => x.Spaces).HasForeignKey(x => x.HouseId);
            });
        }

        private static void ConfigPhoto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Image).IsRequired().HasMaxLength(200);
                builder.HasOne<House>().WithMany(x => x.Photos).HasForeignKey(x => x.HouseId).IsRequired();
                builder.HasOne<Space>().WithMany().OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(x => x.SpaceId);
            });
        }

        private static void ConfigReservationRule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationRule>(builder =>
            {
                builder.SetEntityKey<ReservationRule, string>();
                builder.SetEnumDbMapping(x => x.RejectDate);
                builder.HasOne<House>().WithOne(x => x.ReservationRule).HasForeignKey<ReservationRule>(x => x.Id);
            });
        }

        private static void ConfigPolicy(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>(builder =>
            {
                builder.SetEntityKey<Policy, string>();
                builder.SetEnumDbMapping(x => x.CancelPolicy);
                builder.Property(x => x.CashPledge).HasColumnType(Define.SqlServer.MONEY_TYPE);
                builder.HasOne<House>().WithOne(x => x.Policy).HasForeignKey<Policy>(x => x.Id);
            });
        }

        private static void ConfigHouseRule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseRule>(builder =>
            {
                builder.SetEntityKey<HouseRule, string>();
                builder.SetPropMaxLength(x => x.Other, 1000);
                builder.HasOne<House>().WithOne(x => x.HouseRule).HasForeignKey<HouseRule>(x => x.Id);
            });
        }

        private static void ConfigHouseDescription(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseDescription>(builder =>
            {
                builder.SetEntityKey<HouseDescription, string>();
                builder.SetPropMaxLength(x => x.Description, 1000);
                builder.SetPropMaxLength(x => x.SpaceDescription, 1000);
                builder.SetPropMaxLength(x => x.GuestPermission, 1000);
                builder.SetPropMaxLength(x => x.Others, 1000);
                builder.SetEnumDbMapping(x => x.HouseHighlight);
                builder.HasOne<House>().WithOne(x => x.HouseDescription).HasForeignKey<HouseDescription>(x => x.Id);
            });
        }

        private static void ConfigHouseLocation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseLocation>(builder =>
            {
                builder.SetEntityKey<HouseLocation, string>();
                builder.SetPropMaxLength(x => x.Country, 50);
                builder.SetPropMaxLength(x => x.City, 50);
                builder.SetPropMaxLength(x => x.Town, 50);
                builder.SetPropMaxLength(x => x.ZipCode, 10);
                builder.SetPropMaxLength(x => x.AddressDetail, 100);
                builder.SetPropMaxLength(x => x.LocationDescription, 1000);
                builder.SetPropMaxLength(x => x.TrafficDescription, 250);
                builder.HasOne<House>().WithOne(x => x.HouseLocation).HasForeignKey<HouseLocation>(x => x.Id);
            });
        }

        private static void ConfigHousePrice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HousePrice>(builder =>
            {
                builder.SetEntityKey<HousePrice, string>();
                builder.Property(x => x.PerNight).HasColumnType(Define.SqlServer.MONEY_TYPE).IsRequired();
                builder.Property(x => x.PerWeekNight).HasColumnType(Define.SqlServer.MONEY_TYPE);
                builder.Property(x => x.Fee).HasJsonConversion();
                builder.Property(x => x.Discount).HasJsonConversion();

                builder.HasOne<House>().WithOne(x => x.HousePrice).HasForeignKey<HousePrice>(x => x.Id);
            });
        }

        #endregion

        #region MemberConfig

        private static void ConfigMember(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>(builder =>
            {
                builder.SetEntityKey<Member, string>();
                builder.SetPropMaxLength(x => x.Name, 50);
                builder.SetEnumDbMapping(x => x.Gender);
                builder.SetPropMaxLength(x => x.Email, 50);
                builder.SetPropMaxLength(x => x.Country, 50);
                builder.SetPropMaxLength(x => x.City, 50);
                builder.SetPropMaxLength(x => x.Town, 50);
                builder.SetPropMaxLength(x => x.AddressDetail, 50);
                builder.SetPropMaxLength(x => x.Phone, 50);
                builder.SetPropMaxLength(x => x.Cover, 200);
                builder.Property(x => x.RegisterTime).IsRequired();
            });
        }

        private static void ConfigMemberLogInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberLoginInfo>(builder =>
            {
                builder.SetEntityKey<MemberLoginInfo, string>();
                builder.SetEnumDbMapping(x => x.LoginType).IsRequired();
                builder.SetPropMaxLength(x => x.Account, 50).IsRequired();
                builder.SetPropMaxLength(x => x.Token, 2000);
                builder.SetPropMaxLength(x => x.Password, 1000);
                builder.SetPropMaxLength(x => x.RefreshToken, 2000);
                builder.SetPropMaxLength(x => x.ThirdPartyToken, 2000);
                builder.SetPropMaxLength(x => x.ThirdPartyRefreshToken, 2000);

                builder.HasOne<Member>().WithOne(x => x.MemberLoginInfo).HasForeignKey<MemberLoginInfo>(x => x.Id);
            });
        }

        private static void ConfigMemberInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberInfo>(builder =>
            {
                builder.SetEntityKey<MemberInfo, string>();
                builder.SetPropMaxLength(x => x.About, 1000);
                builder.SetPropMaxLength(x => x.Location, 100);

                builder.HasOne<Member>().WithOne(x => x.MemberInfo).HasForeignKey<MemberInfo>(x => x.Id);
            });
        }

        private static void ConfigEmergencyContact(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmergencyContact>(builder =>
            {
                builder.SetEntityKey<EmergencyContact, string>();
                builder.SetPropMaxLength(x => x.Name, 50);
                builder.SetPropMaxLength(x => x.Phone, 50);

                builder.HasOne<Member>().WithOne(x => x.EmergencyContact).HasForeignKey<EmergencyContact>(x => x.Id);
            });
        }

        private static void ConfigWishList(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WishList>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.SetPropMaxLength(x => x.Name, 50);
                builder.SetPropMaxLength(x => x.Cover, 200);
                builder.Property(x => x.Houses).HasJsonConversion();
                builder.HasOne<Member>().WithMany(x => x.WishLists).HasForeignKey(x => x.MemberId);
            });
        }

        #endregion

        #region OrdersConfig

        private static void ConfigOrder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(builder =>
            {
                builder.SetEntityKey<Order, string>();
                builder.SetEnumDbMapping(x => x.State);
                builder.Property(x => x.IsDeleted).IsRequired();
                
                
                builder.HasOne(x => x.Member).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId).IsRequired();
                builder.HasOne(x => x.House).WithMany().OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.HouseId).IsRequired();
            });
        }

        private static void ConfigOrderDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>(builder =>
            {
                builder.SetEntityKey<OrderDetail, string>();
                builder.Property(x => x.StartDate).IsRequired();
                builder.Property(x => x.EndDate).IsRequired();
                builder.Property(x => x.Adult).IsRequired();
                builder.Property(x => x.Child).IsRequired();
                builder.Property(x => x.Baby).IsRequired();

                builder.HasOne<Order>().WithOne(x => x.OrderDetail).HasForeignKey<OrderDetail>(x => x.Id);
            });
        }

        private static void ConfigPayment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(builder =>
            {
                builder.SetEntityKey<Payment, string>();
                builder.SetEnumDbMapping(x => x.PayState).IsRequired();
                builder.SetEnumDbMapping(x => x.PayType).IsRequired();
                builder.Property(x => x.Refund).HasColumnType(Define.SqlServer.MONEY_TYPE);

                builder.HasOne<Order>().WithOne(x => x.Payment).HasForeignKey<Payment>(x => x.Id);
            });
        }

        private static void ConfigPriceDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderPriceDetail>(builder =>
            {
                builder.SetEntityKey<OrderPriceDetail, string>();
                builder.Property(x => x.PricePerNight).HasColumnType(Define.SqlServer.MONEY_TYPE).IsRequired();
                builder.Property(x => x.Total).HasColumnType(Define.SqlServer.MONEY_TYPE).IsRequired();
                builder.Property(x => x.Discount).HasJsonConversion();
                builder.Property(x => x.Fee).HasJsonConversion();

                builder.HasOne<Order>().WithOne(x => x.OrderPriceDetail).HasForeignKey<OrderPriceDetail>(x => x.Id);
            });
        }

        #endregion

        #region CommentsConfig

        private static void ConfigComment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(builder =>
            {
                builder.SetEntityKey<Comment, string>();
                builder.SetPropMaxLength(x => x.Content, 1000);
                builder.Property(x => x.CommentTime).IsRequired();

                builder.HasOne<House>().WithMany(x => x.Comments).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(x => x.HouseId);
                builder.HasOne<Member>().WithMany().OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(x => x.AuthorId);
                builder.HasOne<Member>().WithMany().OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(x => x.ReceiverId);
                builder.HasOne<Order>().WithOne().OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey<Comment>(x => x.OrderId);
            });
        }

        private static void ConfigStar(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Star>(builder =>
            {
                builder.SetEntityKey<Star, string>();
                builder.Property(x => x.AccuracyScore).IsRequired();
                builder.Property(x => x.CheapScore).IsRequired();
                builder.Property(x => x.CleanScore).IsRequired();
                builder.Property(x => x.CommunicationScore).IsRequired();
                builder.Property(x => x.ExperienceScore).IsRequired();
                builder.Property(x => x.LocationScore).IsRequired();
                builder.Ignore(x => x.Total);
                builder.HasOne<Comment>().WithOne(x => x.Star).HasForeignKey<Star>(x => x.Id);
            });
        }

        #endregion

        #region Messages

        private static void ConfigMessage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(builder =>
            {
                builder.SetEntityKey<Message, string>();
                builder.Property(x => x.MemberOneId).IsRequired();
                builder.Property(x => x.MemberTwoId).IsRequired();
                builder.Property(x => x.HouseId).IsRequired();
                builder.Property(x => x.Contents).HasJsonConversion();

                builder.HasOne(x => x.House).WithMany().HasForeignKey(x => x.HouseId);
            });
        }

        #endregion
    }
}