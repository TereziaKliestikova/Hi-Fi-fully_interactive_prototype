using Microsoft.EntityFrameworkCore;
using HIPA_BE.Models;

namespace HIPA_BE.Data.Seeding
{
    public class DiagnosisSeeding
    {
        private readonly static string[] _diagnoses = [
            "Hypertenzívna srdcová choroba",
            "Choroba koronárnych artérií",
            "Chronické ochorenie obličiek",
            "Cirhóza pečene",
            "Zápalové ochorenie čriev (IBD)",
            "Žalúdočný vred",
            "Pankreatitída",
            "Žlčníkové kamene",
            "Cholecystitída",
            "Crohnova choroba",
            "Hepatitída",
            "Divertikulitída",
            "Kolorektálny karcinóm",
            "Renálna insuficiencia",
            "Peptický vred",
            "Gastroezofageálny refluxný syndróm (GERD)",
            "Chronická pankreatitída",
            "Hepatocelulárny karcinóm"
        ];

    public static List<Diagnosis> GetModels()
    {
        var diagnoses = new List<Diagnosis>();
        for (int i = 0; i < _diagnoses.Length; i++)
        {
            diagnoses.Add(new Diagnosis { ID = i + 1, Name = _diagnoses[i] });
        }
        return diagnoses;
    }
    }
}