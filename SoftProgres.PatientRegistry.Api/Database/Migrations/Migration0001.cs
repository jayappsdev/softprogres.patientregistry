using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace SoftProgres.PatientRegistry.Api.Database.Migrations;

public class Migration0001 : MigrationBase
{
    public class Patient
    {
        [AutoIncrement]
        [PrimaryKey]
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(15)]
        [Unique]
        public string BirthNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Description("1 - male, 2 - female")]
        [Default(null)]
        public int? Sex { get; set; }

        [Default(null)]
        [StringLength(250)]
        public string? Email { get; set; }
        
        [Default(null)]
        [StringLength(50)]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string StreetAndNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string State { get; set; } = string.Empty;
        
        [Default(null)]
        [StringLength(200)]
        public string? Workplace { get; set; }

        [Default(null)]
        public int? Age { get; set; }

        [Default(null)]
        public DateTime? DateOfBirth { get; set; }
    }
    
    public override void Up()
    {
        base.Up();
        Db.CreateTable<Patient>();
        AddSeedPatients().Wait();
    }

    public override void Down()
    {
        Db.DropTable<Patient>();
        base.Down();
    }

    private async Task AddSeedPatients()
    {
        await Db.InsertAsync(new Patient()
        {
            BirthNumber = "121212/1212",
            FirstName = "Mário",
            LastName = "Kubička",
            Sex = 1,
            Email = "mario.kubicka@example.com",
            PhoneNumber = "0922 112 233",
            StreetAndNumber = "Einsteinová 110",
            City = "Bratislava",
            State = "Slovensko",
            PostalCode = "85101",
            Workplace = "Základná škola, 6. ročník",
            DateOfBirth = new DateTime(2012, 12, 12),
            Age = 11
        });
        
        await Db.InsertAsync(new Patient()
        {
            BirthNumber = "955915/5187",
            FirstName = "Kristína",
            LastName = "Odolná",
            Sex = 2,
            Email = "kristina.odolna@example.com",
            PhoneNumber = "0999 222 889",
            StreetAndNumber = "Bratislavská 24",
            City = "Trnava",
            State = "Slovensko",
            PostalCode = "91701",
            Workplace = "Kaderníčka, Beatuy salón Kika, Trnava",
            DateOfBirth = new DateTime(1995, 5, 15),
            Age = 29
        });
        
        await Db.InsertAsync(new Patient()
        {
            BirthNumber = "740829/9844",
            FirstName = "Ján",
            LastName = "Novák",
            Sex = 1,
            Email = "jan.novak@example.com",
            PhoneNumber = "+421 998 123 987",
            StreetAndNumber = "č.d. 255",
            City = "Jelšovce",
            State = "Slovensko",
            PostalCode = "95143",
            Workplace = "Účtovník, Accounting s.r.o., Nitra",
            DateOfBirth = new DateTime(1974, 8, 29),
            Age = 50
        });
        
        await Db.InsertAsync(new Patient()
        {
            BirthNumber = "996120/3386",
            FirstName = "Martina",
            LastName = "Navrátilová",
            Sex = 2,
            Email = "martina.navratilov@example.com",
            PhoneNumber = "+421 33 33 88 000",
            StreetAndNumber = "Komenského 114/7",
            City = "Špačince",
            State = "Slovensko",
            PostalCode = "91951",
            Workplace = "Predavačka, Jednota Trnava",
            DateOfBirth = new DateTime(1999, 11, 20),
            Age = 24
        });
        
        await Db.InsertAsync(new Patient()
        {
            BirthNumber = "510430/795",
            FirstName = "Jozef",
            LastName = "Starý",
            Sex = 1,
            Email = "jozef.stary@example.com",
            PhoneNumber = "+421 998 123 987",
            StreetAndNumber = "č.d. 255",
            City = "Jelšovce",
            State = "Slovensko",
            PostalCode = "95143",
            Workplace = "Dôchodca",
            DateOfBirth = new DateTime(1951, 8, 29),
            Age = 73
        });
        
        await Db.InsertAsync(new Patient()
        {
            BirthNumber = "015901/1978",
            FirstName = "Ivana",
            LastName = "Krátka",
            Sex = 2,
            Email = "ivana.kratka@example.com",
            PhoneNumber = "+421 122 234 566",
            StreetAndNumber = "Nálepková 1189/2",
            City = "Bratislava",
            State = "Slovensko",
            PostalCode = "85101",
            Workplace = "Študentka, Masaryková univerzita, Brno",
            DateOfBirth = new DateTime(2001, 09, 01),
            Age = 23
        });
    }
}