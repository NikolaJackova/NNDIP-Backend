using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Entities;
using Action = NNDIP.Api.Entities.Action;

namespace NNDIP.Api.NNDIPDbContext
{
    public partial class NndipDbContext : DbContext
    {
        public NndipDbContext()
        {
        }

        public NndipDbContext(DbContextOptions<NndipDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; } = null!;
        public virtual DbSet<AddressState> AddressStates { get; set; } = null!;
        public virtual DbSet<ControlledDeviceAddressConfig> ControlledDeviceAddressConfigs { get; set; } = null!;
        public virtual DbSet<DashboardSensorConfig> DashboardSensorConfigs { get; set; } = null!;
        public virtual DbSet<Databasechangelog> Databasechangelogs { get; set; } = null!;
        public virtual DbSet<Databasechangeloglock> Databasechangeloglocks { get; set; } = null!;
        public virtual DbSet<Data> Data { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventAction> EventActions { get; set; } = null!;
        public virtual DbSet<GpioPlan> GpioPlans { get; set; } = null!;
        public virtual DbSet<LimitPlan> LimitPlans { get; set; } = null!;
        public virtual DbSet<ManualGpioPlan> ManualGpioPlans { get; set; } = null!;
        public virtual DbSet<ManualPlan> ManualPlans { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<Sensor> Sensors { get; set; } = null!;
        public virtual DbSet<TimeGpioPlan> TimeGpioPlans { get; set; } = null!;
        public virtual DbSet<TimePlan> TimePlans { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<YearPeriod> YearPeriods { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_czech_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Action>(entity =>
            {
                entity.ToTable("action");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.IsDefault)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_default")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.OutputType)
                    .HasMaxLength(255)
                    .HasColumnName("output_type");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<AddressState>(entity =>
            {
                entity.HasKey(e => new { e.Address, e.OutputType })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("address_state");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.OutputType).HasColumnName("output_type");

                entity.Property(e => e.ActionName)
                    .HasMaxLength(255)
                    .HasColumnName("action_name");

                entity.Property(e => e.PlanName)
                    .HasMaxLength(255)
                    .HasColumnName("plan_name");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<ControlledDeviceAddressConfig>(entity =>
            {
                entity.HasKey(e => e.ControlledDeviceType)
                    .HasName("PRIMARY");

                entity.ToTable("controlled_device_address_config");

                entity.Property(e => e.ControlledDeviceType).HasColumnName("controlled_device_type");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.OutputType)
                    .HasMaxLength(255)
                    .HasColumnName("output_type");
            });

            modelBuilder.Entity<DashboardSensorConfig>(entity =>
            {
                entity.ToTable("dashboard_sensor_config");

                entity.HasIndex(e => e.SensorId, "ix_dsc_sensor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MeasuredValueType)
                    .HasMaxLength(255)
                    .HasColumnName("measured_value_type");

                entity.Property(e => e.SensorId).HasColumnName("sensor_id");

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.DashboardSensorConfigs)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dsc_sensor");
            });

            modelBuilder.Entity<Databasechangelog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("databasechangelog");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .HasColumnName("AUTHOR");

                entity.Property(e => e.Comments)
                    .HasMaxLength(255)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.Contexts)
                    .HasMaxLength(255)
                    .HasColumnName("CONTEXTS");

                entity.Property(e => e.Dateexecuted)
                    .HasColumnType("datetime")
                    .HasColumnName("DATEEXECUTED");

                entity.Property(e => e.DeploymentId)
                    .HasMaxLength(10)
                    .HasColumnName("DEPLOYMENT_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Exectype)
                    .HasMaxLength(10)
                    .HasColumnName("EXECTYPE");

                entity.Property(e => e.Filename)
                    .HasMaxLength(255)
                    .HasColumnName("FILENAME");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("ID");

                entity.Property(e => e.Labels)
                    .HasMaxLength(255)
                    .HasColumnName("LABELS");

                entity.Property(e => e.Liquibase)
                    .HasMaxLength(20)
                    .HasColumnName("LIQUIBASE");

                entity.Property(e => e.Md5sum)
                    .HasMaxLength(35)
                    .HasColumnName("MD5SUM");

                entity.Property(e => e.Orderexecuted).HasColumnName("ORDEREXECUTED");

                entity.Property(e => e.Tag)
                    .HasMaxLength(255)
                    .HasColumnName("TAG");
            });

            modelBuilder.Entity<Databasechangeloglock>(entity =>
            {
                entity.ToTable("databasechangeloglock");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Locked)
                    .HasColumnType("bit(1)")
                    .HasColumnName("LOCKED");

                entity.Property(e => e.Lockedby)
                    .HasMaxLength(255)
                    .HasColumnName("LOCKEDBY");

                entity.Property(e => e.Lockgranted)
                    .HasColumnType("datetime")
                    .HasColumnName("LOCKGRANTED");
            });

            modelBuilder.Entity<Data>(entity =>
            {
                entity.ToTable("data");

                entity.HasIndex(e => e.SensorId, "fk_data_sensor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Co2).HasColumnName("co2");

                entity.Property(e => e.DataTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("data_timestamp");

                entity.Property(e => e.Hits).HasColumnName("hits");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.SensorId).HasColumnName("sensor_id");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.Data)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_data_sensor");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<EventAction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("event_actions");

                entity.HasIndex(e => e.ActionId, "ix_ea_al");

                entity.HasIndex(e => e.EventId, "ix_ea_event");

                entity.Property(e => e.ActionId).HasColumnName("action_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.HasOne(d => d.Action)
                    .WithMany()
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ea_al");

                entity.HasOne(d => d.Event)
                    .WithMany()
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ea_event");
            });

            modelBuilder.Entity<GpioPlan>(entity =>
            {
                entity.ToTable("gpio_plan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DefaultState)
                    .HasMaxLength(255)
                    .HasColumnName("default_state");

                entity.Property(e => e.PinAddress).HasColumnName("pin_address");

                entity.Property(e => e.PinName)
                    .HasMaxLength(255)
                    .HasColumnName("pin_name");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.GpioPlan)
                    .HasForeignKey<GpioPlan>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_gp_plan");
            });

            modelBuilder.Entity<LimitPlan>(entity =>
            {
                entity.ToTable("limit_plan");

                entity.HasIndex(e => e.YearPeriodId, "fk_lp_yp");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("bit(1)")
                    .HasColumnName("active");

                entity.Property(e => e.LastTriggered)
                    .HasColumnType("datetime")
                    .HasColumnName("last_triggered");

                entity.Property(e => e.OptimalValue).HasColumnName("optimal_value");

                entity.Property(e => e.ThresholdValue).HasColumnName("threshold_value");

                entity.Property(e => e.ValueType)
                    .HasMaxLength(255)
                    .HasColumnName("value_type");

                entity.Property(e => e.YearPeriodId).HasColumnName("year_period_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.LimitPlan)
                    .HasForeignKey<LimitPlan>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lp_plan");

                entity.HasOne(d => d.YearPeriod)
                    .WithMany(p => p.LimitPlans)
                    .HasForeignKey(d => d.YearPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lp_yp");
            });

            modelBuilder.Entity<ManualGpioPlan>(entity =>
            {
                entity.ToTable("manual_gpio_plan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("bit(1)")
                    .HasColumnName("active")
                    .HasDefaultValueSql("b'0'");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ManualGpioPlan)
                    .HasForeignKey<ManualGpioPlan>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mgp_gp");
            });

            modelBuilder.Entity<ManualPlan>(entity =>
            {
                entity.ToTable("manual_plan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ManualPlan)
                    .HasForeignKey<ManualPlan>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mp_plan");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("plan");

                entity.HasIndex(e => e.EventId, "ix_plan_event");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PlanType)
                    .HasMaxLength(255)
                    .HasColumnName("plan_type");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_plan_event");
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.ToTable("sensor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.SensorType)
                    .HasMaxLength(255)
                    .HasColumnName("sensor_type");
            });

            modelBuilder.Entity<TimeGpioPlan>(entity =>
            {
                entity.ToTable("time_gpio_plan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.LastTriggered)
                    .HasColumnType("datetime")
                    .HasColumnName("last_triggered");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.TimeGpioPlan)
                    .HasForeignKey<TimeGpioPlan>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tgp_gp");
            });

            modelBuilder.Entity<TimePlan>(entity =>
            {
                entity.ToTable("time_plan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FromTime)
                    .HasColumnType("time")
                    .HasColumnName("from_time");

                entity.Property(e => e.ToTime)
                    .HasColumnType("time")
                    .HasColumnName("to_time");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.TimePlan)
                    .HasForeignKey<TimePlan>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tp_plan");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.HashedPassword)
                    .HasMaxLength(255)
                    .HasColumnName("hashed_password");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ProfilePictureUrl).HasColumnName("profile_picture_url");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_roles");

                entity.HasIndex(e => e.UserId, "ix_ur_user");

                entity.Property(e => e.Roles).HasColumnName("roles");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ur_user");
            });

            modelBuilder.Entity<YearPeriod>(entity =>
            {
                entity.ToTable("year_period");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("bit(1)")
                    .HasColumnName("active");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
