namespace ActiveForum.Database.Migrations
{
    using System.Collections.Generic;
    using ActiveForum.Database.Entities;
    using ActiveForum.Database.Entities.ForumEntities;
    using FluentMigrator;
    using FluentMigrator.SqlServer;

    [Migration(202202012355)]
    public class SeedSportsData : Migration
    {
        public override void Up()
        {
            var sports = new List<SportEntity>
            {
                new SportEntity()
                {
                       Id = 1,
                       Name = "Trójbój",
                       Description = "Sport siłowy na, który składają się 3 ćwiczenia.",
                       ImageUrl = "https://e7.pngegg.com/pngimages/225/558/png-clipart-powerlifting-powerlifting.png"
                },
                new SportEntity()
                {
                       Id = 2,
                       Name = "Crossfit",
                       Description = "Program treningu siłowego i kondycyjnego, który opiera się na wzroście dziesięciu najważniejszych zdolności siłowych",
                       ImageUrl = "https://vialise.pl/wp-content/uploads/2019/12/bieganie.jpg"
                },
                new SportEntity()
              {
                       Id = 3,
                       Name = "Kulturystyka",
                       Description = "Jej głównym celem jest zbudowanie mieśniowej i skupienie się głównie na wyglądzie.",
                       ImageUrl = "https://www.vhv.rs/dpng/d/445-4458174_workout-png-free-download-fitness-point-app-transparent.png"
              },
                new SportEntity()
                {
                        Id = 4,
                        Name = "Armwrestling",
                        Description = "Dwóch znajdujących się naprzeciw siebie zawodników zwiera dłonie (najczęściej prawe) w uścisk i, trzymając łokcie na płaskiej powierzchni, stara się przeciągnąć rękę przeciwnika w dół. Podobnie jak w zapasach i judo o zwycięstwie decyduje siła mięśni zawodników, ale także technika, taktyka, szybkość i wytrzymałość psychiczna.",
                        ImageUrl = "https://us.123rf.com/450wm/natbasil/natbasil1902/natbasil190200031/118848898-arm-wrestling-two-men-hands-shaking-silhouette-vector-illustration.jpg?ver=6"
                },
                new SportEntity()
              {
                        Id = 5,
                        Name = "Podnoszenie ciężarów",
                        Description = "Dyscyplina ciężkiej atletyki, polegająca na podnoszeniu przez zawodnika sztangi o określonej masie.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/ed/Andrei_Rybakov.jpg"
              },
                new SportEntity()
                {
                    Id = 6,
                    Name = "Strongman",
                    Description = "Określenie zawodnika oraz dyscypliny sportu siłowego polegającej na podnoszeniu, podrzucaniu lub przemieszczaniu bloku o dużej masie.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/26/Mi%C4%99dzynarodowy_Puchar_Polski_Strongman_Czymanowo_2018.jpg/1280px-Mi%C4%99dzynarodowy_Puchar_Polski_Strongman_Czymanowo_2018.jpg"
                },
                new SportEntity()
             {
                  Id = 7,
                  Name = "Bieg długodystansowy",
                  Description = "Biegi lekkoatletyczne obejmujące w swój skład dystanse dłuższe od biegu na 3000 metrów.",
                  ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/44/3000WSP_final.JPG/330px-3000WSP_final.JPG"
             },
                new SportEntity()
                {
                    Id = 8,
                    Name = "Bieg krótkodystansowy/Sprint",
                    Description = "Sprint to bieg na krótkich dystansach: 60 metrów (hala), 100 metrów, 200 metrów, 400 metrów, sztafety 4 × 100 metrów i 4 × 400 metrów.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2a/Marcus_Boyd.JPG/300px-Marcus_Boyd.JPG"
                }
            };

            sports.ForEach(x => Insert.IntoTable("Sports").WithIdentityInsert().Row(new { x.Id, x.Name, x.Description, x.ImageUrl }));
        }

        public override void Down()
        {
            Delete.FromTable("Sports").AllRows();
        }
    }
}
