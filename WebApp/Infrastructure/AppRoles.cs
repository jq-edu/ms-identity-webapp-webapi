namespace WebApp.Infrastructure
{
    /// <summary>
    /// Contains a list of all the Azure AD app roles this app depends on and works with.
    /// </summary>
    public static class AppRole
    {
        /// <summary>
        /// Prof basic profiles
        /// </summary>
        public const string Prof = "Prof.All";

        /// <summary>
        /// Etudiant basic profiles
        /// </summary>
        public const string Etudiant = "Etudiant.All";
    }

    /// <summary>
    /// Wrapper class the contain all the authorization policies available in this application.
    /// </summary>
    public static class AuthorizationPolicies
    {
        public const string AssignmentToProfRequired = "AssignmentToProfRequired";
        public const string AssignmentToEtudiantRequired = "AssignmentToEtudiantRequired";
    }
}