using HIPA_BE.Data;
using HIPA_BE.Models.OrganModels;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using HIPA_BE.ServiceErrors;
using HIPA_BE.Models.SampleImageModels;
using HIPA_BE.Contracts;
using HIPA_BE.Models.BaseModels;
using HIPA_BE.Controllers.Resources;
using log4net;
using System.Reflection;

/// Potom vymyslim nieco lepsie mozno, ale ne slubujem (toto sluzi na zistenie userid pre Favorite samples)
using Microsoft.AspNetCore.Identity;
using HIPA_BE.Models;

/// Potom vymyslim nieco lepsie mozno, ale ne slubujem (toto sluzi na zistenie userid pre Favorite samples)
using Microsoft.AspNetCore.Identity;
using HIPA_BE.Models;

namespace HIPA_BE.Services.OrganServices
{
    public class OrganService : IOrganService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(OrganService));

        private readonly AppDbContext _context;
        public OrganService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<OrgansListDto>> GetListOfAllOrgans()
        {
            Log.Info("Executing service.");

            try
            {
                var groupedOrgans = await _context.SampleImages
                    .Where(si => si.Organ != null)
                    .Include(si => si.Organ)
                    .Include(si => si.Diagnosis)
                    .GroupBy(si => si.Organ)
                    .Select(g => new {
                        Organ = g.Key,
                        Diagnoses = g.Select(si => si.Diagnosis).ToList()
                    })
                    .ToListAsync();

                if (groupedOrgans == null) return Errors.Models.OrgansDbError;

                return new OrgansListDto { Organs = groupedOrgans.Select(g => new OrganDiagnosesDto {
                    ID = g.Organ.ID,
                    Name = g.Organ.Name,
                    IconPath = g.Organ.IconPath,
                    Diagnoses = g.Diagnoses
                }).ToList() };
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<byte[]>> GetOrganIcon(string iconName)
        {
            try
            {
                // load path to icons from appsettings.json
                string? assetsPath = Environment.GetEnvironmentVariable("ICONS_PATH");
                if (assetsPath == null) return Errors.FileSystem.FileNotFound;

                string iconPath = Path.Combine(assetsPath, iconName);
                var imgBytes = await File.ReadAllBytesAsync(iconPath);

                return imgBytes;
            }
            catch (Exception)
            {
                return Errors.FileSystem.FileNotFound;
            }
        }

        public async Task<ErrorOr<OrganDetailDto>> GetOrganDetailById(int id, string userId)
        {
            Log.Info($"In organ service with {id} ({userId})");
            try
            {

                var organDetail = await _context.Organs
                    .Where(o => o.ID == id)
                    .Select(o => new OrganDetailDto
                    {
                        OrganDescription = new OrganDescriptionDto
                        {
                            ID = o.ID,
                            Name = o.Name,
                            Description = o.Description
                        },

                        OrganPdf = o.Pdfs.FirstOrDefault(),


                        SampleImages = _context.SampleImages
                            .Where(si => si.Organ.ID == id && si.IsVisible == true)
                            .Select(si => new SampleImageDiagnosisDto
                            {
                                ID = si.ID,
                                Name = si.Name,
                                // Check if there are any annotations for this sample image
                                HasAnnotation = _context.SampleImageAnnotations
                                    .Any(annotation => annotation.SampleImageID == si.ID),
                                Diagnosis = si.Diagnosis.Name,
                                IsFavorite =_context.FavoriteSamples
                                    .Any(favorite => favorite.UserID == userId && favorite.SampleImageID == si.ID),
                                CaustryFile = si.CaustryFile,
                                Note = _context.SampleImageNotes
                                .FirstOrDefault(note => note.SampleImageID == si.ID && note.UserID == userId).Note ?? "",
                                BodySystemNames = si.Organ.BodySystems.Select(bs => bs.Name).ToList(),
                                OrganName = si.Organ.Name,
                                KeyWords = si.KeyWords

                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (organDetail == null) return Errors.Models.OrgansDbError;
                return organDetail;
            }
            catch (Exception)
            {

                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<IconPathDto>> GetOrganIconPathById(int id)
        {
            var organ = await _context.Organs
                .Select(o => new IconPathDto { ID = o.ID, IconPath = "static/" + o.IconPath })
                .FirstOrDefaultAsync(o => o.ID == id);

            if (organ == null) return Errors.Models.OrgansDbError;

            return organ;
        }
    }
}