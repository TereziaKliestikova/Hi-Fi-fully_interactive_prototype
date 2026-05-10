namespace HIPA_BE.Data.Seeding
{

  public class PdfFileSeeding
  {

    public static List<object> GetModels()
    {
      return new List<object>(){
            new { ID = 1, Name = "script.pdf", Path = "/assets/pdf/script.pdf", OrganID = 1,  },
            new { ID = 2, Name = "test.pdf", Path = "/assets/pdf/test.pdf", BodySystemID = 1 },
            new { ID = 3, Name = "script2.pdf", Path = "/assets/pdf/script2.pdf", OrganID = 1 },
            new { ID = 4, Name = "sample.pdf", Path = "/assets/pdf/sample.pdf", BodySystemID = 9 },
            new { ID = 5, Name = "sample 2.pdf", Path = "/assets/pdf/sample2.pdf", BodySystemID = 2 },
      };

    }
  }

}
