using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class MyDBModels
{
    public class DB : DbContext
    {
        public DB() : base(nameOrConnectionString: "PgSql") { }

        public DbSet<Message> message { get; set; }
        //public DbSet<Dialog> dialog { get; set; }
        public DbSet<Communication> communication { get; set; }
        public DbSet<EveryDayProfileStatistics> everyDayProfileStatistics { get; set; }
        public DbSet<ProfileStatistics> profileStatistics { get; set; }
        public DbSet<Profile> profile { get; set; }
        public DbSet<Group> group { get; set; }
        public DbSet<Arduino> arduino { get; set; }
        //public DbSet<Friend> friend { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<SignUp> signUp { get; set; }

        ////////////////////////////////////////////////////////////////////////////
        public DbSet<Driver> driver { get; set; }
        public DbSet<Dispatcher> dispatcher { get; set; }
        public DbSet<Bus> bus { get; set; }
        public DbSet<WorkList> workList { get; set; }
        public DbSet<DateWorkList> dateWorkList { get; set; }
        public DbSet<RepairList> repairList { get; set; }
        public DbSet<GasList> gasList { get; set; }
        public DbSet<Account> account { get; set; }

       
    }

    [Table("message", Schema = "public")]
    public class Message
    {
        [Key]
        [Column("message_id")]
        public int MessageId { get; set; }
        [Column("data_message")]
        public string DataMessage { get; set; }
        [Column("time_written")]
        public DateTime TimeWritten { get; set; }
        [Column("is_read")]
        public string IsReadProfileIdString { get; set; }
        public int[] IsReadProfileId
        {
            get
            {
                return IsReadProfileIdString.Count() > 0 ? Array.ConvertAll(IsReadProfileIdString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                IsReadProfileIdString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        [Column("profile_id")]
        public int ProfileId { get; set; }
        [Column("type_message")]
        public int TypeMessage { get; set; }
    }

    /*[Table("dialog", Schema = "public")]
    public class Dialog
    {
        [Key]
        [Column("dialog_id")]
        public int DialogId { get; set; }
        [Column("key_dialog")]
        public string KeyDialog { get; set; }
        [Column("message_id_array")]
        public string MessageIdArrayString { get; set; }
        public int[] MessageIdArray
        {
            get
            {
                return MessageIdArrayString.Count() > 0 ? Array.ConvertAll(MessageIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                MessageIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
    }*/

    [Table("communication", Schema = "public")]
    public class Communication
    {
        [Key]
        [Column("communication_id")]
        public int CommunicationId { get; set; }
        [Column("key_dialog")]
        public string KeyDialog { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("photo_url")]
        public string PhotoUrl { get; set; }
        [Column("participant_profile_id_array")]
        public string ParticipantProfileIdArrayString { get; set; }
        public int[] ParticipantProfileIdArray
        {
            get
            {
                return ParticipantProfileIdArrayString.Count() > 0 ? Array.ConvertAll(ParticipantProfileIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                ParticipantProfileIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        [Column("message_id_array")]
        public string MessageIdArrayString { get; set; }
        public int[] MessageIdArray
        {
            get
            {
                return MessageIdArrayString.Count() > 0 ? Array.ConvertAll(MessageIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                MessageIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        [Column("creater_profile_id")]
        public int CreaterProfileId { get; set; }
        [Column("add_profile_timestamp_array")]
        public string AddProfileTimestampArrayString { get; set; }
        public int[] AddProfileTimestampArray
        {
            get
            {
                return AddProfileTimestampArrayString.Count() > 0 ? Array.ConvertAll(AddProfileTimestampArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                AddProfileTimestampArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        [Column("is_group")]
        public bool IsGroup { get; set; }

    }

    [Table("every_day_profile_statistics", Schema = "public")]
    public class EveryDayProfileStatistics
    {
        [Key]
        [Column("every_day_profile_statistics_id")]
        public int EveryDayProfileStatisticsId { get; set; }
        [Column("count_distance")]
        public double CountDistance { get; set; }
        [Column("middle_speed")]
        public double MiddleSpeed { get; set; }
        [Column("time_in_trip")]
        public TimeSpan TimeInTrip { get; set; }
        [Column("calories")]
        public int Calories { get; set; }
        [Column("time_create")]
        public DateTime TimeCreate { get; set; }
    }

    [Table("profile_statistics", Schema = "public")]
    public class ProfileStatistics
    {
        [Key]
        [Column("profile_statistics_id")]
        public int ProfileStatisticsId { get; set; }
        [Column("count_distance_total")]
        public double CountDistanceTotal { get; set; }
        [Column("middle_speed_total")]
        public double MiddleSpeedTotal { get; set; }
        [Column("time_in_trip_total")]
        public TimeSpan TimeInTripTotal { get; set; }
        [Column("calories_total")]
        public int CaloriesTotal { get; set; }
        [Column("count_dangerous_situation")]
        public int CountDangerousSituation { get; set; }
        [Column("count_atteempted_theft")]
        public int CountAttemptedTheft { get; set; }
        [Column("every_day_profile_statistics_id_array")]
        public string EveryDayProfileStatisticsIdArrayString { get; set; }
        public int[] EveryDayProfileStatisticsIdArray
        {
            get
            {
                return EveryDayProfileStatisticsIdArrayString.Count() > 0 ? Array.ConvertAll(EveryDayProfileStatisticsIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                EveryDayProfileStatisticsIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
    }

    [Table("profile", Schema = "public")]
    public class Profile
    {
        [Key]
        [Column("profile_id")]
        public int ProfileId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("photo_url")]
        public string PhotoUrl { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("time_last_active")]
        public DateTime TimeLastActive { get; set; }
        [Column("profile_statistics_id")]
        public int ProfileStatisticsId { get; set; }
    }

    [Table("group_list", Schema = "public")]
    public class Group
    {
        [Key]
        [Column("group_id")]
        public int GroupId { get; set; }
        [Column("key_group")]
        public string KeyGroup { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("photo_url")]
        public string PhotoUrl { get; set; }
        [Column("date_finish")]
        public DateTime DateFinish { get; set; }
        [Column("position_participant_profile_id_array")]
        public string PositionParticipantProfileIdArrayString { get; set; }
        public int[] PositionParticipantProfileIdArray
        {
            get
            {
                return PositionParticipantProfileIdArrayString.Count() > 0 ? Array.ConvertAll(PositionParticipantProfileIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                PositionParticipantProfileIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
    }

    [Table("arduino", Schema = "public")]
    public class Arduino
    {
        [Key]
        [Column("arduino_id")]
        public int ArduinoId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("photo_url")]
        public string Mac { get; set; }
    }

    /*[Table("friend", Schema = "public")]
    public class Friend
    {
        [Key]
        [Column("friend_id")]
        public int FriendId { get; set; }
        [Column("profile_id")]
        public int ProfileId { get; set; }
    }*/

    [Table("user_data", Schema = "public")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("profile_id")]
        public int ProfileId { get; set; }
        [Column("friend_id_array")]
        public string FriendIdArrayString { get; set; }
        public int[] FriendIdArray
        {
            get
            {
                return FriendIdArrayString.Count() > 0 ? Array.ConvertAll(FriendIdArrayString.Split(';'), int.Parse) : new int[] { } ;
            }
            set
            {
                FriendIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

        [Column("communication_id_array")]
        public string CommunicationIdArrayString { get; set; }
        public int[] CommunicationIdArray
        {
            get
            {
                return CommunicationIdArrayString.Count() > 0 ? Array.ConvertAll(CommunicationIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                CommunicationIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

        [Column("group_id_array")]
        public string GroupIdArrayString { get; set; }
        public int[] GroupIdArray
        {
            get
            {
                return GroupIdArrayString.Count() > 0 ? Array.ConvertAll(GroupIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                GroupIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

        [Column("arduino_id_array")]
        public string ArduinoIdArrayString { get; set; }
        public int[] ArduinoIdArray
        {
            get
            {
                return ArduinoIdArrayString.Count() > 0 ? Array.ConvertAll(ArduinoIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                ArduinoIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

        [Column("firebase_token")]
        public string FirebaseToken { get; set; }

        [Column("friend_possible_id_array")]
        public string FriendPossibleIdArrayString { get; set; }
        public int[] FriendPossibleIdArray
        {
            get
            {
                return FriendPossibleIdArrayString.Count() > 0 ? Array.ConvertAll(FriendPossibleIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                FriendPossibleIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

        [Column("communication_pin_id_array")]
        public string CommunicationPinIdArrayString { get; set; }
        public int[] CommunicationPinIdArray
        {
            get
            {
                return CommunicationPinIdArrayString.Count() > 0 ? Array.ConvertAll(CommunicationPinIdArrayString.Split(';'), int.Parse) : new int[] { };
            }
            set
            {
                CommunicationPinIdArrayString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

    }

    [Table("sign_up", Schema = "public")]
    public class SignUp
    {
        [Key]
        [Column("base_code")]
        public string BaseCode { get; set; }
        [Column("login_encode")]
        public string LoginEncode { get; set; }
        [Column("password_encode")]
        public string PasswordEncode { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
    }

    //////////////////////////////////////////////////////////////////////////////////
    [Table("driver", Schema = "public")]
    public class Driver
    {
        [Key]
        [Column("driver_id")]
        public int DriverId { get; set; }
        [Column("driver_number")]
        public int DriverNumber { get; set; }
        [Column("secondname")]
        public string Secondname { get; set; }
        [Column("image")]
        public string Image { get; set; }
        [Column("experience")]
        public int Experience { get; set; }
        [Column("salary")]
        public int Salary { get; set; }
        [Column("qualification")]
        public string Qualification { get; set; }
    }

    [Table("dispatcher", Schema = "public")]
    public class Dispatcher
    {
        [Key]
        [Column("dispatcher_id")]
        public int DispatcherId { get; set; }
        [Column("dispatcher_number")]
        public int DispatcherNumber { get; set; }
        [Column("secondname")]
        public string Secondname { get; set; }
        [Column("image")]
        public string Image { get; set; }
    }


    [Table("bus", Schema = "public")]
    public class Bus
    {
        [Key]
        [Column("bus_id")]
        public int BusId { get; set; }
        [Column("bus_number")]
        public int BusNumber { get; set; }
        [Column("model")]
        public string Model { get; set; }
        [Column("bus_condition")]
        public string BusCondition { get; set; }


    }

    [Table("work_list", Schema = "public")]
    public class WorkList
    {
        [Key]
        [Column("work_list_id")]
        public int WorkListId { get; set; }
        [Column("driver_id")]
        public int DriverId { get; set; }
        [Column("bus_id")]
        public int BusId { get; set; }
        [Column("second_name_dispatcher")]
        public string SecondNameDispatcher { get; set; }
        [Column("date_action")]
        public DateTime DateAction { get; set; }

    }

    [Table("date_work_list", Schema = "public")]
    public class DateWorkList
    {
        [Key]
        [Column("date_id")]
        public int DateId { get; set; }
        [Column("date_action")]
        public DateTime DateAction { get; set; }
        [Column("work_list_id")]
        public int WorkListId { get; set; }


    }

    [Table("service_station", Schema = "public")]
    public class RepairList
    {
        [Key]
        [Column("list_service_id")]
        public int ServiceListId { get; set; }
        [Column("bus_id")]
        public int BusId { get; set; }
        [Column("time_get")]
        public DateTime TimeGet { get; set; }
        [Column("bus_condition")]
        public string BusCondition { get; set; }


    }

    [Table("gas_station", Schema = "public")]
    public class GasList
    {
        [Key]
        [Column("gas_list_id")]
        public int GasListId { get; set; }
        [Column("bus_id")]
        public int BusId { get; set; }
        [Column("count_litre")]
        public int CountLitre { get; set; }
        [Column("type_gas")]
        public string TypeGas { get; set; }
        [Column("cost_gas")]
        public int CostGas { get; set; }
        [Column("time_get")]
        public DateTime TimeGet { get; set; }

    }

    [Table("login", Schema = "public")]
    public class Account
    {
        [Key]
        [Column("login_id")]
        public string LoginId { get; set; }
        [Column("password_worker")]
        public string PasswordWorker { get; set; }
        [Column("role_worker")]
        public string RoleWorker { get; set; }
        [Column("number_worker")]
        public string NumberWorker { get; set; }
        [Column("token")]
        public string Token { get; set; }
    }

}


