using System.Collections.ObjectModel;

namespace DevSpot.Constants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string JobSeeker = "JobSeeker";
        public const string Employer = "Employer";


        public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(
   [
       Admin,
    JobSeeker,
            Employer
   ]);

        public static bool IsDefaultRole(string roleName) => DefaultRoles.Contains(roleName);
    }
}
