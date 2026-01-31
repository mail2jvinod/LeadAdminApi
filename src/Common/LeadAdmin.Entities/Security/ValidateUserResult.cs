namespace LeadAdmin.Entities.Security
{
    public class ValidateUserResult
    {
        public List<Claim> Claims { get; set; }
        public List<vLoginEntity> LoginEntities { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Claim
    {
        public Claim()
        {

        }
        public Claim(string claimType, string claimValue, string statusMessage)
        {
            this.ClaimType = claimType;
            this.ClaimValue = claimValue;
            this.StatusMessage = StatusMessage;
        }

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string StatusMessage { get; set; }

        public override string ToString()
        {
            return $"{ClaimType} - {ClaimValue}";
        }

        public override bool Equals(object obj)
        {
            var result = default(bool);
            var targetObject = obj as Claim;

            if (targetObject != null)
            {
                result = string.Equals(this.ClaimType, targetObject.ClaimType, System.StringComparison.OrdinalIgnoreCase)
                    && string.Equals(this.ClaimValue, targetObject.ClaimValue, System.StringComparison.OrdinalIgnoreCase);
            }

            return result;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(this.ClaimType, this.ClaimValue).GetHashCode();
        }

    }

}
