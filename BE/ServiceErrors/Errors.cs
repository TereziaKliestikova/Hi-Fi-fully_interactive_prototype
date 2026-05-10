using ErrorOr;

namespace HIPA_BE.ServiceErrors
{
    public static class Errors
    {
        /// <summary>
        /// Used for errors connected to the file system and file operations
        /// <example>
        ///     when a icon image for an organ is not found in the file system
        /// </example>
        /// </summary>
        public static class FileSystem
        {
            public static Error FileNotFound => Error.Unexpected(
                "FileSystem.FileNotFound",
                "api.error.fileSystem.fileNotFound");
        }

        /// <summary>
        /// Used for error connected to the database and models
        /// <example>
        ///    when a model/record is not found in the database
        /// </example>
        /// </summary>
        public static class Models
        {    
            public static Error DbError => Error.Unexpected(
                    "Models.DbError",
                    "api.error.models.dbError");
            public static Error BodySystemsDbError => Error.NotFound(
                    "Models.BodySystemsDbError",
                    "api.error.bodySystem.dataNotFound");
            public static Error OrgansDbError => Error.NotFound(
                    "Generic.OrgansDbError",
                    "api.error.organ.dataNotFound");
            public static Error DiagnosisDbError => Error.NotFound(
                    "Generic.DiagnosisDbError",
                    "api.error.diagnosis.dataNotFound");
            public static Error SampleImageDbError => Error.NotFound(
                    "Generic.SampleImageDbError",
                    "api.error.sampleImage.dataNotFound");
            public static Error SampleImageMetadataNotFound => Error.NotFound(
                    "Generic.SampleImageMetadataNotFound",
                    "api.error.sampleImage.metadataNotFound");
            public static Error SampleImageMetadataInvalid => Error.Validation(
                    "Generic.SampleImageMetadataInvalid",
                    "api.error.sampleImage.metadataInvalid");
            public static Error SampleImageAnnotationDbError => Error.NotFound(
                    "Generic.SampleImageAnnotationNotFound",
                    "api.error.sampleImageAnnotation.dataNotFound");
            public static Error SampleImageAnnotationInvalidForm => Error.Validation(
                    "Generic.SampleImageAnnotationInvalidForm",
                    "api.error.sampleImageAnnotation.invalidForm");

            public static ErrorOr<bool> BodySystemNotFound { get; internal set; }
        }

        public static class ApplicationUser
        {
            // Validation Error == Status code 400
            // is translated on FE and the status code 400 
            // is used as identifier for expected errors
            public static Error UserAlreadyExists => Error.Validation(
                "ApplicationUser.AlreadyExists",
                "api.error.registration.userAlreadyExists");
            public static Error InvalidEmail => Error.Validation(
                "ApplicationUser.InvalidEmail",
                "api.error.registration.invalidForm");
            public static Error UnauthorizedAccess => Error.Unauthorized(
                "ApplicationUser.InvalidUser",
                "api.error.login.invalidUserCredentials");
            public static Error EmailNotVerified => Error.Validation(
                "ApplicationUser.EmailNotVerified",
                "api.error.login.emailNotVerified");
            public static Error InvalidRegistrationPassword => Error.Validation(
                "ApplicationUser.InvalidPassword",
                "api.error.registration.invalidPassword");
            // Should not be returned to FE 
            // so that attackers can't know if the user exists
            // tech story was created to remove this error
            public static Error UserDoesNotExist => Error.Validation(
                "ApplicationUser.UserDoesNotExist",
                "api.error.changePassword.userDoesNotExist");
            public static Error InvalidResetPassword => Error.Validation(
                "ApplicationUser.InvalidPassword",
                "api.error.changePassword.invalidPassword");
            public static Error InvalidResetPasswordToken => Error.Validation(
                "ApplicationUser.InvalidResetPasswordToken",
                "api.error.changePassword.invalidResetPasswordToken");
        }
    }
}
