namespace HIPA_BE.Data.Seeding
{

  public class BodySystemOrganSeeding
  {

    public static List<object> GetModels()
    {
      return new List<object>(){
            //Kardiovaskularny system
            new { BodySystemsID = 1, OrgansID = 1 }, // Circulatory - Heart
            new { BodySystemsID = 1, OrgansID = 2 },
            //Hematopoeticky system
            // new { BodySystemsID = 2, OrgansID = 8 },
            // new { BodySystemsID = 2, OrgansID = 9 },
            // new { BodySystemsID = 2, OrgansID = 15 },
            // new { BodySystemsID = 3, OrgansID = 3 },
            //Dychaci system
            new { BodySystemsID = 3, OrgansID = 10 },
            new { BodySystemsID = 3, OrgansID = 11 },
            //Ustna dutina
            // new { BodySystemsID = 4, OrgansID = 12 },
            // new { BodySystemsID = 4, OrgansID = 13 },
            // new { BodySystemsID = 4, OrgansID = 14 },
            // new { BodySystemsID = 4, OrgansID = 15 },
            //GIT
            new { BodySystemsID = 5, OrgansID = 5 },
            new { BodySystemsID = 5, OrgansID = 6 },
            new { BodySystemsID = 5, OrgansID = 7 },
            new { BodySystemsID = 5, OrgansID = 11 },
            new { BodySystemsID = 5, OrgansID = 12 },
            new { BodySystemsID = 5, OrgansID = 16 },
            new { BodySystemsID = 5, OrgansID = 17 },
            new { BodySystemsID = 5, OrgansID = 18 },
            new { BodySystemsID = 5, OrgansID = 19 },
            new { BodySystemsID = 5, OrgansID = 44 },
            new { BodySystemsID = 5, OrgansID = 45 },
            //Pohlavny muz
            // new { BodySystemsID = 6, OrgansID = 20 },
            // new { BodySystemsID = 6, OrgansID = 21 },
            // new { BodySystemsID = 6, OrgansID = 23 },
            // new { BodySystemsID = 6, OrgansID = 24 },
            //Pohlavny zena
            // new { BodySystemsID = 7, OrgansID = 25 },
            // new { BodySystemsID = 7, OrgansID = 26 },
            // new { BodySystemsID = 7, OrgansID = 27 },
            // new { BodySystemsID = 7, OrgansID = 28 },
            // new { BodySystemsID = 7, OrgansID = 29 },
            // new { BodySystemsID = 7, OrgansID = 30 },
            // new { BodySystemsID = 7, OrgansID = 31 },
            //Endokriny system
            // new { BodySystemsID = 8, OrgansID = 4 },
            // new { BodySystemsID = 8, OrgansID = 7 },
            // new { BodySystemsID = 8, OrgansID = 20 },
            // new { BodySystemsID = 8, OrgansID = 25 },
            // new { BodySystemsID = 8, OrgansID = 32 },
            // new { BodySystemsID = 8, OrgansID = 33 },
            // new { BodySystemsID = 8, OrgansID = 34 },
            // new { BodySystemsID = 8, OrgansID = 35 },
            // new { BodySystemsID = 8, OrgansID = 36 },

            //Pohobovy system
            //Centralny nervovy system
            new { BodySystemsID = 10, OrgansID = 32 },
            new { BodySystemsID = 10, OrgansID = 35 },
            new { BodySystemsID = 10, OrgansID = 37 },
            //Periferny nervovy system
            new { BodySystemsID = 11, OrgansID = 38 },
            new { BodySystemsID = 11, OrgansID = 39 },
            new { BodySystemsID = 11, OrgansID = 40 },
            //Vylucovaci system
            new { BodySystemsID = 12, OrgansID = 4 },
            new { BodySystemsID = 12, OrgansID = 20 },
            new { BodySystemsID = 12, OrgansID = 22 },
            new { BodySystemsID = 12, OrgansID = 24 },
            new { BodySystemsID = 12, OrgansID = 41 },
            new { BodySystemsID = 12, OrgansID = 42 },

            //Kosti
            //Detsky

            //Koza
            new { BodySystemsID = 15, OrgansID = 36 },
            new { BodySystemsID = 15, OrgansID = 43 },
            new { BodySystemsID = 15, OrgansID = 46 },
            new { BodySystemsID = 15, OrgansID = 47 },
      };

    }
  }

}
